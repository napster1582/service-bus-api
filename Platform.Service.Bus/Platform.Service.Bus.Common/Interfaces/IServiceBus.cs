using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service.Bus.Common.Interfaces
{
    public interface IServiceBus
    {
        QueueClient GetQueueClient(string queue);
        MessageReceiver GetMessageReceiver(string queue);
        string GetQueue(string queueName);
    }
}
