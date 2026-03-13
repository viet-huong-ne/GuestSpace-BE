using AutoMapper;
using Contract.Repositories.Entity;
using Contract.Repositories.Interface;
using Contract.Services.Interface;
using Core.Base;
using Core.Store;
using Microsoft.EntityFrameworkCore;
using ModelViews.RoomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RoomService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseResponse<RoomModel>> CreateRoomAsync(CreateRoomModel model, int userId)
        {
            try
            {
                // Map từ model sang entity
                var room = _mapper.Map<Room>(model);
                room.HomestayId = model.HomestayId;

                // Đảm bảo trạng thái mặc định
                room.IsAvailable = model.IsAvailable ?? true;

                // Thêm phòng trước để có Id
                await _unitOfWork.GetRepository<Room>().InsertAsync(room);
                await _unitOfWork.SaveAsync();

                // Nếu có ảnh thì thêm vào RoomImage
                if (model.RoomImages != null && model.RoomImages.Any())
                {
                    var roomImages = model.RoomImages.Select((url, index) => new RoomImage
                    {
                        ImageUrl = url,
                        RoomId = room.Id,
                        DisplayOrder = index,
                        IsMain = (index == model.MainImageIndex)
                    }).ToList();
                    foreach(var image in roomImages) {
                        await _unitOfWork.GetRepository<RoomImage>().InsertAsync(image);
                    }                    
                    await _unitOfWork.SaveAsync();
                }

                // Map lại sang RoomModel để trả về
                var roomDto = _mapper.Map<RoomModel>(room);

                return new BaseResponse<RoomModel>(StatusCodeHelper.Created, "201", roomDto);
            }
            catch (Exception ex)
            {
                return new BaseResponse<RoomModel>(StatusCodeHelper.ServerError, "500",
                    $"An error occurred while creating the room: {ex.Message}");
            }
        }

        public async Task<BaseResponse<BasePaginatedList<RoomModel>>> GetRoomOfHomestay(int pageNumber, int pageSize, int? homestayId)
        {
            try
            {
                if (pageNumber < 1) pageNumber = 1;
                if (pageSize < 1) pageSize = 10;

                var ordersQuery = _unitOfWork.GetRepository<Room>()
                    .Entities.Include(o => o.Homestay).Include(p => p.RoomImages).Where(c => !c.DeletedTime.HasValue)
                    .AsQueryable();
                if (homestayId.HasValue)
                {
                    ordersQuery = ordersQuery.Where(b => b.Homestay.Id == homestayId);
                }
                //if (!string.IsNullOrEmpty(name))
                //{
                //    ordersQuery = ordersQuery.Where(b => b.Name.Contains(name));
                //}

                var paginatedItems = await ordersQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
                var roomDtos = _mapper.Map<List<RoomModel>>(paginatedItems);
                int total = roomDtos.Count();
                var RoomResults = new BasePaginatedList<RoomModel>(roomDtos, total, pageNumber, pageSize);
                return new BaseResponse<BasePaginatedList<RoomModel>>(StatusCodeHelper.OK, "200", RoomResults);
            }
            catch (Exception ex)
            {

                return new BaseResponse<BasePaginatedList<RoomModel>>(StatusCodeHelper.Notfound, "400", "An error occured while retrieving the Room");
            }
        }
        public async Task UpdateRoomAsync(UpdateRoomModel model)
        {
            try
            {
                // Tìm room cần cập nhật
                var room = await _unitOfWork.GetRepository<Room>().GetByIdAsync(model.Id);
                if (room == null)
                    throw new KeyNotFoundException($"Room với Id {model.Id} không tồn tại.");

                // Cập nhật các trường nếu có giá trị mới
                if (!string.IsNullOrWhiteSpace(model.RoomName) && model.RoomName != room.RoomName)
                    room.RoomName = model.RoomName;

                if (!string.IsNullOrWhiteSpace(model.Description) && model.Description != room.Description)
                    room.Description = model.Description;

                if (model.Capacity.HasValue && model.Capacity.Value != room.Capacity)
                    room.Capacity = model.Capacity.Value;

                if (model.PricePerNight.HasValue && model.PricePerNight.Value != room.PricePerNight)
                    room.PricePerNight = model.PricePerNight.Value;

                if (model.IsAvailable.HasValue && model.IsAvailable.Value != room.IsAvailable)
                    room.IsAvailable = model.IsAvailable.Value;

                // Nếu RoomImages != null → thay toàn bộ danh sách ảnh
                if (model.RoomImages != null)
                {
                    // Lấy ảnh cũ
                    var existingImages = await _unitOfWork.GetRepository<RoomImage>().Entities.Where(x => x.RoomId == room.Id).ToListAsync();

                    // Xóa từng ảnh cũ
                    foreach (var img in existingImages)
                        await _unitOfWork.GetRepository<RoomImage>().DeleteAsync(img.Id);

                    // Thêm từng ảnh mới
                    for (int i = 0; i < model.RoomImages.Count; i++)
                    {
                        var roomImage = new RoomImage
                        {
                            ImageUrl = model.RoomImages[i],
                            RoomId = room.Id,
                            DisplayOrder = i + 1,
                            IsMain = model.MainImageIndex.HasValue && model.MainImageIndex.Value == i
                        };

                        await _unitOfWork.GetRepository<RoomImage>().InsertAsync(roomImage);
                    }
                }
                else if (model.MainImageIndex.HasValue)
                {
                    // Không thay đổi danh sách ảnh nhưng thay đổi ảnh chính
                    var images = await _unitOfWork.GetRepository<RoomImage>().Entities.Where(x => x.RoomId == room.Id).ToListAsync();

                    foreach (var img in images)
                    {
                        img.IsMain = false;
                        await _unitOfWork.GetRepository<RoomImage>().UpdateAsync(img);
                    }

                    var mainImage = images.ElementAtOrDefault(model.MainImageIndex.Value);
                    if (mainImage != null)
                    {
                        mainImage.IsMain = true;
                        await _unitOfWork.GetRepository<RoomImage>().UpdateAsync(mainImage);
                    }
                }

                // Cập nhật phòng
                await _unitOfWork.GetRepository<Room>().UpdateAsync(room);

                // Lưu thay đổi
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật phòng", ex);
            }
        }


    }
}
