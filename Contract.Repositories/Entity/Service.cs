using Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Repositories.Entity
{
    public class Service : BaseEntity
    { 
        public int Id { get; set; }
        public int? HomestayId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public bool? IsActive { get; set; }

        [ForeignKey(nameof(HomestayId))]
        public virtual Homestay? Homestay { get; set; }
        public virtual ICollection<BookingService>? BookingServices { get; set; }
    }

}
