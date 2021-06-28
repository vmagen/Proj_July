using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DATA.EF;
using webAPI.DTO;
using System.Data.Entity;

namespace webAPI.Models
{
    public class ServiceImageModel
    {
        public static RV_ServiceImage GetServiceImage(int id, ArvinoDbContext db)
        {
            return db.RV_ServiceImage.Include(x => x.RV_Service).SingleOrDefault(x => x.imgId == id);
        }

        public static List<ServiceImageDTO> GetAllServiceImages(int id, ArvinoDbContext db)
        {
            return db.RV_ServiceImage.Where(i => i.serviceId == id).Select(value => new ServiceImageDTO()
            {
                ImgPath = value.ImgPath,
                imgId = value.imgId
            }).ToList();
        }
    }
}