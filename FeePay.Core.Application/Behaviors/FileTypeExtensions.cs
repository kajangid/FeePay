using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FeePay.Core.Application.Enums.FileTypeEnum;

namespace FeePay.Core.Application.Behaviors
{
    internal static class FileTypeExtensions
    {
        public static string ToDescriptionString(this FileType val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
                .GetType()
                .GetField(val.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}
