using ContactService.Application.Common.Mappings;
using ContactService.Domain.Entities;

namespace ContactService.Application.Contacts.Queries
{
    public class ContactDto : IMapFrom<Contact>
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FullName { get { return string.Concat(this.Name, " ", this.Surname); } }


    }
}
