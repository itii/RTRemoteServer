using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace RTServer.Controllers
{
    public class ValuesController : ApiController
    {
        const string connectionString = "mongodb://itii:falconsoft@ds037737.mongolab.com:37737/rt_mongo";

        // GET api/values
        public IEnumerable<Customer> Get()
        {
            var server = MongoServer.Create(connectionString);
            var db = server.GetDatabase("rt_mongo");
            var collection = db.GetCollection<Customer>("Customers");
            return collection.FindAll();
        }

        // GET api/values/5
        public Customer Get(int id)
        {
            var server = MongoServer.Create(connectionString);
            var db = server.GetDatabase("rt_mongo");
            var collection = db.GetCollection<Customer>("Customers");
            var customer = collection.AsQueryable().FirstOrDefault(f => f.OrderId == id);
            if (customer == null) return new Customer();
            return customer;
        }

        // POST api/values
        public void Post(Customer value)
        {
            var server = MongoServer.Create(connectionString);
            var db = server.GetDatabase("rt_mongo");
            var collection = db.GetCollection<Customer>("Customers");
            collection.Insert(value);
        }

        // PUT api/values/5
        public void Put(int id, Customer value)
        {
            var server = MongoServer.Create(connectionString);
            var db = server.GetDatabase("rt_mongo");
            var collection = db.GetCollection<Customer>("Customers");
            var update = Update.Set("OrderId", value.OrderId)
                               .Set("CustomerId", value.CustomerId)
                               .Set("EmployeeId", value.EmployeeId)
                               .Set("OrderDate", value.OrderDate)
                               .Set("Freight", value.Freight)
                               .Set("ShipName", value.ShipName)
                               .Set("ShipAdress", value.ShipAdress);
            collection.Update(Query.EQ("OrderId", value.OrderId),update);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            var server = MongoServer.Create(connectionString);
            var db = server.GetDatabase("rt_mongo");
            var collection = db.GetCollection<Customer>("Customers");
            collection.Remove(Query.EQ("OrderId", id));
        }
    }
    [BsonIgnoreExtraElements]
    public class Customer
    {
        public int Index { get; set; }

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
                Index = Index,
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