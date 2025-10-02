namespace Bookings.Entity
{
    public class Booking
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public DateTimeOffset From { get; set; }
        public DateTimeOffset To { get; set; }
        public string Comments { get; set; }
        public int RoomId { get; set; }
    }
}
