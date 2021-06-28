using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DATA.EF;
using webAPI.DTO;
using System.Data.Entity;

namespace webAPI.Models
{
    public class AreaCategoryModel
    {
        public static List<AreaCategoryDTO> GetAreaCategories(ArvinoDbContext db)
        {
            return db.RV_AreaCategory.Select(a => new AreaCategoryDTO()
            {
                areaId = a.areaId,
                areaName = a.areaName
            }).ToList();
        }
    }
}