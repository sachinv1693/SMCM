using SmartMeterConsumerManagement.Enums;
using SmartMeterConsumerManagement.ServiceContracts;

namespace SmartMeterConsumerManagement.Controllers.ComplaintResolver
{
    public class ComplaintFactory
    {
        private readonly IComplaintRepository ComplaintRepository;
        public ComplaintFactory(IComplaintRepository complaintRepository)
        {
            ComplaintRepository = complaintRepository;
        }
        public IComplaintResolver GetComplaintResolver(ComplaintType type)
        {
            return type switch
            {
                ComplaintType.BILL_NOT_RECEIVED => new BillNotReceivedComplaintResolver(ComplaintRepository),
                ComplaintType.FAULTY_SMART_METER => new FaultySmartMeterComplaintResolver(ComplaintRepository),
                ComplaintType.LAST_BILL_ISSUE_DATE => new LastBillIssueDateComplaintResolver(ComplaintRepository),
                ComplaintType.LAST_METER_READING => new LastMeterReadingComplaintResolver(ComplaintRepository),
                ComplaintType.LAST_METER_READING_DATE => new LastMeterReadingDateComplaintResolver(ComplaintRepository),
                ComplaintType.LAST_PAYMENT_AMOUNT => new LastPaymentAmountComplaintResolver(ComplaintRepository),
                ComplaintType.LAST_PAYMENT_DATE => new LastPaymentDateComplaintResolver(ComplaintRepository),
                ComplaintType.LAST_SIX_MONTH_CONSUMPTION => new LastSixMonthConsumptionComplaintResolver(ComplaintRepository),
                ComplaintType.METER_BURNED => new MeterBurnedComplaintResolver(ComplaintRepository),
                ComplaintType.METER_HIGH_BILL => new MeterHighBillComplaintResolver(ComplaintRepository),
                ComplaintType.NO_POWER => new NoPowerComplaintResolver(ComplaintRepository),
                _ => null,
            };
        }
    }
}
