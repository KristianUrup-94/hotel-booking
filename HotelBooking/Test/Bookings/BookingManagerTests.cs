using Bookings.Entity;
using Bookings.Services;
using Bookings.Services.Interfaces;
using Moq;
using Shared.Interfaces;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Bookings
{
    public class BookingManagerTests
    {
        private readonly Mock<IRepository<Booking>> _repoMock;
        private readonly Mock<ISimpleService<Booking>> _serviceMock;

        public static IEnumerable<object[]> BookingRequestsInvalid
        {
            get
            {
                yield return new object[] { 1, new DateTimeOffset(2026, 1, 2, 15, 0, 0, TimeSpan.Zero), new DateTimeOffset(2026, 1, 4, 10, 0, 0, TimeSpan.Zero) };
                yield return new object[] { 1, new DateTimeOffset(2026, 1, 5, 15, 0, 0, TimeSpan.Zero), new DateTimeOffset(2026, 1, 6, 10, 0, 0, TimeSpan.Zero) };
                yield return new object[] { 1, new DateTimeOffset(2026, 1, 7, 15, 0, 0, TimeSpan.Zero), new DateTimeOffset(2026, 1, 9, 10, 0, 0, TimeSpan.Zero) };
            }
        }

        public static IEnumerable<object[]> BookingRequestsValid
        {
            get
            {
                yield return new object[] { 1, new DateTimeOffset(2025, 12, 30, 15, 0, 0, TimeSpan.Zero), new DateTimeOffset(2026, 1, 2, 10, 0, 0, TimeSpan.Zero) };
                yield return new object[] { 1, new DateTimeOffset(2026, 1, 8, 15, 0, 0, TimeSpan.Zero), new DateTimeOffset(2026, 1, 10, 10, 0, 0, TimeSpan.Zero) };
                yield return new object[] { 1, new DateTimeOffset(2025, 12, 30, 15, 0, 0, TimeSpan.Zero), new DateTimeOffset(2025, 12, 31, 10, 0, 0, TimeSpan.Zero) };
                yield return new object[] { 1, new DateTimeOffset(2026, 1, 10, 15, 0, 0, TimeSpan.Zero), new DateTimeOffset(2026, 1, 12, 10, 0, 0, TimeSpan.Zero) };
            }
        }

        public BookingManagerTests()
        {
            _repoMock = new Mock<IRepository<Booking>>();
        }
        

        /// <summary>
        /// CheckRoomAvailability should throw error due to date from is exceeded
        /// </summary>
        [Fact]
        public void CheckRoomAvailability_DateFromIsExceeded_ShouldThrowError()
        {
            // Arrange
            List<Booking> bookings = new List<Booking>
            {
                new Booking
                {
                    BookingId = 10001,
                    Comments = "Testing",
                    From = new DateTimeOffset(2025, 1, 2, 0, 0,0 , TimeSpan.Zero),
                    To = new DateTimeOffset(2025, 1, 8, 0, 0,0 , TimeSpan.Zero),
                    RoomId = 1
                }
            };
            BookingRequest request = new BookingRequest
            {
                RoomId = 1,
                From = new DateTimeOffset(2025, 1, 2, 0, 0, 0, TimeSpan.Zero),
                To = new DateTimeOffset(2025, 1, 8, 0, 0, 0, TimeSpan.Zero),
            };

            _repoMock.Setup(mock => mock.Query()).Returns(bookings.AsQueryable());
            IBookingManager bookingHandler = new BookingManager(_repoMock.Object);

            // Action
            // Assert 
            InvalidDataException ex = Assert.Throws<InvalidDataException>(() => bookingHandler.CheckRoomAvailability(request));
            Assert.Equal("The from date is already exceeded", ex.Message);
        }

        /// <summary>
        /// CheckRoomAvailability should throw error due to date to is before date from
        /// </summary>
        [Fact]
        public void CheckRoomAvailability_DateToIsBeforeDateFrom_ShouldThrowError()
        {
            // Arrange
            List<Booking> bookings = new List<Booking>
            {
                new Booking
                {
                    BookingId = 10001,
                    Comments = "Testing",
                    From = new DateTimeOffset(2025, 1, 2, 0, 0,0 , TimeSpan.Zero),
                    To = new DateTimeOffset(2025, 1, 8, 0, 0,0 , TimeSpan.Zero),
                    RoomId = 1
                }
            };
            BookingRequest request = new BookingRequest
            {
                RoomId = 1,
                From = new DateTimeOffset(2025, 1, 10, 0, 0, 0, TimeSpan.Zero),
                To = new DateTimeOffset(2025, 1, 8, 0, 0, 0, TimeSpan.Zero),
            };

            _repoMock.Setup(mock => mock.Query()).Returns(bookings.AsQueryable());
            IBookingManager bookingHandler = new BookingManager(_repoMock.Object);

            // Action
            // Assert 
            InvalidDataException ex = Assert.Throws<InvalidDataException>(() => bookingHandler.CheckRoomAvailability(request));
            Assert.Equal("The to date is before from date", ex.Message);
        }

        /// <summary>
        /// Checks if the room is available
        /// <para>Should return false</para>
        /// </summary>
        /// <param name="request">Request for a booking</param>
        [Theory]
        [MemberData(nameof(BookingRequestsInvalid))]
        public void CheckRoomAvailability_ShouldReturnFalse_RoomNotAvailable(int roomId, DateTimeOffset from, DateTimeOffset to)
        {
            // Arrange
            List<Booking> bookings = new List<Booking>
            {
                new Booking
                {
                    BookingId = 10001,
                    Comments = "Testing",
                    From = new DateTimeOffset(2026, 1, 2, 10, 0,0 , TimeSpan.Zero),
                    To = new DateTimeOffset(2026, 1, 8, 15, 0,0 , TimeSpan.Zero),
                    RoomId = 1
                }
            };
            var request = new BookingRequest
            {
                RoomId = roomId,
                From = from,
                To = to
            };

            _repoMock.Setup(mock => mock.Query()).Returns(bookings.AsQueryable());
            IBookingManager bookingHandler = new BookingManager(_repoMock.Object);

            // Action
            bool result = bookingHandler.CheckRoomAvailability(request);

            // Assert 
            Assert.False(result);
            _repoMock.Verify(repo => repo.Query(), Times.Once());
        }
        /// <summary>
        /// Checks if the room is available
        /// <para>Should return true</para>
        /// </summary>
        /// <param name="request">Request for a booking</param>
        [Theory]
        [MemberData(nameof(BookingRequestsValid))]
        public void CheckRoomAvailability_ShouldReturnFalse_RoomIsAvailable(int roomId, DateTimeOffset from, DateTimeOffset to)
        {
            // Arrange
            List<Booking> bookings = new List<Booking>
            {
                new Booking
                {
                    BookingId = 10001,
                    Comments = "Testing",
                    From = new DateTimeOffset(2026, 1, 2, 15, 0,0 , TimeSpan.Zero),
                    To = new DateTimeOffset(2026, 1, 8, 10, 0,0 , TimeSpan.Zero),
                    RoomId = 1
                }
            };
            BookingRequest request = new BookingRequest
            {
                RoomId = roomId,
                From = from,
                To = to
            };

            _repoMock.Setup(mock => mock.Query()).Returns(bookings.AsQueryable());
            IBookingManager bookingHandler = new BookingManager(_repoMock.Object);

            // Action
            bool result = bookingHandler.CheckRoomAvailability(request);

            // Assert 
            Assert.True(result);
            _repoMock.Verify(repo => repo.Query(), Times.Once());
        }

        /// <summary>
        /// BookRoom succeeds booking
        /// </summary>
        [Fact]
        public void BookRoom_Succeeds_ShouldReturnInteger()
        {
            // Arrange
            List<Booking> bookings = new List<Booking>
            {
                new Booking
                {
                    BookingId = 10001,
                    Comments = "Testing",
                    From = new DateTimeOffset(2026, 1, 2, 0, 0,0 , TimeSpan.Zero),
                    To = new DateTimeOffset(2026, 1, 8, 0, 0,0 , TimeSpan.Zero),
                    RoomId = 1
                }
            };
            BookingRequest request = new BookingRequest
            {
                RoomId = 1,
                From = new DateTimeOffset(2026, 1, 10, 0, 0, 0, TimeSpan.Zero),
                To = new DateTimeOffset(2026, 1, 12, 0, 0, 0, TimeSpan.Zero),
            };

            _repoMock.Setup(mock => mock.Query()).Returns(bookings.AsQueryable());
            IBookingManager bookingHandler = new BookingManager(_repoMock.Object);

            // Action
            var result = bookingHandler.BookRoom(request);

            // Assert 
            Assert.IsType<int>(result);
        }

        /// <summary>
        /// BookRoom fails booking
        /// </summary>
        [Fact]
        public void BookRoom_Fails_ShouldThrowException()
        {
            // Arrange
            List<Booking> bookings = new List<Booking>
            {
                new Booking
                {
                    BookingId = 10001,
                    Comments = "Testing",
                    From = new DateTimeOffset(2026, 1, 2, 0, 0,0 , TimeSpan.Zero),
                    To = new DateTimeOffset(2026, 1, 8, 0, 0,0 , TimeSpan.Zero),
                    RoomId = 1
                }
            };
            BookingRequest request = new BookingRequest
            {
                RoomId = 1,
                From = new DateTimeOffset(2026, 1, 7, 0, 0, 0, TimeSpan.Zero),
                To = new DateTimeOffset(2026, 1, 12, 0, 0, 0, TimeSpan.Zero),
            };

            _repoMock.Setup(mock => mock.Query()).Returns(bookings.AsQueryable());
            IBookingManager bookingHandler = new BookingManager(_repoMock.Object);

            // Action
            // Assert 
            InvalidDataException ex = Assert.Throws<InvalidDataException>(() => bookingHandler.BookRoom(request));
            Assert.Equal("Room is not available", ex.Message);
        }
    }
}
