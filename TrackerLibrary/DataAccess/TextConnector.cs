using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using TrackerLibrary.DataAccess.TextConnector;
using TrackerLibrary.DataAccess;
using System.IO;

namespace TrackerLibrary.TextHelpers
{
    public class TextConnector : IDataConnection
    {
        //this is where all prize models will be stored
        private const string PrizesFile = "PrizeModels.txt";
        //All Person Models stored here
        private const string PeopleFile = "PersonModels.txt";
        //Team name and id stored here
        private const string TeamsFile = "TeamsModels.txt";
        //Tournaments are stored here
        private const string TournamentsFile = "TournamentModels.txt";
        //Matchups file
        private const string MatchupsFile = "MatchupModels.txt";
        //Matchup Entry file
        private const string MatchupEntriesFile = "MatchupEntryModels.txt";


        /// <summary>
        /// Saves a new prize to the txt doc
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The prize information, inluding unique identifier</returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            //loads the text file and converts to list of prize models
            List<PrizeModel> prizes =  PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();

            //orders the prizes by id in descending order and then saves the id + 1 to use for the new model
            int currentId = 1;

            if (prizes.Count > 0)
            {
                currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
            }
            
            model.Id = currentId;

            prizes.Add(model);

            prizes.SaveToPrizeFile(PrizesFile);

            return model;
        }

        public PersonModel CreatePerson(PersonModel model)
        {
            //Loads people.csv and converts to list of PersonModel
            List<PersonModel> people = PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();

            //order by id descending and saves the highest id value +1 for the new model
            int currentId = 1;

            if (people.Count > 0)
            {
                currentId = people.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;

            people.Add(model);

            people.SaveToPersonFile(PeopleFile);

            return model;
        }

        public List<PersonModel> GetPerson_All()
        {
            return PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();
        }

        public TeamModel CreateTeam(TeamModel model)
        {
            List<TeamModel> teams = TeamsFile.FullFilePath().LoadFile().ConvertToTeamModels(PeopleFile);

            int currentId = 1;

            if (teams.Count > 0)
            {
                currentId = teams.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;

            teams.Add(model);

            teams.SaveToTeamsFile(TeamsFile);

            return model;

        }

        public List<TeamModel> GetTeam_All()
        {
            return TeamsFile.FullFilePath().LoadFile().ConvertToTeamModels(PeopleFile);
        }

        public void CreateTournament(TournamentModel model)
        {
            List<TournamentModel> tournaments = TournamentsFile
                .FullFilePath()
                .LoadFile()
                .ConvertToTournamentModels(TeamsFile, PrizesFile, PeopleFile);

            int currentId = 1;

            if (tournaments.Count > 0)
            {
                currentId = tournaments.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;

            model.SaveRoundsToFile(MatchupsFile, MatchupEntriesFile, TeamsFile, PeopleFile);

            tournaments.Add(model);

            tournaments.SaveToTournamentsFile(TournamentsFile);

        }
    }
}
