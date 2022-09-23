using SmartMeterConsumerManagement.Enums;

namespace SmartMeterConsumerManagement.PaymentProcessor
{
    public class PaymentTypeFactory
    {
        public static IPaymentType GetPaymentTypeObject(PaymentType type)
        {
            return type switch
            {
                PaymentType.BHRAT_PE => new BharatPePayment(),
                PaymentType.BILL_DESK => new BillDeskPayment(),
                PaymentType.GOOGLE_PAY => new GooglePayPayment(),
                PaymentType.PAYTM => new PaytmPayment(),
                PaymentType.OFFLINE => new OfflinePayment(),
                _ => null
            };
        }
    }
}
