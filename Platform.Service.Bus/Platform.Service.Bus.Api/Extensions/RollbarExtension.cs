
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rollbar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Platform.Service.Bus.Api.Extensions
{
    public static class RollbarExtension
    {
        public static void ConfigureRollbar(this IServiceCollection services,IConfiguration configuration)
        {
            string accessToken = configuration.GetSection("RollbarConfiguration")["AccessToken"];
            string environment = configuration.GetSection("RollbarConfiguration")["Environment"];
            RollbarLocator.RollbarInstance.Configure(new RollbarLoggerConfig(accessToken, environment));
            RollbarLocator.RollbarInstance.Info("Rollbar is up and running.");
        }

    }
}
