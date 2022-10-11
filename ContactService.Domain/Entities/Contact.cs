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
    [Table("contact")]
    public class Contact : IEntityBase
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; }
        [Column("phonenumber")]

        public string PhoneNumber { get; set; }
        [Column("email")]

        public string Email { get; set; }
        [Column("location")]
        public string Location { get; set; }

        [Column("personid")]
        public int PersonId { get; set; }
        public Person Person { get; set; }


    }

}
