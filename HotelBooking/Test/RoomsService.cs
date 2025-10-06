using Moq;
using Rooms.Entity;
using Shared.Interfaces;
using Shared.Interfaces.BaseClasses;

namespace Test
{
    public class RoomsService
    {
        private readonly Mock<IRepository<Room>> _repoMock;

        public RoomsService()
        {
            _repoMock = new Mock<IRepository<Room>>();
        }
        [Fact]
        public void GetAll_ReturnsList_ChecksType()
        {
            // Arrange
            List<Room> rooms = new List<Room>
            {
            new Room { Id = 1, Name = "Test room 1" },
            new Room { Id = 2, Description = "This is the best room", Name = "Test room 2" }
            };
            _repoMock.Setup(repo => repo.GetAll()).Returns(rooms);

            // Action
            var roomService = new Rooms.Services.Service(_repoMock.Object);
            var result = roomService.GetAll();

            // Assert
            Assert.IsType<List<Room>>(rooms);
            Assert.Equivalent(2, result.Count());
            _repoMock.Verify((mock) => mock.GetAll(), Times.Once());
        }

        [Fact]
        public void Create_VerifiesCallsRepoOnce()
        {
            // Arrange
            Room room = new Room { Id = 1, Name = "Testing Create" };

            // Action 
            var roomService = new Rooms.Services.Service(_repoMock.Object);
            roomService.Create(room);

            // Assert
            _repoMock.Verify((mock) => mock.Add(room), Times.Once());
        }
    }
}