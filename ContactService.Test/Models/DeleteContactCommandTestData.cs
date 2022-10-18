using ContactService.Application.Contacts.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Test.Models
{
    public class DeleteContactCommandTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new DeleteContactCommand(1) };
            yield return new object[] { new DeleteContactCommand (2) };
            yield return new object[] { new DeleteContactCommand (3) };
            yield return new object[] { new DeleteContactCommand (4) };
            yield return new object[] { new DeleteContactCommand (90) };

        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
