using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class BaseEntity
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}