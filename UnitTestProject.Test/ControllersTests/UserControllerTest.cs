using DataAccessRepository;
using FluentAssertions;
using Kaizen_Task.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using ModelsRepository;
using ModelsRepository.IRepositries;
using ModelsRepository.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestProject.Test.MocData;
using Xunit;

namespace UnitTestProject.Test.ControllersTests
{
    public class UserControllerTest : BaseUnitTest
    {

        [Fact]
        public async Task GetUsers_ShouldReturn200Status()
        {
            var configuration = new ConfigurationBuilder().AddInMemoryCollection().Build();

            _context.User.AddRange(UserMocData.GetUsers().ToList());
            _context.SaveChanges();
            IManagerUnit _mangaer = new ManagerUnit(_context);

            var userTest = new UserContraller(configuration, _mangaer);
            var result = (OkObjectResult)userTest.GetUsers(1, 3);

            result.StatusCode.Should().Be(200);
        }
    }
}
