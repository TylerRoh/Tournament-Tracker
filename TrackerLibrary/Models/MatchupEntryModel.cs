using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class MatchupEntryModel
    {
        /// <summary>
        /// Represents unique identifier for the Matchup model.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The Id from the database that will be used to identify the team competing
        /// </summary>
        public int TeamCompetingId { get; set; }
        /// <summary>
        /// Represents one team in the matchup.
        /// </summary>
        public TeamModel TeamCompeting { get; set; }
        /// <summary>
        /// Represents score for this particular team.
        /// </summary>
        public double Score { get; set; }
        /// <summary>
        /// The Id from the database that will be used to identify the parent matchup
        /// </summary>
        public int ParentMatchupId { get; set; }
        /// <summary>
        /// Represents the round the team was in previously.
        /// </summary>
        public MatchupModel ParentMatchup { get; set; }

    }
}
