using Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Repositories.Entity
{
    public class RevenueReport : BaseEntity
    {
        public int Id { get; set; }
        public int? HomestayId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? TotalBookings { get; set; }
        public decimal? TotalRevenue { get; set; }

        // Quan hệ
        public virtual Homestay? Homestay { get; set; }
    }

}
