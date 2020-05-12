using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    public class TextConnection : IDataConnection
    {
        // TODO - Wire up the CreatePrize for the text files
        /// <summary>
        /// Saves a new prize to the txt doc
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The prize information, inluding unique identifier</returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            model.Id = 1;
            return model;
        }
    }
}
