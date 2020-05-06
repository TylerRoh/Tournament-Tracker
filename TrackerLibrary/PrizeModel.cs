using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    public class PrizeModel
    {
        /// <summary>
        /// Represents what place the prize is for.
        /// </summary>
        public int PlaceNumber { get; set; }
        /// <summary>
        /// Represents what the place is called for the prize.
        /// </summary>
        public string PlaceName { get; set; }
        /// <summary>
        /// Represents the percentage of overall money the prize awards.
        /// </summary>
        public double PrizePercentage { get; set; }
        /// <summary>
        /// Represents the numerical value of the cash prize.
        /// </summary>
        public decimal PrizeAmount { get; set; }

    }
}
