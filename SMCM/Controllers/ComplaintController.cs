using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartMeterConsumerManagement.Controllers.ComplaintResolver;
using SmartMeterConsumerManagement.Controllers.ControllerUtils;
using SmartMeterConsumerManagement.Enums;
using SmartMeterConsumerManagement.Models.DBContext;
using SmartMeterConsumerManagement.ServiceContracts;
using System;
using System.Collections.Generic;

namespace SmartMeterConsumerManagement.Controllers
{
    public class ComplaintController : Controller
    {
        private readonly IComplaintRepository _complaintRepository;
        public ComplaintController(IComplaintRepository complaintRepository)
        {
            _complaintRepository = complaintRepository;
        }

        [Authorize(Roles = "CONSUMER")]
        public IActionResult RaiseComplaint()
        {
            DropdownSelectListBuilder listBuilder = new DropdownSelectListBuilder();
            ViewBag.ComplaintTypes = listBuilder.GetEnumTypeSelectList<ComplaintType>();
            return View();
        }

        [Authorize(Roles = "CONSUMER")]
        [HttpPost]
        public IActionResult RaiseComplaint(Complaint complaint)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserClaimsHandler userClaims = new UserClaimsHandler(HttpContext?.User);
                    string userEmailId = userClaims.GetUserEmailFromCurrentUserClaim();
                    complaint.Date = DateTime.Now;
                    complaint.ConsumerEmailId = userEmailId;
                    complaint.Status = ComplaintStatus.PENDING.ToString();
                    _complaintRepository.AddComplaint(complaint);
                    ViewData["ComplaintRaised"] = true;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error in raising complaint: " + exception.Message);
                ViewData["ComplaintRaised"] = false;
            }
            return RaiseComplaint();
        }

        [Authorize(Roles = "PROJECT_MANAGER")]
        public IActionResult ConsumerComplaints()
        {
            List<Complaint> complaints = (List<Complaint>)_complaintRepository.GetAllComplaints();
            ViewData["ConsumerComplaints"] = (complaints.Count > 0) ? complaints : null;
            return View();
        }

        [Authorize(Roles = "PROJECT_MANAGER")]
        [HttpPost]
        public IActionResult ConsumerComplaints(Complaint complaint)
        {
            if (ModelState.IsValid)
            {
                Enum.TryParse(complaint.Type.Trim(), out ComplaintType complaintType);
                ComplaintFactory factory = new ComplaintFactory(_complaintRepository);
                IComplaintResolver complaintResolver = factory.GetComplaintResolver(complaintType);
                complaintResolver.ResolveConsumerComplaint(complaint);
            }
            return ConsumerComplaints();
        }

        [Authorize(Roles = "CONSUMER")]
        public IActionResult GetComplaints()
        {
            UserClaimsHandler userClaims = new UserClaimsHandler(HttpContext?.User);
            string consumerEmailId = userClaims.GetUserEmailFromCurrentUserClaim();
            List<Complaint> complaints = (List<Complaint>)_complaintRepository.GetAllComplaintsByConsumerEmailId(consumerEmailId);
            ViewData["MyComplaints"] = (complaints.Count > 0) ? complaints: null;
            return View();
        }
    }
}
