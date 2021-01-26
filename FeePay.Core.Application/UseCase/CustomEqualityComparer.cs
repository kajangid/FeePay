using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.UseCase
{
    public class CustomEqualityComparer<T> : IEqualityComparer<T?>
        where T : struct
    {
        public bool Equals(T? x, T? y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            return x.Equals(y);
        }

        public int GetHashCode(T? obj)
        {
            return obj.GetHashCode();
        }
    }
}
