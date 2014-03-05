using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace RTServer.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<Record> Get()
        {
            //var list = new List<Record>();

            //for (int i = 0; i < 10; i++)
            //{
            //    list.Add(new Record
            //    {
            //        RecordKey = i,
            //        Value = "Value "+ i,
            //        Description = "Description : " + DateTime.Now
            //    });
            //}
            //return list;
            var outputDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var d = Path.Combine(outputDirectory, "Customers.txt");
            var d1 = new Uri(d).LocalPath;
            using (var sr = new StreamReader(Path.Combine(outputDirectory,"Customers.txt")))
            {
                while (!sr.EndOfStream)
                {
                    var strLine = sr.ReadLine();
                    var data = strLine.Split('\t');

                    foreach (var s in data)
                    {

                    }
                }
            }
            return null;
        }


        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }

    public class Record
    {
        public int RecordKey { get; set; }

        public string Value { get; set; }

        public string Description { get; set; }
    }
}