using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RTServer.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<Customer> Get()
        {
            var list = new List<Customer>();

            for (int i = 0; i < 10; i++)
            {
                list.Add(new Customer
                {
                    OrderId = i * 123,
                    CustomerId = "Customer "+ i,
                    EmployeeId = i,
                    Freight = (double)i/32,
                    OrderDate = DateTime.Now,
                    ShipAdress = "Lviv, Street " + i *2 ,
                    ShipName = "SomeName"
                });
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