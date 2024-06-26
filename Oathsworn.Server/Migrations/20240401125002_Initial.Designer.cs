﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oathsworn;

#nullable disable

namespace Oathsworn.Server.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240401125002_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("Oathsworn.Entities.Attack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BonusDamage")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BossId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("BossPart")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("EmpowerTokensUsed")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PlayerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RerollTokensUsed")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BossId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Attacks");
                });

            modelBuilder.Entity("Oathsworn.Entities.AttackMinion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AttackId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MinionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AttackId");

                    b.HasIndex("MinionId");

                    b.ToTable("AttackMinions");
                });

            modelBuilder.Entity("Oathsworn.Entities.Boss", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ActionComponentIndex")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CustomData")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Defence")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Direction")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EncounterId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Health")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Might")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TargetId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("XPosition")
                        .HasColumnType("INTEGER");

                    b.Property<int>("YPosition")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EncounterId")
                        .IsUnique();

                    b.HasIndex("TargetId");

                    b.ToTable("Bosses");
                });

            modelBuilder.Entity("Oathsworn.Entities.BossAction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BossId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Stage")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BossId");

                    b.ToTable("BossActions");
                });

            modelBuilder.Entity("Oathsworn.Entities.BossAttack", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BossId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BossId");

                    b.ToTable("BossAttacks");
                });

            modelBuilder.Entity("Oathsworn.Entities.BossAttackPlayer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BossAttackId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PlayerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BossAttackId");

                    b.HasIndex("PlayerId");

                    b.ToTable("BossAttackPlayers");
                });

            modelBuilder.Entity("Oathsworn.Entities.Encounter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CharacterPerformingAction")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateStarted")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Encounters");
                });

            modelBuilder.Entity("Oathsworn.Entities.EncounterMightDeck", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EncounterId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsFreeCompanyDeck")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EncounterId");

                    b.ToTable("EncounterMightDecks");
                });

            modelBuilder.Entity("Oathsworn.Entities.EncounterPlayer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CurrentAnimus")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CurrentHealth")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EncounterId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PlayerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Tokens")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("XPosition")
                        .HasColumnType("INTEGER");

                    b.Property<int>("YPosition")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EncounterId");

                    b.HasIndex("PlayerId")
                        .IsUnique();

                    b.ToTable("EncounterPlayers");
                });

            modelBuilder.Entity("Oathsworn.Entities.FreeCompany", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("FreeCompanies");
                });

            modelBuilder.Entity("Oathsworn.Entities.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Battleflow")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Defence")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Effects")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Might")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Oathsworn.Entities.MightCard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AttackId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BossAttackId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DeckId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsCritical")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDrawnFromCritical")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Value")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AttackId");

                    b.HasIndex("BossAttackId");

                    b.HasIndex("DeckId");

                    b.ToTable("MightCards");
                });

            modelBuilder.Entity("Oathsworn.Entities.Minion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Defence")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EncounterId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Health")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Might")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("XPosition")
                        .HasColumnType("INTEGER");

                    b.Property<int>("YPosition")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EncounterId");

                    b.ToTable("Minons");
                });

            modelBuilder.Entity("Oathsworn.Entities.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AnimusRegen")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Class")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Defence")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("FreeCompanyId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Health")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MaxAnimus")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Might")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FreeCompanyId");

                    b.HasIndex("UserId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Oathsworn.Entities.PlayerAbility", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Battleflow")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PlayerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayerAbilities");
                });

            modelBuilder.Entity("Oathsworn.Entities.PlayerItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Battleflow")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PlayerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayerItems");
                });

            modelBuilder.Entity("Oathsworn.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Oathsworn.Entities.Attack", b =>
                {
                    b.HasOne("Oathsworn.Entities.Boss", "Boss")
                        .WithMany()
                        .HasForeignKey("BossId");

                    b.HasOne("Oathsworn.Entities.Player", "Player")
                        .WithMany("Attacks")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Boss");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("Oathsworn.Entities.AttackMinion", b =>
                {
                    b.HasOne("Oathsworn.Entities.Attack", "Attack")
                        .WithMany("AttackMinions")
                        .HasForeignKey("AttackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Oathsworn.Entities.Minion", "Minion")
                        .WithMany()
                        .HasForeignKey("MinionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attack");

                    b.Navigation("Minion");
                });

            modelBuilder.Entity("Oathsworn.Entities.Boss", b =>
                {
                    b.HasOne("Oathsworn.Entities.Encounter", "Encounter")
                        .WithOne("Boss")
                        .HasForeignKey("Oathsworn.Entities.Boss", "EncounterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Oathsworn.Entities.EncounterPlayer", "Target")
                        .WithMany()
                        .HasForeignKey("TargetId");

                    b.Navigation("Encounter");

                    b.Navigation("Target");
                });

            modelBuilder.Entity("Oathsworn.Entities.BossAction", b =>
                {
                    b.HasOne("Oathsworn.Entities.Boss", "Boss")
                        .WithMany("BossActions")
                        .HasForeignKey("BossId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Boss");
                });

            modelBuilder.Entity("Oathsworn.Entities.BossAttack", b =>
                {
                    b.HasOne("Oathsworn.Entities.Boss", "Boss")
                        .WithMany()
                        .HasForeignKey("BossId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Boss");
                });

            modelBuilder.Entity("Oathsworn.Entities.BossAttackPlayer", b =>
                {
                    b.HasOne("Oathsworn.Entities.BossAttack", "BossAttack")
                        .WithMany("BossAttackPlayers")
                        .HasForeignKey("BossAttackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Oathsworn.Entities.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BossAttack");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("Oathsworn.Entities.EncounterMightDeck", b =>
                {
                    b.HasOne("Oathsworn.Entities.Encounter", "Encounter")
                        .WithMany("EncounterMightDecks")
                        .HasForeignKey("EncounterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Encounter");
                });

            modelBuilder.Entity("Oathsworn.Entities.EncounterPlayer", b =>
                {
                    b.HasOne("Oathsworn.Entities.Encounter", "Encounter")
                        .WithMany("EncounterPlayers")
                        .HasForeignKey("EncounterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Oathsworn.Entities.Player", "Player")
                        .WithOne("EncounterPlayer")
                        .HasForeignKey("Oathsworn.Entities.EncounterPlayer", "PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Encounter");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("Oathsworn.Entities.MightCard", b =>
                {
                    b.HasOne("Oathsworn.Entities.Attack", null)
                        .WithMany("MightCards")
                        .HasForeignKey("AttackId");

                    b.HasOne("Oathsworn.Entities.BossAttack", null)
                        .WithMany("MightCards")
                        .HasForeignKey("BossAttackId");

                    b.HasOne("Oathsworn.Entities.EncounterMightDeck", "Deck")
                        .WithMany("MightCards")
                        .HasForeignKey("DeckId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Deck");
                });

            modelBuilder.Entity("Oathsworn.Entities.Minion", b =>
                {
                    b.HasOne("Oathsworn.Entities.Encounter", "Encounter")
                        .WithMany("Minions")
                        .HasForeignKey("EncounterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Encounter");
                });

            modelBuilder.Entity("Oathsworn.Entities.Player", b =>
                {
                    b.HasOne("Oathsworn.Entities.FreeCompany", "FreeCompany")
                        .WithMany("Players")
                        .HasForeignKey("FreeCompanyId");

                    b.HasOne("Oathsworn.Entities.User", "User")
                        .WithMany("Players")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FreeCompany");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Oathsworn.Entities.PlayerAbility", b =>
                {
                    b.HasOne("Oathsworn.Entities.Player", null)
                        .WithMany("PlayerAbilities")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Oathsworn.Entities.PlayerItem", b =>
                {
                    b.HasOne("Oathsworn.Entities.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Oathsworn.Entities.Player", null)
                        .WithMany("PlayerItems")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("Oathsworn.Entities.Attack", b =>
                {
                    b.Navigation("AttackMinions");

                    b.Navigation("MightCards");
                });

            modelBuilder.Entity("Oathsworn.Entities.Boss", b =>
                {
                    b.Navigation("BossActions");
                });

            modelBuilder.Entity("Oathsworn.Entities.BossAttack", b =>
                {
                    b.Navigation("BossAttackPlayers");

                    b.Navigation("MightCards");
                });

            modelBuilder.Entity("Oathsworn.Entities.Encounter", b =>
                {
                    b.Navigation("Boss")
                        .IsRequired();

                    b.Navigation("EncounterMightDecks");

                    b.Navigation("EncounterPlayers");

                    b.Navigation("Minions");
                });

            modelBuilder.Entity("Oathsworn.Entities.EncounterMightDeck", b =>
                {
                    b.Navigation("MightCards");
                });

            modelBuilder.Entity("Oathsworn.Entities.FreeCompany", b =>
                {
                    b.Navigation("Players");
                });

            modelBuilder.Entity("Oathsworn.Entities.Player", b =>
                {
                    b.Navigation("Attacks");

                    b.Navigation("EncounterPlayer")
                        .IsRequired();

                    b.Navigation("PlayerAbilities");

                    b.Navigation("PlayerItems");
                });

            modelBuilder.Entity("Oathsworn.Entities.User", b =>
                {
                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}
