﻿// <auto-generated />

namespace Infrastructure.DataAccess.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Metadata;

    [DbContext(typeof(MangaContext))]
    internal class MangaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Infrastructure.DataAccess.Entities.Account", b =>
            {
                b.Property<Guid>("AccountId")
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("Currency")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<Guid>("CustomerId")
                    .HasColumnType("uniqueidentifier");

                b.HasKey("AccountId");

                b.HasIndex("CustomerId");

                b.ToTable("Account");

                b.HasData(
                    new
                    {
                        AccountId = new Guid("4c510cfe-5d61-4a46-a3d9-c4313426655f"),
                        Currency = "USD",
                        CustomerId = new Guid("197d0438-e04b-453d-b5de-eca05960c6ae")
                    });
            });

            modelBuilder.Entity("Infrastructure.DataAccess.Entities.Credit", b =>
            {
                b.Property<Guid>("CreditId")
                    .HasColumnType("uniqueidentifier");

                b.Property<Guid>("AccountId")
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("Currency")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime>("TransactionDate")
                    .HasColumnType("datetime2");

                b.Property<decimal>("Value")
                    .HasColumnType("decimal(18,2)");

                b.HasKey("CreditId");

                b.HasIndex("AccountId");

                b.ToTable("Credit");

                b.HasData(
                    new
                    {
                        CreditId = new Guid("7bf066ba-379a-4e72-a59b-9755fda432ce"),
                        AccountId = new Guid("4c510cfe-5d61-4a46-a3d9-c4313426655f"),
                        Currency = "USD",
                        TransactionDate =
                            new DateTime(2020, 7, 24, 11, 39, 34, 140, DateTimeKind.Utc).AddTicks(3504),
                        Value = 400m
                    });
            });

            modelBuilder.Entity("Infrastructure.DataAccess.Entities.Customer", b =>
            {
                b.Property<Guid>("CustomerId")
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("FirstName")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("LastName")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("SSN")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<Guid>("UserId")
                    .HasColumnType("uniqueidentifier");

                b.HasKey("CustomerId");

                b.HasIndex("UserId");

                b.ToTable("Customer");

                b.HasData(
                    new
                    {
                        CustomerId = new Guid("197d0438-e04b-453d-b5de-eca05960c6ae"),
                        FirstName = "Ivan Paulovich",
                        LastName = "Ivan Paulovich",
                        SSN = "8608179999",
                        UserId = new Guid("e278ee65-6c41-42d6-9a73-838199a44d62")
                    });
            });

            modelBuilder.Entity("Infrastructure.DataAccess.Entities.Debit", b =>
            {
                b.Property<Guid>("DebitId")
                    .HasColumnType("uniqueidentifier");

                b.Property<Guid>("AccountId")
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("Currency")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<DateTime>("TransactionDate")
                    .HasColumnType("datetime2");

                b.Property<decimal>("Value")
                    .HasColumnType("decimal(18,2)");

                b.HasKey("DebitId");

                b.HasIndex("AccountId");

                b.ToTable("Debit");

                b.HasData(
                    new
                    {
                        DebitId = new Guid("31ade963-bd69-4afb-9df7-611ae2cfa651"),
                        AccountId = new Guid("4c510cfe-5d61-4a46-a3d9-c4313426655f"),
                        Currency = "USD",
                        TransactionDate =
                            new DateTime(2020, 7, 24, 11, 39, 34, 140, DateTimeKind.Utc).AddTicks(5105),
                        Value = 400m
                    });
            });

            modelBuilder.Entity("Infrastructure.DataAccess.Entities.User", b =>
            {
                b.Property<Guid>("UserId")
                    .HasColumnType("uniqueidentifier");

                b.Property<string>("ExternalUserId")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("UserId");

                b.ToTable("User");

                b.HasData(
                    new {UserId = new Guid("e278ee65-6c41-42d6-9a73-838199a44d62"), ExternalUserId = "GitHub/7133698"});
            });

            modelBuilder.Entity("Infrastructure.DataAccess.Entities.Account", b =>
            {
                b.HasOne("Infrastructure.DataAccess.Entities.Customer", "Customer")
                    .WithMany("AccountsCollection")
                    .HasForeignKey("CustomerId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Infrastructure.DataAccess.Entities.Credit", b =>
            {
                b.HasOne("Infrastructure.DataAccess.Entities.Account", "Account")
                    .WithMany("CreditsCollection")
                    .HasForeignKey("AccountId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Infrastructure.DataAccess.Entities.Customer", b =>
            {
                b.HasOne("Infrastructure.DataAccess.Entities.User", "User")
                    .WithMany("CustomersCollection")
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity("Infrastructure.DataAccess.Entities.Debit", b =>
            {
                b.HasOne("Infrastructure.DataAccess.Entities.Account", "Account")
                    .WithMany("DebitsCollection")
                    .HasForeignKey("AccountId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });
#pragma warning restore 612, 618
        }
    }
}
