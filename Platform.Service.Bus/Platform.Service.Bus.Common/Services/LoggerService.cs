using Platform.Service.Bus.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rollbar;

namespace Platform.Service.Bus.Common.Services
{
    public class LoggerService : ILoggerService
    {
        public void Critical(Exception exception) => RollbarLocator.RollbarInstance.Critical(exception);
        public void Error(Exception exception) => RollbarLocator.RollbarInstance.Critical(exception);
        public void Warning(string message) => RollbarLocator.RollbarInstance.Warning(message);
        public void Info(string message) => RollbarLocator.RollbarInstance.Info(message);
        public void Debug(string message) => RollbarLocator.RollbarInstance.Debug(message);
    }
}
