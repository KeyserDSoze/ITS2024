using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Amazon.Payment.Sql2.Models;

public partial class EfcoreExampleContext : DbContext
{
    public EfcoreExampleContext()
    {
    }

    public EfcoreExampleContext(DbContextOptions<EfcoreExampleContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Item> Items { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EFCoreExample;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
