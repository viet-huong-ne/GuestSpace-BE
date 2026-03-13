

using Core.Base;

namespace Contract.Repositories.Entity
{
    public class BookingService : BaseEntity
    {
        public int Id { get; set; }
        public int? BookingId { get; set; }
        public int? ServiceId { get; set; }
        public int? Quantity { get; set; }
        public int? TotalPrice { get; set; }
        public virtual Booking? Booking { get; set; }
        public virtual Service? Service { get; set; }
    }

}
