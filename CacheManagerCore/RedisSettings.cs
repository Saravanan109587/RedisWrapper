 
using System.Configuration;
 

namespace SmartCache
{
    public class RedisSettings : IRedisSettings
    {
        private const string ConnectionStringKey = "RedisConnection";
        private const string DatabaseNumberKey = "RedisDatabaseNumber";

        /// <summary>
        /// Creates a new instance of <see cref="RedisSettings"/>
        /// </summary>
        public RedisSettings()
        {
            //ConnectionString = ConfigurationManager.ConnectionStrings[ConnectionStringKey]?.ConnectionString;
            //int dbNumber;
            //int.TryParse(ConfigurationManager.AppSettings[DatabaseNumberKey], out dbNumber);
            //DatabaseNumber = dbNumber;
        }

        /// <inheritdoc />
        public string ConnectionString { get; }

        /// <inheritdoc />
        public int DatabaseNumber { get; }
    }
}
