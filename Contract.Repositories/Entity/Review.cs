

using Core.Base;

namespace Contract.Repositories.Entity
{
    public class Review : BaseEntity
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? HomestayId { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }
        public virtual User? User { get; set; }
        public virtual Homestay? Homestay { get; set; }
    }

}
