// <auto-generated />
using System;
using MariaDb.API.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MariaDb.API.Migrations
{
    [DbContext(typeof(MariaDbDataAccess))]
    partial class MariaDbDataAccessModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MariaDb.API.Models.ProductModel", b =>
                {
                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<ushort>("Category")
                        .HasColumnType("smallint unsigned");

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("PriceTag")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<ushort>("Quantity")
                        .HasColumnType("smallint unsigned");

                    b.HasKey("ProductId")
                        .HasName("PrimaryKey_ProductId");

                    b.ToTable("ProductTable");
                });

            modelBuilder.Entity("MariaDb.API.Models.UserModel", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<short>("Age")
                        .HasColumnType("smallint");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("UserId")
                        .HasName("PrimaryKey_UserId");

                    b.ToTable("UserTable");
                });
#pragma warning restore 612, 618
        }
    }
}
