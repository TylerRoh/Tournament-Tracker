using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class MatchupModel
    {
        /// <summary>
        /// Represents unique identifier for the Matchup model.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Represents the teams competing in the matchup.
        /// </summary>
        public List<MatchupEntryModel> Entries { get; set; } = new List<MatchupEntryModel>();
        /// <summary>
        /// The Id from the database that will be used to identify the winner
        /// </summary>
        public int WinnerId { get; set; }
        /// <summary>
        /// Represents the team that won the matchup.
        /// </summary>
        public TeamModel Winner { get; set; }
        /// <summary>
        /// Represents the round that the matchup is taking place in.
        /// </summary>
        public int MatchupRound { get; set; }
        /// <summary>
        /// Returns the Team names in the tournament viewer to display the matchup
        /// </summary>
        public string DisplayName
        {
            get
            {
                List<string> teamNames = new List<string>();
                foreach (MatchupEntryModel me in Entries)
                {
                    if (me.TeamCompeting != null)
                    {
                        teamNames.Add(me.TeamCompeting.TeamName);
                    }
                }
                if (teamNames.Count >= 1)
                {
                    return String.Join(" vs. ", teamNames);
                }
                else
                {
                    return "TBD";
                }
                
            }
        }
    }
}
