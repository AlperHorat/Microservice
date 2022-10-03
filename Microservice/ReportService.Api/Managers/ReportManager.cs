using Microsoft.EntityFrameworkCore;
using ReportService.Api.Context;
using ReportService.Api.Domain.Entities;
using ReportService.Api.Domain.Models;
using ReportService.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportService.Api.Managers
{
    public class ReportManager : IReportService
    {
        private readonly PostgreSqlDbContext _context;

        public ReportManager(PostgreSqlDbContext context)
        {
            _context = context;
        }
        public bool AddReportRequest(Report entity)
        {
            try
            {
                _context.Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public Report GetReportById(Guid id)
        {
            return _context.Report.Where(a => a.Id == id).FirstOrDefault();
        }

        public List<ReportModel> GetReports()
        {
            List<ReportModel> list = new List<ReportModel>();
            var result = _context.Report.ToList();
            foreach (var item in result)
            {
                ReportModel entity = new ReportModel();
                entity.Id = item.Id;
                entity.ReportDate = item.ReportDate;
                entity.ReportUrl = item.ReportUrl;
                entity.ReportStatusDesc = item.ReportStatus == null ? null : ReportTypeData.ReportTypeDataList.Where(a => a.Id == item.ReportStatus).FirstOrDefault().Description;

                list.Add(entity);
            }
            return list;
        }

        public bool UpdateReportRequest(Report entity)
        {
            try
            {
                var updatedEntity = _context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
