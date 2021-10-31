using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HCI_Restaurants.Models
{
    public partial class hcirestaurantsContext : DbContext
    {
        public hcirestaurantsContext()
        {
        }

        public hcirestaurantsContext(DbContextOptions<hcirestaurantsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<Covid19> Covid19 { get; set; }
        public virtual DbSet<Cuisines> Cuisines { get; set; }
        public virtual DbSet<Restaurants> Restaurants { get; set; }
        public virtual DbSet<Reviews> Reviews { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlite("DataSource=./data/hci-restaurants.db;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cities>(entity =>
            {
                entity.ToTable("cities");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CityName)
                    .HasColumnName("city_name")
                    .HasColumnType("VARCHAR (70)");

                entity.Property(e => e.CountryName)
                    .HasColumnName("country_name")
                    .HasColumnType("VARCHAR (70)");

                entity.Property(e => e.FullName)
                    .HasColumnName("full_name")
                    .HasColumnType("VARCHAR (70)");

                entity.Property(e => e.StateCode)
                    .HasColumnName("state_code")
                    .HasColumnType("VARCHAR (3)");

                entity.Property(e => e.StateName)
                    .HasColumnName("state_name")
                    .HasColumnType("VARCHAR (50)");
            });

            modelBuilder.Entity<Covid19>(entity =>
            {
                entity.ToTable("covid19");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Comments)
                    .HasColumnName("comments")
                    .HasColumnType("VARCHAR (200)");

                entity.Property(e => e.Curbside)
                    .HasColumnName("curbside")
                    .HasColumnType("BOOLEAN");

                entity.Property(e => e.IndoorDining)
                    .HasColumnName("indoor_dining")
                    .HasColumnType("BOOLEAN");

                entity.Property(e => e.LimitSeating)
                    .HasColumnName("limit_seating")
                    .HasColumnType("BOOLEAN");

                entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");

                entity.Property(e => e.TakeOut)
                    .HasColumnName("take_out")
                    .HasColumnType("BOOLEAN");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Covid19)
                    .HasForeignKey(d => d.RestaurantId);
            });

            modelBuilder.Entity<Cuisines>(entity =>
            {
                entity.ToTable("cuisines");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CuisineName)
                    .HasColumnName("cuisine_name")
                    .HasColumnType("VARCHAR (70)");
            });

            modelBuilder.Entity<Restaurants>(entity =>
            {
                entity.ToTable("restaurants");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasColumnType("VARCHAR (70)");

                entity.Property(e => e.AggregateRating)
                    .HasColumnName("aggregate_rating")
                    .HasColumnType("VARCHAR (10)");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasColumnType("VARCHAR (50)");

                entity.Property(e => e.CityId).HasColumnName("city_id");

                entity.Property(e => e.CuisineId).HasColumnName("cuisine_id");

                entity.Property(e => e.Cuisines)
                    .HasColumnName("cuisines")
                    .HasColumnType("VARCHAR (50)");

                entity.Property(e => e.Currency)
                    .HasColumnName("currency")
                    .HasColumnType("VARCHAR (1)");

                entity.Property(e => e.Establishment)
                    .HasColumnName("establishment")
                    .HasColumnType("VARCHAR (35)");

                entity.Property(e => e.HasDelivery).HasColumnName("has_delivery");

                entity.Property(e => e.HasTakeaway).HasColumnName("has_takeaway");

                entity.Property(e => e.Locality)
                    .HasColumnName("locality")
                    .HasColumnType("VARCHAR (30)");

                entity.Property(e => e.LocalityVerbose)
                    .HasColumnName("locality_verbose")
                    .HasColumnType("VARCHAR (50)");

                entity.Property(e => e.MenuUrl)
                    .HasColumnName("menu_url")
                    .HasColumnType("VARCHAR (200)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("VARCHAR (50)");

                entity.Property(e => e.PriceRange).HasColumnName("price_range");

                entity.Property(e => e.RatingText)
                    .HasColumnName("rating_text")
                    .HasColumnType("VARCHAR (50)");

                entity.Property(e => e.StateCode)
                    .HasColumnName("state_code")
                    .HasColumnType("VARCHAR (2)");

                entity.Property(e => e.Telephone)
                    .HasColumnName("telephone")
                    .HasColumnType("VARCHAR (25)");

                entity.Property(e => e.Timings)
                    .HasColumnName("timings")
                    .HasColumnType("VARCHAR (100)");

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasColumnType("VARCHAR (200)");

                entity.Property(e => e.ZipCode)
                    .HasColumnName("zip_code")
                    .HasColumnType("VARCHAR (10)");

                entity.HasOne(d => d.CityNavigation)
                    .WithMany(p => p.Restaurants)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Cuisine)
                    .WithMany(p => p.Restaurants)
                    .HasForeignKey(d => d.CuisineId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Reviews>(entity =>
            {
                entity.ToTable("reviews");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CustomerName)
                    .HasColumnName("customer_name")
                    .HasColumnType("VARCHAR (50)");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.RatingText)
                    .HasColumnName("rating_text")
                    .HasColumnType("VARCHAR (250)");

                entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");

                entity.Property(e => e.ReviewText)
                    .HasColumnName("review_text")
                    .HasColumnType("VARCHAR (250)");

                entity.Property(e => e.ReviewTimeFriendly)
                    .HasColumnName("review_time_friendly")
                    .HasColumnType("VARCHAR (50)");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.RestaurantId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
