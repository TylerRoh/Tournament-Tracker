using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class PrizeModel
    {
        /// <summary>
        /// The unique identifier for the prize
        /// </summary>
        public int Id { get; set; }

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

        public PrizeModel()
        {

        }
        public PrizeModel(string placeName, string placeNumber, string prizeAmount, string prizePercentage)
        {
            PlaceName = placeName;

            int placeNumberValue = 0;
            int.TryParse(placeNumber, out placeNumberValue);
            PlaceNumber = placeNumberValue;

            decimal prizeAmountValue = 0;
            decimal.TryParse(prizeAmount, out prizeAmountValue);
            PrizeAmount = prizeAmountValue;

            double prizePercentageValue = 0;
            double.TryParse(prizePercentage, out prizePercentageValue);
            PrizePercentage = prizePercentageValue;

        }

    }
}
