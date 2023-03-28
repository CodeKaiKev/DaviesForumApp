using Microsoft.AspNetCore.SignalR;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaviesForumApp.Models
{
    public class MessagePost
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public string Title { get; set; }


        [DisplayName("UserId")]
        public int UserId { get; set; }
        //User Relations
        public User? User { get; set; }
       

        //Replies Relations
        public List<Replies>? Replies { get; set; }
    }
}
