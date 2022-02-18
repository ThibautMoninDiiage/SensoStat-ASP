using SensoStatApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensoStatWeb.Repository.Interfaces
{
    public interface IAdministratorRepository
    {
        public Administrator Login(string username, string password);
    }
}
