using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RentACar.Domain.Entities;

namespace RentACar.Persistence.Contexts
{
    public class BaseDbContext:DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration):base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //    base.OnConfiguring(
            //        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SomeConnectionString")));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>(a =>
            {
                a.ToTable("Brands").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
                a.HasMany(p => p.Models);
            });

            modelBuilder.Entity<Model>(m =>
            {
                m.ToTable("Models").HasKey(k => k.Id);
                m.Property(p => p.Id).HasColumnName("Id");
                m.Property(p => p.BrandId).HasColumnName("BrandId");
                m.Property(p => p.Name).HasColumnName("Name");
                m.Property(p => p.DailyPrice).HasColumnName("DailyPrice");
                m.Property(p => p.ImageUrl).HasColumnName("ImageUrl");
                m.HasOne(p => p.Brand);
            });

            modelBuilder.Entity<User>(user =>
            {
                user.ToTable("Users");
                user.HasKey(k => k.Id);
                user.Property<string>(p => p.FirstName).HasColumnName<string>("FirstName");
                user.Property<string>(p => p.LastName).HasColumnName<string>("LastName");
                user.Property<string>(p => p.Email).HasColumnName<string>("Email");
                user.Property<byte[]>(p => p.PasswordSalt).HasColumnName<byte[]>("PasswordSalt");
                user.Property<byte[]>(p => p.PasswordHash).HasColumnName<byte[]>("PasswordHash");
                user.Property(p => p.Status).HasColumnName("Status");
                user.Property(p => p.AuthenticatorType).HasColumnName("AuthenticatorType");
                user.HasMany(user => user.UserOperationClaims);
                user.HasMany(user => user.RefreshTokens);
            });

            modelBuilder.Entity<OperationClaim>(oc =>
            {
                oc.ToTable("OperationClaims");
                oc.HasKey(k => k.Id);
                oc.Property<string>(p => p.Name).HasColumnName<string>("Name");
            });

            modelBuilder.Entity<UserOperationClaim>(uoc =>
            {
                uoc.ToTable("UserOperationClaims");
                uoc.HasKey(k => k.Id);
                uoc.Property(p => p.UserId).HasColumnName("UserId");
                uoc.Property(p => p.OperationClaimId).HasColumnName("OperationClaimId");
                uoc.HasOne(p => p.User);
                uoc.HasOne(p => p.OperationClaim);
            });

            modelBuilder.Entity<RefreshToken>(rt =>
            {
                rt.ToTable("RefreshTokens");
                rt.HasKey(k => k.Id);
                rt.Property(p => p.UserId).HasColumnName("UserId");
                rt.Property(p => p.Token).HasColumnName("Token");
                rt.Property(p => p.Expires).HasColumnName("Expires");
                rt.Property(p => p.Created).HasColumnName("Created");
                rt.Property(p => p.CreatedByIp).HasColumnName("CreatedByIp");
                rt.Property(p => p.Revoked).HasColumnName("Revoked");
                rt.Property(p => p.RevokedByIp).HasColumnName("RevokedByIp");
                rt.Property(p => p.ReplacedByToken).HasColumnName("ReplacedByToken");
                rt.Property(p => p.ReasonRevoked).HasColumnName("ReasonRevoked");
                rt.HasOne(p => p.User);
            });


            Brand[] brandEntitySeeds = { new(1, "BMW"), new(2, "Mercedes") };
            modelBuilder.Entity<Brand>().HasData(brandEntitySeeds);

            Model[] modelEntitySeeds = { new(1, 1, "Series 4", 1500, ""), new(2, 1, "Series 3", 1200, ""), new(3, 2, "A 180", 1000, "") };
            modelBuilder.Entity<Model>().HasData(modelEntitySeeds);
        }

    }
}
