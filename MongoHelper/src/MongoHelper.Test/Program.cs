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

            //Select Count no filter
            tests.RegisterTest("Select count no filter", 7 == mongo.SelectCount("collection1"));
            //Select Count empty filter
            tests.RegisterTest("Select count empty filter", 7 == mongo.SelectCount("collection1", new BsonDocument()));
            //Select Count filter (field, value(string))
            tests.RegisterTest("Select count filter (field, value(string))", 1 == mongo.SelectCount("collection1", "String", "abc"));
            //Select Count filter (field, id)
            tests.RegisterTest("Select count filter (field, id)", 1 == mongo.SelectCount("collection1", "_id", ObjectId.Parse("582866814d8a102a38716a9d")));
            //Select Count filter (field, value(int))
            tests.RegisterTest("Select count filter (field, value(int))", 1 == mongo.SelectCount<int>("collection1", "Int", 79));

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
