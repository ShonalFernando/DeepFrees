using CsvHelper.Configuration;
using CsvHelper;
using System.Data;
using System.Globalization;
using Microsoft.AspNetCore.Hosting;

namespace DeepFrees.WebPro.Services
{
    public class CSVReaderService
    {
        public async Task<List<Dictionary<string, string>>> ReadandParse()
        {
            var filePath = Path.Combine("Uploads/RouteData/DistanceMatrix.csv");

            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));

            var records = csv.GetRecords<dynamic>().ToList();

            var data = new List<Dictionary<string, string>>();

            foreach (var record in records)
            {
                var dictionary = new Dictionary<string, string>();
                foreach (var property in record.GetType().GetProperties())
                {
                    dictionary.Add(property.Name, property.GetValue(record)?.ToString());
                }
                data.Add(dictionary);
            }

            return data;
        }
    }
}
