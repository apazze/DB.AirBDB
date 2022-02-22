using System;

namespace DB.AirBDB.DAL.Repository.DatabaseEntity
{
    internal class Booking
    {
        public int BookingId { get; set; }
        public int UserId { get; set; } = 1;
        public User User { get; set; }
        public int PlaceId { get; set; } = 1;
        public Place Place { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

    }
}