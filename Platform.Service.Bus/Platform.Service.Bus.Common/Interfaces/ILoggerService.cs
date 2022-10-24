using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service.Bus.Common.Interfaces
{
    public interface ILoggerService
    {
        void Critical(Exception exception);
        void Error(Exception exception);
        void Warning(string message);
        void Info(string message);
        void Debug(string message);
    }
}
