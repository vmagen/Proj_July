using DATA.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webAPI.Models
{
    public class GroupModel
    {
        public static List<RV_Group> GetAllGroups(ArvinoDbContext db)
        {
            return db.RV_Group.ToList();
            //Test ron
        }
    }
}