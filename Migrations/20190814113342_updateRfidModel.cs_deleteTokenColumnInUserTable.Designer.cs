﻿// <auto-generated />
using System;
using ColaTerminal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ColaTerminal.Migrations
{
    [DbContext(typeof(traperto_kurtContext))]
    [Migration("20190905133820_deleteTokenColumnInUserTable")]
    partial class deleteTokenColumnInUserTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ColaTerminal.Models.BalanceTransaction", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<double?>("Amount")
                        .HasColumnName("amount");

                    b.Property<DateTime?>("Date")
                        .HasColumnName("date")
                        .HasColumnType("datetime");

                    b.Property<uint?>("UserId")
                        .HasColumnName("userId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .HasName("balanceTransaction_user");

                    b.ToTable("balanceTransaction");
                });

            modelBuilder.Entity("ColaTerminal.Models.Drink", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("varchar(20)");

                    b.Property<double>("Price")
                        .HasColumnName("price");

                    b.Property<int?>("Quantity")
                        .HasColumnName("quantity")
                        .HasColumnType("int(2)");

                    b.HasKey("Id");

                    b.ToTable("drink");
                });

            modelBuilder.Entity("ColaTerminal.Models.Proceed", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<DateTime?>("Date")
                        .HasColumnName("date")
                        .HasColumnType("datetime");

                    b.Property<uint?>("DrinkId")
                        .HasColumnName("drinkId");

                    b.Property<double>("Price");

                    b.Property<uint?>("UserId")
                        .HasColumnName("userId");

                    b.HasKey("Id");

                    b.HasIndex("DrinkId")
                        .HasName("proceed_drink");

                    b.HasIndex("UserId")
                        .HasName("proceed_user");

                    b.ToTable("proceed");
                });

            modelBuilder.Entity("ColaTerminal.Models.Refill", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<DateTime?>("Date")
                        .HasColumnName("date")
                        .HasColumnType("datetime");

                    b.Property<double?>("Price")
                        .HasColumnName("price");

                    b.Property<uint?>("UserId")
                        .HasColumnName("userId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .HasName("refill_user");

                    b.ToTable("refill");
                });

            modelBuilder.Entity("ColaTerminal.Models.RefillContainment", b =>
                {
                    b.Property<uint>("DrinkId")
                        .HasColumnName("drinkId");

                    b.Property<uint>("RefillId")
                        .HasColumnName("refillId");

                    b.Property<int?>("Quantity")
                        .HasColumnName("quantity")
                        .HasColumnType("int(11)");

                    b.HasKey("DrinkId", "RefillId")
                        .HasName("PRIMARY");

                    b.HasIndex("RefillId")
                        .HasName("conainment_refill");

                    b.ToTable("refillContainment");
                });

            modelBuilder.Entity("ColaTerminal.Models.User", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<double?>("Balance")
                        .HasColumnName("balance");

                    b.Property<string>("FirstName")
                        .HasColumnName("firstName")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("LastName")
                        .HasColumnName("lastName")
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Password")
                        .HasColumnName("password")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UserName")
                        .HasColumnName("userName")
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("user");
                });

            modelBuilder.Entity("ColaTerminal.Models.BalanceTransaction", b =>
                {
                    b.HasOne("ColaTerminal.Models.User", "User")
                        .WithMany("BalanceTransaction")
                        .HasForeignKey("UserId")
                        .HasConstraintName("balanceTransaction_user");
                });

            modelBuilder.Entity("ColaTerminal.Models.Proceed", b =>
                {
                    b.HasOne("ColaTerminal.Models.Drink", "Drink")
                        .WithMany("Proceed")
                        .HasForeignKey("DrinkId")
                        .HasConstraintName("proceed_drink");

                    b.HasOne("ColaTerminal.Models.User", "User")
                        .WithMany("Proceed")
                        .HasForeignKey("UserId")
                        .HasConstraintName("proceed_user");
                });

            modelBuilder.Entity("ColaTerminal.Models.Refill", b =>
                {
                    b.HasOne("ColaTerminal.Models.User", "User")
                        .WithMany("Refill")
                        .HasForeignKey("UserId")
                        .HasConstraintName("refill_user");
                });

            modelBuilder.Entity("ColaTerminal.Models.RefillContainment", b =>
                {
                    b.HasOne("ColaTerminal.Models.Drink", "Drink")
                        .WithMany("RefillContainment")
                        .HasForeignKey("DrinkId")
                        .HasConstraintName("containment_drink");

                    b.HasOne("ColaTerminal.Models.Refill", "Refill")
                        .WithMany("RefillContainment")
                        .HasForeignKey("RefillId")
                        .HasConstraintName("conainment_refill");
                });
#pragma warning restore 612, 618
        }
    }
}
