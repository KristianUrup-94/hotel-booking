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
        #region Private properties
        private readonly Mock<IRepository<Booking>> _repoMock;
        private static readonly List<Booking> InitialBookings = new List<Booking>
        {
            new Booking
            {
                Id = 1,
                BookingNo = 1001,
                From = new DateTimeOffset(2026, 10, 3, 15, 0, 0, TimeSpan.Zero),
                To = new DateTimeOffset(2026, 10, 10, 10, 0, 0, TimeSpan.Zero),
                RoomId = 1
            },
            new Booking
            {
                Id = 2,
                BookingNo = 1002,
                From = new DateTimeOffset(2026, 10, 3, 15, 0, 0, TimeSpan.Zero),
                To = new DateTimeOffset(2026, 10, 6, 10, 0, 0, TimeSpan.Zero),
                RoomId = 2
            },
            new Booking
            {
                Id = 3,
                BookingNo = 1003,
                From = new DateTimeOffset(2026, 10, 9, 15, 0, 0, TimeSpan.Zero),
                To = new DateTimeOffset(2026, 10, 12, 10, 0, 0, TimeSpan.Zero),
                RoomId = 2
            },
            new Booking
            {
                Id = 4,
                BookingNo = 1004,
                From = new DateTimeOffset(2026, 10, 12, 15, 0, 0, TimeSpan.Zero),
                To = new DateTimeOffset(2026, 10, 14, 10, 0, 0, TimeSpan.Zero),
                RoomId = 3
            }
        };

        public static IEnumerable<object[]> EmptyListExpected
        {
            get
            {
                yield return new object[] { new DateTimeOffset(2026, 10, 14, 15, 0, 0, TimeSpan.Zero), new DateTimeOffset(2026, 10, 15, 10, 0, 0, TimeSpan.Zero) };
                yield return new object[] { new DateTimeOffset(2026, 10, 1, 15, 0, 0, TimeSpan.Zero), new DateTimeOffset(2026, 10, 3, 10, 0, 0, TimeSpan.Zero) };
            }
        }

        public static IEnumerable<object[]> OneInTheListExpected
        {
            get
            {
                yield return new object[] { new DateTimeOffset(2026, 10, 11, 15, 0, 0, TimeSpan.Zero), new DateTimeOffset(2026, 10, 12, 10, 0, 0, TimeSpan.Zero) };
                yield return new object[] { new DateTimeOffset(2026, 10, 12, 15, 0, 0, TimeSpan.Zero), new DateTimeOffset(2026, 10, 15, 10, 0, 0, TimeSpan.Zero) };
            }
        }

        public static IEnumerable<object[]> TwoInTheListExpected
        {
            get
            {
                yield return new object[] { new DateTimeOffset(2026, 10, 9, 15, 0, 0, TimeSpan.Zero), new DateTimeOffset(2026, 10, 10, 10, 0, 0, TimeSpan.Zero) };
                yield return new object[] { new DateTimeOffset(2026, 10, 2, 15, 0, 0, TimeSpan.Zero), new DateTimeOffset(2026, 10, 10, 10, 0, 0, TimeSpan.Zero) };
                yield return new object[] { new DateTimeOffset(2026, 10, 2, 15, 0, 0, TimeSpan.Zero), new DateTimeOffset(2026, 10, 12, 10, 0, 0, TimeSpan.Zero) };
            }
        }

        public static IEnumerable<object[]> ThreeInTheListExpected
        {
            get
            {
                yield return new object[] { new DateTimeOffset(2026, 10, 5, 15, 0, 0, TimeSpan.Zero), new DateTimeOffset(2026, 10, 16, 10, 0, 0, TimeSpan.Zero) };
                yield return new object[] { new DateTimeOffset(2026, 10, 5, 15, 0, 0, TimeSpan.Zero), new DateTimeOffset(2026, 10, 13, 10, 0, 0, TimeSpan.Zero) };
                yield return new object[] { new DateTimeOffset(2026, 10, 1, 15, 0, 0, TimeSpan.Zero), new DateTimeOffset(2026, 10, 13, 10, 0, 0, TimeSpan.Zero) };
            }
        }


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
        #endregion

        #region Constructor
        public BookingManagerTests()
        {
            _repoMock = new Mock<IRepository<Booking>>();
        }
        #endregion 

        #region Check Room Availability
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
                    BookingNo = 10001,
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
                    BookingNo = 10001,
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
                    BookingNo = 10001,
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
                    BookingNo = 10001,
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

        #endregion

        #region Book Room
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
                    BookingNo = 10001,
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
                    BookingNo = 10001,
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
        #endregion

        #region Get RoomIds Booked In Period

        /// <summary>
        /// Gets the Id of the booked rooms in the given Period 
        /// <para>Expects empty list</para>
        /// </summary>
        /// <param name="from">When the period starts</param>
        /// <param name="to">When the period ends</param>
        [Theory]
        [MemberData(nameof(EmptyListExpected))]
        public void GetRoomIdsBookedInPeriod_ShouldReturnEmptyList(DateTimeOffset from, DateTimeOffset to)
        {
            // Arrange
            AvailableRoomsRequest request = new AvailableRoomsRequest
            {
                From = from,
                To = to
            };

            _repoMock.Setup(mock => mock.Query()).Returns(InitialBookings.AsQueryable());
            IBookingManager bookingHandler = new BookingManager(_repoMock.Object);

            // Action
            List<int> result = bookingHandler.GetRoomIdsBookedInPeriod(request);

            // Assert 
            Assert.Empty(result);
            _repoMock.Verify(repo => repo.Query(), Times.Once());
        }

        /// <summary>
        /// Gets the Id of the booked rooms in the given Period 
        /// <para>Expects 1</para>
        /// </summary>
        /// <param name="from">When the period starts</param>
        /// <param name="to">When the period ends</param>
        [Theory]
        [MemberData(nameof(OneInTheListExpected))]
        public void GetRoomIdsBookedInPeriod_ShouldReturnListWithOne(DateTimeOffset from, DateTimeOffset to)
        {
            // Arrange
            AvailableRoomsRequest request = new AvailableRoomsRequest
            {
                From = from,
                To = to
            };

            _repoMock.Setup(mock => mock.Query()).Returns(InitialBookings.AsQueryable());
            IBookingManager bookingHandler = new BookingManager(_repoMock.Object);

            // Action
            List<int> result = bookingHandler.GetRoomIdsBookedInPeriod(request);

            // Assert 
            Assert.Single(result);
            _repoMock.Verify(repo => repo.Query(), Times.Once());
        }

        /// <summary>
        /// Gets the Id of the booked rooms in the given Period
        /// <para>Expects 2</para>
        /// </summary>
        /// <param name="from">When the period starts</param>
        /// <param name="to">When the period ends</param>
        [Theory]
        [MemberData(nameof(TwoInTheListExpected))]
        public void GetRoomIdsBookedInPeriod_ShouldReturnListWithTwo(DateTimeOffset from, DateTimeOffset to)
        {
            // Arrange
            AvailableRoomsRequest request = new AvailableRoomsRequest
            {
                From = from,
                To = to
            };

            _repoMock.Setup(mock => mock.Query()).Returns(InitialBookings.AsQueryable());
            IBookingManager bookingHandler = new BookingManager(_repoMock.Object);

            // Action
            List<int> result = bookingHandler.GetRoomIdsBookedInPeriod(request);

            // Assert 
            Assert.Equal(2, result.Count());
            _repoMock.Verify(repo => repo.Query(), Times.Once());
        }

        /// <summary>
        /// Gets the Id of the booked rooms in the given Period
        /// <para>Expects 2</para>
        /// </summary>
        /// <param name="from">When the period starts</param>
        /// <param name="to">When the period ends</param>
        [Theory]
        [MemberData(nameof(ThreeInTheListExpected))]
        public void GetRoomIdsBookedInPeriod_ShouldReturnListWithThree(DateTimeOffset from, DateTimeOffset to)
        {
            // Arrange
            AvailableRoomsRequest request = new AvailableRoomsRequest
            {
                From = from,
                To = to
            };

            _repoMock.Setup(mock => mock.Query()).Returns(InitialBookings.AsQueryable());
            IBookingManager bookingHandler = new BookingManager(_repoMock.Object);

            // Action
            List<int> result = bookingHandler.GetRoomIdsBookedInPeriod(request);

            // Assert 
            Assert.Equal(3, result.Count());
            _repoMock.Verify(repo => repo.Query(), Times.Once());
        }

        /// <summary>
        /// CheckRoomAvailability should throw error due to date from is exceeded
        /// </summary>
        [Fact]
        public void GetRoomIdsBookedInPeriod_DateFromIsExceeded_ShouldThrowError()
        {
            // Arrange
            AvailableRoomsRequest request = new AvailableRoomsRequest
            {
                From = new DateTimeOffset(2025, 1, 2, 0, 0, 0, TimeSpan.Zero),
                To = new DateTimeOffset(2025, 1, 8, 0, 0, 0, TimeSpan.Zero),
            };

            IBookingManager bookingHandler = new BookingManager(_repoMock.Object);

            // Action
            // Assert 
            InvalidDataException ex = Assert.Throws<InvalidDataException>(() => bookingHandler.GetRoomIdsBookedInPeriod(request));
            Assert.Equal("The from date is already exceeded", ex.Message);
        }

        /// <summary>
        /// CheckRoomAvailability should throw error due to date from is exceeded
        /// </summary>
        [Fact]
        public void GetRoomIdsBookedInPeriod_DateToIsBeforeDateFrom_ShouldThrowError()
        {
            // Arrange
            AvailableRoomsRequest request = new AvailableRoomsRequest
            {
                From = new DateTimeOffset(2026, 1, 10, 0, 0, 0, TimeSpan.Zero),
                To = new DateTimeOffset(2026, 1, 8, 0, 0, 0, TimeSpan.Zero),
            };

            IBookingManager bookingHandler = new BookingManager(_repoMock.Object);

            // Action
            // Assert 
            InvalidDataException ex = Assert.Throws<InvalidDataException>(() => bookingHandler.GetRoomIdsBookedInPeriod(request));
            Assert.Equal("The to date is before from date", ex.Message);
        }

        #endregion 

    }
}
