using System;

namespace SensoStatWeb.Business.Interfaces
{
    public interface IFileService
    {
        Task<T> ReadCsvFile<T>(StreamReader stream);
    }
}

