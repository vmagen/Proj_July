using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DATA.EF;
using System.Data.Entity;
using webAPI.DTO;

namespace webAPI.Models
{
    public class WineryCommentModel
    {
        public static List<RV_WineryCommand> GetAllWineryComments(int id, ArvinoDbContext db)
        {
            try
            {
                return db.RV_WineryCommand.Where(x => x.wineryId == id).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}