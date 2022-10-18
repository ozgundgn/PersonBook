using ReportService.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReportService.Domain.Entities
{
    [Table("report")]
    public class Report : IEntityBase
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        [Column("createddate")]
        public DateTime CreatedDate { get; set; }

        [Column("uuid")]
        public Guid Uuid { get; set; }
        [Column("state")]
        public int State { get; set; }
        [Column("path")]
        public string Path { get; set; }
    }
}
