using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DATA.EF;
using webAPI.DTO;
using System.Data.Entity;

namespace webAPI.Models
{
    public class LikesModel
    {
        public static List<LikesDTO> GetLikeByEntityTypeAndID(ArvinoDbContext db, int entityType, int entityId)
        {
            try
            {
                if (entityId > 0 && entityType > 0)
                {
                    return db.RV_LikesUsers
                              .Where(i => i.entityType == entityType && i.entityId == entityId)
                                  .Select(dt => new LikesDTO()
                                  {
                                      likeId = dt.ID,
                                      userName = db.RV_User.FirstOrDefault(w => w.email == dt.email).Name,
                                      userImage = db.RV_User.FirstOrDefault(w => w.email == dt.email).picture

                                  }).ToList();
                }
                else
                {
                    throw new Exception("could not find entity ID or Type");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}