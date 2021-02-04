using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Threading;
using FeePay.Core.Application.Interface;

namespace FeePay.Core.Application.UseCase
{
    public class CultureFormatProvider : ICultureFormatProvider
    {
        public string FormatCurrencyIntoIndianCalture(int amount)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-IN");
            return amount.ToString("c");
        }
        public string FormatCurrencyIntoIndianCalture(decimal amount)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-IN");
            return amount.ToString("c");
        }
    }
}
