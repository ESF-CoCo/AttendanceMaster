﻿using Microsoft.EntityFrameworkCore;

using Server.Models;
using Server.Models.JoinTables;

namespace Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Class> Classes => Set<Class>();
        public DbSet<Person> People => Set<Person>();
        public DbSet<ClassList> ClassList => Set<ClassList>();

        public DbSet<Staff> Staffs => Set<Staff>();
        public DbSet<StaffClass> StaffClass => Set<StaffClass>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the Person-Class many-to-many relationship
            modelBuilder.Entity<ClassList>()
                .HasKey(cl => new { cl.PersonID, cl.ClassID });

            modelBuilder.Entity<Person>()
                .HasMany(p => p.Classes)
                .WithMany(p => p.People)
                .UsingEntity<ClassList>(
                    r => r.HasOne(x => x.Class)
                          .WithMany()
                          .HasForeignKey(x => x.ClassID),
                    l => l.HasOne(x => x.Person)
                          .WithMany()
                          .HasForeignKey(x => x.PersonID)
                );

            // Configure the Staff-Class many-to-many relationship
            modelBuilder.Entity<StaffClass>()
                .HasKey(cl => new { cl.StaffID, cl.ClassID });

            modelBuilder.Entity<Staff>()
                .HasMany(s => s.Classes)
                .WithMany(c => c.Staffs)
                .UsingEntity<StaffClass>(
                    r => r.HasOne(x => x.Class)
                          .WithMany()
                          .HasForeignKey(x => x.ClassID),
                    l => l.HasOne(x => x.Staff)
                          .WithMany()
                          .HasForeignKey(x => x.StaffID)
                );

            // Configure the Staff-Session one-to-many relationship
            modelBuilder.Entity<Staff>()
                .HasMany(s => s.Sessions)
                .WithOne(s => s.staff)
                .HasForeignKey(s => s.staffID)
                .IsRequired();

            // Seed data
            modelBuilder.Entity<Person>()
                .HasData(Seed.Person());

            modelBuilder.Entity<Class>()
                .HasData(Seed.Class());

            modelBuilder.Entity<ClassList>()
                .HasData(Seed.ClassList());

            modelBuilder.Entity<Staff>()
                .HasData(Seed.Staff());

            modelBuilder.Entity<StaffClass>()
                .HasData(Seed.StaffClass());
        }
    }
}
