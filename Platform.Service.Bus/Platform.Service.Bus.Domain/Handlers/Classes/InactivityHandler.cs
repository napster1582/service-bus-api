using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Newtonsoft.Json;
using Platform.Service.Bus.Common.Interfaces;
using Platform.Service.Bus.Domain.Handlers.Interfaces;
using Platform.Service.Bus.Domain.Interfaces;
using Platform.Service.Bus.Domain.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service.Bus.Domain.Handlers
{
    public class InactivityHandler : IHandlerQueue<TransferModel>
    {


        #region Attributes
        private readonly IServiceBus _serviceBus;
        private readonly ICacheService _cacheService;
        private readonly ILoggerService _loggerService;
        #endregion

        #region Constructor
        public InactivityHandler(IServiceBus serviceBus,
                               ICacheService cacheService,
                               ILoggerService loggerService
      )
        {
            _serviceBus = serviceBus;
            _cacheService = cacheService;
            _loggerService = loggerService;
        } 
        #endregion

        public async Task<Response> SendMessageScheduleToQueueAsync(TransferModel transferModel)
        {

            string lockKey = $"IBANG_LOCK_INACTIVITY_MESSGAGE_{transferModel.Pages.PageId}_{transferModel.User.UserId}";
            string lockToken = Guid.NewGuid().ToString();

            bool lockFinished = false;
            short maxLoopCount = 30;
            short count = 0;

            long sequenceNumber = 0;


            while (count < maxLoopCount && !lockFinished)
            {
                if (await _cacheService.AcquireLock(lockKey, lockToken, TimeSpan.FromMinutes(2)))
                {
                    try
                    {
                        var messageReceiver = _serviceBus.GetMessageReceiver("bot-queue-inactivity");
                        var client = _serviceBus.GetQueueClient("bot-queue-inactivity");

                        Message message = await messageReceiver.PeekAsync();

                        if (message != null)
                        {
                            await client.CancelScheduledMessageAsync(message.SystemProperties.SequenceNumber);
                        }

                        transferModel.Activity.Text = transferModel.Pages.BlockIdInactivity;
                        transferModel.Activity.Type = "payload";

                        var contentBody = JsonConvert.SerializeObject(transferModel);

                        sequenceNumber = await client.ScheduleMessageAsync(
                           new Message(Encoding.UTF8.GetBytes(contentBody)),
                           DateTimeOffset.Now.AddMinutes((double)transferModel.Pages.InactivityGapInMinutes)
                       );

                        await client.CloseAsync();
                    }
                    catch (Exception ex)
                    {
                        _loggerService.Critical(ex);
                    }
                    finally
                    {
                        bool isReleased = await _cacheService.ReleaseLock(lockKey, lockToken);
                        if (!isReleased)
                        {
                            _loggerService.Warning($"Error al desbloquear el lock de redisCache {lockKey} con token {lockToken}, en el método");
                        }

                        lockFinished = true;
                    }
                }
                else
                {
                    count++;
                    await Task.Delay(TimeSpan.FromMilliseconds(500));
                }
            }

            if (count == maxLoopCount)
            {
                _loggerService.Warning($"Error, nunca se libero el lock del redisCache tras {maxLoopCount} intentos y {500 * maxLoopCount} milisegundos, en el método.");
            }
       
           return new Response() { 
               IsSuccess = sequenceNumber != 0 ? true : false, 
               Message = sequenceNumber != 0 ? "ScheduleMessage OK" : "ScheduleMessage Failed",
               Data = sequenceNumber 
           };

        }
    }
}
