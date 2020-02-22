
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartCache
{
    public class SmartCacheProvider : ISmartCache
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
        public SmartCacheProvider(string connectionHost, int Databasenumber = 0)
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
  
        /// <summary>
        /// Remove all the matching keys with the pattern
        /// </summary>
        /// <param name="keypattern"></param>
        /// <returns></returns>
    public bool RemoveAll(string keypattern)
    {
        try
        {     
                var endpoints = _redisConnection.GetEndPoints(true);
                var server = _redisConnection.GetServer(endpoints[0]);
                if (server != null)
                {
                    foreach (var skey in server.Keys(database:DbNumber, pattern: keypattern))
                    {
                        _db.KeyDelete(skey);
                    }
                }
               
        }
        catch (RedisConnectionException)
        {
                return false;
        }
            return true;
        }
    /// <inheritdoc />
    public T Get<T>(string key)
        {
            try
            {
                var redisObject = _db.StringGet(key);
                if (redisObject.HasValue)
                {
                    return JsonConvert.DeserializeObject<T>(redisObject);
                }
            }
            catch (RedisConnectionException)
            {

            }
            return default(T);
        }

        /// <inheritdoc />
        public bool Set<T>(string key, T objectToCache)
        {
            string serializedObject = JsonConvert.SerializeObject(objectToCache);
            try
            {
                return _db.StringSet(key, serializedObject);
            }
            catch (RedisConnectionException)
            {

            }
            return false;
        }

        public bool Set<T>(string key, T objectToCache,TimeSpan expiry)
        {
            string serializedObject = JsonConvert.SerializeObject(objectToCache);
            try
            {
                return _db.StringSet(key, serializedObject, expiry: expiry);
            }
            catch (RedisConnectionException)
            {

            }
            return false;
        }
        public bool Set<T>(string key, T objectToCache, TimeSpan expiry,When when)
        {
            string serializedObject = JsonConvert.SerializeObject(objectToCache);
            try
            {
                return _db.StringSet(key, serializedObject, expiry: expiry,when: when);
            }
            catch (RedisConnectionException)
            {

            }
            return false;
        }

        /// <inheritdoc />
        public bool Remove(string key)
        {
            return _db.KeyDelete(key);
        }

        /// <inheritdoc />
        public long Remove(IEnumerable<string> keys)
        {
            return _db.KeyDelete(keys.Select(key => (RedisKey)key).ToArray());
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

        public void FlushDB(int DatabaseNumber=0)
        {
            var endpoints = _redisConnection.GetEndPoints(true);
            foreach (var endpoint in endpoints)
            {
                var server = _redisConnection.GetServer(endpoint);
                server.FlushDatabase(DatabaseNumber);
            }
        }

        public int GetKeyCount()
        {
            var endpoints = _redisConnection.GetEndPoints(true); 
             var server = _redisConnection.GetServer(endpoints[0]);
           return server.Keys().Count();
        }

        public List<string> GetAllKeys(string keypattern)
        {
            var endpoints = _redisConnection.GetEndPoints(true);
            var server = _redisConnection.GetServer(endpoints[0]);
            if (server != null)
            {
                return server.Keys(database: DbNumber, pattern: keypattern).Select(v=>v.ToString()).ToList();               
            }
            return null;
        }
    }

}
