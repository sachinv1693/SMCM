namespace SmartMeterConsumerManagement.PaymentProcessor
{
    public class BharatPePayment : IPaymentType
    {
        public bool Pay(double? amount)
        {
            // 1. Payment Gateway calls BharatPe service
            return true;
        }

        public bool Refund(double? amount)
        {
            throw new System.NotImplementedException();
        }
    }
}