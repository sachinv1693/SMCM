using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMeterConsumerManagement.ConsumerImports;
using SmartMeterConsumerManagement.Controllers.ControllerUtils;
using SmartMeterConsumerManagement.Enums;
using SmartMeterConsumerManagement.Models;
using SmartMeterConsumerManagement.ServiceContracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SmartMeterConsumerManagement.Controllers
{
    public class ImportController : Controller
    {
        private readonly IWebHostEnvironment Environment;
        private readonly IUserRepository _userRepository;

        public ImportController(IWebHostEnvironment environment, IUserRepository userRepository)
        {
            Environment = environment;
            _userRepository = userRepository;
        }

        [Authorize(Roles = "PROJECT_MANAGER")]
        public IActionResult GetLegacyUsers()
        {
            DropdownSelectListBuilder listBuilder = new DropdownSelectListBuilder();
            ViewBag.ImportTypes = listBuilder.GetEnumTypeSelectList<ImportType>();
            return View();
        }

        [Authorize(Roles = "PROJECT_MANAGER")]
        [HttpPost]
        public IActionResult GetLegacyUsers(ImportDataModel importData)
        {
            if (ModelState.IsValid)
            {
                TempData.Put("ImportData", importData);
                return RedirectToAction("ImportSelectionHandler", "Import");
            }
            return GetLegacyUsers();
        }

        [Authorize(Roles = "PROJECT_MANAGER")]
        public IActionResult ImportSelectionHandler()
        {
            ImportDataModel importData = TempData.Get<ImportDataModel>("ImportData");
            return View(importData);
        }

        [Authorize(Roles = "PROJECT_MANAGER")]
        [HttpPost]
        public async Task<IActionResult> ImportSelectionHandler(ImportDataModel importData, List<IFormFile> postedFiles)
        {
            try
            {
                string dirPath = Path.Combine(Environment.ContentRootPath, "ConsumerImports\\CSV_Files");
                Directory.CreateDirectory(dirPath);
                if (postedFiles.Count == 0)
                {
                    ModelState.AddModelError("InvalidFileSelection", "Please upload valid csv files before proceeding.");
                    return View(importData);
                }
                foreach (IFormFile postedFile in postedFiles)
                {
                    string postedFileName = postedFile.FileName;
                    var ext = Path.GetExtension(postedFileName).ToLowerInvariant();
                    bool isUserOrUserLocationFile = postedFileName.StartsWith("user") || postedFileName.StartsWith("userLocation");
                    if (string.IsNullOrEmpty(ext) || ext != ".csv" || !isUserOrUserLocationFile)
                    {
                        ModelState.AddModelError("InvalidFileSelection", "Invalid file! Please upload valid csv files.");
                        return View(importData);
                    }
                    using FileStream stream = new FileStream(Path.Combine(dirPath, postedFileName), FileMode.Create);
                    await postedFile.CopyToAsync(stream);
                }
                ImportFactory factory = new ImportFactory(Environment, _userRepository);
                IImportType importObject = factory.GetImportTypeObject(importData.ImportDataType);
                bool isImportSuccessful = importObject.ImportConsumers();
                ViewData["ImportSuccessful"] = isImportSuccessful;
                return View(importData);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error in importing consumer data: " + exception.Message);
                if (exception.InnerException != null)
                {
                    Console.WriteLine("Error in importing consumer data: " + exception.InnerException.Message);
                }
                ViewData["ImportSuccessful"] = false;
                return View(importData);
            }
        }
    }
}
