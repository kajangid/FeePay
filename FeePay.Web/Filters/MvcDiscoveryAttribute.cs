using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeePay.Web.Filters
{
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public class MvcDiscoveryAttribute : Attribute
    {
    }
}
