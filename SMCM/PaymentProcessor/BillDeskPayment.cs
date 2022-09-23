namespace SmartMeterConsumerManagement.PaymentProcessor
{
    public class BillDeskPayment : IPaymentType
    {
        public bool Pay(double? amount)
        {
            throw new System.NotImplementedException();
        }

        public bool Refund(double? amount)
        {
            throw new System.NotImplementedException();
        }
    }
}