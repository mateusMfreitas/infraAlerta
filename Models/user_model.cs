using infraAlerta.Helper;
using System.ComponentModel.DataAnnotations;
namespace infraAlerta.Models
{
    public class User{
        [Key]
        public int user_id{get;set;}
        public string name {get;set;}
        public string cpf{get;set;}
        public string phone{get;set;}
        public bool admin{get;set;}
        public string login { get;set;}
        public string email { get;set;}
        public string password { get;set;}
        public DateTime birthDate { get;set;} 

        public void SetPasswordHash()
        {
            password = password.GenerateHash();
        }

        public string NewPassword()
        {
            string newPassword = Guid.NewGuid().ToString().Substring(0, 8);
            password = newPassword.GenerateHash();
            return newPassword;
        }
        
    }
}