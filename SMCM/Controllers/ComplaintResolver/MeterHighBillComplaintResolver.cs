using SmartMeterConsumerManagement.Models.DBContext;
using SmartMeterConsumerManagement.ServiceContracts;

namespace SmartMeterConsumerManagement.Controllers.ComplaintResolver
{
    public class MeterHighBillComplaintResolver : IComplaintResolver
    {
        private readonly IComplaintRepository _complaintRepository;

        public MeterHighBillComplaintResolver(IComplaintRepository complaintRepository)
        {
            _complaintRepository = complaintRepository;
        }

        public void ResolveConsumerComplaint(Complaint complaint)
        {
            throw new System.NotImplementedException();
        }
    }
}