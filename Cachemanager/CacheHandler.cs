 
using System;
using System.Collections.Generic;
 
using System.Runtime.InteropServices;
 
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using StackExchange.Redis;

namespace ASCacheManager
{
    public class ASCacheProvider : IASCacheProvider
    {
         
        private static Lazy<ConnectionMultiplexer> _lazyRedisConnection;
        private static ConnectionMultiplexer _redisConnection => _lazyRedisConnection.Value;

        private readonly int DbNumber;
        private IDatabase _db => _redisConnection.GetDatabase(DbNumber);
        ConfigurationOptions options;
        /// <summary>
        /// Creates a new instance of <see cref="RedisCache"/>
        /// </summary>
        /// <param name="settings">The Redis connection settings</param>
        public ASCacheProvider(string connectionHost,int Databasenumber=0)
        {
            DbNumber = Databasenumber;
            options = new ConfigurationOptions()
            {
                AllowAdmin = true,
                ConnectTimeout = 60 * 1000,
            };
            options.EndPoints.Add(connectionHost);
            _lazyRedisConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(options), true);
        }

        /// <inheritdoc />
        public   T Get<T>(string key)
        {
            try
            {
                var redisObject =   _db.StringGet(key);
                if (redisObject.HasValue)
                {
                    return JsonConvert.DeserializeObject<T>(redisObject);
                }
            }
            catch (RedisConnectionException e)
            {
               
            }
            return default(T);
        }

        /// <inheritdoc />
        public bool Set<T>(string key, T objectToCache, TimeSpan? expiry = null)
        {
            string serializedObject = JsonConvert.SerializeObject(objectToCache);
            try
            {
                 
                return  _db.StringSet (key, serializedObject, expiry);
            }
            catch (RedisConnectionException e)
            {
                
            }
            return false;
        }

        /// <inheritdoc />
        public bool Remove(string key)
        {
            return   _db.KeyDelete(key);
        }

        /// <inheritdoc />
        public long Remove(IEnumerable<string> keys)
        {
            return   _db.KeyDelete(keys.Select(key => (RedisKey)key).ToArray());
        }

        /// <inheritdoc />
        public void FlushALLDB()
        {
            var endpoints = _redisConnection.GetEndPoints(true);
            foreach (var endpoint in endpoints)
            {
                var server = _redisConnection.GetServer(endpoint);
                  server.FlushAllDatabases();
            }
        }

        
    }

}
