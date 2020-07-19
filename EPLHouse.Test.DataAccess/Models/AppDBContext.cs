using System;
using System.Collections.Generic;
using System.Text;
using EPLHouse.Cards.DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EPLHouse.Test.DataAccess.Models
{
    public class AppDBContext : IdentityDbContext<AppUser>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<CardsLevel> CardsLevels { get; set; }
        public DbSet<CardsType> CardsTypes { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<PrintRequests> printRequests { get; set; }

    }
}
