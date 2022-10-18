using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ReportService.Application.Common.Interfaces;
using ReportService.Domain.Entities;

namespace ReportService.Infrastructure.Persistence
{
    public class ReportDbContext : DbContext, IApplicationReportDbContext
    {
        public ReportDbContext(DbContextOptions<ReportDbContext> options) : base(options)
        {
        }
        public ReportDbContext()
        {

        }
        public virtual  DbSet<Report> Reports => Set<Report>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Report>().ToTable("report").HasKey(o => o.Id);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
