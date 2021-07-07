using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DATA.EF;
using webAPI.DTO;
using System.Data.Entity;

namespace webAPI.Models
{
    public class WineModel
    {
        public static List<WineDTO> GetWineByCategory(int WineCategoryId, ArvinoDbContext db)
        {
            return db.RV_Wine.Where(i => i.categoryId == WineCategoryId)
                    .Select(w => new WineDTO()
                    {
                        wineId = w.wineId,
                        wineName = w.wineName,
                        wineImgPath = w.wineImgPath,
                        content = w.content,
                        price = w.price,
                        wineLabelPath = w.wineLabelPath,
                        categoryId = w.categoryId ?? 0,
                        wineryId = w.wineryId,
                        wineryName = db.RV_Winery.FirstOrDefault(i => i.wineryId == w.wineryId).wineryName,
                        wineryImage = db.RV_Winery.FirstOrDefault(i => i.wineryId == w.wineryId).IconImgPath,
                        areaCategoryName = db.RV_AreaCategory
                        .FirstOrDefault(i => i.areaId == db.RV_Winery
                            .FirstOrDefault(r => r.wineryId == w.wineryId).areaId)
                                .areaName,

                    }).ToList();
        }

        public static List<WineDTO> GetAllWines(ArvinoDbContext db)
        {
            List<WineDTO> wineList = new List<WineDTO>();
            foreach (var item in db.RV_Wine)
            {
                try
                {
                    wineList.Add(getWIneByID(item.wineId, db));
                }
                catch
                {
                    break;
                }
            }

            return wineList;
        }

        public static List<WineCommentDTO> GetAllWineComments(int wineId, ArvinoDbContext db)
        {
            try
            {

                return db.RV_WineComment
                    .OrderByDescending(i => i.date)
                        .Where(i => i.wineId == wineId)
                            .Select(w => new WineCommentDTO()
                            {
                                id = w.id,
                                email = w.email,
                                text = w.text,
                                date = w.date,
                                wineId = w.wineId,
                                userName = db.RV_User.FirstOrDefault(e => e.email == w.email).Name,
                                UserPitcure = db.RV_User.FirstOrDefault(e => e.email == w.email).picture
                            }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<WineCommentDTO> GetAllWineryWineComments(int wineryId, ArvinoDbContext db)
        {
            try
            {
                return db.RV_WineComment
                    .Include(x => x.RV_Wine)
                        .Where(x => x.RV_Wine.wineryId == wineryId)
                        .Select(w => new WineCommentDTO()
                        {
                            id = w.id,
                            email = w.email,
                            text = w.text,
                            date = w.date,
                            wineId = w.wineId,
                            userName = db.RV_User.FirstOrDefault(e => e.email == w.email).Name,
                            UserPitcure = db.RV_User.FirstOrDefault(e => e.email == w.email).picture,
                            wineImgPath = db.RV_Wine.FirstOrDefault(e => e.wineId == w.wineId).wineImgPath

                        }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static WineDTO getWIneByID(int wineId, ArvinoDbContext db)
        {
            try
            {
                return db.RV_Wine.Where(i => i.wineId == wineId)
                            .Select(w => new WineDTO()
                            {
                                wineId = w.wineId,
                                wineName = w.wineName,
                                wineImgPath = w.wineImgPath,
                                content = w.content,
                                price = w.price,
                                wineLabelPath = w.wineLabelPath,
                                categoryId = w.categoryId ?? 0,
                                wineryId = w.wineryId,
                                wineryName = db.RV_Winery.FirstOrDefault(i => i.wineryId == w.wineryId).wineryName,
                                wineryImage = db.RV_Winery.FirstOrDefault(i => i.wineryId == w.wineryId).IconImgPath,
                                areaCategoryName = db.RV_AreaCategory
                                            .FirstOrDefault(i => i.areaId == db.RV_Winery
                                                .FirstOrDefault(r => r.wineryId == w.wineryId).areaId)
                                                    .areaName,
                                rate = db.RV_WineComment
                                        .Where(i => i.wineId == w.wineId)
                                                .Select(i => i.rate)
                                                    .Average(),
                                likes = db.RV_LikesUsers
                                            .Where(i => i.entityType == 2 && i.entityId == w.wineId)
                                              .Select(dt => new LikesDTO()
                                              {
                                                  likeId = dt.ID,
                                                  userName = db.RV_User.FirstOrDefault(x => x.email == dt.email).Name,
                                                  userImage = db.RV_User.FirstOrDefault(x => x.email == dt.email).picture

                                              }).ToList(),
                            }).First();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }







        public static List<WineDTO> GetWineryWines(int id, ArvinoDbContext db)
        {
            return db.RV_Wine.Where(i => i.wineryId == id).Select(w => new WineDTO()
            {
                wineId = w.wineId,
                wineName = w.wineName,
                wineImgPath = w.wineImgPath,
                content = w.content,
                price = w.price,
                categoryId = w.categoryId ?? 0,
                wineryId = w.wineryId,
                categoryName = db.RV_WineCategory.FirstOrDefault(i => i.categoryId == w.categoryId).categoryName,
            }).ToList();
        }

        public static List<RV_WineCategory> GetCategory(ArvinoDbContext db)
        {
            return db.RV_WineCategory.ToList();
        }

        public static List<WineDTO> sortCategory(int id, int wineryId, ArvinoDbContext db)
        {
            return db.RV_Wine.Where(c => c.categoryId == id && c.wineryId == wineryId).Select(w => new WineDTO()
            {
                wineId = w.wineId,
                wineName = w.wineName,
                wineImgPath = w.wineImgPath,
                content = w.content,
                price = w.price,
                categoryId = w.categoryId ?? 0,
                wineryId = w.wineryId,
                categoryName = db.RV_WineCategory.FirstOrDefault(i => i.categoryId == w.categoryId).categoryName,
            }).ToList();
        }
    }
}