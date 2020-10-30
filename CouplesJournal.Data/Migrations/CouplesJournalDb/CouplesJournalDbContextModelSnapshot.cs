﻿// <auto-generated />
using System;
using CouplesJournal.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CouplesJournal.Migrations.CouplesJournalDb
{
    [DbContext(typeof(CouplesJournalDbContext))]
    partial class CouplesJournalDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0-rc.2.20475.6");

            modelBuilder.Entity("CouplesJournal.Data.Entities.EmailNotification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Body")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("From")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Processed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Subject")
                        .HasColumnType("TEXT");

                    b.Property<string>("To")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("EmailNotifications");
                });

            modelBuilder.Entity("CouplesJournal.Data.Entities.JournalEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<int?>("JournalStatusId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("MarkedForDeletion")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("TEXT");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasMaxLength(450)
                        .HasColumnType("TEXT");

                    b.Property<int?>("fk_journalstatus")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("JournalStatusId");

                    b.HasIndex("fk_journalstatus");

                    b.ToTable("JournalEntries");
                });

            modelBuilder.Entity("CouplesJournal.Data.Entities.JournalReply", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("JournalEntryId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("MarkedForDeletion")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasMaxLength(450)
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("fk_journalentry")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("JournalEntryId");

                    b.HasIndex("fk_journalentry");

                    b.ToTable("JournalReplies");
                });

            modelBuilder.Entity("CouplesJournal.Data.Entities.JournalStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("JournalStatuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Value = "Draft"
                        },
                        new
                        {
                            Id = 2,
                            Value = "Final"
                        });
                });

            modelBuilder.Entity("CouplesJournal.Data.Entities.JournalEntry", b =>
                {
                    b.HasOne("CouplesJournal.Data.Entities.JournalStatus", null)
                        .WithMany()
                        .HasForeignKey("JournalStatusId");

                    b.HasOne("CouplesJournal.Data.Entities.JournalStatus", "Status")
                        .WithMany()
                        .HasForeignKey("fk_journalstatus");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("CouplesJournal.Data.Entities.JournalReply", b =>
                {
                    b.HasOne("CouplesJournal.Data.Entities.JournalEntry", null)
                        .WithMany()
                        .HasForeignKey("JournalEntryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CouplesJournal.Data.Entities.JournalEntry", null)
                        .WithMany("Replies")
                        .HasForeignKey("fk_journalentry");
                });

            modelBuilder.Entity("CouplesJournal.Data.Entities.JournalEntry", b =>
                {
                    b.Navigation("Replies");
                });
#pragma warning restore 612, 618
        }
    }
}
