using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeePay.Core.Application.Interface;

namespace FeePay.Core.Application.UseCase
{
    public class ConnectionStringConfig : IConnectionStringConfig
    {
        public string Server { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string PostString { get; set; }
    }
}
