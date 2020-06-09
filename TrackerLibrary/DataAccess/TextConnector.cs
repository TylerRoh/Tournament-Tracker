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

        /// <summary>
        /// Saves a new prize to the txt doc
        /// </summary>
        /// <param name="model">prize model</param>
        /// <returns>The prize information, inluding unique identifier</returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            //loads the text file and converts to list of prize models
            List<PrizeModel> prizes =  GlobalConfig.PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();

            //orders the prizes by id in descending order and then saves the id + 1 to use for the new model
            int currentId = 1;

            if (prizes.Count > 0)
            {
                currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
            }
            
            model.Id = currentId;

            prizes.Add(model);

            prizes.SaveToPrizeFile();

            return model;
        }

        /// <summary>
        /// Creates Person models based off of data stored in the People text file.
        /// </summary>
        /// <param name="model">person model</param>
        /// <returns></returns>
        public PersonModel CreatePerson(PersonModel model)
        {
            //Loads people.csv and converts to list of PersonModel
            List<PersonModel> people = GlobalConfig.PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();

            //order by id descending and saves the highest id value +1 for the new model
            int currentId = 1;

            if (people.Count > 0)
            {
                currentId = people.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;

            people.Add(model);

            people.SaveToPersonFile();

            return model;
        }

        /// <summary>
        /// returns all lines in the PeopleFile converted to person models
        /// </summary>
        /// <returns>A list of PersonModel</returns>
        public List<PersonModel> GetPerson_All()
        {
            return GlobalConfig.PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();
        }

        /// <summary>
        /// Creates team model, saves it to Teams text file
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public TeamModel CreateTeam(TeamModel model)
        {
            List<TeamModel> teams = GlobalConfig.TeamsFile.FullFilePath().LoadFile().ConvertToTeamModels();

            int currentId = 1;

            if (teams.Count > 0)
            {
                currentId = teams.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;

            teams.Add(model);

            teams.SaveToTeamsFile();

            return model;

        }

        /// <summary>
        /// Reads all lines of Teams text file and returns as TeamModels
        /// </summary>
        /// <returns>List of Team Models</returns>
        public List<TeamModel> GetTeam_All()
        {
            return GlobalConfig.TeamsFile.FullFilePath().LoadFile().ConvertToTeamModels();
        }

        /// <summary>
        /// Saves tournament model, matchups, and entries to text file.
        /// </summary>
        /// <param name="model"></param>
        public void CreateTournament(TournamentModel model)
        {
            List<TournamentModel> tournaments = GlobalConfig.TournamentsFile
                .FullFilePath()
                .LoadFile()
                .ConvertStringsToTournamentModels();

            int currentId = 1;

            if (tournaments.Count > 0)
            {
                currentId = tournaments.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;

            model.SaveRoundsToFile();

            tournaments.Add(model);

            tournaments.SaveToTournamentFile();

        }

        /// <summary>
        /// Reads all lines in the tournament, matchup and entries text files and returns them as a list of tournament models
        /// </summary>
        /// <returns>List of all tournament models</returns>
        public List<TournamentModel> GetTournament_All()
        {
            return GlobalConfig.TournamentsFile.FullFilePath().LoadFile().ConvertStringsToTournamentModels();
        }
    }
}
