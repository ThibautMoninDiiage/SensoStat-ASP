using System;
using SensoStatWeb.Business.Interfaces;

namespace SensoStatWeb.Business
{
    public class FileService : IFileService
    {
        public async Task<T> ReadCsvFile<T>(StreamReader stream)
        {
            try
            {

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return default(T);
        }
    }
}

