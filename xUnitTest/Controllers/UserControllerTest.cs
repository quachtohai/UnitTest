using App.Controllers;
using App.Models;
using App.Repository;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace xUnitTest.Controllers
{
    public class UserControllerTest
    {
        private readonly IUserInterface UserInterface;
        private readonly UserController UserController;

        public UserControllerTest()
        {
            this.UserInterface =A.Fake<IUserInterface>();

            this.UserController = new UserController(UserInterface);

        }
        private static User CreateFakeUser() => A.Fake<User>();

        [Fact]
        public async void UserController_Create_ReturnCreated()
        {
            var user = CreateFakeUser();
            A.CallTo(() => UserInterface.CreateAsync(user)).Returns(true);
            var result = (CreatedAtActionResult)await UserController.Create(user);
            result.StatusCode.Should().Be(201);
            result.Should().NotBeNull();
        }

        [Fact]
        public async void UserController_GetUsers_ReturnOk()
        {
            var users = A.Fake<List<User>>();
            users.Add(new User() { Email="quachtohai@gmail.com", Name="Quach To Hai"});
            A.CallTo(()=>UserInterface.GetAllAsync()).Returns(users);
            var result = (OkObjectResult)await UserController.GetUsers();
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Should().NotBeNull();

        }
        [Theory]
        [InlineData(1)]
        public async void UserController_GetUser_ReturnOk(int id)
        {
            var user = A.Fake<User>();
            user.Name = "quachtohai";
            user.Email = "quachtohai@gmail.com";
            A.CallTo(()=>UserInterface.GetByIdAsync(id)).Returns(user);
            var result = (OkObjectResult)await UserController.GetUser(id);
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Should().NotBeNull();


        }

        [Fact]
        public async void UserController_Update_ReturnOk()
        {
            var user = CreateFakeUser();
            A.CallTo(() => UserInterface.UpdateAsync(user)).Returns(true);

            var result = (OkResult)await UserController.UpdateUser(user);
            result.StatusCode.Should().Be(200);
            result.Should().NotBeNull();


        }

        [Fact]
        public async void UserController_Delete_ReturnNoContent()
        {
            int userId = 1;           
            A.CallTo(() => UserInterface.DeleteAsync(userId)).Returns(true);

            var result = (NoContentResult)await UserController.DeleteUser(userId);
            result.StatusCode.Should().Be(StatusCodes.Status204NoContent);
            result.Should().NotBeNull();


        }

    }
}
