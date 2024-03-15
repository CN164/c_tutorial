using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ctutorial.Models
{
    [Table("accounting", Schema = "go_tutorial")]
    public class AccountingEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(true)]
        [Column("id")]
        public long id { get; set; }
        [Column("amount")]
        public decimal amount { get; set; }
        [Column("balance")]
        public decimal balance { get; set; }
        [Column("user_id")]
        public long userId { get; set; }
        [Column("outdated_by")]
        public long? outdatedBy { get; set; }
        [Column("created_at")]
        public DateTimeOffset? createdAt { get; set; }
        [Column("updated_at")]
        public DateTimeOffset? updatedAt { get; set; }
        [ForeignKey(nameof(userId))]
        public UsersEntity usersEntity { get; set; }
    }
}
