using Amazon.Authentication.Domain;
using Amazon.Authentication.Sql.Models;
using Microsoft.EntityFrameworkCore;

namespace Amazon.Authentication.Sql
{
    public sealed class AuthenticationDbContext : DbContext
    {
        public DbSet<ApiKeyTable> ApiKeys { get; set; }
        public AuthenticationDbContext() {         }
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseInMemoryDatabase("MyDatabase");
            optionsBuilder.UseSqlServer(@"Server=sql_server;Initial Catalog=Authentication;User Id=SA;Password=C4nD1ti55#;Encrypt=false;TrustServerCertificate=true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApiKeyTable>().HasKey(i => i.Id);
        }
    }
}
