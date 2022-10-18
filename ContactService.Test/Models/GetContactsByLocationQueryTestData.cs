using ContactService.Application.Contacts.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Test.Models
{
    internal class GetContactsByLocationQueryTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new GetContactsByLocationQuery { Location="Kırklareli"} };
            yield return new object[] { new GetContactsByLocationQuery { Location = "Edirne" } };

        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
