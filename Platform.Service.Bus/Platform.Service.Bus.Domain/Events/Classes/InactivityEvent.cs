using Platform.Service.Bus.Domain.Events.Interfaces;
using Platform.Service.Bus.Domain.Interfaces;
using Platform.Service.Bus.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service.Bus.Domain.Events.Classes
{
    public class InactivityEvent : IHandlerEvent<TransferModel>
    {
        #region Attributes
        private readonly IHttpSendFactoryService _httpSendFactory;
        #endregion

        #region Constructor
        public InactivityEvent(IHttpSendFactoryService httpSendFactory)
        {
            _httpSendFactory = httpSendFactory;
        } 
        #endregion

        public Task Execute(TransferModel @event)
        {
            return _httpSendFactory.SendMessageInactivityBot(@event);
        }
    }
}
