using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Domain.Settings
{
    public class RecoveryLink
    {
        public RecoveryLink(string text, string url)
        {
            Text = text;
            Url = url;
        }

        public string Url { get; set; }
        public string Text { get; set; }
    }
}
