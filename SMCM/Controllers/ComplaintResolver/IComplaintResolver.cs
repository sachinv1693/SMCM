using SmartMeterConsumerManagement.Models.DBContext;

namespace SmartMeterConsumerManagement.Controllers.ComplaintResolver
{
    public interface IComplaintResolver
    {
        void ResolveConsumerComplaint(Complaint complaint);
    }
}
