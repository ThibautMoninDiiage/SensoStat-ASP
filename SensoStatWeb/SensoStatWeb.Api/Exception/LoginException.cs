using System;
namespace SensoStatWeb.WebApplication.Exceptions
{
    public class LoginException : Exception
    {
        public LoginException() { }

        public LoginException(string username, string password)
            : base(String.Format("Identifiant " + username + " ou mot de passe " + password + " invalide")) { }
    }
}