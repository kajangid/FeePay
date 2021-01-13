using FeePay.Core.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Exceptions
{
    /// <summary>
    /// Use
    /// throw new MyAppException("A severe error occurred").AddRecoveryLink("Google", "http://www.google.com");
    /// catch (MyVerySpecializedException e) when (e.Status == 500)
    /// <summary>
    public class AppException : Exception
    {
        public AppException AddRecoveryLink(string text, string url)
        {
            RecoveryLinks.Add(new RecoveryLink(text, url));
            return this;
        }
        public AppException AddRecoveryLink(RecoveryLink link)
        {
            RecoveryLinks.Add(link);
            return this;
        }
        public AppException() : base() { RecoveryLinks = new List<RecoveryLink>(); }
        public AppException(string message, Exception inner) : base(message, inner) { }
        public AppException(Exception exception) : this(exception.Message) { }
        public AppException(string message) : base(message) { RecoveryLinks = new List<RecoveryLink>(); }
        public int Status { get; set; }
        public List<RecoveryLink> RecoveryLinks { get; }
    }
}
