using ContactService.Application.Persons.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Test.Models
{
    public class CreatePersonCommandTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new CreatePersonCommand { Uuid = Guid.NewGuid(),Company = "Company1",Name = "ERHAN", Surname = "Özek" } };
            yield return new object[] { new CreatePersonCommand { Uuid = Guid.NewGuid(), Company = "Company2", Name = "Özgür", Surname = "Nebi" } };
            yield return new object[] { new CreatePersonCommand { Uuid = Guid.NewGuid(), Company = "Company3", Name = "Sinan", Surname = "Can" } };

        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
