

using ContactService.Application.Persons.Commands;

namespace ContactService.Test.Models
{
    public class DeletePersonCommandTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { new DeletePersonCommand(1) };
        yield return new object[] { new DeletePersonCommand(2) };

    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
}
}
