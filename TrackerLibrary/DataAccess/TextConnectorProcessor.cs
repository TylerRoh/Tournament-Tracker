using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
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

        public static void SaveToPrizeFile(this List<PrizeModel> models)
        {
            List<string> lines = new List<string>();

            foreach (PrizeModel p in models)
            {
                lines.Add($"{p.Id}|{p.PlaceNumber}|{p.PlaceName}|{p.PrizeAmount}|{p.PrizePercentage}");
            }

            File.WriteAllLines(GlobalConfig.PrizesFile.FullFilePath(), lines);
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
                        LookUpPersonById(int.Parse(id));
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

        public static void SaveToTournamentsFile(this List<TournamentModel> models)
        {
            List<string> lines = new List<string>();

            foreach (TournamentModel t in models)
            {
                string newLine = $@"{t.Id}|
                                    {t.TournamentName}|
                                    {t.EntryFee}|
                                    {String.Join(",", t.EnteredTeams.Select(x => x.Id))}|
                                    {String.Join(",", t.Prizes.Select(x => x.Id))}|
                                    {ConvertRoundListToString(t.Rounds)}";

                lines.Add(newLine);
                t.SaveRoundsToFile();
            }

            File.WriteAllLines(GlobalConfig.TournamentsFile.FullFilePath(), lines);
        }

        public static void SaveMatchupToFile(this List<MatchupModel> matchups)
        {
            List<string> lines = new List<string>();

            foreach (MatchupModel m in matchups)
            {
                string newline = $@"{m.Id}|{String.Join(",", m.Entries.Select(x => x.Id))}|";
                if (m.Winner == null)
                {
                    newline += $@"|{m.MatchupRound}";
                }
                else
                {
                    newline += $@"{m.Winner.Id}|{m.MatchupRound}";
                }
                lines.Add(newline);
            }
            File.WriteAllLines(GlobalConfig.MatchupsFile.FullFilePath(), lines);
        }

        public static void SaveMatchupEntryToFile(this List<MatchupEntryModel> matchupEntries)
        {
            List<string> lines = new List<string>();

            foreach (MatchupEntryModel m in matchupEntries)
            {
                string newline = $@"{m.Id}|";
                if (m.TeamCompeting == null)
                {
                    newline += $@"|{m.Score}|";
                }
                else
                {
                    newline += $@"{m.TeamCompeting.Id}|{m.Score}|";
                }
                if (m.ParentMatchup != null)
                {
                    newline += $@"{m.ParentMatchup.Id}";
                }
                lines.Add(newline);
            }

            File.WriteAllLines(GlobalConfig.MatchupEntryFile.FullFilePath(), lines);
        }

        public static void SaveRoundsToFile(this TournamentModel model)
        {
            List<MatchupModel> matchups = GlobalConfig.MatchupsFile.FullFilePath().LoadFile().ConvertToMatchupModels();
            List<MatchupEntryModel> entries = GlobalConfig.MatchupEntryFile.FullFilePath().LoadFile().ConvertToMatchupEntryModels();
            //Loop Through each round
            foreach (List<MatchupModel> round in model.Rounds)
            {
                //Loop Through each matchup
                foreach (MatchupModel matchup in round)
                {

                    //get top id and add 1
                    int currentId = 1;

                    if (matchups.Count > 0)
                    {
                        currentId = matchups.OrderByDescending(x => x.Id).First().Id + 1;
                    }
                    //Store the id
                    matchup.Id = currentId;

                    //save the matchup
                    matchups.Add(matchup);

                    //loop through and save the rounds
                    foreach (MatchupEntryModel entry in matchup.Entries)
                    {
                        currentId = 1;

                        if (entries.Count > 0)
                        {
                            currentId = entries.OrderByDescending(x => x.Id).First().Id + 1;
                        }

                        entry.Id = currentId;

                        entries.Add(entry);
                    }
                }
            }
            matchups.SaveMatchupToFile();
            entries.SaveMatchupEntryToFile();
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

        private static string ConvertRoundListToString(List<List<MatchupModel>> rounds)
        {
            string output = "";

            if (rounds.Count == 0)
            {
                return "";
            }

            foreach (List<MatchupModel> r in rounds)
            {
                foreach (MatchupModel m in r)
                {
                    output += $"{String.Join("^", r.Select(x => x.Id))}";
                }
                output += ",";

            }
            output = output.Substring(0, output.Length - 1);

            return output;
        }

        public static List<TournamentModel> ConvertToTournamentModels(this List<string> lines)
        {
            List<TournamentModel> output = new List<TournamentModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split('|');

                TournamentModel t = new TournamentModel();
                t.Id = int.Parse(cols[0]);
                t.TournamentName = cols[1];
                t.EntryFee = decimal.Parse(cols[2]);

                string[] entryIds = cols[3].Split(',');

                foreach (string id in entryIds)
                {
                    t.EnteredTeams.Add(LookUpTeamById(int.Parse(id)));
                }

                string[] prizeIds = cols[4].Split(',');

                foreach (string id in prizeIds)
                {
                    t.Prizes.Add(LookUpPrizeById(int.Parse(id)));
                }

                // TODO - Capture rounds info
                //splits into lists of MatchupModels
                string[] parsedRounds = cols[5].Split(',');

                foreach (string round in parsedRounds)
                {
                    //Splits into individual MatchupModels
                    string[] parsedRound = round.Split('^');
                    List<MatchupModel> r = new List<MatchupModel>();
                    foreach (string m in parsedRound)
                    {
                        r.Add(LookUpMatchupById(int.Parse(m)));
                    }
                    t.Rounds.Add(r);
                }
                output.Add(t);
            }

            return output;
        }

        public static List<MatchupEntryModel> ConvertToMatchupEntryModels(this List<string> lines)
        {
            List<MatchupEntryModel> output = new List<MatchupEntryModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split('|');

                MatchupEntryModel model = new MatchupEntryModel();

                model.Id = int.Parse(cols[0]);
                if (!String.IsNullOrWhiteSpace(cols[1]))
                {
                    model.TeamCompeting = LookUpTeamById(int.Parse(cols[1]));
                }
                if (!String.IsNullOrWhiteSpace(cols[2]))
                {
                    model.Score = double.Parse(cols[2]);
                }
                if (!String.IsNullOrWhiteSpace(cols[3]))
                {
                    model.ParentMatchup = LookUpMatchupById(int.Parse(cols[3]));
                }    

                output.Add(model);

            }
            return output;
        }

        public static List<MatchupModel> ConvertToMatchupModels(this List<string> lines)
        {
            List<MatchupModel> output = new List<MatchupModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split('|');

                MatchupModel matchup = new MatchupModel();
                matchup.Id = int.Parse(cols[0]);

                matchup.Entries = ConvertColToMatchupEntryModels(cols[1]);

                if (!String.IsNullOrWhiteSpace(cols[2]))
                {
                    matchup.Winner = LookUpTeamById(int.Parse(cols[2]));
                }

                matchup.MatchupRound = int.Parse(cols[3]);

            }

            return output;
        }
        public static List<MatchupEntryModel> ConvertColToMatchupEntryModels(string input)
        {
            string[] ids = input.Split(',');
            List<MatchupEntryModel> output = new List<MatchupEntryModel>();
            List<MatchupEntryModel> entries = GlobalConfig.MatchupEntryFile.FullFilePath().LoadFile().ConvertToMatchupEntryModels();

            foreach (string id in ids)
            {
                output.Add(entries.Where(x => x.Id == int.Parse(id)).First());
            }
            return output;
        }

        private static TeamModel LookUpTeamById(int id)
        {
            List<TeamModel> teams = GlobalConfig.TeamsFile.FullFilePath().LoadFile().ConvertToTeamModels();

            return teams.Where(x => x.Id == id).First();
        }
        private static MatchupModel LookUpMatchupById(int id)
        {
            List<MatchupModel> matchups = GlobalConfig.MatchupsFile.FullFilePath().LoadFile().ConvertToMatchupModels();

            return matchups.Where(x => x.Id == id).First();
        }
        private static MatchupEntryModel LookUpMatchupEntryById(int id)
        {
            List<MatchupEntryModel> entries = GlobalConfig.MatchupEntryFile.FullFilePath().LoadFile().ConvertToMatchupEntryModels();

            return entries.Where(x => x.Id == id).First();
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
