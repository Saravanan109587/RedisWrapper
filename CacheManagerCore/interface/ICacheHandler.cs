using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartCache
{
    public interface ISmartCache
    {
        /// <summary>
        /// Retrieves a value from the cache
        /// </summary>
        /// <typeparam name="T">Type of object to get</typeparam>
        /// <param name="key">Cache key</param>
        /// <returns>Deserialized object from cache</returns>
       T Get<T>(string key);

        /// <summary>
        /// Saves an object in cache
        /// </summary>
        /// <typeparam name="T">Type of object to cache</typeparam>
        /// <param name="key">Cache key</param>
        /// <param name="objectToCache">Object to cache</param>
        bool Set<T>(string key, T objectToCache);

        /// <summary>
        /// Removes a key-value pair from the cache
        /// </summary>
        /// <param name="key">Cache key</param>
        /// <returns>Whether the operation succeded</returns>
        bool Remove(string key);
        bool RemoveAll(string keypattern);
        List<string> GetAllKeys(string keypattern);
        /// <summary>
        /// Removes multiple key-value pairs from the cache
        /// </summary>
        /// <param name="keys">Cache keys</param>
        /// <returns>The number of items removed</returns>
        long Remove(IEnumerable<string> keys);
        int GetKeyCount();
        /// <summary>
        /// Flushes the entire cache
        /// </summary>
        void FlushALLDB();
        void FlushDB(int DatabaseNumber = 0);
    }
}
