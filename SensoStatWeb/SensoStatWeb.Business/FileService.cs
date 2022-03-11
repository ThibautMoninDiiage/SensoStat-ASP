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
            var csvContent = string.Empty;

            // We browse each property of our object and we write the name of this property in top of column
            content?.FirstOrDefault()?.GetType().GetProperties().ToList()
                .ForEach(property => csvContent += $"{property.Name};");
            csvContent += "\r\n"; // New line


            // For each item in the list
            content.ToList().ForEach(line =>
            {
                // We get each property of each objects
                line.GetType().GetProperties().ToList()
                    .ForEach(property => csvContent += $"{(string)property.GetValue(line)};");

                csvContent += "\r\n"; // New line
            });


            return new MemoryStream(Encoding.ASCII.GetBytes(csvContent));
        }
    }
}

