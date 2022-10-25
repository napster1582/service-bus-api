using Microsoft.Extensions.DependencyInjection;
using Platform.Service.Bus.Common.Interfaces;
using Platform.Service.Bus.Common.Services;
using Platform.Service.Bus.Domain.Events.Classes;
using Platform.Service.Bus.Domain.Events.Interfaces;
using Platform.Service.Bus.Domain.Handlers;
using Platform.Service.Bus.Domain.Handlers.Interfaces;
using Platform.Service.Bus.Domain.Interfaces;
using Platform.Service.Bus.Domain.Models;
using Platform.Service.Bus.Domain.Services;

namespace Platform.Service.Bus.Api.Extensions
{
    public static class DependencyInjectionExtension
    {

        public static void ConfigureDI(this IServiceCollection services)
        {
            // Config Service Bus Azure
            services.AddTransient<IServiceBus, ServiceBus>();

            // Handler Inactivity
            services.AddTransient<IHandlerQueue<TransferModel>, InactivityHandler>();

            // Handler Event Inactivity
            services.AddTransient<IHandlerEvent<TransferModel>, InactivityEvent>();

            // Service HttpClient
            services.AddTransient<IHttpSendFactoryService, HttpSendFactory>();

            // Logger Rollbar
            services.AddTransient<ILoggerService, LoggerService>();

        }
    }
}
