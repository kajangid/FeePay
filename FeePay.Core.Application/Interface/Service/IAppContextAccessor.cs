using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Interface.Service
{
    public interface IAppContextAccessor
    {
        string GetUserIP();
        List<Claim> GetCurrentUserClaims();
    }
}
