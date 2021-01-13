using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Enums
{
    public class Notification
    {
        public enum NotificationType
        {
            error,
            success,
            warning,
            info,
            question,

            theme,
            dark,
            danger,
            light,
            primary,
            secondary
        }
    }
}
