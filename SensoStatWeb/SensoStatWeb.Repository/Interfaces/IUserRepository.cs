using System;
using SensoStatWeb.Models.Entities;

namespace SensoStatWeb.Repository.Interfaces
{
	public interface IUserRepository
	{
		public User GetUser(int id);
	}
}

