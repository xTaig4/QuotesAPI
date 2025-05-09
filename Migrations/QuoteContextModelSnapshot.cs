﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QuoteAPI.Data;

#nullable disable

namespace QuoteAPI.Migrations
{
    [DbContext(typeof(QuoteContext))]
    partial class QuoteContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("QuoteAPI.Models.Quote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("anime")
                        .HasColumnType("TEXT");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("image")
                        .HasColumnType("TEXT");

                    b.Property<string>("lastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("quote")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Quotes");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            anime = "Naruto",
                            firstName = "Itachi",
                            image = "https://i.pinimg.com/736x/72/b7/18/72b718edd0ad5bb48601e2243c4663b4.jpg",
                            lastName = "Uchiha",
                            quote = "Each of us lives, dependent and bound by our individual knowledge and our awareness. All that is what we call reality. However, both knowledge and awareness are equivocal. One’s reality might be another’s illusion. We all live inside our own fantasies."
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
