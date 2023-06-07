
namespace Domain.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } 
        public bool SoftDelete { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}
