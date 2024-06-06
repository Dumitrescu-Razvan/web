using BE.Model;
using Microsoft.EntityFrameworkCore;

namespace BE.Data
{
    public class Contex : DbContext
    {
        public Contex(DbContextOptions<Contex> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Url> Urls { get; set; }


    }
}
