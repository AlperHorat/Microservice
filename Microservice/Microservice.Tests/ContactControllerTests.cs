using ContactService.Api.Context;
using ContactService.Api.Controllers;
using ContactService.Api.Domain.Entities;
using ContactService.Api.Managers;
using ContactService.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Microservice.Tests
{
    public class ContactControllerTests
    {

        private readonly IContactService _service;
        private readonly ContactController _controller;

        public ContactControllerTests()
        {
            var options = new DbContextOptionsBuilder<PostgreSqlDbContext>().UseNpgsql(Values.ContactConString).Options;

            var _context = new PostgreSqlDbContext(options);
            _service = new ContactManager(_context);
            _controller = new ContactController(_service);
        }
        [Fact]
        public void GetPersons_NotNullControl()
        {
            // Act
            var result = _controller.GetPersons();

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public void AddPerson_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            // Act
            var badResponse = _controller.AddPerson(null,null,"test");

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        [Fact]
        public void AddPerson_ValidObjectPassed_ReturnsOk()
        {
            // Arrange
            // Act
            var Response = _controller.AddPerson("Alper", "Horat", "test");

            // Assert
            Assert.IsType<OkObjectResult>(Response);
        }
        [Fact]
        public void GetPersonById_InvalidObjectPassed_ReturnsNull()
        {
            // Arrange
            Guid id = new Guid();
            // Act
            var response = _controller.GetPersonById(id);

            // Assert
            Assert.Null(response);
        }
        [Fact]
        public void AddContactInfo_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            Guid personid = new Guid();
            // Act
            var badResponse = _controller.AddPersonContactInfo("123654", personid, ContactType.Phone);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
        [Fact]
        public void GetPersonContacts_InvalidObjectPassed_ReturnsNull()
        {
            // Arrange
            Guid personid = new Guid();
            // Act
            var response = _controller.GetPersonContacts(personid);

            // Assert
            Assert.Null(response);
        }
    }
}
