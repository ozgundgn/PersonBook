using ContactService.Application.Contacts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Test.Models
{
    public class CreateContactCommandTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new CreateContactCommand { PersonId = 1, Location = "Denizli", Email = "deneme1@gmail.com", PhoneNumber = "05378964788" } };
            yield return new object[] { new CreateContactCommand { PersonId = 1, Location = "Mardin", Email = "deneme2@gmail.com", PhoneNumber = "05378964789" } };
            yield return new object[] { new CreateContactCommand { PersonId = 1, Location = "Kayseri", Email = "deneme3@gmail.com", PhoneNumber = "05378964790" } };
            yield return new object[] { new CreateContactCommand { PersonId = 1, Location = "Bartın", Email = "deneme4@gmail.com", PhoneNumber = "05378964791" } };
            yield return new object[] { new CreateContactCommand { PersonId = 1, Location = "Ağrı", Email = "deneme5@gmail.com", PhoneNumber = "05378964792" } };

        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
