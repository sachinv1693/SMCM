using SmartMeterConsumerManagement.Models.DBContext;
using SmartMeterConsumerManagement.ServiceContracts;

namespace SmartMeterConsumerManagement.Controllers.ComplaintResolver
{
    public class LastMeterReadingDateComplaintResolver : IComplaintResolver
    {
        private readonly IComplaintRepository _complaintRepository;

        public LastMeterReadingDateComplaintResolver(IComplaintRepository complaintRepository)
        {
            _complaintRepository = complaintRepository;
        }

        public void ResolveConsumerComplaint(Complaint complaint)
        {
            throw new System.NotImplementedException();
        }
    }
}