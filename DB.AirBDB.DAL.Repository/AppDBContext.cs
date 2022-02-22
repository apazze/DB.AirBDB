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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
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

            User[] UserSeed()
            {
                User[] usersSeed = new User[]
                {
                    new User
                    {
                        UserId = 1,
                        UserName = "caio_martins",
                        Name = "Caio César Martins",
                        Password = "P4$$W0RDseguro"
                    },
                    new User
                    {
                        UserId = 2,
                        UserName = "alisson_pazze",
                        Name = "Alisson dos Santos Pazze",
                        Password = "P4$$W0RDmaisS3GUR0"
                    }
                };

                return usersSeed;
            }

            Place[] PlaceSeed()
            {
                Place[] placesSeed = new Place[]
                {
                    new Place
                    {
                        PlaceId = 1,
                        Description = "Casa a 3 quadras do mar",
                        AccomodationType = "Casa",
                        City = "Tramandaí",
                        Value = 250
                    },
                    new Place
                    {
                        PlaceId = 2,
                        Description = "Apartamento no bairro Cidade Baixa",
                        AccomodationType = "Apartamento",
                        City = "Porto Alegre",
                        Value = 500
                    }
                };

                return placesSeed;
            }

            Booking[] BookingSeed()
            {
                Booking[] bookingsSeed = new Booking[]
                {
                    new Booking
                    {
                        BookingId = 1,
                        DataInicio = new DateTime(2022, 1, 1, 00, 00, 01),
                        DataFim = new DateTime(2022, 1, 10, 23, 59, 59)
                    }
                };

                return bookingsSeed;
            }

        }

        #endregion

        #region ENTITIES

        internal DbSet<User> Users { get; set; }
        internal DbSet<Place> Places { get; set; }
        internal DbSet<Booking> Bookings { get; set; }

        #endregion
    }
}
