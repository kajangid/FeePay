using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeePay.Infrastructure.PayPal.Enums
{
    /// <summary>
    /// 
    /// </summary>
    public enum CurrencyCode
    {
        /// <summary>
        /// Indian rupee
        /// </summary>
        INR,
        /// <summary>
        /// Great British Pound
        /// </summary>
        GBP,
        /// <summary>
        /// US Dollar
        /// </summary>
        USD,
        /// <summary>
        /// Euro
        /// </summary>
        EUR
    }
    /// <summary>
    /// The intent to either capture payment immediately or
    /// authorize a payment for an order after order creation.
    /// </summary>
    public enum CheckoutPaymentIntent
    {
        /// <summary>
        /// The merchant intends to capture payment immediately after the customer makes a payment.
        /// </summary>
        CAPTURE,
        /// <summary>
        /// The merchant intends to authorize a payment and
        /// place funds on hold after the customer makes a payment.
        /// Authorized payments are guaranteed for up to three days but
        /// are available to capture for up to 29 days.
        /// After the three-day honor period, the original authorized payment expires
        /// and you must re-authorize the payment.
        /// You must make a separate request to capture payments on demand.
        /// This intent is not supported when you have more than one `purchase_unit` within your order.
        /// </summary>
        AUTHORIZE

    }
    /// <summary>
    /// Configures a Continue or Pay Now checkout flow.
    /// Source: https://developer.paypal.com/docs/api/orders/v2/
    /// </summary>
    public enum UserAction
    {
        /// <summary>
        /// After you redirect the customer to the PayPal payment page,
        /// a Continue button appears. Use this option when the final amount is not known
        /// when the checkout flow is initiated and you want to redirect
        /// the customer to the merchant page without processing the payment.
        /// </summary>
        CONTINUE,
        /// <summary>
        /// After you redirect the customer to the PayPal payment page,
        /// a Pay Now button appears.
        /// Use this option when the final amount is known when the checkout is initiated
        /// and you want to process the payment immediately when the customer clicks Pay Now.
        /// </summary>
        PAY_NOW
    }
    /// <summary>
    /// The type of landing page to show on the PayPal site for customer checkout.
    /// Default: NO_PREFERENCE.
    /// Source: https://developer.paypal.com/docs/api/orders/v2/
    /// </summary>
    public enum LandingPage
    {
        /// <summary>
        /// When the customer clicks PayPal Checkout, the customer is redirected 
        /// to a page to log in to PayPal and approve the payment.
        /// </summary>
        LOGIN,
        /// <summary>
        /// When the customer clicks PayPal Checkout, 
        /// the customer is redirected to a page to enter credit or 
        /// debit card and other relevant billing information required to complete the purchase.
        /// </summary>
        BILLING,
        /// <summary>
        /// When the customer clicks PayPal Checkout,
        /// the customer is redirected to either a page to log in to PayPal and
        /// approve the payment or to a page to enter credit or
        /// debit card and other relevant billing information
        /// required to complete the purchase, depending on their previous interaction with PayPal.
        /// </summary>
        NO_PREFERENCE
    }
    /// <summary>
    /// The shipping preference:
    /// <ul>
    /// <li>Displays the shipping address to the customer.</li>
    /// <li>Enables the customer to choose an address on the PayPal site.</li>
    /// <li>Restricts the customer from changing the address during the payment-approval process.</li>
    /// </ul>
    /// Default: GET_FROM_FILE.
    /// Source: https://developer.paypal.com/docs/api/orders/v2/
    /// </summary>
    public enum ShippingPreference
    {
        /// <summary>
        /// Use the customer-provided shipping address on the PayPal site.
        /// </summary>
        GET_FROM_FILE,
        /// <summary>
        /// Redact the shipping address from the PayPal site. Recommended for digital goods.
        /// </summary>
        NO_SHIPPING,
        /// <summary>
        /// Use the merchant-provided address. The customer cannot change this address on the PayPal site.
        /// </summary>
        SET_PROVIDED_ADDRESS
    }
    /// <summary>
    /// 
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// The order was created with the specified context.
        /// </summary>
        CREATED,
        /// <summary>
        /// The order was saved and persisted. The order status continues to be in progress 
        /// until a capture is made with final_capture = true for all purchase units within the order.
        /// </summary>
        SAVED,
        /// <summary>
        /// The customer approved the payment through the PayPal wallet or another form of guest or unbranded payment.
        /// For example, a card, bank account, or so on.
        /// </summary>
        APPROVED,
        /// <summary>
        /// All purchase units in the order are voided.
        /// </summary>
        VOIDED,
        /// <summary>
        /// The payment was authorized or the authorized payment was captured for the order.
        /// </summary>
        COMPLETED,
        /// <summary>
        /// The order requires an action from the payer (e.g. 3DS authentication). Redirect 
        /// the payer to the "rel":"payer-action" HATEOAS link returned as part of
        /// the response prior to authorizing or capturing the order.
        /// </summary>
        PAYER_ACTION_REQUIRED
    }
    /// <summary>
    /// Phone Type
    /// </summary>
    public enum PhoneType
    {
        /// <summary>
        /// 
        /// </summary>
        FAX,
        /// <summary>
        /// 
        /// </summary>
        HOME,
        /// <summary>
        /// 
        /// </summary>
        MOBILE,
        /// <summary>
        /// 
        /// </summary>
        OTHER,
        /// <summary>
        /// 
        /// </summary>
        PAGER
    }
}
