using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Application.Reports.Queries
{
    public class CreateReportExport
    {
        public string Path { get; set; }
        public Guid Uuid { get; set; }
        public int PersonsCount { get; set; }
        public int PhonesCount { get; set; }

    }
}
