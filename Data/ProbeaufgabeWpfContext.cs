﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Probeaufgabe_WPF.Models;

namespace Probeaufgabe_WPF.Data;

public partial class ProbeaufgabeWpfContext : DbContext
{

    public ProbeaufgabeWpfContext()
    {
    }

    public ProbeaufgabeWpfContext(DbContextOptions<ProbeaufgabeWpfContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Person> Person { get; set; }

    public virtual DbSet<PersonPhonenumber> PersonPhonenumbers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=H:\\Program Files\\DB Browser for SQLite\\Probeaufgabe WPF.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.ToTable("Person");

            entity.HasIndex(e => e.Id, "IX_Person_ID").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Plz).HasColumnName("PLZ");
        });

        modelBuilder.Entity<PersonPhonenumber>(entity =>
        {
            entity.ToTable("Person_Phonenumber");

            entity.HasIndex(e => e.Id, "IX_Person_Phonenumber_ID").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.PersonId).HasColumnName("Person_ID");

            entity.HasOne(d => d.Person).WithMany(p => p.PersonPhonenumbers)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
