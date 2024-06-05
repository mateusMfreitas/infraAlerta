using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace infraAlerta.Models
{
    public class Comment
    {
        [Key]
        public int comment_id { get; set; }
        public int pro_id { get; set; }
        public int user_id { get; set; }
        public string comment_text { get; set; } = string.Empty;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime created_at { get; set; } = DateTime.Now;

        [ForeignKey("pro_id")]
        public Problem Problem { get; set; }

        [ForeignKey("user_id")]
        public User User { get; set; }
    }
}
