using CsvHelper;
using SmartMeterConsumerManagement.Models.DBContext;
using SmartMeterConsumerManagement.ServiceContracts;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace SmartMeterConsumerManagement.ConsumerImports
{
    public class CsvFileImport : IImportType
    {
        private readonly string CsvFilePath;
        private readonly IUserRepository _userRepository;

        public CsvFileImport(string csvFilePath, IUserRepository userRepository)
        {
            CsvFilePath = csvFilePath;
            _userRepository = userRepository;
        }

        public bool ImportConsumers()
        {
            string[] filePaths = Directory.EnumerateFiles(CsvFilePath, "*.csv").ToArray();
            if (filePaths.Length == 0)
            {
                return false; // No csv files to process
            }
            IList<UserLocation> userLocationRecords = null;
            IList<User> userRecords = null;
            foreach (var filePath in filePaths)
            {
                using TextReader reader = new StreamReader(filePath);
                var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
                string fileName = Path.GetFileName(filePath);
                if (fileName.StartsWith("userLocation"))
                {
                    userLocationRecords = csvReader.GetRecords<UserLocation>().ToList();
                }
                else if (fileName.StartsWith("user"))
                {
                    csvReader.Configuration.HeaderValidated = null;
                    csvReader.Configuration.MissingFieldFound = (headerNames, index, context) => {};
                    userRecords = csvReader.GetRecords<User>().ToList();
                }
            }
            for (int index = 0; index < userRecords.Count; index++)
            {
                userRecords[index].Location = userLocationRecords[index];
                _userRepository.AddUser(userRecords[index]);
            }
            return true;
        }
    }
}
