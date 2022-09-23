using SmartMeterConsumerManagement.Enums;
using SmartMeterConsumerManagement.Models.DBContext;
using SmartMeterConsumerManagement.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartMeterConsumerManagement.Repositories
{
    public class ComplaintRepository : IComplaintRepository
    {
        private readonly SMCM_DBContext _dataContext;

        public ComplaintRepository(SMCM_DBContext dBContext)
        {
            _dataContext = dBContext;
        }

        public void AddComplaint(Complaint complaint)
        {
            _dataContext.Complaints.Add(complaint);

            _dataContext.SaveChanges();
        }

        public void UpdateComplaint(Complaint complaint, string consumerEmailId)
        {
            throw new NotImplementedException();
        }

        public IList<Complaint> GetAllComplaints()
        {
            return _dataContext.Complaints.ToList<Complaint>();
        }

        public IList<Complaint> GetAllComplaintsByType(ComplaintType type)
        {
            return _dataContext.Complaints.Where(e => e.Type == type.ToString()).ToList<Complaint>();
        }

        public IList<Complaint> GetAllComplaintsByConsumerEmailId(string consumerEmailId)
        {
            return _dataContext.Complaints.Where(e => e.ConsumerEmailId == consumerEmailId).ToList<Complaint>();
        }

        public IList<Complaint> GetAllComplaintsByDate(DateTime date)
        {
            return _dataContext.Complaints.Where(e => e.Date.DayOfYear.CompareTo(date.DayOfYear) == 0).ToList<Complaint>();
        }

        public IList<Complaint> GetAllUnresolvedComplaints()
        {
            return _dataContext.Complaints.Where(e => e.Status != ComplaintStatus.RESOLVED.ToString()).ToList<Complaint>();
        }

        public IList<Complaint> GetAllResolvedComplaints()
        {
            return _dataContext.Complaints.Where(e => e.Status == ComplaintStatus.RESOLVED.ToString()).ToList<Complaint>();
        }

        public string GetComplaintType(long complaintId)
        {
            var complaint = _dataContext.Complaints.FirstOrDefault(e => e.Id == complaintId);
            return complaint.Type;
        }

        public void SetComplaintStatus(long complaintId, ComplaintStatus status)
        {
            (from p in _dataContext.Complaints
             where p.Id == complaintId             
                select p).ToList().ForEach(x => x.Status = Convert.ToString(status));
            _dataContext.SaveChanges();
        }

        public string GetComplaintStatus(long complaintId)
        {
            var complaint = _dataContext.Complaints.FirstOrDefault(e => e.Id == complaintId);
            return complaint.Status;
        }

        public void SetComplaintType(long complaintId, ComplaintType type)
        {
            (from p in _dataContext.Complaints
             where p.Id == complaintId
             select p).ToList().ForEach(x => x.Type = Convert.ToString(type));
            _dataContext.SaveChanges();
        }

        public void RemoveComplaintById(long complaintId)
        {
            throw new NotImplementedException();
        }
    }
}
