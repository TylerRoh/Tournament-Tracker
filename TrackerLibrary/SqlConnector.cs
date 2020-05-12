using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    public class SqlConnector : IDataConnection
    {
        // TODO - Make the CreatePrize method actually save to the database
        /// <summary>
        /// Saves a new prize to the sql database
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
