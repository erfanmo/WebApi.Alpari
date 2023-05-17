using Microsoft.EntityFrameworkCore;
using WebApi.Alpari.Models.Entities;

namespace WebApi.Alpari.Models.Context
{
    public class DataBaseContext :DbContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ToDo> Todo { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> userTokens { get; set; }
        public DbSet<SmsCode> SmsCode { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDo>().HasQueryFilter(p=> !p.IsDeleted);
        }

    }
}
