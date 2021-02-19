using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Enums
{
    /// <summary>
    /// Payment Status
    /// </summary>
    public enum PaymentStatus
    {
        Paied,
        Not_Paied,
        Partial_Paied
    }
    /// <summary>
    /// Payment Modes  
    /// </summary>
    public enum PaymentMode
    {
        ONLINE,
        CASH,
        CHECK,
        DEMAND_DRAFT
    }
    /// <summary>
    /// Transaction Status
    /// </summary>
    public enum TransactionStatus
    {
        CREATED,
        CAPTURED,
        COMPLETED,
        FAILED,
        CANCELED
    }
    /// <summary>
    /// Transaction Type 
    /// </summary>
    public enum TransactionMode
    {
        UPI,
        BANK_TRANSACTION,
        DEBIT_CARD,
        CREDIT_CARD
    }
}
