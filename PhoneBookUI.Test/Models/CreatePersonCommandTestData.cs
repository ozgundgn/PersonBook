using ContactService.Application.Persons.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookUI.Test.Models
{
    public class CreatePersonCommandTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new CreatePersonCommand { Name="Ayşe",Surname="Demir",Company="Rise"} };
            //yield return new object[] { new CreatePersonCommand { Name = "Elif", Surname = "Gür", Company = "Rise" } };

        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
