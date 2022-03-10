namespace SensoStatWeb.Business.Interfaces
{
    public interface IFileService
    {
        Task<string> ReadCsvFile(Stream stream);

        Task<Stream> WriteCsvFile<T>(IEnumerable<T> content);
    }
}

