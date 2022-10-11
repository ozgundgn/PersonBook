using ContactService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Person> Persons { get; }
        public DbSet<Contact> Contacts { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
