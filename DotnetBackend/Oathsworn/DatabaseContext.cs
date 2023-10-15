using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.Extensions.Configuration;
using RAZE.Entities;

namespace RAZE
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) :
            base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured) options.UseSqlite("Data Source=RAZE.db");
        }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<BonusType> BonusTypes { get; set; }

        public DbSet<Building> Buildings { get; set; }

        public DbSet<BuildingCost> BuildingCosts { get; set; }

        public DbSet<Element> Elements { get; set; }

        public DbSet<GameRoom> GameRooms { get; set; }

        public DbSet<PlayerBonus> PlayerBonuses { get; set; }

        public DbSet<PlayerBuilding> PlayerBuildings { get; set; }

        public DbSet<PlayerProduction> PlayerProductions { get; set; }

        public DbSet<PlayerResource> PlayerResources { get; set; }

        public DbSet<PlayerSession> PlayerSessions { get; set; }

        public DbSet<PlayerTroop> PlayerTroops { get; set; }

        public DbSet<Request> Requests { get; set; }

        public DbSet<StatusType> StatusTypes { get; set; }

        public DbSet<Troop> Troops { get; set; }

        public DbSet<TroopCost> TroopCosts { get; set; }
    }
}
