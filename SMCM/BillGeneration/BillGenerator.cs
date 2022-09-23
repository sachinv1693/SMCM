using SmartMeterConsumerManagement.Enums;
using SmartMeterConsumerManagement.Models.DBContext;
using SmartMeterConsumerManagement.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartMeterConsumerManagement.BillGeneration
{
    public class BillGenerator : IBillGenerator
    {

        private readonly SMCM_DBContext _dbContext;
        private readonly IBillRepository _billRepository;

        public BillGenerator(SMCM_DBContext context, IBillRepository billRepository)
        {
            _dbContext = context;
            _billRepository = billRepository;
        }

        public void GenerateBillsForAllConsumers()
        {
            try
            {
                // Assuming head end system sends smartmeter data that gets updated into the smart meter database
                Console.WriteLine("Bill generation begins...");
                // Read each connected smart meter data
                List<SmartMeter> allConnectedMeters = _dbContext.SmartMeters.Where(meter => meter.Status == SmartMeterStatus.CONNECTED.ToString()).ToList<SmartMeter>();

                // Get the most recent Bill for chosen smart meter id
                var previousMonth = DateTime.Now.AddMonths(-2).ToString("MMM").ToUpper(); // Previous Billing month must be 2 months ago of current date
                Enum.TryParse(previousMonth, out Month lastMonth);
                foreach (var smartMeter in allConnectedMeters)
                {
                    Bill mostRecentBill = _billRepository.GetMonthWiseBill(smartMeter.Id, lastMonth);
                    User associatedConsumer = _billRepository.GetConsumerBySmartMeterId(smartMeter.Id);
                    // Prepare new Bill object
                    // Make its PreviousReadingUnit as CurrentReadingUnit of the last bill
                    // and PreviousBillingAmount with that of CurrentBillingAmount of last bill
                    if (associatedConsumer != null)
                    {
                        Bill currentMonthBill = new Bill()
                        {
                            ConsumerEmailId = associatedConsumer.EmailAddress.Trim(),
                            Date = DateTime.Now,
                            SmartMeterId = smartMeter.Id,
                            // Map CurrentReadingUnit of smart meter to CurrentMonthReading of new Bill.
                            CurrentReadingUnit = smartMeter.CurrentMonthReading,
                            PreviousReadingUnit = (mostRecentBill != null) ? mostRecentBill.CurrentReadingUnit : 0,
                            PreviousBillingAmount = (mostRecentBill != null) ? mostRecentBill.CurrentBillingAmount : 0,
                            CurrentBillingAmount = GetBillingAmount(smartMeter.CurrentMonthReading, associatedConsumer.UserCategory),
                            // The billing month should be last month as the bill generator gets triggered on 1st day of each month
                            CurrentBillingMonth = DateTime.Now.AddMonths(-1).ToString("MMM").ToUpper(),
                            PaymentStatus = PaymentStatus.NOT_PAID.ToString(),
                            PaymentState = PaymentState.NOT_INITIATED.ToString()
                        };
                        _billRepository.AddBill(currentMonthBill);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Bill Generator Exception: " + exception.Message);
            }
        }

        private double? GetBillingAmount(double? currentMonthReading, string userCategory)
        {
            try
            {
                // Depending on the user category - Apply following ratePerUnit -
                // RESIDENTIAL - ₹ 8, COMMERCIAL - ₹ 10, AGRICULTURE - ₹ 5, INDUSTRIAL - ₹ 15
                // Assuming equal rates across all the states, so no dependency on UserLocation table
                Enum.TryParse(userCategory.Trim(), out UserCategory category); // Get corresponding UserCategory Enum type
                // Calculate CurrentBillingAmount using CurrentReadingUnit as per the ratePerUnit * CurrentReadingUnit
                return category switch
                {
                    UserCategory.RESIDENTIAL => currentMonthReading * 8.0,
                    UserCategory.COMMERCIAL => currentMonthReading * 10.0,
                    UserCategory.AGRICULTURE => currentMonthReading * 5.0,
                    UserCategory.INDUSTRIAL => currentMonthReading * 15.0,
                    _ => 0.0
                };
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception in Bill Amount calculation: " + exception.Message);
                return 0.0;
            }
        }
    }
}