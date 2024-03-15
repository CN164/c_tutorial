using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ctutorial.Models
{
    [Table("users", Schema = "go_tutorial")]
    public class UsersEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(true)]
        [Column("id")]
        public long id { get; set; }
        [Column("user_name")]
        public string userName { get; set; }
        [Column("type_user")]
        public int typeUser { get; set; }
        [Column("created_at")]
        public DateTimeOffset? createdAt { get; set; }
        [Column("updated_at")]
        public DateTimeOffset? updatedAt { get; set; }
    }
}
