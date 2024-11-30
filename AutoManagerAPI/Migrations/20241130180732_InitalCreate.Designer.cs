﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AutoManagerAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241130180732_InitalCreate")]
    partial class InitalCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("AutoManagerAPI.Models.Car", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Color")
                        .HasColumnType("TEXT");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Plate")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Year")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Cars");

                    b.HasData(
                        new
                        {
                            Id = "c1",
                            Brand = "Toyota",
                            ClientId = "bfe4e7dc-81e4-4e47-a67b-d4fbf3e124bd",
                            Color = "Preto",
                            Model = "Corolla",
                            Plate = "ABC1234",
                            Year = "2020"
                        },
                        new
                        {
                            Id = "c2",
                            Brand = "Honda",
                            ClientId = "6d091456-5a2f-4b5a-98fc-f1a3b50a627d",
                            Color = "Prata",
                            Model = "Civic",
                            Plate = "DEF5678",
                            Year = "2019"
                        },
                        new
                        {
                            Id = "c3",
                            Brand = "Ford",
                            ClientId = "39be53a2-fc09-4b6a-bafa-18a6a23c8f6e",
                            Color = "Branco",
                            Model = "Fiesta",
                            Plate = "GHI9012",
                            Year = "2018"
                        });
                });

            modelBuilder.Entity("AutoManagerAPI.Models.CarService", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("CarServices");

                    b.HasData(
                        new
                        {
                            Id = "cs1",
                            Category = "Manutenção",
                            Description = "Substituição de óleo do motor e filtro.",
                            Name = "Troca de Óleo",
                            Price = 150.0
                        },
                        new
                        {
                            Id = "cs2",
                            Category = "Pneus",
                            Description = "Serviço para ajuste das rodas e equilíbrio.",
                            Name = "Alinhamento e Balanceamento",
                            Price = 120.0
                        });
                });

            modelBuilder.Entity("AutoManagerAPI.Models.Client", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Bairro")
                        .HasColumnType("TEXT");

                    b.Property<string>("Cep")
                        .HasColumnType("TEXT");

                    b.Property<string>("Cidade")
                        .HasColumnType("TEXT");

                    b.Property<string>("Cpf")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Estado")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Logradouro")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Clients");

                    b.HasData(
                        new
                        {
                            Id = "63e9fcb4-7b6f-4ac7-b3ec-720b315ed38c",
                            Bairro = "Portao",
                            Cep = "82930230",
                            Cidade = "Curitiba",
                            Cpf = "89126811105",
                            Email = "admin@oficina.up",
                            Estado = "PR",
                            IsAdmin = true,
                            Logradouro = "Rua Jo",
                            Name = "Admin",
                            Phone = "41991022117",
                            Senha = "admin123"
                        },
                        new
                        {
                            Id = "bfe4e7dc-81e4-4e47-a67b-d4fbf3e124bd",
                            Bairro = "Centro",
                            Cep = "80000000",
                            Cidade = "Curitiba",
                            Cpf = "12345678900",
                            Email = "felipe@gmail.com",
                            Estado = "PR",
                            IsAdmin = false,
                            Logradouro = "Rua das Flores, 123",
                            Name = "Felipe",
                            Phone = "41999999999",
                            Senha = "senha123"
                        },
                        new
                        {
                            Id = "6d091456-5a2f-4b5a-98fc-f1a3b50a627d",
                            Bairro = "Batel",
                            Cep = "81000000",
                            Cidade = "Curitiba",
                            Cpf = "98765432100",
                            Email = "icaro@gmail.com",
                            Estado = "PR",
                            IsAdmin = false,
                            Logradouro = "Av. Vicente Machado, 456",
                            Name = "Icaro",
                            Phone = "41988888888",
                            Senha = "senha456"
                        },
                        new
                        {
                            Id = "39be53a2-fc09-4b6a-bafa-18a6a23c8f6e",
                            Bairro = "Santa Felicidade",
                            Cep = "82000000",
                            Cidade = "Curitiba",
                            Cpf = "11223344556",
                            Email = "rafael@gmail.com",
                            Estado = "PR",
                            IsAdmin = false,
                            Logradouro = "Rua Itália, 789",
                            Name = "Rafael",
                            Phone = "41977777777",
                            Senha = "senha789"
                        });
                });

            modelBuilder.Entity("AutoManagerAPI.Models.Order", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("CarId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CarServiceId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("CarServiceId");

                    b.HasIndex("ClientId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = "o1",
                            CarId = "c1",
                            CarServiceId = "cs1",
                            ClientId = "bfe4e7dc-81e4-4e47-a67b-d4fbf3e124bd",
                            CreatedDate = new DateTime(2024, 11, 30, 15, 7, 31, 991, DateTimeKind.Local).AddTicks(9530),
                            Description = "Serviço de troca de óleo para o Corolla do Felipe.",
                            Name = "Troca de óleo para Corolla",
                            Status = true,
                            TotalPrice = 150.0
                        },
                        new
                        {
                            Id = "o2",
                            CarId = "c2",
                            CarServiceId = "cs2",
                            ClientId = "63e9fcb4-7b6f-4ac7-b3ec-720b315ed38c",
                            CreatedDate = new DateTime(2024, 11, 30, 15, 7, 31, 991, DateTimeKind.Local).AddTicks(9534),
                            Description = "Alinhamento e balanceamento do Civic do Admin.",
                            Name = "Alinhamento para Civic",
                            Status = false,
                            TotalPrice = 120.0
                        });
                });

            modelBuilder.Entity("AutoManagerAPI.Models.Car", b =>
                {
                    b.HasOne("AutoManagerAPI.Models.Client", "Client")
                        .WithMany("Cars")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("AutoManagerAPI.Models.Order", b =>
                {
                    b.HasOne("AutoManagerAPI.Models.Car", "Car")
                        .WithMany("OrdersHistoric")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AutoManagerAPI.Models.CarService", "CarService")
                        .WithMany()
                        .HasForeignKey("CarServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AutoManagerAPI.Models.Client", "Client")
                        .WithMany("Orders")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("CarService");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("AutoManagerAPI.Models.Car", b =>
                {
                    b.Navigation("OrdersHistoric");
                });

            modelBuilder.Entity("AutoManagerAPI.Models.Client", b =>
                {
                    b.Navigation("Cars");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}