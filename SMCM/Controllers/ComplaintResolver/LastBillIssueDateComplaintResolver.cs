using SmartMeterConsumerManagement.Models.DBContext;
using SmartMeterConsumerManagement.ServiceContracts;

namespace SmartMeterConsumerManagement.Controllers.ComplaintResolver
{
    internal class LastBillIssueDateComplaintResolver : IComplaintResolver
    {
        private readonly IComplaintRepository _complaintRepository;

        public LastBillIssueDateComplaintResolver(IComplaintRepository complaintRepository)
        {
            _complaintRepository = complaintRepository;
        }

        public void ResolveConsumerComplaint(Complaint complaint)
        {
            throw new System.NotImplementedException();
        }
    }
}