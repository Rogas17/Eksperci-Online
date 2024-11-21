﻿// <auto-generated />
using System;
using EksperciOnline.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EksperciOnline.Migrations
{
    [DbContext(typeof(EksperciOnlineDbContext))]
    partial class EksperciOnlineDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.20");

            modelBuilder.Entity("EksperciOnline.Models.Domain.Kategoria", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("NazwaKategorii")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UrlZdjęcia")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Kategorie");
                });

            modelBuilder.Entity("EksperciOnline.Models.Domain.ServiceComment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Grade")
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("UsługaId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UsługaId");

                    b.ToTable("ServiceComment");
                });

            modelBuilder.Entity("EksperciOnline.Models.Domain.Usługa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Autor")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("CenaDo")
                        .HasColumnType("REAL");

                    b.Property<double>("CenaOd")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("DataPulikacji")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("KategoriaId")
                        .HasColumnType("TEXT");

                    b.Property<string>("KrótkiOpis")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Lokalizacja")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("NrTelefonu")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Opis")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Tytuł")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UrlBaneru")
                        .HasColumnType("TEXT");

                    b.Property<string>("UrlZdjęcia")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Widoczność")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("KategoriaId");

                    b.ToTable("Usługi");
                });

            modelBuilder.Entity("EksperciOnline.Models.Domain.ServiceComment", b =>
                {
                    b.HasOne("EksperciOnline.Models.Domain.Usługa", null)
                        .WithMany("Comments")
                        .HasForeignKey("UsługaId");
                });

            modelBuilder.Entity("EksperciOnline.Models.Domain.Usługa", b =>
                {
                    b.HasOne("EksperciOnline.Models.Domain.Kategoria", "Kategoria")
                        .WithMany("Usługi")
                        .HasForeignKey("KategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kategoria");
                });

            modelBuilder.Entity("EksperciOnline.Models.Domain.Kategoria", b =>
                {
                    b.Navigation("Usługi");
                });

            modelBuilder.Entity("EksperciOnline.Models.Domain.Usługa", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
