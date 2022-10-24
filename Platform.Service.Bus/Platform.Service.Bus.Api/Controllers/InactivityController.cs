using Microsoft.AspNetCore.Mvc;
using Platform.Service.Bus.Domain.Handlers.Interfaces;
using Platform.Service.Bus.Domain.Models;
using System.Threading.Tasks;

namespace Platform.Service.Bus.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class InactivityController : Controller
    {


        #region Attributes
        private readonly IHandlerQueue<TransferModel> _handlerQueue;
        #endregion

        #region Constructor
        public InactivityController(IHandlerQueue<TransferModel> handlerQueue)
        {
            _handlerQueue = handlerQueue;
        } 
        #endregion


        /// <summary>
        /// POST: api/v1.0/Inactivity/
        /// receive a message from a user and send replies
        /// </summary>
        [HttpPost]
        public virtual async Task<ActionResult<Response>> CreateMesageInactivity([FromBody] TransferModel transferModel)
        {
           return  await _handlerQueue.SendMessageScheduleToQueueAsync(transferModel);
           
        }

    }
}
