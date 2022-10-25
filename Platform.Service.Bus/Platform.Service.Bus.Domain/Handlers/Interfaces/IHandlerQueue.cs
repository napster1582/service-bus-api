using Platform.Service.Bus.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service.Bus.Domain.Handlers.Interfaces
{
    public interface IHandlerQueue<TModel> where TModel : class
    {
        public Task SendMessageScheduleToQueueAsync(TModel transferModel);
    }
}
