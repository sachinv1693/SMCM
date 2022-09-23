using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartMeterConsumerManagement.Controllers.ControllerUtils;
using SmartMeterConsumerManagement.Enums;
using SmartMeterConsumerManagement.Models.DBContext;
using SmartMeterConsumerManagement.PaymentProcessor;
using SmartMeterConsumerManagement.ServiceContracts;
using System;
using System.Collections.Generic;

namespace SmartMeterConsumerManagement.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IBillRepository _billRepository;
        public PaymentController(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }

        [Authorize(Roles = "CONSUMER")]
        public IActionResult ViewBills()
        {
            List<Bill> consumerBills = GetConsumerBills(); // Populate all bill details for current user
            ViewData["ConsumerBills"] = (consumerBills.Count > 0) ? consumerBills : null; 
            return View();
        }

        [Authorize(Roles = "CONSUMER")]
        public IActionResult MakePayment(long id)
        {
            Bill bill = _billRepository.GetBillById(id);
            DropdownSelectListBuilder listBuilder = new DropdownSelectListBuilder();
            ViewBag.PaymentTypes = listBuilder.GetEnumTypeSelectList<PaymentType>();
            bill.PaymentType = null;
            return View(bill);
        }

        [Authorize(Roles = "CONSUMER")]
        [HttpPost]
        public IActionResult MakePayment(Bill bill)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _billRepository.UpdateBillDate(bill.Id, DateTime.Now);
                    ViewData["PaymentSuccessful"] = false;
                    Enum.TryParse(bill.PaymentType.Trim(), out PaymentType payType);
                    IPaymentType paymentType = PaymentTypeFactory.GetPaymentTypeObject(payType);
                    bool isSuccessfullyPaid = paymentType.Pay(bill.CurrentBillingAmount);
                    ViewData["PaymentSuccessful"] = isSuccessfullyPaid;
                    if (isSuccessfullyPaid)
                    {
                        _billRepository.UpdatePaymentType(bill.Id, payType);
                        _billRepository.UpdatePaymentStatus(bill.Id, PaymentStatus.PAID);
                        _billRepository.UpdatePaymentState(bill.Id, PaymentState.SUCCESSFUL);
                    }
                    else
                    {
                        _billRepository.UpdatePaymentStatus(bill.Id, PaymentStatus.PAYMENT_ERROR);
                        _billRepository.UpdatePaymentState(bill.Id, PaymentState.FAILED);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error in processing payment: " + exception.Message);
                _billRepository.UpdatePaymentStatus(bill.Id, PaymentStatus.PAYMENT_ERROR);
                _billRepository.UpdatePaymentState(bill.Id, PaymentState.ABORTED);
            }
            return View(bill);
        }

        private List<Bill> GetConsumerBills()
        {
            UserClaimsHandler userClaims = new UserClaimsHandler(HttpContext?.User);
            string consumerEmailId = userClaims.GetUserEmailFromCurrentUserClaim();
            
            List<Bill> consumerBills = (List<Bill>)_billRepository.GetBillsByConsumerEmailId(consumerEmailId);
            return consumerBills;
        }
    }
}
