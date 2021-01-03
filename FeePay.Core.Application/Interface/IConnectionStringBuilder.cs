using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Interface
{
    public interface IConnectionStringBuilder
    {
        string GetDefaultConnectionString();
        string GetDynamicConnectionString();
        string GetConnectionString(string SchoolUniqueId);
        string GetSuperUserConnectionString();

    }
}
