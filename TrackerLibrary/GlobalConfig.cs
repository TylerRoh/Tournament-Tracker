using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using TrackerLibrary.DataAccess;
using System.Configuration;
using TrackerLibrary.TextHelpers;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        //this is where all prize models will be stored
        public const string PrizesFile = "PrizeModels.txt";
        //All Person Models stored here
        public const string PeopleFile = "PersonModels.txt";
        //Team name and id stored here
        public const string TeamsFile = "TeamsModels.txt";
        //Tournaments are stored here
        public const string TournamentsFile = "TournamentModels.txt";
        //Matchups file
        public const string MatchupsFile = "MatchupModels.txt";
        //Matchup Entry file
        public const string MatchupEntryFile = "MatchupEntryModels.txt";

        public static IDataConnection Connection { get; private set; }

        public static void InitializeConnections(DatabaseType db)
        {

            if (db == DatabaseType.Sql)
            {
                
                SqlConnector sql = new SqlConnector();
                Connection = sql;
            }
            else if (db == DatabaseType.TextFile)
            {
                
                TextConnector text = new TextConnector();
                Connection = text;
            }
        }
        public static string CnnString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }


    }
    
}
