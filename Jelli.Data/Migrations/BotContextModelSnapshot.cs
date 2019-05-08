﻿// <auto-generated />
using System;
using Jelli.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Jelli.Data.Migrations
{
    [DbContext(typeof(BotContext))]
    partial class BotContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("Jelli.Data.Models.AliasCommand", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AliasTo");

                    b.Property<string>("Command");

                    b.Property<ulong>("GuildId");

                    b.HasKey("Id");

                    b.HasIndex("GuildId");

                    b.ToTable("AliasCommands");
                });

            modelBuilder.Entity("Jelli.Data.Models.ChannelEnforcement", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<ulong>("ChannelId");

                    b.Property<ulong>("GuildId");

                    b.Property<int?>("MinimumCharacters");

                    b.Property<int?>("MinimumDiscordAgeDays");

                    b.Property<int?>("MinimumGuildJoinedAgeDays");

                    b.Property<bool?>("RequirePictures");

                    b.Property<bool?>("RequireText");

                    b.Property<bool?>("RestrictPictures");

                    b.Property<bool?>("RestrictText");

                    b.HasKey("Id");

                    b.HasIndex("GuildId");

                    b.ToTable("ChannelEnforcements");
                });

            modelBuilder.Entity("Jelli.Data.Models.Guild", b =>
                {
                    b.Property<ulong>("GuildId");

                    b.Property<string>("CommandPrefix");

                    b.HasKey("GuildId");

                    b.ToTable("Guilds");
                });

            modelBuilder.Entity("Jelli.Data.Models.GuildCustomCommand", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Command");

                    b.Property<ulong>("GuildId");

                    b.Property<string>("Response");

                    b.HasKey("Id");

                    b.HasIndex("GuildId");

                    b.ToTable("GuildCustomCommands");
                });

            modelBuilder.Entity("Jelli.Data.Models.GuildRole", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<ulong>("GuildId");

                    b.Property<string>("RoleDisplayName");

                    b.Property<ulong>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("GuildId");

                    b.ToTable("GuildRoles");
                });

            modelBuilder.Entity("Jelli.Data.Models.GuildUserNote", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("Created");

                    b.Property<ulong>("GuildId");

                    b.Property<ulong>("SubmitterId");

                    b.Property<ulong>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("GuildId");

                    b.ToTable("GuildUserNotes");
                });

            modelBuilder.Entity("Jelli.Data.Models.AliasCommand", b =>
                {
                    b.HasOne("Jelli.Data.Models.Guild", "Guild")
                        .WithMany()
                        .HasForeignKey("GuildId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Jelli.Data.Models.ChannelEnforcement", b =>
                {
                    b.HasOne("Jelli.Data.Models.Guild", "Guild")
                        .WithMany()
                        .HasForeignKey("GuildId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Jelli.Data.Models.GuildCustomCommand", b =>
                {
                    b.HasOne("Jelli.Data.Models.Guild", "Guild")
                        .WithMany("GuildCustomCommands")
                        .HasForeignKey("GuildId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Jelli.Data.Models.GuildRole", b =>
                {
                    b.HasOne("Jelli.Data.Models.Guild", "Guild")
                        .WithMany("GuildRoles")
                        .HasForeignKey("GuildId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Jelli.Data.Models.GuildUserNote", b =>
                {
                    b.HasOne("Jelli.Data.Models.Guild", "Guild")
                        .WithMany("GuildUserNotes")
                        .HasForeignKey("GuildId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
