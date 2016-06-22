using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alumini.Logger
{
  public  class Logger:ILogger
    {
      NLog.Logger logger =LogManager.GetCurrentClassLogger();

      public void Error(string message, Exception ex)
      {
          logger.Error(message, ex);
      }

      public void Fatal(string message, Exception ex)
      {
          logger.Fatal(message, ex);
      }

      public void Debug(string message, Exception ex)
      {
          logger.Debug(message, ex);
      }
    }
}
