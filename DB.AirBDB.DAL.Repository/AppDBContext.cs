using DB.AirBDB.DAL.Repository.DatabaseEntity;
using Microsoft.EntityFrameworkCore;
using System;

namespace DB.AirBDB.DAL.Repository
{
    public class AppDBContext : DbContext
    {
        #region CONFIGURATIONS
        public AppDBContext()
        {
        }
        public AppDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=AirBDB-Database");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seedando as tabelas
            SeedEntity(UserSeed());
            SeedEntity(PlaceSeed());
            SeedEntity(BookingSeed());

            // Métodos auxiliares via template + internal methods
            void SeedEntity<T>(T[] seedData) where T : class
                => modelBuilder.Entity<T>().HasData(seedData);

            Usuario[] UserSeed()
            {
                Usuario[] usersSeed = new Usuario[]
                {
                    new Usuario
                    {
                        UsuarioId = 1,
                        Login = "caio_martins",
                        Nome = "Caio César Martins",
                        Senha = "P4$$W0RDseguro"
                    },
                    new Usuario
                    {
                        UsuarioId = 2,
                        Login = "alisson_pazze",
                        Nome = "Alisson dos Santos Pazze",
                        Senha = "P4$$W0RDmaisS3GUR0"
                    }
                };

                return usersSeed;
            }

            Lugar[] PlaceSeed()
            {
                Lugar[] placesSeed = new Lugar[]
                {
                    new Lugar
                    {
                        LugarId = 1,
                        Descricao = "Casa a 3 quadras do mar",
                        TipoDeAcomodacao = "Casa",
                        Cidade = "Tramandaí",
                        Valor = 250
                    },
                    new Lugar
                    {
                        LugarId = 2,
                        Descricao = "Apartamento no bairro Cidade Baixa",
                        TipoDeAcomodacao = "Apartamento",
                        Cidade = "Porto Alegre",
                        Valor = 500
                    }
                };

                return placesSeed;
            }

            Reserva[] BookingSeed()
            {
                Reserva[] bookingsSeed = new Reserva[]
                {
                    new Reserva
                    {
                        ReservaId = 1,
                        DataInicio = new DateTime(2022, 1, 1, 00, 00, 01),
                        DataFim = new DateTime(2022, 1, 10, 23, 59, 59)
                    }
                };

                return bookingsSeed;
            }

        }

        #endregion

        #region ENTITIES

        internal DbSet<Usuario> Usuarios { get; set; }
        internal DbSet<Lugar> Lugares { get; set; }
        internal DbSet<Reserva> Reservas { get; set; }

        #endregion
    }
}
