using Newtonsoft.Json;
using Platform.Service.Bus.Domain.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service.Bus.Domain.Services
{
    public class CacheService : ICacheService
    {
        #region Attributes
        private readonly IDatabase Rediscache;
        #endregion

        #region Constructor
        public CacheService(IDatabase rediscache)
        {
            Rediscache = rediscache;
        } 
        #endregion

        public async Task<bool> AcquireLock(string key, string value, TimeSpan expiration)
        {
            key = UpdateKey(key);
            return await Rediscache.LockTakeAsync(key, value, expiration);
        }

        public void Add(string key, string value)
        {
            try
            {
                key = UpdateKey(key);
                Rediscache.StringSet(key, JsonConvert.SerializeObject(value));
            }
            catch (Exception)
            {
                //logger error
            }
        }

        public void Delete(string key)
        {
            try
            {
                key = UpdateKey(key);
                Rediscache.KeyDelete(key);
            }
            catch (Exception)
            {
                //logger error
            }
        }

        public async Task<bool> ExtendLock(string key, string value, TimeSpan expiration)
        {
            key = UpdateKey(key);
            return await Rediscache.LockExtendAsync(key, value, expiration);
        }

        public T Find<T>(string key, Func<T> func)
        {
            try
            {
                key = UpdateKey(key);
                var valFromKey = Rediscache.StringGet(key);
                if (string.IsNullOrEmpty(valFromKey))
                {
                    var response = func();
                    Rediscache.StringSet(key, JsonConvert.SerializeObject(response));
                    return response;
                }
                else
                {
                    return JsonConvert.DeserializeObject<T>(valFromKey);
                }
            }
            catch (Exception)
            {
                //logger error
                return func();
            }
        }

        public T Get<T>(string key)
        {
            try
            {
                key = UpdateKey(key);
                var valFromKey = Rediscache.StringGet(key);
                return JsonConvert.DeserializeObject<T>(valFromKey);
            }
            catch (Exception)
            {
                return default;
            }
        }

        public async Task<bool> ReleaseLock(string key, string value)
        {
            key = UpdateKey(key);
            return await Rediscache.LockReleaseAsync(key, value);
        }

        private string UpdateKey(string key)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (env == "Development")
                return _ = $"DEV_{key}";
            else
                return key;
        }
    }
}
