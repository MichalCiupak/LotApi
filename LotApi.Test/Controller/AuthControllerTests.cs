using System;
using LotApi.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using LotApi.Models;
using LotApi.Controllers;
using FluentAssertions;
using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using LotApi.Dto;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;


namespace LotApi.Test.Controller
{
    public class AuthControllerTests 
    {

        private readonly DbContextOptions<DataContext> _dbContextOptions;
        private readonly IConfiguration _configuration;

        public AuthControllerTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "AppSettings:Token", "xXxXxXxXxXxXxXxXxXxXxXxXxXxXxXxXxXxXxXxXxXxXxXxXxXxXxXxXxXxXxXxXxXxXxXxX" }
            })
            .Build();
        }

        [Fact]
        public void AuthController_Register()
        {

            var mockDataContext = new DataContext(_dbContextOptions);

            var userDto = new UserDto
            {
                Username = "testuser",
                Password = "password123"
            };

            var controller = new AuthController(mockDataContext, _configuration);


            var positiveResult = controller.Register(userDto).Result;
            Assert.IsType<OkObjectResult>(positiveResult);


            var user = mockDataContext.UserData.FirstOrDefault();
            Assert.IsType<UserData>(user);
            Assert.Equal("testuser", user.Username);
            Assert.NotNull(user.PasswordHash);


            var badResult = controller.Register(userDto).Result;
            var users = mockDataContext.UserData;
            Assert.Equal(1, users.Count());
            Assert.IsType<BadRequestObjectResult>(badResult);

        }


        [Fact]
        public void AuthController_Login_ReturnOk()
        {

            var mockDataContext = new DataContext(_dbContextOptions);

            var userDto = new UserDto
            {
                Username = "string",
                Password = "string"
            };

            var usersData = new List<UserData>
            {
                new UserData
                {
                    Id = 1,
                    Username = "string",
                    PasswordHash = "$2a$11$A/g8hfknJ2aXLDu0RuCsLu47Qf2w6M8n5dzH90wc9l4rPM7ojskE2"
                }
            }.AsQueryable();


            var controller = new AuthController(mockDataContext, _configuration);
            mockDataContext.UserData = MockDbSet(usersData);
            var result = controller.Login(userDto).Result;
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void AuthController_Login_BadRequest()
        {


            var mockDataContext = new DataContext(_dbContextOptions);

            var userDto = new UserDto
            {
                Username = "string",
                Password = "BadPassword"
            };

            var usersData = new List<UserData>
            {
                new UserData
                {
                    Id = 1,
                    Username = "string",
                    PasswordHash = "$2a$11$A/g8hfknJ2aXLDu0RuCsLu47Qf2w6M8n5dzH90wc9l4rPM7ojskE2"
                }
            }.AsQueryable();


            var controller = new AuthController(mockDataContext, _configuration);
            mockDataContext.UserData = MockDbSet(usersData);
            var result = controller.Login(userDto).Result;
            Assert.IsType<BadRequestObjectResult>(result);
        }

        public static DbSet<UserData> MockDbSet(IQueryable<UserData> data)
        {
            var mockSet = new Mock<DbSet<UserData>>();
            mockSet.As<IQueryable<UserData>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<UserData>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<UserData>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<UserData>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            return mockSet.Object;
        }

    }
}
