

using Core.Base;

namespace Contract.Repositories.Entity
{
    public class Homestay : BaseEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public int? TotalRooms { get; set; }
        public string? Field { get; set; }

        // Quan hệ
        public virtual ICollection<Room>? Rooms { get; set; }
        public virtual ICollection<Service>? Services { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
        public virtual ICollection<RevenueReport>? RevenueReports { get; set; }
    }

}
