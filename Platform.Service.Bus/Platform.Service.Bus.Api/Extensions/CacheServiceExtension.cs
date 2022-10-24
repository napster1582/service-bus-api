using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Platform.Service.Bus.Domain.Interfaces;
using Platform.Service.Bus.Domain.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platform.Service.Bus.Api.Extensions
{
    public static class CacheServiceExtension
    {

        public static void ConfigureCacheService(this IServiceCollection services,IConfiguration configuration)
        {
            ConnectionMultiplexer connect = ConnectionMultiplexer.Connect(configuration.GetSection("Redis")["StringConnection"]);
            services.AddSingleton<ICacheService>(provider => new CacheService(connect.GetDatabase()));
        }
    }
}
