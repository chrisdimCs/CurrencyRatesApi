using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RatesData.Entities;

namespace RatesData.Data;

public partial class RatesDbContext : DbContext
{
    public RatesDbContext()
    {
    }

    public RatesDbContext(DbContextOptions<RatesDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ConvertRate> ConvertRates { get; set; }

    public virtual DbSet<EcbRate> EcbRates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\RatesDb;Database=RatesDb;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ConvertRate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ConvertR__3214EC0704C07CF4");
        });

        modelBuilder.Entity<EcbRate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EcbRates__3214EC0747854F0B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
