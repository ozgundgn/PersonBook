using ReportService.Application.Reports.Commands;
using ReportService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ReportService.Test.Models
{
    public class CreateReportCommandTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new CreateReportCommand { Uuid=Guid.NewGuid() } };
            yield return new object[] { new CreateReportCommand { Uuid = Guid.NewGuid() } };

        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }

}
