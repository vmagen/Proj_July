using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DATA.EF;
using webAPI.DTO;
using System.Data.Entity;

namespace webAPI.Models
{
    public class WineryModel
    {
        public static List<WineryDTO> GetWinery(ArvinoDbContext db)
        {
            return db.RV_Winery.Include(x => x.RV_AreaCategory)
                               .Select(e => new WineryDTO()
                               {
                                   wineryId = e.wineryId,
                                   wineryName = e.wineryName,
                                   wineryAddress = e.wineryAddress,
                                   wineryEmail = e.wineryEmail,
                                   phone = e.phone,
                                   wineryImage = e.IconImgPath,
                                   wineryAreaName = e.RV_AreaCategory.areaName,
                                   likes = db.RV_LikesUsers
                                            .Where(i => i.entityType == 3 && i.entityId == e.wineryId)
                                              .Select(dt => new LikesDTO()
                                              {
                                                  likeId = dt.ID,
                                                  userName = db.RV_User.FirstOrDefault(w => w.email == dt.email).Name,
                                                  userImage = db.RV_User.FirstOrDefault(w => w.email == dt.email).picture

                                              }).ToList(),
                                   wineList = db.RV_Wine
                                                .Where(i => i.wineryId == e.wineryId)
                                                .Select(w => new WineDTO()
                                                {
                                                    wineId = w.wineId,
                                                    wineName = w.wineName,
                                                    wineImgPath = w.wineImgPath,
                                                    content = w.content,
                                                    price = w.price,
                                                    wineLabelPath = w.wineLabelPath,
                                                    categoryId = w.categoryId ?? 0,

                                                }).ToList(),
                                   serviceList = db.RV_Service.Select(s => new ServiceDTO()
                                   {
                                       serviceId = s.serviceId,
                                       serviceName = s.serviceName,
                                       serviceCategory = s.serviceCategory,
                                       content = s.content,
                                       price = s.price,
                                       images = db.RV_ServiceImage
                                                         .Where(i => i.serviceId == s.serviceId)
                                                                    .Select(dt => new ServiceImageDTO()
                                                                    {
                                                                        imgId = dt.imgId,
                                                                        ImgPath = dt.ImgPath

                                                                    }).ToList()
                                   }).ToList()

                               }).ToList();
        }

        public static List<RV_Winery> GetWineryByArea(ArvinoDbContext db, int areaId)
        {
            return db.RV_Winery.Where(e => e.areaId == areaId).ToList();
        }

        public static RV_Winery GetWineryByUser(ArvinoDbContext db, string wineryManagerEmail)
        {
            return db.RV_Winery.SingleOrDefault(e => e.wineryManagerEmail == wineryManagerEmail);
        }

        public static WineryDTO GetWineryUser(string email, ArvinoDbContext db)
        {
            return db.RV_Winery.Where(x => email == x.wineryManagerEmail).Select(x => new WineryDTO()
            {
                wineryId = x.wineryId,
                wineryName = x.wineryName,
                wineryEmail = x.wineryEmail,
                wineryAddress = x.wineryAddress,
                phone = x.phone,
                wineryImage = x.IconImgPath,
                wineryAreaName = db.RV_AreaCategory.FirstOrDefault(y => y.areaId == x.areaId).areaName,
                email = db.RV_User.FirstOrDefault(u => u.email == email).email,
                Name = db.RV_User.FirstOrDefault(u => u.email == email).Name,
                password = db.RV_User.FirstOrDefault(u => u.email == email).password,
                userphone = db.RV_User.FirstOrDefault(u => u.email == email).phone
            }).Single();
            
        }
    }


}