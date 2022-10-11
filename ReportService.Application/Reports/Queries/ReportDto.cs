using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Application.Reports.Queries
{
    public class ReportDto
    {
        public DateTime CreatedDate { get; set; }
        public int Id { get; set; }
        public string Path { get; set; }
        public int State { get; set; }
        public Guid Uuid { get; set; }


    }
}
