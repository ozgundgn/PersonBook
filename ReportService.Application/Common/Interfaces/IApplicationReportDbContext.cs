
using Microsoft.EntityFrameworkCore;
using ReportService.Domain.Entities;

namespace ReportService.Application.Common.Interfaces
{
    public interface IApplicationReportDbContext
    {
        public DbSet<Report> Reports { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
