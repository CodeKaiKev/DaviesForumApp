using System.ComponentModel.DataAnnotations;

namespace DaviesForumApp.Models
{
    public class Replies
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Reply { get; set; }
        public string? Name { get; set; }

        //Relational to message Database

        public MessagePost? MessagePost { get; set; }

        public int MessagePostId { get; set; }

        //Relational to Replies

        
    }
}
