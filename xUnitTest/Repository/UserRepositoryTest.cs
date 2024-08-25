using App.Data;
using App.Models;
using App.Repository;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace xUnitTest.Repository
{
    public class UserRepositoryTest
    {
        private readonly UserRepository UserRepository;
        public UserRepositoryTest()
        {
            var userDbContext = GetDatabaseContext().Result;
            UserRepository = new UserRepository(userDbContext);
        }
        private async Task<UserDbContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<UserDbContext>()
                .UseSqlServer
                ("Server=10.36.10.27\\TDB02;Database=Tmp;User Id=sa;Password=@357ithmtd;Encrypt=False;TrustServerCertificate=True")
                .Options;

            var userDbContext = new UserDbContext(options);
            userDbContext.Database.EnsureCreated();
            if (!await userDbContext.Users.AnyAsync())
            {
                userDbContext.Users.Add(new App.Models.User()
                {
                    Email = "quachtohai@gmail.com",
                    Name = "Test",
                });
            }
            return userDbContext;
        }
        [Fact]
        private async void UserRepository_Create_ReturnTrue()
        {
            var user = A.Fake<User>();
            var result = await UserRepository.CreateAsync(user);
            result.Should().Be(true);        
        }

        [Fact]
        private async void UserRepository_GetUsers_ReturnUsers()
        {
           
            var result = await UserRepository.GetAllAsync();
            result.Should().AllBeAssignableTo<User>();
        }
        [Theory]
        [InlineData(2)]
        private async void UserRepository_GetUser_ReturnUser(int id)
        {
            var result = await UserRepository.GetByIdAsync(id);
            result.Should().BeAssignableTo<User>();
        }

        [Theory]
        [InlineData(2)]
        private async void UserRepository_UpdateUser_ReturnTrue(int id)
        {
            var user = await UserRepository.GetByIdAsync(id);
            user.Name = "Test";
            var result = await UserRepository.UpdateAsync(user);
            result.Should().Be(true);

        }
        [Fact]
        private async void UserRepository_DeleteUser_ReturnTrue()
        {
            int id = 3;
            var result = await UserRepository.DeleteAsync(id);
            result.Should().Be(true);

        }

    }
}
