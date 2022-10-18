using ReportService.Application.Reports.Commands;

namespace ReportService.Test.Models
{
    public class DeleteReportCommandTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new DeleteReportCommand(1) };

        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
