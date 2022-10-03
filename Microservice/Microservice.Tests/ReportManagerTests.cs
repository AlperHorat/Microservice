using ReportService.Api.Context;
using Microsoft.EntityFrameworkCore;
using ReportService.Api.Managers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using ReportService.Api.Domain.Entities;

namespace Microservice.Tests
{
    public class ReportManagerTests
    {
        private readonly ReportManager _manager;

        public ReportManagerTests()
        {
            var options = new DbContextOptionsBuilder<PostgreSqlDbContext>().UseNpgsql(Values.ReportConString).Options;

            var _context = new PostgreSqlDbContext(options);
            _manager = new ReportManager(_context);
        }
        [Fact]
        public void GetReports_NotNullControl()
        {
            // Act
            var result = _manager.GetReports();

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public void AddReportRequest__ReturnsTrue()
        {
            // Arrange
            Report entity = new Report();
            entity.Id = Guid.NewGuid();
            entity.ReportDate = DateTime.Now;
            entity.ReportStatus = ReportType.Hazırlanıyor;
            // Act
            var Response = _manager.AddReportRequest(entity);
            // Assert
            Assert.True(true.Equals(Response));
        }
        [Fact]
        public void GetReportById_InvalidObjectPassed_ReturnsNull()
        {
            // Arrange
            Guid id = new Guid();
            // Act
            var response = _manager.GetReportById(id);

            // Assert
            Assert.Null(response);
        }
        [Fact]
        public void UpdateReportRequest_InvalidObjectPassed_ReturnsFalse()
        {
            // Arrange
            Report entity = new Report();
            entity.Id = Guid.NewGuid();
            entity.ReportDate = DateTime.Now;
            entity.ReportStatus = null;
            // Act
            var Response = _manager.UpdateReportRequest(entity);

            // Assert
            Assert.True(false.Equals(Response));
        }
    }
}
