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
        public IEnumerable<SamplePortfolio> Get()
        {
            var server = MongoServer.Create(connectionString);
            var db = server.GetDatabase("rt_mongo");
            var collection = db.GetCollection<SamplePortfolio>("SamplePortfolio");
            return collection.FindAll();
        }

        // GET api/values/5
        public SamplePortfolio Get(string id)
        {
            var server = MongoServer.Create(connectionString);
            var db = server.GetDatabase("rt_mongo");
            var collection = db.GetCollection<SamplePortfolio>("SamplePortfolio");
            var sample = collection.AsQueryable().FirstOrDefault(f => f.StuffID == id);
            if (sample == null) return new SamplePortfolio();
            return sample;
        }

        // POST api/values
        public void Post(SamplePortfolio value)
        {
            var server = MongoServer.Create(connectionString);
            var db = server.GetDatabase("rt_mongo");
            var collection = db.GetCollection<SamplePortfolio>("SamplePortfolio");
            collection.Insert(value);
        }

        // PUT api/values/5
        public void Put(string id, SamplePortfolio value)
        {
            var server = MongoServer.Create(connectionString);
            var db = server.GetDatabase("rt_mongo");
            var collection = db.GetCollection<SamplePortfolio>("SamplePortfolio");
            var update = Update.Set("StuffID", value.StuffID)
                .Set("TVol", value.TVol)
                .Set("TValue", value.TValue)
                .Set("PriceC", value.PriceC)
                .Set("OfferPrice", value.OfferPrice)
                .Set("IRate", value.IRate);
            collection.Update(Query.EQ("StuffID", value.StuffID), update);
        }

        // DELETE api/values/5
        public void Delete(string id)
        {
            var server = MongoServer.Create(connectionString);
            var db = server.GetDatabase("rt_mongo");
            var collection = db.GetCollection<SamplePortfolio>("SamplePortfolio");
            collection.Remove(Query.EQ("StuffID", id));
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
     [BsonIgnoreExtraElements]
    public class SamplePortfolio
    {
        public int Index { get; set; }

        public string StuffID { get; set; }

        public double BidPrice { get; set; }

        public double OfferPrice { get; set; }

        public double PriceC { get; set; }

        public double TVol { get; set; }

        public double TValue { get; set; }

        public double IRate { get; set; }
    }
}