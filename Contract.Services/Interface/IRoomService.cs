using Core.Base;
using ModelViews.RoomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Services.Interface
{
    public interface IRoomService
    {
        Task<BaseResponse<BasePaginatedList<RoomModel>>> GetRoomOfHomestay(int pageNumber, int pageSize, int? homestayId);
        Task<BaseResponse<RoomModel>> CreateRoomAsync(CreateRoomModel model, int userId);
    }
}
