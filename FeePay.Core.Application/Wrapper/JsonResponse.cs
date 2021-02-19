using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Wrapper
{
    public class JsonResponse<T>
    {
        public JsonResponse()
        {
            Success = false;
        }
        public JsonResponse(string message)
        {
            Success = false;
            Message = message;
        }
        public JsonResponse(List<string> errors, string message = null)
        {
            Success = false;
            Message = message;
            Errors = errors;
        }
        public JsonResponse(Exception exception, string message = null)
        {
            Success = false;
            Message = message;
            Exception = exception;
        }
        public JsonResponse(T data, string message = null)
        {
            Success = true;
            Message = message;
            Data = data;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public Exception Exception { get; set; }
        public T Data { get; set; }
    }
}
