using Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Repositories.Entity
{
    public class Booking : BaseEntity
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? RoomId { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public string? Status { get; set; }
        public string? PaymentStatus { get; set; }
        public virtual User? User { get; set; }
        public virtual Room? Room { get; set; }
        public virtual ICollection<BookingService>? BookingServices { get; set; }
        public virtual ICollection<Payment>? Payments { get; set; }
    }

}
