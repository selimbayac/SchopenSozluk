using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchopenSozlukEntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SchopenSozlukDataAccessLayer.Concrete
{
    public class Context :  IdentityDbContext<AppUser, AppRole, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public Context(DbContextOptions<Context> options) : base (options)
        {
            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS01;Initial Catalog= SchopenSozluk;Integrated Security=True;");

        //}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Baslik>()
                .HasMany(b => b.Entries)
                .WithOne(e => e.Baslik)
                .HasForeignKey(e => e.BaslikId)
                .OnDelete(DeleteBehavior.Restrict); // Optionally, adjust delete behavior as needed

            builder.Entity<Entry>()
                .HasOne(e => e.User)
                .WithMany(u => u.Entries)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete entries when user is deleted

            builder.Entity<Like>()
                .HasOne(l => l.Entry)
                .WithMany(e => e.Likes)
                .HasForeignKey(l => l.EntryId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete likes when entry is deleted

            builder.Entity<Dislike>()
                .HasOne(d => d.Entry)
                .WithMany(e => e.Dislikes)
                .HasForeignKey(d => d.EntryId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete dislikes when entry is deleted

            builder.Entity<Dislike>()
                .HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete user-related dislikes

            builder.Entity<Comment>()
                .HasOne(c => c.Entry)
                .WithMany(e => e.Comments)
                .HasForeignKey(c => c.EntryId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete comments when entry is deleted
        }

        public DbSet<Baslik> Basliklar { get; set; }
        public DbSet<Entry> Entryler { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Dislike> Dislikes { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }  
}
