﻿// <auto-generated />
using ATM.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ATM.Infrastructure.Migrations
{
    [DbContext(typeof(ATMDBContext))]
    [Migration("20180806170130_NewDatabaseForPK")]
    partial class NewDatabaseForPK
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ATM.Core.Entities.BankAccount", b =>
                {
                    b.Property<string>("AccountNumber")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountName");

                    b.Property<decimal>("Balance");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("UserID");

                    b.HasKey("AccountNumber");

                    b.ToTable("BankAccounts");
                });

            modelBuilder.Entity("ATM.Core.Entities.Transaction", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountNumber");

                    b.Property<string>("Amount");

                    b.Property<string>("Recipient");

                    b.HasKey("ID");

                    b.HasIndex("AccountNumber");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("ATM.Core.Entities.Transaction", b =>
                {
                    b.HasOne("ATM.Core.Entities.BankAccount", "BankAccount")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountNumber");
                });
#pragma warning restore 612, 618
        }
    }
}
