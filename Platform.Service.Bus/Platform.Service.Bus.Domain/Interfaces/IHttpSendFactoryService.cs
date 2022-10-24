using Platform.Service.Bus.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service.Bus.Domain.Interfaces
{
   public interface IHttpSendFactoryService
   {
        public Task<Response> SendMessageInactivityBot(TransferModel transferModel);
   }
}
