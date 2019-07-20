using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
            Database.Migrate();
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>(ConfigureCustomer);
            builder.Entity<Transaction>(ConfigureTransaction);
        }

        private void ConfigureCustomer(EntityTypeBuilder<Customer> builder)
        {
            //configure uniqueness for email property
            builder.HasIndex(u => u.Email).IsUnique();

            builder.Property(c => c.Email)
                .HasMaxLength(25)
                .IsRequired();

            builder.Property(c => c.Mobile)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(c => c.Name)
                .HasMaxLength(25)
                .IsRequired();
        }

        private void ConfigureTransaction(EntityTypeBuilder<Transaction> builder)
        {
            builder.Property(c => c.Date)
                .IsRequired();

            builder.Property(t => t.Currency)
                .HasMaxLength(3)
                .IsRequired();
        }
    }
}
