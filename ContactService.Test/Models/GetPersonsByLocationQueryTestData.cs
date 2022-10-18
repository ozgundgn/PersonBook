using ContactService.Application.Persons.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Test.Models
{
    public class GetPersonsByLocationQueryTestData:IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new GetPersonsByLocationQuery { Location = "Kırklareli" } };
            yield return new object[] { new GetPersonsByLocationQuery { Location = "Edirne" } };

        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }


}
