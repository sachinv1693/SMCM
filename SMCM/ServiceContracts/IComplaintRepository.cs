using SmartMeterConsumerManagement.Enums;
using SmartMeterConsumerManagement.Models.DBContext;
using System;
using System.Collections.Generic;

namespace SmartMeterConsumerManagement.ServiceContracts
{
    public interface IComplaintRepository
    {
        void AddComplaint(Complaint complaint);
        void UpdateComplaint(Complaint complaint, string consumerEmailId);
        IList<Complaint> GetAllComplaints();
        IList<Complaint> GetAllComplaintsByType(ComplaintType type);
        IList<Complaint> GetAllComplaintsByConsumerEmailId(string consumerEmailId);
        IList<Complaint> GetAllComplaintsByDate(DateTime date);
        IList<Complaint> GetAllUnresolvedComplaints();
        IList<Complaint> GetAllResolvedComplaints();
        string GetComplaintType(long complaintId);
        void SetComplaintStatus(long complaintId, ComplaintStatus status);
        string GetComplaintStatus(long complaintId);
        void SetComplaintType(long complaintId, ComplaintType type);
        void RemoveComplaintById(long complaintId);
    }
}
