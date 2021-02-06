namespace FeePay.Infrastructure.PayPal.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using FeePay.Infrastructure.PayPal.Services;

    public interface IValueObject
    {
        Prefer ResponsePrefer { get; }
        LocalCode LocalCode { get; }
        LocalCode_RestAPI LocalCodeApi { get; }
    }
}
