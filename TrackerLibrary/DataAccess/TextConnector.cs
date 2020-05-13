using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using TrackerLibrary.DataAccess.TextConnector;
using TrackerLibrary.DataAccess;
using System.IO;

namespace TrackerLibrary.TextHelpers
{
    public class TextConnector : IDataConnection
    {
        //this is where all prize models will be stored
        private const string PrizesFile = "PrizeModels.csv";
        /// <summary>
        /// Saves a new prize to the txt doc
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The prize information, inluding unique identifier</returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            //loads the text file and converts to list of prize models
            List<PrizeModel> prizes =  PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();

            //orders the prizes by id in descending order and then saves the id + 1 to use for the new model
            int currentId = 1;

            if (prizes.Count > 0)
            {
                currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
            }
            
            model.Id = currentId;

            prizes.Add(model);

            prizes.SaveToPrizeFile(PrizesFile);

            return model;
        }
    }
}
