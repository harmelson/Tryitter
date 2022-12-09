﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tryitter.Repository;

#nullable disable

namespace Tryitter.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20221209161441_Users")]
    partial class Users
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Tryitter.Models.Post", b =>
                {
                    b.Property<int>("IdPost")
                        .HasColumnType("int");

                    b.Property<int>("LikesPost")
                        .HasColumnType("int");

                    b.Property<string>("MessagePost")
                        .IsRequired()
                        .HasMaxLength(280)
                        .HasColumnType("nvarchar(280)");

                    b.Property<int>("SharesPost")
                        .HasColumnType("int");

                    b.HasKey("IdPost");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("Tryitter.Models.PostUser", b =>
                {
                    b.Property<int>("IdPostUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPostUser"));

                    b.Property<int>("IdPost")
                        .HasColumnType("int");

                    b.Property<int>("IdUser")
                        .HasColumnType("int");

                    b.HasKey("IdPostUser");

                    b.HasIndex("IdUser");

                    b.ToTable("PostUser");
                });

            modelBuilder.Entity("Tryitter.Models.User", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUser"));

                    b.Property<string>("EmailUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUser");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Tryitter.Models.Post", b =>
                {
                    b.HasOne("Tryitter.Models.PostUser", "PostUsers")
                        .WithMany("Post")
                        .HasForeignKey("IdPost")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PostUsers");
                });

            modelBuilder.Entity("Tryitter.Models.PostUser", b =>
                {
                    b.HasOne("Tryitter.Models.User", "User")
                        .WithMany("PostUsers")
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Tryitter.Models.PostUser", b =>
                {
                    b.Navigation("Post");
                });

            modelBuilder.Entity("Tryitter.Models.User", b =>
                {
                    b.Navigation("PostUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
