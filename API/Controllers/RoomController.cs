using Contract.Services.Interface;
using Core.Base;
using Core.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelViews.RoomModel;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }
        [HttpGet]
        public async Task<ActionResult<BaseResponse<BasePaginatedList<RoomModel>>>> GetRoomOfHomestay([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] int? homestayId = null)
        {
            try
            {

                var Rooms = await _roomService.GetRoomOfHomestay(pageNumber, pageSize, homestayId);
                return (bool)Rooms.IsSuccess ? Ok(Rooms) : BadRequest(Rooms);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new BaseResponse<BasePaginatedList<RoomModel>>(StatusCodeHelper.ServerError, "500", "Internal server error"));
            }
        }
        [HttpPost]
        public async Task<ActionResult<BaseResponse<RoomModel>>> CreateRoom([FromBody] CreateRoomModel model)
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null)
                {
                    return BadRequest(new BaseResponse<RoomModel>(StatusCodeHelper.Notfound, "400", "Invalid user"));
                }
                var result = await _roomService.CreateRoomAsync(model, userId.Value);
                return result.IsSuccess ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new BaseResponse<RoomModel>(StatusCodeHelper.ServerError, "500", "Internal server error"));
            }
        }
        private int? GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst("userId")?.Value ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out var userId))
            {
                return userId;
            }
            return null;
        }
    }
}
