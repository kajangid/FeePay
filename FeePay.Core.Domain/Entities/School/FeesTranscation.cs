namespace FeePay.Core.Domain.Entities.School
{
    using FeePay.Core.Domain.Entities.Student;
    using System;
    public class FeesTranscation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string TransactionId { get; set; }
        public string TransactionMode { get; set; }
        public decimal Amount { get; set; }
        public bool IsComplete { get; set; }
        public string State { get; set; }
        public DateTime? Date { get; set; }
        public string Receipt { get; set; }

        public StudentAdmission StudentAdmission { get; set; }
    }
}
