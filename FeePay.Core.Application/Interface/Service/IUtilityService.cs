using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Interface.Service
{
    public interface IUtilityService
    {
        string EncodeUrl(string param);
        string DecodeUrl(string param);

    }
}
