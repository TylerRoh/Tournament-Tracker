using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary
{
    public static class TournamentLogic
    {
        
        
        
        //Create first round of matchups
        //Create every round after that - 8 matchups - 4 matchups - 2 - 1

        public static void CreateRounds(TournamentModel model)
        {
            List<TeamModel> randomizedTeams = RandomizeTeamOrder(model.EnteredTeams);

            int rounds = FindNumberOfRounds(model.EnteredTeams.Count);

            int byes = FindNumberOfByes(rounds, model.EnteredTeams.Count);

        }

        private static List<MatchupModel> CreateFirstRound(int byes, List<TeamModel> teams)
        {
            
        }

        //Check if list is big enough - if not, add byes
        private static int FindNumberOfByes(int rounds, int numberOfTeams)
        {

            int totalTeams = 1;

            for (int i = 1; i < rounds; i++)
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
