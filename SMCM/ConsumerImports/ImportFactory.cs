using Microsoft.AspNetCore.Hosting;
using SmartMeterConsumerManagement.Enums;
using SmartMeterConsumerManagement.ServiceContracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMeterConsumerManagement.ConsumerImports
{
    public class ImportFactory
    {
        private readonly string CsvFilePath;
        private readonly IUserRepository _userRepository;
        public ImportFactory(IWebHostEnvironment environment, IUserRepository userRepository)
        {
            CsvFilePath = Path.Combine(environment.ContentRootPath, "ConsumerImports\\CSV_Files");
            _userRepository = userRepository;
        }

        public IImportType GetImportTypeObject(ImportType type)
        {
            return type switch
            {
                ImportType.CSV_FILE_IMPORT => new CsvFileImport(CsvFilePath, _userRepository),
                ImportType.SHARE_POINT_IMPORT => new SharePointImport(),
                _ => null
            };
        }
    }
}
