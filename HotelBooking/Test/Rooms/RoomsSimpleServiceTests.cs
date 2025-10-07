using Moq;
using Rooms.Entity;
using Rooms.Services;
using Shared.Interfaces;
using Shared.Interfaces.BaseClasses;

namespace Test.Rooms
{
    public class RoomsSimpleServiceTests
    {
        private readonly Mock<IRepository<Room>> _repoMock;

        public RoomsSimpleServiceTests()
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
            ISimpleService<Room> roomService = new Service(_repoMock.Object);

            // Action
            var result = roomService.GetAll();

            // Assert
            Assert.IsType<List<Room>>(result);
            Assert.Equivalent(2, result.Count());
            _repoMock.Verify((mock) => mock.GetAll(), Times.Once());
        }

        [Fact]
        public void Create_VerifiesCallsRepoOnce()
        {
            // Arrange
            Room room = new Room { Id = 1, Name = "Testing Create" };
            ISimpleService<Room> roomService = new Service(_repoMock.Object);

            // Action 
            roomService.Create(room);

            // Assert
            _repoMock.Verify((mock) => mock.Add(room), Times.Once());
        }

        [Fact]
        public void Update_VerifiesCallsRepoOnce()
        {
            // Arrange
            Room room = new Room { Id = 1, Name = "Testing Update" };
            ISimpleService<Room> roomService = new Service(_repoMock.Object);

            // Action 
            roomService.Update(room);

            // Assert
            _repoMock.Verify((mock) => mock.Update(room), Times.Once());
        }

        [Fact]
        public void Delete_VerifiesCallsRepoOnce()
        {
            // Arrange
            int id = 1;
            ISimpleService<Room> roomService = new Service(_repoMock.Object);

            // Action 
            roomService.Delete(id);

            // Assert
            _repoMock.Verify((mock) => mock.Delete(id), Times.Once());
        }
    }
}