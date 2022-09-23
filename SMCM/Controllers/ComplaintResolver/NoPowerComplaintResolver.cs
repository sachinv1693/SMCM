using SmartMeterConsumerManagement.Enums;
using SmartMeterConsumerManagement.Models.DBContext;
using SmartMeterConsumerManagement.Repositories;
using SmartMeterConsumerManagement.ServiceContracts;

namespace SmartMeterConsumerManagement.Controllers.ComplaintResolver
{
    public class NoPowerComplaintResolver : IComplaintResolver
    {
        private readonly IComplaintRepository _complaintRepository;

        public NoPowerComplaintResolver(IComplaintRepository complaintRepository)
        {
            _complaintRepository = complaintRepository;
        }

        public void ResolveConsumerComplaint(Complaint complaint)
        {
            // Logic to notify consumer via email or phone number
            // Update complaint status
            _complaintRepository.SetComplaintStatus(complaint.Id, ComplaintStatus.IN_REVIEW);
        }
    }
}