using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoHelper
{
    public interface ILogger
    {
        void WriteError(string title, string function, string message);
    }
}
