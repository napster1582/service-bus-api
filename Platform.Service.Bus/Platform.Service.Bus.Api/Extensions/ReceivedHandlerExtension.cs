
using Microsoft.AspNetCore.Builder;
using Microsoft.Azure.ServiceBus;

using Microsoft.Extensions.DependencyInjection;
using Platform.Service.Bus.Common.Interfaces;

using Platform.Service.Bus.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Platform.Service.Bus.Domain.Events.Interfaces;
using System.Text.Json;
using Newtonsoft.Json;

namespace Platform.Service.Bus.Api.Extensions
{
    public static class ReceivedHandlerExtension
    {


        public static void UseReceivedHandler(this IApplicationBuilder app)
        {
            var serviceBus = app.ApplicationServices.GetService<IServiceBus>();

            // Handlers
            var transferModelModelEvent = app.ApplicationServices.GetService<IHandlerEvent<TransferModel>>();

            Register(serviceBus, serviceBus.GetQueue("QueueNameInactivity"), transferModelModelEvent);
        }


        private  static void Register<T>(
         IServiceBus service,
         string queue,
         IHandlerEvent<T> handler) where T : class
        {
            var client = service.GetQueueClient(queue);

            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };



            client.RegisterMessageHandler(async (Message message, CancellationToken token) => {

                var payload = JsonConvert.DeserializeObject<T>(
                    Encoding.UTF8.GetString(message.Body)
                );
                await client.CompleteAsync(message.SystemProperties.LockToken);
                await handler.Execute(payload);
            }, messageHandlerOptions);
        }

        private static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            return Task.CompletedTask;
        }

    }
}
