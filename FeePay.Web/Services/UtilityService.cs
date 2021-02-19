using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FeePay.Core.Application.Interface.Service;

namespace FeePay.Web.Services
{
    public class UtilityService : IUtilityService
    {
        /// <summary>
        /// Encode string for url 
        /// </summary>
        /// <param name="param"> String To Encode </param>
        /// <returns> Encoded String</returns>
        public string EncodeUrl(string param)
        {
            return HttpUtility.UrlEncode(param);
        }

        /// <summary>
        /// Decode string url 
        /// </summary>
        /// <param name="param"> String To Decode</param>
        /// <returns> Decoded String </returns>
        public string DecodeUrl(string param)
        {
            return HttpUtility.UrlDecode(param);
        }
    }
}
