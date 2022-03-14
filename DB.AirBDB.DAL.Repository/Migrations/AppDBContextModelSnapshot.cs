﻿// <auto-generated />
using System;
using DB.AirBDB.DAL.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DB.AirBDB.DAL.Repository.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DB.AirBDB.DAL.Repository.DatabaseEntity.Lugar", b =>
                {
                    b.Property<int>("LugarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("StatusLocacao")
                        .HasColumnType("int");

                    b.Property<string>("TipoDeAcomodacao")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<double>("Valor")
                        .HasColumnType("float");

                    b.HasKey("LugarId");

                    b.ToTable("Lugares");

                    b.HasData(
                        new
                        {
                            LugarId = 1,
                            Ativo = true,
                            Cidade = "Tramandaí",
                            Descricao = "Casa a 3 quadras do mar",
                            StatusLocacao = 0,
                            TipoDeAcomodacao = "Casa",
                            Valor = 250.0
                        },
                        new
                        {
                            LugarId = 2,
                            Ativo = true,
                            Cidade = "Porto Alegre",
                            Descricao = "Apartamento no bairro Cidade Baixa",
                            StatusLocacao = 0,
                            TipoDeAcomodacao = "Apartamento",
                            Valor = 500.0
                        });
                });

            modelBuilder.Entity("DB.AirBDB.DAL.Repository.DatabaseEntity.Reserva", b =>
                {
                    b.Property<int>("ReservaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataFim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("datetime2");

                    b.Property<int>("LugarId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("ReservaId");

                    b.HasIndex("LugarId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Reservas");

                    b.HasData(
                        new
                        {
                            ReservaId = 1,
                            DataFim = new DateTime(2022, 1, 10, 23, 59, 59, 0, DateTimeKind.Unspecified),
                            DataInicio = new DateTime(2022, 1, 1, 0, 0, 1, 0, DateTimeKind.Unspecified),
                            LugarId = 1,
                            UsuarioId = 1
                        });
                });

            modelBuilder.Entity("DB.AirBDB.DAL.Repository.DatabaseEntity.Usuario", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuarios");

                    b.HasData(
                        new
                        {
                            UsuarioId = 1,
                            Login = "caio_martins",
                            Nome = "Caio César Martins",
                            Senha = "P4$$W0RDseguro"
                        },
                        new
                        {
                            UsuarioId = 2,
                            Login = "alisson_pazze",
                            Nome = "Alisson dos Santos Pazze",
                            Senha = "P4$$W0RDmaisS3GUR0"
                        });
                });

            modelBuilder.Entity("DB.AirBDB.DAL.Repository.DatabaseEntity.Reserva", b =>
                {
                    b.HasOne("DB.AirBDB.DAL.Repository.DatabaseEntity.Lugar", "Lugar")
                        .WithMany("ListaReservas")
                        .HasForeignKey("LugarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DB.AirBDB.DAL.Repository.DatabaseEntity.Usuario", "Usuario")
                        .WithMany("listaReservas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lugar");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("DB.AirBDB.DAL.Repository.DatabaseEntity.Lugar", b =>
                {
                    b.Navigation("ListaReservas");
                });

            modelBuilder.Entity("DB.AirBDB.DAL.Repository.DatabaseEntity.Usuario", b =>
                {
                    b.Navigation("listaReservas");
                });
#pragma warning restore 612, 618
        }
    }
}
