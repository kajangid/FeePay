using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Interface
{
    public interface ICultureFormatProvider
    {
        string FormatCurrencyIntoIndianCalture(int amount);
        string FormatCurrencyIntoIndianCalture(decimal amount);
    }
}
