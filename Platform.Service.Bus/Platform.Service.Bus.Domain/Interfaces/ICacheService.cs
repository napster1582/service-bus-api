using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service.Bus.Domain.Interfaces
{
    public interface ICacheService
    {
        T Find<T>(string key, Func<T> func);
        void Delete(string key);
        T Get<T>(string key);
        void Add(string key, string value);
        Task<bool> AcquireLock(string key, string value, TimeSpan expiration);
        Task<bool> ReleaseLock(string key, string value);
        Task<bool> ExtendLock(string key, string value, TimeSpan expiration);
    }
}
