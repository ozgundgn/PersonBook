using ContactService.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Domain.Entities
{
    [Table("person")]
    public class Person : IEntityBase
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]

        public int Id { get; }

        [Column("uuid")]
        public Guid Uuid { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("surname")]
        public string Surname { get; set; }
        [Column("company")]
        public string? Company { get; set; }
        public virtual ICollection<Contact>? Contacts { get; set; }
    }
}
