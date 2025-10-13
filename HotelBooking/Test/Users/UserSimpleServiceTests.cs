using Bookings.Entity;
using Moq;
using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Entity;
using Users.Services;

namespace Test.Users
{
    public class UserSimpleServiceTests
    {
        private readonly Mock<IRepository<User>> _repoMock;

        public UserSimpleServiceTests()
        {
            _repoMock = new Mock<IRepository<User>>();
        }

        [Fact]
        public void GetAll_ReturnsList_ChecksType()
        {
            // Arrange
            List<User> users = new List<User>
            {
                new User
                {
                    FirstName = "Tom",
                    LastName = "Hollad",
                    Address = "Buevænget 2",
                    Id = 1
                },
                new User
                {
                    FirstName = "Chris",
                    LastName = "Evans",
                    Address = "Bøfvænget 2",
                    Id = 2
                }
            };
            _repoMock.Setup(repo => repo.GetAll()).Returns(users);
            ISimpleService<User> userService = new Service(_repoMock.Object);

            // Action
            var result = userService.GetAll();

            // Assert
            Assert.IsType<List<User>>(result);
            Assert.Equivalent(2, result.Count());
            _repoMock.Verify((mock) => mock.GetAll(), Times.Once());
        }

        [Fact]
        public void Create_VerifiesCallsRepoOnce()
        {
            // Arrange
            User user = new User
            {
                FirstName = "Tom",
                LastName = "Hollad",
                Address = "Buevænget 2",
                Id = 1
            };
            ISimpleService<User> userService = new Service(_repoMock.Object);

            // Action 
            userService.Create(user);

            // Assert
            _repoMock.Verify((mock) => mock.Add(user), Times.Once());
        }

        [Fact]
        public void Update_VerifiesCallsRepoOnce()
        {
            // Arrange
            User user = new User
            {
                FirstName = "Tom",
                LastName = "Hollad",
                Address = "Buevænget 2",
                Id = 1
            };
            ISimpleService<User> userService = new Service(_repoMock.Object);

            // Action 
            userService.Update(user);

            // Assert
            _repoMock.Verify((mock) => mock.Update(user), Times.Once());
        }

        [Fact]
        public void Delete_VerifiesCallsRepoOnce()
        {
            // Arrange
            int id = 1;
            ISimpleService<User> userService = new Service(_repoMock.Object);

            // Action 
            userService.Delete(id);

            // Assert
            _repoMock.Verify((mock) => mock.Delete(id), Times.Once());
        }
    }
}
