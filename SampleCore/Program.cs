using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace SampleCore
{
    class Program
    {
        static void Main(string[] args)
        {

            RepoTest c = new RepoTest();
            c.LoadAllEncryptedTs();



         
        }

    }

    public class RepoTest
    {
        IASCacheProvider cache;
        public RepoTest()
        {
            cache = new CacheHandlerExchange("10.10.100.63:6379");
        }
        [Serializable]
        public class EncryptedTimeseriesData123
        {
            public int FundID { get; set; }
            public string FundName { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
            public byte[] returnsData { get; set; }

        }
        public void LoadAllEncryptedTs()
        {
            EncryptedTimeseriesData123 d = new EncryptedTimeseriesData123();
             
            var test = cache.Get<string>("441109");
             
        }
    }
}
