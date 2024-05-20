using Amazon.Payment.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Payment.Sql
{
    public sealed class PaymentDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public PaymentDbContext() {         }
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseInMemoryDatabase("MyDatabase");
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFCoreExample;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Item>().HasKey(i => i.Id);
        }
    }
}
