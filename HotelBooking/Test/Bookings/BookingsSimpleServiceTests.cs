using Bookings.Entity;
using Bookings.Services;
using Moq;
using Rooms.Entity;
using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Bookings
{
    public class BookingsSimpleServiceTests
    {
        private readonly Mock<IRepository<Booking>> _repoMock;

        public BookingsSimpleServiceTests()
        {
            _repoMock = new Mock<IRepository<Booking>>();
        }

        [Fact]
        public void GetAll_ReturnsList_ChecksType()
        {
            // Arrange
            List<Booking> bookings = new List<Booking>
            {
                new Booking 
                { 
                    Id = 1, 
                    BookingId = 10001, 
                    Comments = "Needs flowers on the table", 
                    From = new DateTimeOffset(2026, 2, 4, 15, 0, 0, TimeSpan.Zero), 
                    To = new DateTimeOffset(2026, 2, 7, 10, 0, 0, TimeSpan.Zero),
                    RoomId = 1,
                },
                new Booking
                {
                    Id = 2,
                    BookingId = 10002,
                    Comments = "Needs flowers on the table",
                    From = new DateTimeOffset(2026, 2, 4, 15, 0, 0, TimeSpan.Zero),
                    To = new DateTimeOffset(2026, 2, 7, 10, 0, 0, TimeSpan.Zero),
                    RoomId = 2,
                }
            };
            _repoMock.Setup(repo => repo.GetAll()).Returns(bookings);
            ISimpleService<Booking> bookingsService = new Service(_repoMock.Object);

            // Action
            var result = bookingsService.GetAll();

            // Assert
            Assert.IsType<List<Booking>>(result);
            Assert.Equivalent(2, result.Count());
            _repoMock.Verify((mock) => mock.GetAll(), Times.Once());
        }

        [Fact]
        public void Create_VerifiesCallsRepoOnce()
        {
            // Arrange
            Booking booking = new Booking 
            { 
                Id = 1,
                BookingId = 10001,
                Comments = "Test",
                From = new DateTimeOffset(2026, 2, 4, 15, 0, 0, TimeSpan.Zero),
                To = new DateTimeOffset(2026, 2, 7, 10, 0, 0, TimeSpan.Zero),
                RoomId = 1
            };
            ISimpleService<Booking> bookingsService = new Service(_repoMock.Object);

            // Action 
            bookingsService.Create(booking);

            // Assert
            _repoMock.Verify((mock) => mock.Add(booking), Times.Once());
        }

        [Fact]
        public void Update_VerifiesCallsRepoOnce()
        {
            // Arrange
            Booking booking = new Booking
            {
                Id = 1,
                BookingId = 10001,
                Comments = "Test",
                From = new DateTimeOffset(2026, 2, 4, 15, 0, 0, TimeSpan.Zero),
                To = new DateTimeOffset(2026, 2, 7, 10, 0, 0, TimeSpan.Zero),
                RoomId = 1
            };
            ISimpleService<Booking> bookingsService = new Service(_repoMock.Object);

            // Action 
            bookingsService.Update(booking);

            // Assert
            _repoMock.Verify((mock) => mock.Update(booking), Times.Once());
        }

        [Fact]
        public void Delete_VerifiesCallsRepoOnce()
        {
            // Arrange
            int id = 1;
            ISimpleService<Booking> bookingsService = new Service(_repoMock.Object);

            // Action 
            bookingsService.Delete(id);

            // Assert
            _repoMock.Verify((mock) => mock.Delete(id), Times.Once());
        }
    }
}
