using System.ComponentModel.DataAnnotations;
namespace infraAlerta.Models
{
    public class User_address{
        [Key]
        public int ua_id{get;set;}
        public string ua_address {get;set;}
        public int ua_number{get;set;}
        public string ua_neighborhood{get;set;}
        public string ua_city{get;set;}
        public string ua_state{get;set;}
        public int ua_user_id{get;set;}
    }
}