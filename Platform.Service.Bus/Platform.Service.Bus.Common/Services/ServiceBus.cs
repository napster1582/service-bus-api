using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Extensions.Configuration;
using Platform.Service.Bus.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service.Bus.Common.Services
{
    public class ServiceBus : IServiceBus
    {

        #region Attributes
        private readonly IConfiguration _configuration;
        #endregion


        #region Constructor
        public ServiceBus(IConfiguration configuration)
        {
            _configuration = configuration;
        } 
        #endregion

        public string GetQueue(string queueName)
        {

            return _configuration.GetSection($"AzureServiceBus:{queueName}").Value;
                        
        }

        public QueueClient GetQueueClient(string queue)
        {
            return new QueueClient(
                _configuration.GetSection("AzureServiceBus:QueueEndpoint").Value,
                queue
            );
        }

        public MessageReceiver GetMessageReceiver(string queue)
        {
            return new MessageReceiver(
                _configuration.GetSection("AzureServiceBus:QueueEndpoint").Value,
                queue
            );
        }
    }
}
