using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Wrapper
{
    public class Response<T>
    {
        public Response()
        {
            Succeeded = false;
        }
        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }
        public Response(List<string> errors, string message = null)
        {
            Succeeded = false;
            Message = message;
            Errors = errors;
        }
        public Response(Exception exception, string message = null)
        {
            Succeeded = false;
            Message = message;
            Exception = exception;
        }
        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public Exception Exception { get; set; }
        public T Data { get; set; }
    }
}
