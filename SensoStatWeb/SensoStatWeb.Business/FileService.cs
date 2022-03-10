using SensoStatWeb.Business.Interfaces;
using System.Text;

namespace SensoStatWeb.Business
{
    public class FileService : IFileService
    {
        public async Task<string> ReadCsvFile(Stream stream)
        {
            try
            {
                var reader = new StreamReader(stream);
                var content = reader.ReadToEndAsync();

                return await content;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
        }

        public async Task<Stream> WriteCsvFile<T>(IEnumerable<T> content)
        {
            var csvContent = "";

            // For each item in the list
            foreach (var userUrl in content)
            {
                // We get each property of each objects
                foreach (var userUrlProperty in userUrl.GetType().GetProperties())
                {
                    csvContent += (string)userUrlProperty.GetValue(userUrl); // add property value to csv
                    csvContent += ";"; // New cell
                }

                csvContent += "\r\n"; // New line
            }

            var stream = new MemoryStream(Encoding.ASCII.GetBytes(csvContent));

            return stream;
        }
    }
}

