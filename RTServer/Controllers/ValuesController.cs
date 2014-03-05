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
        public IEnumerable<Customer> Get()
        {
            //var list = new List<Customer>();

            //for (int i = 0; i < 10; i++)
            //{
            //    list.Add(new Customer
            //    {
            //        OrderId = i * 123,
            //        CustomerId = "Customer "+ i,
            //        EmployeeId = i,
            //        Freight = (double)i/32,
            //        OrderDate = DateTime.Now,
            //        ShipAdress = "Lviv, Street " + i *2 ,
            //        ShipName = "SomeName"
            //    });
            //}
            //return list;
            var list = new List<Customer>();
            var properties = typeof (Customer).GetProperties();

            var outputDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var logoFile = Path.Combine(outputDirectory, "Customers.txt");
            var realLogo = new Uri(logoFile).LocalPath;

            using (var sr = new StreamReader(realLogo))
            {
                while (!sr.EndOfStream)
                {
                    var strLine = sr.ReadLine();
                    var data = strLine.Split('\t');
                    var customer = new Customer
                    {
                        OrderId = Convert.ToInt32(data[0]),
                        CustomerId = data[1],
                        EmployeeId = Convert.ToInt32(data[2]),
                        OrderDate = Convert.ToDateTime(data[3]),
                        Freight = Convert.ToDouble(data[4]),
                        ShipName = data[5],
                        ShipAdress = data[6]
                    };
                    list.Add(customer);
                }
            }
            return list;
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

    public class Customer
    {
        public int OrderId { get; set; }

        public string CustomerId { get; set; }

        public int EmployeeId { get; set; }

        public DateTime OrderDate { get; set; }

        public double Freight { get; set; }

        public string ShipName { get; set; }

        public string ShipAdress { get; set; }


        public override bool Equals(object obj)
        {
            var record = obj as Customer;

            if (this.OrderId != record.OrderId ||
                this.CustomerId != record.CustomerId ||
                this.EmployeeId != record.EmployeeId ||
                this.OrderDate != record.OrderDate ||
                this.Freight != record.Freight ||
                this.ShipName != record.ShipName ||
                this.ShipAdress != record.ShipAdress)
                return false;
            return true;
        }

        public Customer Clone()
        {
            var temp = new Customer()
            {
                OrderId = OrderId,
                CustomerId = CustomerId,
                EmployeeId = EmployeeId,
                OrderDate = OrderDate,
                Freight = Freight,
                ShipAdress = ShipAdress,
                ShipName = ShipName
            };
            return temp;
        }
    }
}