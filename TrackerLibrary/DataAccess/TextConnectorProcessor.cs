using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess.TextConnector
{
    public static class TextConnectorProcessor
    {
        //this is an extension methot called on a string that will return the full file path to that csv
        public static string FullFilePath(this string fileName)
        {
            return $@"{ConfigurationManager.AppSettings["filePath"]}\{fileName}";
        }

        public static List<string> LoadFile(this string file)
        {
            if (!File.Exists(file))
            {
                return new List<string>();
            }

            return File.ReadAllLines(file).ToList();
        }

        public static List<PrizeModel> ConvertToPrizeModels(this List<string> lines)
        {
            List<PrizeModel> output = new List<PrizeModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split('|');

                PrizeModel p = new PrizeModel();
                p.Id = int.Parse(cols[0]);
                p.PlaceNumber = int.Parse(cols[1]);
                p.PlaceName = cols[2];
                p.PrizeAmount = decimal.Parse(cols[3]);
                p.PrizePercentage = double.Parse(cols[4]);

                output.Add(p);
            }

            return output;
        }

        public static List<PersonModel> ConvertToPersonModels(this List<string> lines)
        {
            List<PersonModel> output = new List<PersonModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split('|');

                PersonModel p = new PersonModel();
                p.Id = int.Parse(cols[0]);
                p.FirstName = cols[1];
                p.LastName = cols[2];
                p.EmailAddress = cols[3];
                p.PhoneNumber = cols[4];

                output.Add(p);
            }

            return output;
        }

        public static List<TeamModel> ConvertToTeamModels(this List<string> lines)
        {
            List<TeamModel> output = new List<TeamModel>();

            foreach (string line in lines)
            {
                if (!String.IsNullOrWhiteSpace(line))
                {
                    string[] cols = line.Split('|');

                    TeamModel t = new TeamModel();
                    t.Id = int.Parse(cols[0]);
                    t.TeamName = cols[1];

                    string[] memberIds = cols[2].Split(',');

                    foreach (string id in memberIds)
                    {
                        t.TeamMembers.Add(LookUpPersonById(int.Parse(id)));
                    }


                    output.Add(t);
                }
            }
            return output;
        }

        public static void SaveToPersonFile(this List<PersonModel> models)
        {
            List<string> lines = new List<string>();

            foreach (PersonModel p in models)
            {
                lines.Add($"{p.Id}|{p.FirstName}|{p.LastName}|{p.EmailAddress}|{p.PhoneNumber}");
            }

            File.WriteAllLines(GlobalConfig.PeopleFile.FullFilePath(), lines);
        }

        public static void SaveToTeamsFile(this List<TeamModel> models)
        {
            List<string> lines = new List<string>();

            foreach (TeamModel t in models)
            {
                string newLine = $"{t.Id}|{t.TeamName}|{String.Join(",", t.TeamMembers.Select(x => x.Id))}";

                lines.Add(newLine);
            }

            File.WriteAllLines(GlobalConfig.TeamsFile.FullFilePath(), lines);
        }

        public static void SaveToPrizeFile(this List<PrizeModel> models)
        {
            List<string> lines = new List<string>();

            foreach (PrizeModel p in models)
            {
                lines.Add($"{p.Id}|{p.PlaceNumber}|{p.PlaceName}|{p.PrizeAmount}|{p.PrizePercentage}");
            }

            File.WriteAllLines(GlobalConfig.PrizesFile.FullFilePath(), lines);
        }

        public static void SaveRoundsToFile(this List<List<MatchupModel>> rounds)
        {
            List<MatchupModel> matchups = GlobalConfig.MatchupsFile
                                                              .FullFilePath()
                                                              .LoadFile()
                                                              .ConvertStringsToMatchupModels();

            List<MatchupEntryModel> entries = GlobalConfig.MatchupEntryFile
                                                                  .FullFilePath()
                                                                  .LoadFile()
                                                                  .ConvertToMatchupEntries();
            foreach (List<MatchupModel> round in rounds)
            {

                foreach (MatchupModel match in round)
                {

                    int currentMatchId = 1;

                    if (matchups.Count > 0)
                    {
                        currentMatchId = matchups.OrderByDescending(x => x.Id).First().Id + 1;
                    }

                    match.Id = currentMatchId;

                    matchups.Add(match);

                    foreach (MatchupEntryModel entry in match.Entries)
                    {
                        int currentEntryId = 1;

                        if (entries.Count > 0)
                        {
                            currentEntryId = entries.OrderByDescending(x => x.Id).First().Id + 1;
                        }

                        entry.Id = currentEntryId;

                        entries.Add(entry);
                    }
                }
            }
            matchups.SaveToMatchupsFile();

            entries.SaveToMatchupEntryFile();

        }

        private static void SaveToMatchupsFile(this List<MatchupModel> models)
        {
            List<string> lines = new List<string>();

            foreach (MatchupModel match in models)
            {
                string newline = "";

                newline += $@"{match.Id}|";

                string entries = String.Join(",", match.Entries.Select(x => x.Id));

                newline += $@"{entries}|";

                if (match.Winner != null)
                {
                    newline += $@"{match.Winner.Id}";
                }

                newline += $@"|{match.MatchupRound}";

                lines.Add(newline);
            }

            File.WriteAllLines(GlobalConfig.MatchupsFile.FullFilePath(), lines);
        }

        private static void SaveToMatchupEntryFile(this List<MatchupEntryModel> models)
        {
            List<string> lines = new List<string>();

            foreach (MatchupEntryModel entry in models)
            {
                string newline = "";

                newline += $@"{entry.Id}|";

                if (entry.TeamCompeting != null)
                {
                    newline += $@"{entry.TeamCompeting.Id}";
                }

                newline += $@"|{entry.Score}|";

                if (entry.ParentMatchup != null)
                {
                    newline += $@"{entry.ParentMatchup.Id}";
                }

                lines.Add(newline);
            }

            File.WriteAllLines(GlobalConfig.MatchupEntryFile.FullFilePath(), lines);
        }

        public static void SaveToTournamentFile(this List<TournamentModel> models)
        {
            List<string> lines = new List<string>();

            foreach (TournamentModel tournament in models)
            {
                string newline = "";

                newline += $@"{tournament.Id}|{tournament.TournamentName}|{tournament.EntryFee}|";

                string enteredTeamsIds = String.Join(",", tournament.EnteredTeams.Select(x => x.Id));

                newline += $@"{enteredTeamsIds}|";

                string prizeIds = String.Join(",", tournament.Prizes.Select(x => x.Id));

                newline += $@"{prizeIds}|";

                List<string> roundStrings = new List<string>();

                foreach (List<MatchupModel> round in tournament.Rounds)
                {
                    roundStrings.Add(String.Join("^", round.Select(x => x.Id)));
                }

                newline += string.Join(",", roundStrings);

                lines.Add(newline);
            }
            

            File.WriteAllLines(GlobalConfig.TournamentsFile.FullFilePath(), lines);
        }


        public static List<TournamentModel> ConvertStringsToTournamentModels(this List<string> lines)
        {
            List<TournamentModel> output = new List<TournamentModel>();

            foreach (string lineOfTournamentInfo in lines)
            {
                string[] cols = lineOfTournamentInfo.Split('|');

                TournamentModel tourney = new TournamentModel();
                tourney.Id = int.Parse(cols[0]);
                tourney.TournamentName = cols[1];
                tourney.EntryFee = decimal.Parse(cols[2]);

                string[] teamIds = cols[3].Split(',');
                foreach (string id in teamIds)
                {
                    tourney.EnteredTeams.Add(LookUpTeamById(int.Parse(id)));
                }

                if (cols[4].Length > 0)
                {
                    string[] prizeIds = cols[4].Split(',');
                    foreach (string id in prizeIds)
                    {
                        tourney.Prizes.Add(LookUpPrizeById(int.Parse(id)));
                    }
                }
                

                string[] rounds = cols[5].Split(',');
                foreach (string listOfMatchups in rounds)
                {
                    tourney.Rounds.Add(ConvertStringToMatchupModels(listOfMatchups));
                }
                output.Add(tourney);
            }
            return output;
        }

        public static List<MatchupModel> ConvertStringsToMatchupModels(this List<string> lines)
        {
            List<MatchupModel> output = new List<MatchupModel>();

            foreach (string lineOfMachupInfo in lines)
            {
                string[] cols = lineOfMachupInfo.Split('|');

                MatchupModel matchup = new MatchupModel();

                matchup.Id = int.Parse(cols[0]);

                matchup.Entries = ConvertStringToMatchupEntryModels(cols[1]);

                if (cols[2].Length > 0)
                {
                    matchup.Winner = LookUpTeamById(int.Parse(cols[2]));
                }
                else
                {
                    matchup.Winner = null;
                }    
                
                matchup.MatchupRound = int.Parse(cols[3]);

                output.Add(matchup);
            }
            return output;
        }

        /// <summary>
        /// This method looks at the matchup entry ids in text from from the 
        /// matchup conversion and finds their matches in the text file
        /// from there it converts all the matching ids into matchupEntryModels
        /// </summary>
        /// <param name="lines"></param>
        /// <returns>List of matchupEntryModels that have the ids passed in</returns>
        public static List<MatchupEntryModel> ConvertStringToMatchupEntryModels(string lines)
        {
            List<MatchupEntryModel> output = new List<MatchupEntryModel>();

            string[] ids = lines.Split(',');
            List<string> entries = GlobalConfig.MatchupEntryFile.FullFilePath().LoadFile();

            List<string> idMatches = new List<string>();

            foreach (string id in ids)
            {
                foreach (string entry in entries)
                {
                    string[] cols = entry.Split('|');
                    if (cols[0] == id)
                    {
                        idMatches.Add(entry);
                    }
                }
            }

            output = idMatches.ConvertToMatchupEntries();

            return output;

        }

        public static List<MatchupModel> ConvertStringToMatchupModels(string lines)
        {
            List<MatchupModel> output = new List<MatchupModel>();

            string[] ids = lines.Split('^');
            List<string> matchups = GlobalConfig.MatchupsFile.FullFilePath().LoadFile();

            List<string> idMatches = new List<string>();

            foreach (string id in ids)
            {
                foreach (string match in matchups)
                {
                    string[] cols = match.Split('|');
                    if (cols[0] == id)
                    {
                        idMatches.Add(match);
                    }
                }

                output = idMatches.ConvertStringsToMatchupModels();
            }

            return output;
        }


        public static List<MatchupEntryModel> ConvertToMatchupEntries(this List<string> lines)
        {
            List<MatchupEntryModel> output = new List<MatchupEntryModel>();

            foreach (string line in lines)
            {
                MatchupEntryModel matchup = new MatchupEntryModel();

                string[] cols = line.Split('|');

                matchup.Id = int.Parse(cols[0]);
                if (!String.IsNullOrWhiteSpace(cols[1]))
                {
                    matchup.TeamCompeting = LookUpTeamById(int.Parse(cols[1]));
                }
                else
                {
                    matchup.TeamCompeting = null;
                }
                
                matchup.Score = double.Parse(cols[2]);

                if (cols[3].Length > 0)
                {
                    matchup.ParentMatchup = ConvertStringToMatchupModels(cols[3]).First();
                }
                else
                {
                    matchup.ParentMatchup = null;
                }
                
                output.Add(matchup);
            }

            return output;
        }


        private static TeamModel LookUpTeamById(int id)
        {
            List<TeamModel> teams = GlobalConfig.TeamsFile.FullFilePath().LoadFile().ConvertToTeamModels();

            return teams.Where(x => x.Id == id).First();
        }
        private static PrizeModel LookUpPrizeById(int id)
        {
            List<PrizeModel> prizes = GlobalConfig.PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();

            return prizes.Where(x => x.Id == id).First();
        }
        private static PersonModel LookUpPersonById(int id)
        {
            List<PersonModel> people = GlobalConfig.PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();

            return people.Where(x => x.Id == id).First();
        }

        
    }
}
