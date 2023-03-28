using DaviesForumApp.Models;
using Microsoft.EntityFrameworkCore;

namespace DaviesForumApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
            
        }

        public DbSet<User> Users { get; set; }

        public DbSet<MessagePost> Posts { get; set; }

        public DbSet<Replies> Replies { get; set; }
    }
}
