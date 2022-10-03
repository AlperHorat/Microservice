using ContactService.Api.Context;
using ContactService.Api.Domain.Entities;
using ContactService.Api.Managers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Microservice.Tests
{
    public class ContactManagerTests
    {
        private readonly ContactManager _manager;

        public ContactManagerTests()
        {
            var options = new DbContextOptionsBuilder<PostgreSqlDbContext>().UseNpgsql(Values.ContactConString).Options;

            var _context = new PostgreSqlDbContext(options);
            _manager = new ContactManager(_context);
        }
        [Fact]
        public void GetPersons_NotNullControl()
        {
            // Act
            var result = _manager.GetPersons();

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public void AddPerson_ValidObjectPassed_ReturnsTrue()
        {
            // Arrange
            Person entity = new Person();
            entity.Id = Guid.NewGuid();
            entity.Name = "test";
            entity.Firm = null;
            // Act
            var response = _manager.AddPerson(entity);

            // Assert
            Assert.True(true.Equals(response));
        }
        [Fact]
        public void GetPersonById_InvalidObjectPassed_ReturnsNull()
        {
            // Arrange
            Guid id = new Guid();
            // Act
            var response = _manager.GetPersonById(id);

            // Assert
            Assert.Null(response);
        }
        [Fact]
        public void DeletePerson_InvalidObjectPassed_ReturnsFalse()
        {
            // Arrange
            Guid id = new Guid();
            // Act
            var response = _manager.DeletePerson(id);

            // Assert
            Assert.True(false.Equals(response));
        }
        [Fact]
        public void DeletePersonContactInfo_InvalidObjectPassed_ReturnsFalse()
        {
            // Arrange
            Guid id = new Guid();
            // Act
            var response = _manager.DeletePersonContactInfo(id);

            // Assert
            Assert.True(false.Equals(response));
        }
        [Fact]
        public void GetPersonContacts_InvalidObjectPassed_ReturnsNull()
        {
            // Arrange
            Guid personid = new Guid();
            // Act
            var response = _manager.GetPersonContacts(personid);

            // Assert
            Assert.Null(response);
        }
    }
}
