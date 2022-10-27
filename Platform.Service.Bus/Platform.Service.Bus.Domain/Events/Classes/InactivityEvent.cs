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
        private readonly ICacheService _cacheService;
        #endregion

        #region Constructor
        public InactivityEvent(IHttpSendFactoryService httpSendFactory, ICacheService cacheService)
        {
            _httpSendFactory = httpSendFactory;
            _cacheService = cacheService;
        }
        #endregion

        public Task Execute(TransferModel @event)
        {
            string sequenceNumberKey = $"IBANG_SERVICE_BUS_SEQUENCE_NUMBER_{@event.Pages.PageId}_{@event.User.UserId}";
            string sequenceId = _cacheService.Get<string>(sequenceNumberKey);

            if (!String.IsNullOrEmpty(sequenceId))
            {
                var split = sequenceId.Split("|");
                var id = split[1];

                if (@event.Id.ToString() != id)
                {
                    return Task.CompletedTask;

                }
                else
                {
                    _cacheService.Delete(sequenceNumberKey);
                }
            }

            return _httpSendFactory.SendMessageInactivityBot(@event);
        }
    }
}
