using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace backend.Database.Models
{
    public class Message
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int Id { get; set; }
        public string Content { get; set; }

        public virtual Theme Theme { get; set; }
        public virtual User User { get; set; }
    }
}
