using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Interface
{
    public interface ISysLogger
    {
        void LogError(Exception exception, string message, params object[] args);
        void LogError(string message, params object[] args);
        void LogWarning(Exception exception, string message, params object[] args);
        void LogWarning(string message, params object[] args);
        void LogInformation(Exception exception, string message, params object[] args);
        void LogInformation(string message, params object[] args);
    }
}
