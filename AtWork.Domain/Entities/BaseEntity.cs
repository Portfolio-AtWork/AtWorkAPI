using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AtWork.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }

        [Required]
        [Column("dt_cad")]
        public DateTime DT_Cad { get; set; }

        [Required]
        [Column("dt_alt")]
        public DateTime DT_Alt { get; set; }
    }
}
