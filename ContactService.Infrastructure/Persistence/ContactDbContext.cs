using ContactService.Application.Common.Interfaces;
using ContactService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace ContactService.Infrastructure.Persistence
{
    public class ContactDbContext : DbContext, IApplicationDbContext
    {
        private readonly ILogger<ContactDbContext> _logger;
        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options)
        {
        }
        public ContactDbContext()
        {

        }
        public virtual DbSet<Person> Persons => Set<Person>();
        public virtual DbSet<Contact> Contacts => Set<Contact>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("person").HasKey(o=>o.Id);
            modelBuilder.Entity<Contact>().ToTable("contact").HasKey(o=>o.Id);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
