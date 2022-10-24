using Platform.Service.Bus.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Service.Bus.Domain.Helpers
{
    public class ExceptionResponse
    {

        public static Response Exception(Exception exception)
        {
            var message = exception.Message;
            var innerException = exception.InnerException != null ? exception.InnerException.Message : "";
            var stackTrace = exception.StackTrace;
            return new Response
            {
                IsSuccess = false,
                Message = $"Message: {message}\n" +
                             $"InnerException: {innerException}\n" +
                             $"\nStackTrace: {stackTrace}",
                Data = null
            };
        }
    }
}
