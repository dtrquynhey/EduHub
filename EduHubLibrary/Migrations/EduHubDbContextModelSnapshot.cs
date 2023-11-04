﻿// <auto-generated />
using System;
using EduHubLibrary.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EduHubLibrary.Migrations
{
    [DbContext(typeof(EduHubDbContext))]
    partial class EduHubDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EduHubLibrary.DataModels.Campaign", b =>
                {
                    b.Property<int>("CampaignId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CampaignId"));

                    b.Property<int>("CampaignType")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CampaignId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Campaigns");
                });

            modelBuilder.Entity("EduHubLibrary.DataModels.CampaignMember", b =>
                {
                    b.Property<int>("CampaignMemberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CampaignMemberId"));

                    b.Property<int>("CampaignId")
                        .HasColumnType("int");

                    b.Property<DateTime>("JoinDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("CampaignMemberId");

                    b.HasIndex("CampaignId");

                    b.HasIndex("MemberId");

                    b.ToTable("CampaignMembers");
                });

            modelBuilder.Entity("EduHubLibrary.DataModels.Engagement", b =>
                {
                    b.Property<int>("EngagementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EngagementId"));

                    b.Property<int>("CampaignId")
                        .HasColumnType("int");

                    b.Property<int>("CommentsCount")
                        .HasColumnType("int");

                    b.Property<int>("LikesCount")
                        .HasColumnType("int");

                    b.Property<int>("ViewsCount")
                        .HasColumnType("int");

                    b.HasKey("EngagementId");

                    b.HasIndex("CampaignId")
                        .IsUnique();

                    b.ToTable("Engagements");
                });

            modelBuilder.Entity("EduHubLibrary.DataModels.Interaction", b =>
                {
                    b.Property<int>("InteractionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InteractionId"));

                    b.Property<int>("CampaignId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("InteractionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("InteractionType")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("InteractionId");

                    b.HasIndex("CampaignId");

                    b.HasIndex("UserId");

                    b.ToTable("Interactions");
                });

            modelBuilder.Entity("EduHubLibrary.DataModels.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EduHubLibrary.DataModels.Campaign", b =>
                {
                    b.HasOne("EduHubLibrary.DataModels.User", "User")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EduHubLibrary.DataModels.CampaignMember", b =>
                {
                    b.HasOne("EduHubLibrary.DataModels.Campaign", "Campaign")
                        .WithMany("Members")
                        .HasForeignKey("CampaignId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EduHubLibrary.DataModels.User", "user")
                        .WithMany("Campaigns")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Campaign");

                    b.Navigation("user");
                });

            modelBuilder.Entity("EduHubLibrary.DataModels.Engagement", b =>
                {
                    b.HasOne("EduHubLibrary.DataModels.Campaign", "Campaign")
                        .WithOne("Engagement")
                        .HasForeignKey("EduHubLibrary.DataModels.Engagement", "CampaignId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Campaign");
                });

            modelBuilder.Entity("EduHubLibrary.DataModels.Interaction", b =>
                {
                    b.HasOne("EduHubLibrary.DataModels.Campaign", "Campaign")
                        .WithMany("Interactions")
                        .HasForeignKey("CampaignId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EduHubLibrary.DataModels.User", "User")
                        .WithMany("UserInteractions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Campaign");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EduHubLibrary.DataModels.Campaign", b =>
                {
                    b.Navigation("Engagement")
                        .IsRequired();

                    b.Navigation("Interactions");

                    b.Navigation("Members");
                });

            modelBuilder.Entity("EduHubLibrary.DataModels.User", b =>
                {
                    b.Navigation("Campaigns");

                    b.Navigation("UserInteractions");
                });
#pragma warning restore 612, 618
        }
    }
}
