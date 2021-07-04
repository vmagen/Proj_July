using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATA.EF;

namespace DATA.Extention
{
    public partial class RV_KNNCategory
    {
        public  static void AddNewUser(ArvinoDbContext db,  DATA.EF.RV_KNNCategory newRecord,  string email)
        {
            try
            {
                DATA.EF.RV_KNNCategory single = db.RV_KNNCategory.SingleOrDefault(i => i.email == email);
                if(single == null)
                    {
                    newRecord.email = email;
                    newRecord.red = 0;
                    newRecord.white = 0;
                    newRecord.rose = 0;
                    newRecord.bubble = 0;
                    newRecord.negev = 0;
                    newRecord.Galil = 0;
                    newRecord.Golan = 0;
                    newRecord.shomron = 0;
                    newRecord.Carmel = 0;
                    newRecord.yehuda = 0;

                    db.RV_KNNCategory.Add(newRecord);
                    db.SaveChanges();
                }

            }
            catch { }
        }
    }
}
