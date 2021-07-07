using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DATA.EF;
using webAPI.DTO;
using System.Data.Entity;

namespace webAPI.Models
{
    public class EventCategoryModel
    {
        public static RV_EventCategory GetCategory(int id, ArvinoDbContext db)
        {
            return db.RV_EventCategory.SingleOrDefault(x => x.categoryId == id);
        }

        public static RV_EventCategory GetCategoryId(string name, ArvinoDbContext db)
        {
            return db.RV_EventCategory.SingleOrDefault(x => x.categoryName == name);
        }

        public static List<EventCategoryDTO> GetAllCategories(ArvinoDbContext db)
        {
            return db.RV_EventCategory.Select(a => new EventCategoryDTO()
            {
                categoryId = a.categoryId,
                categoryName = a.categoryName
            }).ToList();
        }
    }
}