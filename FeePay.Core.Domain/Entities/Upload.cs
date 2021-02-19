namespace FeePay.Core.Domain.Entities
{
    using System;
    public class Upload
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Url { get; set; }
        public string File { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
