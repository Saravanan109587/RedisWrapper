
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseRepo;
using Dapper;
using System.Data;
using Newtonsoft.Json;
using SmartCache; 
//using ASCacheManager;
namespace SampleAPp
{
    class Program 
    {
       
        static   void Main(string[] args)
        {

            RepoTest c = new RepoTest();
             c.LoadAllEncryptedTs();
           
      

            // var test  =c.Get<byte[]>("8866");
        }

       
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
    public class RepoTest
    { 
        ISmartCache cache;
        public RepoTest()
        {
            cache = new SmartCacheProvider("10.10.100.63:6379",1);
            //cache = new SmartCacheProvider("10.10.100.63:6379");


        }
        //BaseRepo.Repository repo = new Repository("data source=PTQAASSRV03;initial catalog=AlternativeSoft_DB;persist security info=True;user id=asoft;Password=alter@123");

        //441109
        //441110
        public   void LoadAllEncryptedTs()
        {
             
            try
            {

                cache.FlushDB(1);
                //cache.Set<EncryptedTimeseriesData123>("88789", new EncryptedTimeseriesData123()
                //{

                //    FundID = 12,
                //    FundName = "Sajaja"
                //});
                //var keys = cache.GetAllKeys("887*");
                //var test12 = cache.Get<EncryptedTimeseriesData123>("88*");
                //cache.Remove("353524");
                int count = cache.GetKeyCount();
                Console.WriteLine("Count is "  + count);
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception Ocuured" +e.Message);
                Console.ReadLine();
                
            }
            Console.WriteLine("Success");
            Console.ReadLine();
            //var test = cache.Get<EncryptedTimeseriesData123>("8877");
            //var counts = cache.GetKeyCount();
            
           // List<EncryptedTimeseriesData123> data = repo.ExecuteProcedureSingleResult<EncryptedTimeseriesData123>("USp_GetEncryptedTimeSeries").ToList();
           
           
           
             
            Console.ReadLine();

        }
    }
}
