using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoHelper.Test
{
    public class Tests
    {
        public List<TestObject> TestList = new List<TestObject>();

        /// <summary>
        /// To register a new test call RegisterTest() with a title and a bool. True means test was successfull, false means it failed 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="success"></param>
        public void RegisterTest(string title, bool success)
        {
            TestList.Add(new TestObject(TestList.Count, title, success));
        }

        public TestObject[] GetTestResults()
        {
            return TestList.ToArray();
        }

        public TestObject GetResult(int id)
        {
            return TestList.ElementAt(id);
        }

        public int GetScore()
        {
            return (100 / TestList.Count() * TestList.Where(t => t.Success == true).Count());
        }
    }

    public class TestObject
    {
        public int Id;
        public string Title;
        public bool Success;

        public TestObject(int id, string title, bool success)
        {
            Id = id;
            Title = title;
            Success = success;
        }
    }
}
