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

            var index = 0;
            // For each item in the list
            foreach (var line in content)
            {
                if (index == 0)
                {
                    // We get each property of each objects
                    foreach (var cell in line.GetType().GetProperties())
                    {
                        csvContent += cell.Name;
                        csvContent += ";"; // New cell
                    }
                    csvContent += "\r\n"; // New line
                }

                // We get each property of each objects
                foreach (var cell in line.GetType().GetProperties())
                {
                    csvContent += (string)cell.GetValue(line); // add property value to csv
                    csvContent += ";"; // New cell
                }

                csvContent += "\r\n"; // New line
                index++;
            }


            var stream = new MemoryStream(Encoding.ASCII.GetBytes(csvContent));

            return stream;
        }
    }
}

