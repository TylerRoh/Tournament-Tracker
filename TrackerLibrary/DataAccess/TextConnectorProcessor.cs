﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
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

        public static void SaveToPrizeFile(this List<PrizeModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (PrizeModel p in models)
            {
                lines.Add($"{p.Id}|{p.PlaceNumber}|{p.PlaceName}|{p.PrizeAmount}|{p.PrizePercentage}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static List<PersonModel> ConvertToPersonModels(this List<string> lines)
        {
            List<PersonModel> output = new List<PersonModel>();

            foreach(string line in lines)
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

        public static List<TeamModel> ConvertToTeamModels(this List<string> lines, string peopleFileName)
        {
            List<TeamModel> output = new List<TeamModel>();
            List<PersonModel> people = peopleFileName.FullFilePath().LoadFile().ConvertToPersonModels();

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
                        t.TeamMembers.Add(people.Where(x => x.Id == int.Parse(id)).First());
                    }


                    output.Add(t);
                }
            }
            return output;
        }

        public static void SaveToPersonFile(this List<PersonModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (PersonModel p in models)
            {
                lines.Add($"{p.Id}|{p.FirstName}|{p.LastName}|{p.EmailAddress}|{p.PhoneNumber}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static void SaveToTeamsFile(this List<TeamModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (TeamModel t in models)
            {
                string newLine = $"{t.Id}|{t.TeamName}|{String.Join(",", t.TeamMembers.Select(x => x.Id))}";

                lines.Add(newLine);
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static List<TournamentModel> ConvertToTournamentModels(this List<string> lines, string teamsFileName, string prizesFileName, string peopleFileName)
        {
            List<TournamentModel> output = new List<TournamentModel>();
            List<TeamModel> teams = teamsFileName.FullFilePath().LoadFile().ConvertToTeamModels(peopleFileName);
            List<PrizeModel> prizes = prizesFileName.FullFilePath().LoadFile().ConvertToPrizeModels();

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
                    t.EnteredTeams.Add(teams.Where(x => x.Id == int.Parse(id)).First());
                }

                string[] prizeIds = cols[4].Split(',');

                foreach (string id in prizeIds)
                {
                    t.Prizes.Add(prizes.Where(x => x.Id == int.Parse(id)).First());
                }

                output.Add(t);
            }

            return output;
        }

        public static void SaveToTournamentsFile(this List<TournamentModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (TournamentModel t in models)
            {
                string newLine = $"{t.Id}|{t.TournamentName}|{t.EntryFee}|{String.Join(",", t.EnteredTeams.Select(x => x.Id))}|{String.Join(",", t.Prizes.Select(x => x.Id))}";

                lines.Add(newLine);
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

    }
}
