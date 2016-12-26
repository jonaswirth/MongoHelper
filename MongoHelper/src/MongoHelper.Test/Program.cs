using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoHelper;
using MongoDB.Bson;

namespace MongoHelper.Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MongoHelper mongo = new MongoHelper("test");
            Tests tests = new Tests();

            //Select Count
            tests.RegisterTest("Select count", 7 == mongo.SelectCount("collection1", new BsonDocument()));

            TestObject[] result = tests.GetTestResults();
            foreach(TestObject res in result)
            {
                Console.WriteLine(res.Id + " > " + res.Title + " : " + res.Success);
            }
            Console.WriteLine(tests.GetScore() + "% Success");
            Console.ReadKey();
            mongo.Dispose();
        }
    }
}
