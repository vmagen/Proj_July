using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DATA.EF;
using webAPI.DTO;
using System.Data.Entity;

namespace webAPI.Models
{
    public class RateModel
    {

        public static List<RateDTO> GetWineRateByWinery(int wineryId, ArvinoDbContext db)
        {
            RV_Rate[] rate = db.RV_Rate.OrderBy(x => x.wineId).Where(x => x.RV_Wine.wineryId == wineryId).ToArray();
            List<RateDTO> rates = new List<RateDTO> { };
            int score = 0;
            for (int i = 0; i < rate.Length; i++)
            {
                for (int j = 0; j < rate.Length; j++)
                {
                    if (rate[i].wineId == rate[j].wineId)
                    {
                        score += rate[j].score;
                    }
                    else
                    {
                        RateDTO r = new RateDTO
                        {
                            wineId = rate[i].wineId,
                            score = (score / i + 1)
                        };
                        rates.Add(r);
                        continue;
                    }
                }
            }
            return rates;
        }

        public static List<RateDTO> GetWineryInfoRates(int wineryId, ArvinoDbContext db)
        {
            try
            {
                return db.RV_Rate
                    .Include(x => x.RV_Wine)
                        .Where(x => x.RV_Wine.wineryId == wineryId)
                        .OrderByDescending(x => x.score)
                        .Select(w => new RateDTO()
                        {
                            rateId = w.rateId,
                            rateDate = w.rateDate,
                            score = w.score,
                            wineId = w.wineId,
                            ratedByUserEmail = w.ratedByUserEmail,
                            userName = db.RV_User.FirstOrDefault(e => e.email == w.ratedByUserEmail).Name,
                            UserPitcure = db.RV_User.FirstOrDefault(e => e.email == w.ratedByUserEmail).picture,
                            winePitcure = db.RV_Wine.FirstOrDefault(e => e.wineId == w.wineId).wineImgPath
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool IsUserAllowedToPublish(int wineId, string email, int days, ArvinoDbContext db)
        {
            try
            {
                bool isAllowed = false;
                var elements = db.RV_WineComment.Where(i => i.wineId == wineId && i.email == email);
                bool exists = elements.Count() > 0 ? true : false;
                if (!exists)
                    isAllowed = true; //ALLOWED
                else if (exists) //Check last message and make sure that it is more than X days
                {
                    RV_WineComment lastComment = db.RV_WineComment
                        .Where(i => i.email == email && i.wineId == wineId)
                            .OrderByDescending(i => i.date)
                                .ToList()
                                    .First();
                    if (DateTime.Now.Subtract(lastComment.date).TotalDays >= days)
                    {
                        isAllowed = true;
                    }
                }
                return isAllowed;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}