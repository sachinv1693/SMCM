using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMeterConsumerManagement.PaymentProcessor
{
    public interface IPaymentType
    {
        bool Pay(double? amount);
        bool Refund(double? amount);
    }
}
