using ContactService.Application.Common.Mappings;
using ContactService.Domain.Entities;

namespace ContactService.Application.Persons.Queries
{
    public class PersonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Company { get; set; }

        public string FullName
        {
            get
            {
                return string.Concat(Name, " ", Surname);
            }
            set { }
        }
    }
}
