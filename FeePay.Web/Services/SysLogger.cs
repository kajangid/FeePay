using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeePay.Core.Application.Interface;
using Microsoft.Extensions.Logging;

namespace FeePay.Web.Services
{
    public class SysLogger : ISysLogger
    {
        private readonly ILogger _logger;
        public SysLogger(ILogger logger)
        {
            _logger = logger;
        }

        public void LogError(Exception exception, string message, params object[] args)
        {
            _logger.LogError(exception, message, args);
        }
        public void LogError(string message, params object[] args)
        {
            _logger.LogError(message, args);
        }
        public void LogWarning(Exception exception, string message, params object[] args)
        {
            _logger.LogWarning(exception, message, args);
        }
        public void LogWarning(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
        }
        public void LogInformation(Exception exception, string message, params object[] args)
        {
            _logger.LogInformation(exception, message, args);
        }
        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }
    }
}
