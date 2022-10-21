using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReportService.Domain.Entities;

namespace ReportService.Infrastructure.Persistence
{
    public class ApplicationReportDbContextInitialiser
    {
        private readonly ILogger<ApplicationReportDbContextInitialiser> _logger;
        private readonly ReportDbContext _context;

        public ApplicationReportDbContextInitialiser(ILogger<ApplicationReportDbContextInitialiser> logger, ReportDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.GetDbConnection() == null)
                    await _context.Database.MigrateAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            // Default data
            // Seed, if necessary

            if (!_context.Reports.Any())
            {

                _context.Reports.Add(new Report { CreatedDate = DateTime.Now, Path = @"c:\", State = 0, Uuid = Guid.NewGuid() });
                _context.Reports.Add(new Report { CreatedDate = DateTime.Now, Path = @"c:\", State = 0, Uuid = Guid.NewGuid() });
                _context.Reports.Add(new Report { CreatedDate = DateTime.Now, Path = @"c:\", State = 0, Uuid = Guid.NewGuid() });

                await _context.SaveChangesAsync();
            }
        }
    }
}
