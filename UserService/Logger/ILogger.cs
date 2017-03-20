using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public interface ILogger
    {
        void Warn(string message);

        void Error(string message);

        void Error(Exception e, string message);

        void Info(string message);
    }
}
