using Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Repositories.Entity
{
    public class Payment : BaseEntity
    {
        public int Id { get; set; }
        public int? BookingId { get; set; }
        public string? PaymentMethod { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? Status { get; set; }
        public virtual Booking? Booking { get; set; }
    }

}
