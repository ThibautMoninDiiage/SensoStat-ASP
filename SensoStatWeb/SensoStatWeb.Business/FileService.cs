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

            foreach (var userUrl in content)
            {
                foreach (var userUrlPropery in userUrl.GetType().GetProperties())
                {
                    var propertyValue = (string)userUrlPropery.GetValue(userUrl);
                    csvContent += propertyValue;
                    csvContent += ";";
                }

                csvContent += "\r\n";
            }

            var stream = new MemoryStream(Encoding.ASCII.GetBytes(csvContent));

            return stream;
        }
    }
}

