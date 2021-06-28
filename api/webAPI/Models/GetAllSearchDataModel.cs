using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DATA.EF;
using webAPI.DTO;
using System.Data.Entity;

namespace webAPI.Models
{
    public class GetAllSearchDataModel
    {
        public static object GetAllData(ArvinoDbContext db)
        {


            List<GetAllSearchDataDTO> wineANDwinery = new List<GetAllSearchDataDTO>(db.RV_Wine.Include(x => x.RV_Winery).Select(data => new GetAllSearchDataDTO()
            {
                wineId = data.wineId,
                wineName = data.wineName,
                wineImgPath = data.wineImgPath,
                content = data.content,
                price = data.price,
                wineLabelPath = data.wineLabelPath,
                wineCategoryName = db.RV_WineCategory.FirstOrDefault(i => i.categoryId == data.categoryId).categoryName,
                wineryId = data.wineryId,
                wineryName = db.RV_Winery.FirstOrDefault(i => i.wineryId == data.wineryId).wineryName,
                IconImgPath = db.RV_Winery.FirstOrDefault(i => i.wineryId == data.wineryId).IconImgPath,
                wineryArea = db.RV_AreaCategory
                       .FirstOrDefault(i => i.areaId == db.RV_Winery
                           .FirstOrDefault(r => r.wineryId == data.wineryId).areaId)
                               .areaName,

            }).ToList());

            List<GetAllSearchDataDTO> events = new List<GetAllSearchDataDTO>(db.RV_Event.Select(e => new GetAllSearchDataDTO()
            {
                eventId = e.eventId,
                eventName = e.eventName,
                content = e.content,
                price = e.price,
                participantsAmount = e.participantsAmount,
                eventDate = e.eventDate,
                startTime = e.startTime,
                eventImgPath = e.eventImgPath,
                eventCategoryName = db.RV_EventCategory.FirstOrDefault(c => c.categoryId == e.categoryId).categoryName
            }).ToList());

            List<GetAllSearchDataDTO> groups = new List<GetAllSearchDataDTO>(db.RV_Group.Select(g => new GetAllSearchDataDTO()
            {
                groupId = g.groupId,
                groupName = g.groupName,
                groupDescription = g.groupDescription,
                ImgPath = g.ImgPath
            }).ToList());

            var newList = wineANDwinery.Concat(events).Concat(groups);
            return newList;
        }
    }
}

