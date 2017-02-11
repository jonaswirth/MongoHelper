using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoHelper.Test
{
    public class MyLogger : ILogger
    {
        public void WriteError(string title, string function, string message)
        {
            throw new NotImplementedException();
        }
    }
}
