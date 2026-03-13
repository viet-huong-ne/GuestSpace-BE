using Core.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Repositories.Entity
{
    public class RoomImage : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string ImageUrl { get; set; } = string.Empty;
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "DisplayOrder must be a non-negative number")]
        public int DisplayOrder { get; set; } = 0;

        /// <summary>
        /// Đánh dấu đây có phải là ảnh chính của phòng không
        /// </summary>
        [Required]
        public bool IsMain { get; set; } = false;
        public int RoomId { get; set; }

        [ForeignKey(nameof(RoomId))]
        public virtual Room Room { get; set; } = null!;
    }

}

