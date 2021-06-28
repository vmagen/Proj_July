using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DATA.EF;
using webAPI.DTO;
using System.Data.Entity;

namespace webAPI.Models
{
    public class WineCategoryModel
    {
        public static RV_WineCategory GetCategory(int id, ArvinoDbContext db)
        {
            return db.RV_WineCategory.SingleOrDefault(x => x.categoryId == id);
        }

        public static List<WineCategoryDTO> GetAllCategories(ArvinoDbContext db)
        {
            return db.RV_WineCategory.Select(a => new WineCategoryDTO()
            {
                categoryId = a.categoryId,
                categoryName = a.categoryName
            }).ToList();
        }
    }
}