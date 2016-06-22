using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alumini.Logger
{
    public interface ILogger
    {

        void Error(string message, Exception ex);
        void Fatal(string message, Exception ex);
        void Debug(string message, Exception ex);

    }
}
