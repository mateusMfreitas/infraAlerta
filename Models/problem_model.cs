using System.ComponentModel.DataAnnotations;
namespace infraAlerta.Models
{
    public class Problem{
        [Key]
        public int pro_id{get;set;}
        public string pro_classification {get;set;}
        public string pro_name{get;set;}
        public string pro_photo{get;set;}
    }
}