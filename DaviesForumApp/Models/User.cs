using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DaviesForumApp.Models
{
   
   
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [DisplayName("Username")]
        public string UserName { get; set; }
        [Required]
        [DisplayName("Password")]
        public string PassWord { get; set; }
        [DisplayName("Email")]
        public string UserEmail { get; set; }
        [DisplayName("Department")]
        public string Department { get; set; }
        [DisplayName("Age")]
        [Range(18, 100, ErrorMessage = "18+")]
        public int Age { get; set; }

        public List<MessagePost>? Posts { get; set; }

        

    }
}
