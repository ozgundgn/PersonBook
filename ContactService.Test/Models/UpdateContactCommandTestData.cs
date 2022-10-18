

using ContactService.Application.Contacts.Commands;

namespace ContactService.Test.Models
{
    public class UpdateContactCommandTestData:IEnumerable<object[]>
    {
         public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new UpdateContactCommand{Id=1, PersonId =2, Location ="Fransa", Email ="degistir@gmail.com", PhoneNumber ="05658796466"} };
            yield return new object[] { new UpdateContactCommand { Id = 1, PersonId = 2, Location = "Fransa", Email = "degistir@gmail.com", PhoneNumber = "05658796466" } };
            yield return new object[] { new UpdateContactCommand { Id = 1, PersonId = 2, Location = "Fransa", Email = "degistir@gmail.com", PhoneNumber = "05658796466" } };
            yield return new object[] { new UpdateContactCommand { Id = 1, PersonId = 2, Location = "Fransa", Email = "degistir@gmail.com", PhoneNumber = "05658796466" } };
            yield return new object[] { new UpdateContactCommand { Id = 1, PersonId = 2, Location = "Fransa", Email = "degistir@gmail.com", PhoneNumber = "05658796466" } };


        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
