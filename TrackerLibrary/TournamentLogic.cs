﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using TrackerLibrary.Models;

namespace TrackerLibrary
{
    public static class TournamentLogic
    {

        public static void CreateRounds(TournamentModel model)
        {
            List<TeamModel> randomizedTeams = RandomizeTeamOrder(model.EnteredTeams);

            int rounds = FindNumberOfRounds(model.EnteredTeams.Count);

            int byes = FindNumberOfByes(rounds, model.EnteredTeams.Count);

            model.Rounds.Add(CreateFirstRound(byes, randomizedTeams));

            CreateOtherRounds(model, rounds);
        }

        public static void GenerateNextRound(TournamentModel model)
        {
            int currentRoundIndex = 0;
            foreach (List<MatchupModel> round in model.Rounds)
            {
                if (round.First().Entries.First().TeamCompeting == null)
                {
                    break;
                }
                else
                {
                    currentRoundIndex++;
                }
            }
            List<TeamModel> winners = new List<TeamModel>();
            foreach (MatchupModel match in model.Rounds[currentRoundIndex - 1])
            {
                winners.Add(match.Winner);
            }
            foreach (MatchupModel match in model.Rounds[currentRoundIndex])
            {
                foreach (TeamModel team in winners.ToList())
                {
                    if (match.Entries[0].TeamCompeting == null)
                    {
                        match.Entries[0].TeamCompeting = team;
                        winners.Remove(team);
                    }
                    else if (match.Entries[1].TeamCompeting == null)
                    {
                        match.Entries[1].TeamCompeting = team;
                        winners.Remove(team);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public static bool RoundCompleteCheck(TournamentModel model)
        {
            bool output = false;
            //Loop through rounds
            foreach (List<MatchupModel> round in model.Rounds)
            {
                //check if current round has teams competing if not return true
                int winners = 0;
                if (round.First().Entries.First().TeamCompeting == null)
                {
                    output = true;
                    break;
                }
                else
                {
                    //count how many matchups are completed
                    foreach (MatchupModel matchup in round)
                    {
                        if (matchup.Winner != null)
                        {
                            winners += 1;
                        }
                    }
                    //if not every matchup has a winner return false
                    if (winners < round.Count)
                    {
                        break;
                    }
                }
            }
            return output;
        }

        //Create every round after that - 8 matchups - 4 matchups - 2 - 1
        private static void CreateOtherRounds(TournamentModel model, int rounds)
        {
            int round = 2;
            List<MatchupModel> previousRound = model.Rounds[0];
            List<MatchupModel> currentRound = new List<MatchupModel>();
            MatchupModel currentMatchup = new MatchupModel();

            while (round <= rounds)
            {
                foreach (MatchupModel match in previousRound)
                {
                    currentMatchup.Entries.Add(new MatchupEntryModel { ParentMatchup = match });

                    if (currentMatchup.Entries.Count > 1)
                    {
                        currentMatchup.MatchupRound = round;
                        currentRound.Add(currentMatchup);
                        currentMatchup = new MatchupModel();
                    }
                }
                model.Rounds.Add(currentRound);
                previousRound = currentRound;
                currentRound = new List<MatchupModel>();
                round += 1;
            }
        }

        //Create first round of matchups
        private static List<MatchupModel> CreateFirstRound(int byes, List<TeamModel> teams)
        {
            List<MatchupModel> output = new List<MatchupModel>();
            MatchupModel current = new MatchupModel();

            foreach (TeamModel team in teams)
            {
                current.Entries.Add(new MatchupEntryModel { TeamCompeting = team });

                //if there is byes left it will add a matchup model with 1 team to a round to output
                //if there are 2 teams in the matchup it will add the matchup model to output
                if (byes > 0 || current.Entries.Count > 1)
                {
                    current.MatchupRound = 1;
                    current.Winner = current.Entries[0].TeamCompeting;
                    output.Add(current);
                    current = new MatchupModel();
                    
                    if (byes > 0)
                    {
                        byes -= 1;
                    }    
                }
            }
            return output;
        }

        //Check if list is big enough - if not, add byes
        private static int FindNumberOfByes(int rounds, int numberOfTeams)
        {

            int totalTeams = 1;

            for (int i = 0; i < rounds; i++)
            {
                totalTeams *= 2;
            }

            return totalTeams - numberOfTeams;

        }
        // Find what power of 2 the tournament will be (number of rounds)
        private static int FindNumberOfRounds(int teamCount)
        {
            int output = 1;
            int val = 2;

            while (val < teamCount)
            {
                val *= 2;
                output += 1;
            }

            return output;
        }
        //Order list randomly of teams
        private static List<TeamModel> RandomizeTeamOrder(List<TeamModel> teams)
        {
            return teams.OrderBy(x => Guid.NewGuid()).ToList();
        }

    }
}
