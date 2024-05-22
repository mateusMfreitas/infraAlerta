using System.ComponentModel.DataAnnotations;
namespace infraAlerta.Models
{
    public class Problem_address{
        [Key]
        public int pa_id{get;set;}
        public int pa_problem_id{get;set;}
        public string pa_address {get;set;}
        public int pa_number{get;set;}
        public string pa_neighborhood{get;set;}
        public string pa_city{get;set;}
        public string pa_state{get;set;}
    }
}