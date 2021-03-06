﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class TournamentModel
    {
        public TournamentModel()
        {

        }
        public TournamentModel(string tournamentName, string entryFee, List<TeamModel> enteredTeams, List<PrizeModel> prizes)
        {
            TournamentName = tournamentName;
            decimal entryFeeValue = 0;
            decimal.TryParse(entryFee, out entryFeeValue);
            EntryFee = entryFeeValue;
            EnteredTeams = enteredTeams;
            Prizes = prizes;
            

        }
        /// <summary>
        /// Represents unique identifier for the tournament model.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Represents the name of the tournament.
        /// </summary>
        public string TournamentName { get; set; }
        /// <summary>
        /// Represents the entry fee per team.
        /// </summary>
        public decimal EntryFee { get; set; }
        /// <summary>
        /// Represents all of the teams competing in the tournament.
        /// </summary>
        public List<TeamModel> EnteredTeams { get; set; } = new List<TeamModel>();
        /// <summary>
        /// Represents all of the prizes awarded in the tournament.
        /// </summary>
        public List<PrizeModel> Prizes { get; set; } = new List<PrizeModel>();
        /// <summary>
        /// Represents a list of all of the machups in each round.
        /// </summary>
        public List<List<MatchupModel>> Rounds { get; set; } = new List<List<MatchupModel>>();
    }
}
