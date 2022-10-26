using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Platform.Service.Bus.Common.Interfaces;
using Platform.Service.Bus.Domain.Helpers;
using Platform.Service.Bus.Domain.Interfaces;
using Platform.Service.Bus.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service.Bus.Domain.Services
{
    public class HttpSendFactory : IHttpSendFactoryService
    {
        #region Attributes
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILoggerService _loggerService;
        #endregion

        #region Constructor
        public HttpSendFactory(IConfiguration configuration, 
                               IHttpClientFactory httpClientFactory,
                               ILoggerService loggerService
        )
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _loggerService = loggerService;
        } 
        #endregion


        public async Task<Response> SendMessageInactivityBot(TransferModel transferModel)
        {
            try
            {
                HttpClient httpClient = _httpClientFactory.CreateClient();

                string baseUrl = _configuration.GetSection("IdentityServer").GetSection("Api").GetSection("Bot:Endpoint").Value;

                string controller = "Messages";

                string resourceName = "Inactivity";

                string path = string.Format("{0}api/v1.0/{1}/{2}", baseUrl, controller, resourceName);

                StringContent stringContent = new(JsonConvert.SerializeObject(transferModel), Encoding.UTF8, "application/json");

                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(path, stringContent);

                var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();

                httpClient.Dispose();

                return new Response
                {
                    IsSuccess = httpResponseMessage.IsSuccessStatusCode,
                    Message = httpResponseMessage.StatusCode.ToString(),
                    Data = responseContent
                };

            }
            catch (Exception Ex)
            {
                _loggerService.Error(Ex);
                return ExceptionResponse.Exception(Ex);
            }
        }
    }
}
