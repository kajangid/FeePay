using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Core.Application.Enums
{
    public enum PaymentStatus
    {
        /// <summary>
        /// The transaction was successfully created.
        /// </summary>
        CREATED,
        /// <summary>
        /// The customer approved the transaction.
        /// The state changes from created to approved on generation of the sale_id for sale transactions,
        /// authorization_id for authorization transactions, or order_id for order transactions.
        /// </summary>
        APPROVED,
        /// <summary>
        /// The transaction request failed.
        /// </summary>
        FAILED


        //CAPTURED,
        //DENIED,
        //EXPIRED,
        //PARTIALLY_CAPTURED,
        //VOIDED,
        //PENDING,
        //COMPLETED
    }
}
