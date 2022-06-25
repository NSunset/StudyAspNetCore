﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nw.LiveBackgroundManagement.DataAccessEFCore;

namespace Nw.LiveBackgroundManagement.DataAccessEFCore.Migrations
{
    [DbContext(typeof(AuthorityDbContext))]
    [Migration("20220601095218_2022-6-1")]
    partial class _202261
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.17");

            modelBuilder.Entity("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.CSComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CSCommentType")
                        .HasColumnType("int");

                    b.Property<int>("CSWorksId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<int>("FromUserId")
                        .HasColumnType("int");

                    b.Property<string>("FromUserName")
                        .HasColumnType("longtext");

                    b.Property<string>("ImgUrl")
                        .HasColumnType("longtext");

                    b.Property<int?>("LastModifierId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifyTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Text")
                        .HasColumnType("longtext");

                    b.Property<int>("ToUserId")
                        .HasColumnType("int");

                    b.Property<string>("ToUserName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CSWorksId");

                    b.ToTable("CSComment");
                });

            modelBuilder.Entity("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.CSCommentReply", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CommentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<int>("FromUserId")
                        .HasColumnType("int");

                    b.Property<string>("FromUserName")
                        .HasColumnType("longtext");

                    b.Property<string>("ImgUrl")
                        .HasColumnType("longtext");

                    b.Property<int?>("LastModifierId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifyTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Text")
                        .HasColumnType("longtext");

                    b.Property<int>("ToUserId")
                        .HasColumnType("int");

                    b.Property<string>("ToUserName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("CSCommentReply");
                });

            modelBuilder.Entity("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.CSFollow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AnchorId")
                        .HasColumnType("int");

                    b.Property<int>("CollAttentionType")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<int?>("LastModifierId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifyTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CSFollow");
                });

            modelBuilder.Entity("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.CSIntegralRecharge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("AmountCount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<long>("IntegralCount")
                        .HasColumnType("bigint");

                    b.Property<int?>("LastModifierId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifyTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("UserIntgralCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CSIntegralRecharge");
                });

            modelBuilder.Entity("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.CSIntegralRechargeDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<int?>("LastModifierId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifyTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("OrderNum")
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CSIntegralRechargeDetail");
                });

            modelBuilder.Entity("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.CSRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CSRoomTypeId")
                        .HasColumnType("int");

                    b.Property<int>("CreateId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<int?>("LastModifyId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifyTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("RoomImgUrl")
                        .HasColumnType("longtext");

                    b.Property<string>("RoomName")
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CSRoomTypeId")
                        .IsUnique();

                    b.ToTable("CSRoom");
                });

            modelBuilder.Entity("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.CSRoomType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Text")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("CSRoomType");
                });

            modelBuilder.Entity("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.CSScoreList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CSWorksId")
                        .HasColumnType("int");

                    b.Property<int>("CreateId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal>("Integral")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("LastModifyId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifyTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CSWorksId");

                    b.ToTable("CSScoreList");
                });

            modelBuilder.Entity("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.CSUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasMaxLength(400)
                        .HasColumnType("varchar(400)");

                    b.Property<int?>("ApplysState")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("CurrentUserRoomId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ImgUrl")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("LastLoginTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("LastModifyId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifyTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Mobile")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<long?>("QQ")
                        .HasColumnType("bigint");

                    b.Property<int?>("RoomId")
                        .HasColumnType("int");

                    b.Property<byte?>("Sex")
                        .HasColumnType("tinyint unsigned");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint unsigned");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.Property<string>("WeChat")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CurrentUserRoomId");

                    b.ToTable("CSUser");
                });

            modelBuilder.Entity("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.CSUserApply", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ApprovalMsg")
                        .HasColumnType("longtext");

                    b.Property<int>("CSUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("LastModifyId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifyTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Remark")
                        .HasColumnType("longtext");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CSUserId");

                    b.ToTable("CSUserApply");
                });

            modelBuilder.Entity("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.CSWorks", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("LastModifierId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifyTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("StarTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("WorkName")
                        .HasColumnType("longtext");

                    b.Property<int>("WorkType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CSWorks");
                });

            modelBuilder.Entity("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.Hangfire.HangfireUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Pwd")
                        .HasColumnType("longtext");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("HangfireUser");
                });

            modelBuilder.Entity("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.SysLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<string>("Detail")
                        .HasMaxLength(4000)
                        .HasColumnType("varchar(4000)");

                    b.Property<string>("Introduction")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.Property<int?>("LastModifierId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifyTime")
                        .HasColumnType("datetime(6)");

                    b.Property<byte>("LogType")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.HasKey("Id");

                    b.ToTable("SysLog");
                });

            modelBuilder.Entity("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.SysMenu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int?>("LastModifierId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifyTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("MenuIcon")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<byte>("MenuLevel")
                        .HasColumnType("tinyint unsigned");

                    b.Property<byte>("MenuType")
                        .HasColumnType("tinyint unsigned");

                    b.Property<int>("ParentId")
                        .HasColumnType("int");

                    b.Property<int>("Sort")
                        .HasColumnType("int");

                    b.Property<string>("SourcePath")
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Url")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.HasKey("Id");

                    b.ToTable("SysMenu");
                });

            modelBuilder.Entity("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.SysRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CreateId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.Property<int?>("LastModifierId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifyTime")
                        .HasColumnType("datetime(6)");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("varchar(36)");

                    b.HasKey("Id");

                    b.ToTable("SysRole");
                });

            modelBuilder.Entity("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.SysRoleMenuMapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("SysMenuId")
                        .HasColumnType("int");

                    b.Property<int>("SysRoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SysMenuId");

                    b.HasIndex("SysRoleId");

                    b.ToTable("SysRoleMenuMapping");
                });

            modelBuilder.Entity("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.SysUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<int>("CreateId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("LastLoginTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("LastModifyId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifyTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Mobile")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<long?>("QQ")
                        .HasColumnType("bigint");

                    b.Property<byte?>("Sex")
                        .HasColumnType("tinyint unsigned");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("WeChat")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("SysUser");
                });

            modelBuilder.Entity("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.SysUserMenuMapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("SysMenuId")
                        .HasColumnType("int");

                    b.Property<int>("SysUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SysMenuId");

                    b.HasIndex("SysUserId");

                    b.ToTable("SysUserMenuMapping");
                });

            modelBuilder.Entity("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.SysUserRoleMapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("SysRoleId")
                        .HasColumnType("int");

                    b.Property<int>("SysUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SysRoleId");

                    b.HasIndex("SysUserId");

                    b.ToTable("SysUserRoleMapping");
                });

            modelBuilder.Entity("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.CSComment", b =>
                {
                    b.HasOne("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.CSWorks", null)
                        .WithMany("CSComment")
                        .HasForeignKey("CSWorksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.CSRoom", b =>
                {
                    b.HasOne("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.CSRoomType", "CSRoomType")
                        .WithOne("CSRoom")
                        .HasForeignKey("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.CSRoom", "CSRoomTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CSRoomType");
                });

            modelBuilder.Entity("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.CSScoreList", b =>
                {
                    b.HasOne("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.CSWorks", null)
                        .WithMany("CSScoreList")
                        .HasForeignKey("CSWorksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.CSUser", b =>
                {
                    b.HasOne("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.CSRoom", "CurrentUserRoom")
                        .WithMany()
                        .HasForeignKey("CurrentUserRoomId");

                    b.Navigation("CurrentUserRoom");
                });

            modelBuilder.Entity("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.CSUserApply", b =>
                {
                    b.HasOne("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.CSUser", null)
                        .WithMany("CSUserApply")
                        .HasForeignKey("CSUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.SysRoleMenuMapping", b =>
                {
                    b.HasOne("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.SysMenu", "SysMenu")
                        .WithMany()
                        .HasForeignKey("SysMenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.SysRole", null)
                        .WithMany("SysRoleMenuMapping")
                        .HasForeignKey("SysRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SysMenu");
                });

            modelBuilder.Entity("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.SysUserMenuMapping", b =>
                {
                    b.HasOne("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.SysMenu", "SysMenu")
                        .WithMany()
                        .HasForeignKey("SysMenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.SysUser", null)
                        .WithMany("SysUserMenuMapping")
                        .HasForeignKey("SysUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SysMenu");
                });

            modelBuilder.Entity("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.SysUserRoleMapping", b =>
                {
                    b.HasOne("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.SysRole", "SysRole")
                        .WithMany("SysUserRoleMapping")
                        .HasForeignKey("SysRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.SysUser", null)
                        .WithMany("SysUserRoleMapping")
                        .HasForeignKey("SysUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SysRole");
                });

            modelBuilder.Entity("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.CSRoomType", b =>
                {
                    b.Navigation("CSRoom");
                });

            modelBuilder.Entity("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.CSUser", b =>
                {
                    b.Navigation("CSUserApply");
                });

            modelBuilder.Entity("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.CSWorks", b =>
                {
                    b.Navigation("CSComment");

                    b.Navigation("CSScoreList");
                });

            modelBuilder.Entity("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.SysRole", b =>
                {
                    b.Navigation("SysRoleMenuMapping");

                    b.Navigation("SysUserRoleMapping");
                });

            modelBuilder.Entity("Nw.LiveBackgroundManagement.DataAccessEFCore.Models.SysUser", b =>
                {
                    b.Navigation("SysUserMenuMapping");

                    b.Navigation("SysUserRoleMapping");
                });
#pragma warning restore 612, 618
        }
    }
}
