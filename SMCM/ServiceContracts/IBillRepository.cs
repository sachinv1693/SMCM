using SmartMeterConsumerManagement.Enums;
using SmartMeterConsumerManagement.Models.DBContext;
using System;
using System.Collections.Generic;


namespace SmartMeterConsumerManagement.ServiceContracts
{
    public interface IBillRepository
    {
        IList<Bill> GetAllBills();
        Bill GetBillById(long billId);
        void AddBill(Bill bill);
        void UpdateBill(Bill bill, string consumerEmailId);
        void UpdateBillDate(long billId, DateTime date);
        void UpdatePaymentType(long billId, PaymentType type);
        void UpdatePaymentStatus(long billId, PaymentStatus status);
        void UpdatePaymentState(long billId, PaymentState state);
        IList<Bill> GetBillsByConsumerEmailId(string consumerEmailId);
        Bill GetMonthWiseBill(long smartMeterId, Month month);
        User GetConsumerBySmartMeterId(long smartMeterId);
    }
}
