﻿// <auto-generated />
using System;
using CoreCMS.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoreCMS.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20201104080240_place")]
    partial class place
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CoreCMS.Domain.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(450)")
                        .HasMaxLength(450);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "7fd2efe6-e036-4004-9a9b-0fdd9a089d03",
                            ConcurrencyStamp = "6674b67c-f24c-42cb-a3dc-10cda31d1734",
                            Description = "ผู้ดูแลระบบ",
                            Name = "super_admin",
                            NormalizedName = "SUPER_ADMIN"
                        });
                });

            modelBuilder.Entity("CoreCMS.Domain.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("AmphurId")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Companyname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ConfirmEmailDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CreateUserDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(450)")
                        .HasMaxLength(450);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("EmployeeCode")
                        .HasColumnType("nvarchar(450)")
                        .HasMaxLength(450);

                    b.Property<string>("FamilyName")
                        .HasColumnType("nvarchar(450)")
                        .HasMaxLength(450);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(450)")
                        .HasMaxLength(450);

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(450)")
                        .HasMaxLength(450);

                    b.Property<string>("GivenName")
                        .HasColumnType("nvarchar(450)")
                        .HasMaxLength(450);

                    b.Property<string>("IdentityId")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<bool>("InActiveStatus")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastLogOnDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(450)")
                        .HasMaxLength(450);

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Nationality")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("OpenID")
                        .HasColumnType("bit");

                    b.Property<string>("PassportId")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneCountryCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(450)")
                        .HasMaxLength(450);

                    b.Property<string>("PostCode")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("ProvinceId")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("RandomCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TumbolId")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("UserType")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new
                        {
                            Id = "1312f09c-4010-429d-89cf-c6742832eec9",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "39f71b09-85fc-4e70-9e1a-6c3d7978810a",
                            Email = "inno@sbpds.co.th",
                            EmailConfirmed = true,
                            InActiveStatus = false,
                            IsDeleted = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "INNO@SBPDS.CO.TH",
                            NormalizedUserName = "ADMINISTRATOR",
                            OpenID = false,
                            PasswordHash = "AQAAAAEAACcQAAAAELqJzeqdY8QSZjiTVnYjNpWaJ3K8uDWP78eWgXMlza29tJURs6BsClQd8vRpN4IAYQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "",
                            TwoFactorEnabled = false,
                            UserName = "administrator"
                        });
                });

            modelBuilder.Entity("CoreCMS.Domain.ImplementationDate", b =>
                {
                    b.Property<int>("ImplementationDateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EndDay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EndTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("InActiveStatus")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("PlaceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("StartDay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StartTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedUserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ImplementationDateId");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("DeletedUserId");

                    b.HasIndex("PlaceId");

                    b.HasIndex("UpdatedUserId");

                    b.ToTable("ImplementationDate");
                });

            modelBuilder.Entity("CoreCMS.Domain.Picture", b =>
                {
                    b.Property<int>("PictureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CodeRef")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DownloadUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("FileInfoId")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("GridFsId")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<bool>("InActiveStatus")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("SysName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("TypeRef")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedUserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PictureId");

                    b.HasIndex("CodeRef");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("DeletedUserId");

                    b.HasIndex("InActiveStatus");

                    b.HasIndex("PictureId");

                    b.HasIndex("TypeRef");

                    b.HasIndex("UpdatedUserId");

                    b.ToTable("Picture");
                });

            modelBuilder.Entity("CoreCMS.Domain.Place", b =>
                {
                    b.Property<Guid>("PlaceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("AMPER_ID")
                        .HasColumnType("int")
                        .HasMaxLength(10);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("InActiveStatus")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<float>("Latitude")
                        .HasColumnType("real");

                    b.Property<float>("Longitude")
                        .HasColumnType("real");

                    b.Property<int?>("PROVINCE_ID")
                        .HasColumnType("int")
                        .HasMaxLength(10);

                    b.Property<string>("PlaceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasMaxLength(450);

                    b.Property<int>("PlaceTypeID")
                        .HasColumnType("int");

                    b.Property<int?>("TAMBON_ID")
                        .HasColumnType("int")
                        .HasMaxLength(10);

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ZIP_CODE")
                        .HasColumnType("nvarchar(5)")
                        .HasMaxLength(5);

                    b.HasKey("PlaceId");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("DeletedUserId");

                    b.HasIndex("PlaceTypeID");

                    b.HasIndex("UpdatedUserId");

                    b.ToTable("Place");
                });

            modelBuilder.Entity("CoreCMS.Domain.PlaceType", b =>
                {
                    b.Property<int>("PlaceTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("InActiveStatus")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("PlaceTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedUserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PlaceTypeID");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("DeletedUserId");

                    b.HasIndex("UpdatedUserId");

                    b.ToTable("PlaceType");
                });

            modelBuilder.Entity("CoreCMS.Domain.RoomType", b =>
                {
                    b.Property<int>("RoomTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("InActiveStatus")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("RoomTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedUserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("RoomTypeID");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("DeletedUserId");

                    b.HasIndex("UpdatedUserId");

                    b.ToTable("RoomType");
                });

            modelBuilder.Entity("CoreCMS.Domain.SystemVariable", b =>
                {
                    b.Property<string>("SystemVariableCode")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int?>("Ordering")
                        .HasColumnType("int");

                    b.Property<string>("SystemVariableCategoryCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SystemVariableName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasMaxLength(450);

                    b.HasKey("SystemVariableCode");

                    b.HasIndex("SystemVariableCategoryCode");

                    b.ToTable("SystemVariables");

                    b.HasData(
                        new
                        {
                            SystemVariableCode = "MEMBER_CLASS_NORMAL",
                            Ordering = 1,
                            SystemVariableCategoryCode = "MEMBER_CLASS",
                            SystemVariableName = "สมาชิกทั่วไป"
                        },
                        new
                        {
                            SystemVariableCode = "MEMBER_CLASS_GOLD",
                            Ordering = 2,
                            SystemVariableCategoryCode = "MEMBER_CLASS",
                            SystemVariableName = "สมาชิกบัตรทอง"
                        });
                });

            modelBuilder.Entity("CoreCMS.Domain.SystemVariableCategory", b =>
                {
                    b.Property<string>("SystemVariableCategoryCode")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("SystemVariableCategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasMaxLength(450);

                    b.HasKey("SystemVariableCategoryCode");

                    b.ToTable("SystemVariableCategories");

                    b.HasData(
                        new
                        {
                            SystemVariableCategoryCode = "MEMBER_CLASS",
                            SystemVariableCategoryName = "ระดับบัตรสมาชิก"
                        });
                });

            modelBuilder.Entity("CoreCMS.Domain.UserSearchView", b =>
                {
                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreateUserDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("InActiveStatus")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastLogOnDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("OpenID")
                        .HasColumnType("bit");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Roles")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("UserSearchView");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");

                    b.HasData(
                        new
                        {
                            UserId = "1312f09c-4010-429d-89cf-c6742832eec9",
                            RoleId = "7fd2efe6-e036-4004-9a9b-0fdd9a089d03"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("CoreCMS.Domain.ImplementationDate", b =>
                {
                    b.HasOne("CoreCMS.Domain.ApplicationUser", "CreatedUser")
                        .WithMany()
                        .HasForeignKey("CreatedUserId");

                    b.HasOne("CoreCMS.Domain.ApplicationUser", "DeletedUser")
                        .WithMany()
                        .HasForeignKey("DeletedUserId");

                    b.HasOne("CoreCMS.Domain.Place", "Place")
                        .WithMany("ImplementationDate")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoreCMS.Domain.ApplicationUser", "UpdatedUser")
                        .WithMany()
                        .HasForeignKey("UpdatedUserId");
                });

            modelBuilder.Entity("CoreCMS.Domain.Picture", b =>
                {
                    b.HasOne("CoreCMS.Domain.ApplicationUser", "CreatedUser")
                        .WithMany()
                        .HasForeignKey("CreatedUserId");

                    b.HasOne("CoreCMS.Domain.ApplicationUser", "DeletedUser")
                        .WithMany()
                        .HasForeignKey("DeletedUserId");

                    b.HasOne("CoreCMS.Domain.ApplicationUser", "UpdatedUser")
                        .WithMany()
                        .HasForeignKey("UpdatedUserId");
                });

            modelBuilder.Entity("CoreCMS.Domain.Place", b =>
                {
                    b.HasOne("CoreCMS.Domain.ApplicationUser", "CreatedUser")
                        .WithMany()
                        .HasForeignKey("CreatedUserId");

                    b.HasOne("CoreCMS.Domain.ApplicationUser", "DeletedUser")
                        .WithMany()
                        .HasForeignKey("DeletedUserId");

                    b.HasOne("CoreCMS.Domain.PlaceType", "PlaceType")
                        .WithMany("Place")
                        .HasForeignKey("PlaceTypeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoreCMS.Domain.ApplicationUser", "UpdatedUser")
                        .WithMany()
                        .HasForeignKey("UpdatedUserId");
                });

            modelBuilder.Entity("CoreCMS.Domain.PlaceType", b =>
                {
                    b.HasOne("CoreCMS.Domain.ApplicationUser", "CreatedUser")
                        .WithMany()
                        .HasForeignKey("CreatedUserId");

                    b.HasOne("CoreCMS.Domain.ApplicationUser", "DeletedUser")
                        .WithMany()
                        .HasForeignKey("DeletedUserId");

                    b.HasOne("CoreCMS.Domain.ApplicationUser", "UpdatedUser")
                        .WithMany()
                        .HasForeignKey("UpdatedUserId");
                });

            modelBuilder.Entity("CoreCMS.Domain.RoomType", b =>
                {
                    b.HasOne("CoreCMS.Domain.ApplicationUser", "CreatedUser")
                        .WithMany()
                        .HasForeignKey("CreatedUserId");

                    b.HasOne("CoreCMS.Domain.ApplicationUser", "DeletedUser")
                        .WithMany()
                        .HasForeignKey("DeletedUserId");

                    b.HasOne("CoreCMS.Domain.ApplicationUser", "UpdatedUser")
                        .WithMany()
                        .HasForeignKey("UpdatedUserId");
                });

            modelBuilder.Entity("CoreCMS.Domain.SystemVariable", b =>
                {
                    b.HasOne("CoreCMS.Domain.SystemVariableCategory", "SystemVariableCategory")
                        .WithMany()
                        .HasForeignKey("SystemVariableCategoryCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("CoreCMS.Domain.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("CoreCMS.Domain.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("CoreCMS.Domain.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("CoreCMS.Domain.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoreCMS.Domain.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("CoreCMS.Domain.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
