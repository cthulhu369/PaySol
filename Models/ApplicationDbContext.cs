using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace SuperPayments.Models {
    public class ApplicationDbContext: DbContext {
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        public string DbPath { get; }
        public ApplicationDbContext() {
            DbPath = "/home/ericp/Coding/csharp/SuperPayments/superpayments.db";
            OnConfiguring(new DbContextOptionsBuilder<ApplicationDbContext>());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Sender)
                .WithMany(u => u.SentTransactions)
                .HasForeignKey(t => t.SenderId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Receiver)
                .WithMany(u => u.ReceivedTransactions)
                .HasForeignKey(t => t.ReceiverId);
        }
    }
}