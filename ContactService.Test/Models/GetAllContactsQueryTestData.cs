using ContactService.Application.Contacts.Queries;

namespace ContactService.Test.Models
{
    public class GetAllContactsQueryTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new GetAllContactsQuery { } };
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
