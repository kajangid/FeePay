using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FeePay.Core.Application.UseCase;

namespace FeePay.Core.Application.IoC
{
    public static class ApplicationExtensionMethods
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static bool IsNumber(this String Value)
        {
            Regex regex = new Regex(@"^[0-9]*$");
            if (regex.IsMatch(Value))
            { return true; }
            else
            { return false; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static bool IsXLFile(this String Value)
        {
            Regex regex = new Regex(@"^.*\.(xls|xlsx)$");
            return regex.IsMatch(Value);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static bool IsCSVFile(this String Value)
        {
            Regex regex = new Regex(@"^.*\.(csv)$");
            return regex.IsMatch(Value);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static bool IsTXTFile(this String Value)
        {
            Regex regex = new Regex(@"^.*\.(txt)$");
            return regex.IsMatch(Value);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ConvertToDate(this String value)
        {
            String[] strDateSplit = value.Split('/');
            DateTime Date = new DateTime(
                Convert.ToInt32(strDateSplit[2]),
                Convert.ToInt32(strDateSplit[0]),
                Convert.ToInt32(strDateSplit[1]));
            return Date;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsValidToken(this String id)
        {
            string DecryptedID = Crypto.DecryptText(id);
            return IsNumber(DecryptedID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string EncryptID(this int id)
        {
            return Crypto.EncryptId(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DecryptID(this String id)
        {
            return Crypto.DecryptId(id);
        }

    }
}
