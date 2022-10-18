

using ContactService.Application.Contacts.Commands;
using ContactService.Application.Persons.Commands;

namespace ContactService.Test.Models
{
    public class UpdatePersonCommandTestData:IEnumerable<object[]>
    {
         public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new UpdatePersonCommand { Id=1, Company = "Company1Update", Name = "ERHAN", Surname = "ÖzekUpdate" } };
            yield return new object[] { new UpdatePersonCommand { Id=2, Company = "Company2Update", Name = "Özgür", Surname = "NebiUpdate" } };
            yield return new object[] { new UpdatePersonCommand { Id=3, Company = "Company3Update", Name = "Sinan", Surname = "CanUpdate" } };

        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
