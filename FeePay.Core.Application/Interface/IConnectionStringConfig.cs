using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Interface
{
    public interface IConnectionStringConfig
    {
        string Server { get; set; }
        string User { get; set; }
        string Password { get; set; }
        string PostString { get; set; }

    }
}
