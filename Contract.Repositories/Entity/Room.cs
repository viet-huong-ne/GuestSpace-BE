

using Core.Base;
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contract.Repositories.Entity
{
    public class Room : BaseEntity
    {
        public int Id { get; set; }
        public string? RoomName { get; set; }
        public string? Description { get; set; }
        public int? Capacity { get; set; }
        public decimal? PricePerNight { get; set; }
        public bool? IsAvailable { get; set; }
        public virtual ICollection<RoomImage> RoomImages { get; set; }
        public int HomestayId { get; set; }

        [ForeignKey(nameof(HomestayId))]
        public virtual Homestay? Homestay { get; set; }  
        public virtual ICollection<Booking>? Bookings { get; set; }
    }

}
