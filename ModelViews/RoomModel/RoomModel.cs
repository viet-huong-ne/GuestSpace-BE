using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelViews.RoomModel
{
    public class RoomModel
    {
    }
    public class CreateRoomModel
    {
        public string? RoomName { get; set; }
        public string? Description { get; set; }
        public int? Capacity { get; set; }
        public decimal? PricePerNight { get; set; }
        public bool? IsAvailable { get; set; } = true;

        /// <summary>
        /// Danh sách URL ảnh phòng
        /// </summary>
        public List<string>? RoomImages { get; set; } = new List<string>();

        /// <summary>
        /// Ảnh nào là ảnh chính (theo chỉ số trong danh sách RoomImages)
        /// </summary>
        public int MainImageIndex { get; set; } = 0;

        public int HomestayId { get; set; }
    }
    public class UpdateRoomModel
    {
        public int Id { get; set; }

        public string? RoomName { get; set; }
        public string? Description { get; set; }
        public int? Capacity { get; set; }
        public decimal? PricePerNight { get; set; }
        public bool? IsAvailable { get; set; }

        /// <summary>
        /// Danh sách URL ảnh mới, có thể null => không cập nhật ảnh
        /// </summary>
        public List<string>? RoomImages { get; set; }

        /// <summary>
        /// Ảnh chính sau khi update (chỉ áp dụng khi RoomImages != null)
        /// </summary>
        public int? MainImageIndex { get; set; } = 0;
    }

}
