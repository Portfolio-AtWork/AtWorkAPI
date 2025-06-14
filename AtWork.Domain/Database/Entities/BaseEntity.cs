using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace AtWork.Domain.Database.Entities
{
    [ExcludeFromCodeCoverage]
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
