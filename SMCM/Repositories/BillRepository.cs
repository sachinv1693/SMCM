using SmartMeterConsumerManagement.Enums;
using SmartMeterConsumerManagement.Models.DBContext;
using SmartMeterConsumerManagement.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartMeterConsumerManagement.Repositories
{
    public class BillRepository : IBillRepository
    {
        private readonly SMCM_DBContext _dataContext;

        public BillRepository(SMCM_DBContext dBContext)
        {
            _dataContext = dBContext;
        }

        public IList<Bill> GetAllBills()
        {
            var bills = from bill in _dataContext.Bills
                        select bill;
            return bills.ToList();
        }

        public void AddBill(Bill bill)
        {
            _dataContext.Bills.Add(bill);
            _dataContext.SaveChanges();
        }

        public void UpdateBill(Bill bill, string consumerEmailId)
        {
            throw new NotImplementedException();
        }

        public void UpdateBillDate(long billId, DateTime date)
        {
            var bill = GetBillById(billId);
            bill.PaymentDate = date;
            _dataContext.SaveChanges();
        }

        public void UpdatePaymentStatus(long billId, PaymentStatus status)
        {
            var bill = GetBillById(billId);
            bill.PaymentStatus = Convert.ToString(status);
            _dataContext.SaveChanges();
        }

        public void UpdatePaymentType(long billId, PaymentType type)
        {
            var bill = GetBillById(billId);
            bill.PaymentType = Convert.ToString(type);
            _dataContext.SaveChanges();
        }

        public void UpdatePaymentState(long billId, PaymentState state)
        {
            var bill = GetBillById(billId);
            bill.PaymentState = Convert.ToString(state);
            _dataContext.SaveChanges();
        }

        public Bill GetBillById(long billId)
        {
            return _dataContext.Bills.Where(bill => bill.Id == billId).FirstOrDefault();
        }

        public IList<Bill> GetBillsByConsumerEmailId(string consumerEmailId)
        {
            return (IList<Bill>)_dataContext.Bills.Where(e => e.ConsumerEmailId == consumerEmailId).ToList<Bill>();
        }

        public IList<Bill> GetBillsBySmartMeterId(long smartMeterId)
        {
            return _dataContext.Bills.Where(e => e.SmartMeterId == smartMeterId).ToList<Bill>();
        }

        public Bill GetMonthWiseBill(long smartMeterId, Month month)
        {
            return (Bill)_dataContext.Bills.Where(e => e.SmartMeterId == smartMeterId).AsEnumerable().Where(e => e.CurrentBillingMonth.Trim() == month.ToString()).FirstOrDefault();
        }

        public User GetConsumerBySmartMeterId(long smartMeterId)
        {
            var consumer = (from user in _dataContext.Users
                                  join smartMeter in _dataContext.SmartMeters
                                  on user.SmartMeterId equals smartMeter.Id
                                  where smartMeter.Id == smartMeterId
                                  select user).FirstOrDefault();
            return consumer;
        }
    }
}
