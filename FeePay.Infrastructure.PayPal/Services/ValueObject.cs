namespace FeePay.Infrastructure.PayPal.Services
{
    using System;
    using FeePay.Infrastructure.PayPal.Interfaces;
    public class ValueObject : IValueObject
    {
        public Prefer ResponsePrefer { get; private set; } = new Prefer();
        public LocalCode LocalCode { get; private set; } = new LocalCode();
        public LocalCode_RestAPI LocalCodeApi { get; private set; } = new LocalCode_RestAPI();
    }
    public class Prefer
    {
        public string Minimal { get; private set; } = "return=minimal";
        public string Representation { get; private set; } = "return=representation";
    }
    public class LocalCode
    {
        public string India { get; set; } = "en_IN";
    }
    public class LocalCode_RestAPI
    {
        public string India { get; set; } = "en-IN";
    }
}
