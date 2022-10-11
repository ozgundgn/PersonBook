using ContactService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace ContactService.Infrastructure.Persistence
{
    public class ApplicationContactDbContextInitialiser
    {
        private readonly ILogger<ApplicationContactDbContextInitialiser> _logger;
        private readonly ContactDbContext _context;

        public ApplicationContactDbContextInitialiser(ILogger<ApplicationContactDbContextInitialiser> logger, ContactDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                await _context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising contact the database.");
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
            if (!_context.Contacts.Any())
            {
                _context.Contacts.Add(new Contact { PhoneNumber = "00001", Email = @"fake1@gmail.com", Location = "Ireland" });
                _context.Contacts.Add(new Contact { PhoneNumber = "00002", Email = @"fake2@gmail.com", Location = "Iceland" });
                _context.Contacts.Add(new Contact { PhoneNumber = "00003", Email = @"fake3@gmail.com", Location = "Canada" });

            }

            if (!_context.Persons.Any())
            {
                _context.Persons.Add(new Person { Name = "Jhon", Surname = @"Jhony", Company = "Container1",Uuid=Guid.NewGuid() });
                _context.Persons.Add(new Person { Name = "Phon", Surname = @"Phony", Company = "Container2", Uuid = Guid.NewGuid() });
                _context.Persons.Add(new Person { Name = "Lhon", Surname = @"Lhony", Company = "Container3", Uuid = Guid.NewGuid() });

            }
            await _context.SaveChangesAsync();

        }
    }
}
