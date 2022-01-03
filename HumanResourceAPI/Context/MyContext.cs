using HumanResourceAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanResourceAPI.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<University> Universities { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Profiling> Profilings { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<University>()
                .HasMany(education => education.Educations)
                .WithOne(university => university.University);

            modelBuilder.Entity<Education>()
                .HasMany(profiling => profiling.Profilings)
                .WithOne(education => education.Education);

            modelBuilder.Entity<Employee>()
               .HasOne(a => a.Account)
               .WithOne(e => e.Employee)
               .HasForeignKey<Account>(e => e.NIK);

            modelBuilder.Entity<Account>()
              .HasOne(p => p.Profiling)
              .WithOne(a => a.Account)
              .HasForeignKey<Profiling>(a => a.NIK);

            modelBuilder.Entity<AccountRole>()
                .HasKey(ar => new { ar.NIK, ar.RoleId });

            modelBuilder.Entity<AccountRole>()
                .HasOne(ar => ar.Account)
                .WithMany(a => a.AccountRoles)
                .HasForeignKey(ar => ar.NIK);

            modelBuilder.Entity<AccountRole>()
                .HasOne(ar => ar.Role)
                .WithMany(r => r.AccountRoles)
                .HasForeignKey(ar => ar.RoleId);
        }
        

    }
}
