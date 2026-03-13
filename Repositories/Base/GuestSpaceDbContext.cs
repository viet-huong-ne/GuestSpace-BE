using Contract.Repositories.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Base
{
    public class GuestSpaceDbContext : IdentityDbContext<User, Role, int, UserClaims, UserRoles, UserLogins, RoleClaims, UserTokens>
    {
        public GuestSpaceDbContext(DbContextOptions<GuestSpaceDbContext> options) : base(options) { }
        public virtual DbSet<User> Users => Set<User>();
        public virtual DbSet<Role> Roles => Set<Role>();
        public virtual DbSet<UserClaims> UserClaims => Set<UserClaims>();
        public virtual DbSet<UserRoles> UserRoles => Set<UserRoles>();
        public virtual DbSet<UserLogins> UserLogins => Set<UserLogins>();
        public virtual DbSet<RoleClaims> RoleClaims => Set<RoleClaims>();
        public virtual DbSet<UserTokens> UserTokens => Set<UserTokens>();
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingService> BookingServices { get; set; }
        public DbSet<Homestay> Homestays { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<RevenueReport> RevenueReports { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<RoomImage> RoomImages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableAnnotation = entityType.GetAnnotation("Relational:TableName");
                string tableName = tableAnnotation?.Value?.ToString() ?? "";
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
            modelBuilder.Entity<BookingService>().Property(a => a.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Booking>().Property(a => a.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Homestay>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Payment>().Property(n => n.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Review>().Property(nw => nw.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<RevenueReport>().Property(m => m.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Room>().Property(mg => mg.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Service>().Property(hr => hr.Id).ValueGeneratedOnAdd();
        }

    }
}
