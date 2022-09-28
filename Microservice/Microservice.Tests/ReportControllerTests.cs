using ReportService.Api.Context;
using Microsoft.EntityFrameworkCore;
using ReportService.Api.Controllers;
using ReportService.Api.Managers;
using ReportService.Api.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using ReportService.Api.Domain.Models;

namespace Microservice.Tests
{
    public class ReportControllerTests
    {
        private readonly IReportService _service;
        private readonly ReportController _controller;

        public ReportControllerTests()
        {
            var options = new DbContextOptionsBuilder<PostgreSqlDbContext>().UseNpgsql(Values.ReportConString).Options;

            var _context = new PostgreSqlDbContext(options);
            _service = new ReportManager(_context);
            _controller = new ReportController(_service);
        }
        [Fact]
        public void GetReports_NotNullControl()
        {
            // Act
            var result = _controller.GetReports();

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public void AddReportRequest__ReturnsOkRequest()
        {
            // Arrange
            // Act
            var Response = _controller.AddReportRequest();
            var result = Response.GetAwaiter().GetResult();
            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void GetReportById_InvalidObjectPassed_ReturnsNull()
        {
            // Arrange
            Guid id = new Guid();
            // Act
            var response = _controller.GetReportById(id);

            // Assert
            Assert.Null(response);
        }
        [Fact]
        public void AddContactInfo_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            ReportRequestModel entity = new ReportRequestModel();
            entity.Id = Guid.NewGuid();
            entity.ReportDate = DateTime.Now;
            entity.ReportStatus = null;
            // Act
            var badResponse = _controller.UpdateReportRequest(entity);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badResponse);
        }
    }
}
