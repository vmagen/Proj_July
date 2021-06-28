using DATA.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data;
using System.Reflection;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;

namespace webAPI.Models
{
    public class UserPrefrencesModel
    {
        public static ArvinoDbContext db = new ArvinoDbContext();


        public static void updateKnnTablesAsync(string email, string freeText, int prefrenceId)
        {
            try
            {
                decimal? prefrenceRank = db.RV_PrefrencesType.Single(i => i.ID == prefrenceId).internalRank;
                switch (prefrenceId)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 5:
                        SaveWinePrefrences(email, freeText, prefrenceRank);
                        break;
                    case 4:
                    case 6:
                    case 7:
                        SaveWineryPrefrences(email, freeText, prefrenceRank);
                        break;
                }
            }
            catch (Exception ex)
            {
                //Add warning
            }
        }

       

        private static void SaveWinePrefrences(string email, string freeText, decimal? internalRank)
        {
            try
            {
                int temp = freeText.Contains(",") ? Convert.ToInt32(freeText.Split(',')[0].Trim()) : Convert.ToInt32(freeText);
                int rate = freeText.Contains(",") ? Convert.ToInt32(freeText.Split(',')[1].Trim()) : 0;
                RV_Wine singleW = db.RV_Wine.Single(i => i.wineId == temp);
                if (singleW != null)
                {
                    RV_WineCategory category = db.RV_WineCategory.Single(i => i.categoryId == singleW.categoryId);
                    int categoryId = category.categoryId;
                    string columnName = "";
                    switch (categoryId)
                    {
                        case 1:
                            columnName = "red";
                            break;
                        case 2:
                            columnName = "rose";
                            break;
                        case 3:
                            columnName = "white";
                            break;
                        case 4:
                            columnName = "bubble";
                            break;
                    }

                    updateSingleLine(email, columnName, internalRank, rate);
                }

            }
            catch (Exception ex)
            {
                // throw new Exception("Cant find the wine by id  , countinue");
            }
        }


        private static void SaveWineryPrefrences(string email, string freeText, decimal? internalRank)
        {
            try
            {

                int temp = Convert.ToInt32(freeText);

                RV_Winery singleWinery = db.RV_Winery.Single(i => i.wineryId == temp);
                if (singleWinery != null)
                {
                    RV_AreaCategory category = db.RV_AreaCategory.Single(i => i.areaId == singleWinery.areaId);
                    int areaId = category.areaId;
                    string columnName = "";
                    switch (areaId)
                    {
                        case 1:
                            columnName = "Galil";
                            break;
                        case 2:
                            columnName = "Golan";
                            break;
                        case 3:
                            columnName = "Carmel";
                            break;
                        case 4:
                            columnName = "shomron";
                            break;
                        case 5:
                            columnName = "yehuda";
                            break;
                        case 6:
                            columnName = "negev";
                            break;
                    }

                    updateSingleLine(email, columnName, internalRank);
                }

            }
            catch (Exception ex)
            {
                // throw new Exception("Cant find the wine by id  , countinue");
            }
        }

        private static void updateSingleLine(string email, string columnName, decimal? internalRank, int rate = 0)
        {
            RV_KNNCategory knnUser = db.RV_KNNCategory.SingleOrDefault(i => i.email == email);
            if (knnUser == null)
            {
                DATA.Extention.RV_KNNCategory.AddNewUser(db, new RV_KNNCategory(), email);
                
            }

            UpdateExistsUser(email, columnName, internalRank, rate);
           
        }

        private static void UpdateExistsUser(string email, string columnName, decimal? internalRank, int rate = 0)
        {
            RV_KNNCategory knnUser = db.RV_KNNCategory.SingleOrDefault(i => i.email == email);

            using (var dbx = new ArvinoDbContext())
            {
                var temp1 = dbx.Database.SqlQuery<int>($"select {columnName} from RV_KNNCategory where email='{email}'")
                       .FirstOrDefault();
                internalRank = internalRank != null ? internalRank : 0;
                int? final = Convert.ToInt32(temp1) + Convert.ToInt32(internalRank) + rate;

                int noOfRowUpdated = dbx.Database.ExecuteSqlCommand($"Update RV_KNNCategory set {columnName} = {final} where email='{email}'");
            }


        }

        public static IEnumerable<KeyValuePair<string, double>> CalculateProximity(string email)
        {
            Dictionary<string, double> kList = new Dictionary<string, double>();
            try
            {
                var single = db.RV_KNNCategory.Single(i => i.email == email);
                if (db.RV_KNNCategory.Count() > 9)
                {
                    kList.Add(email, 0);
                    double k = Math.Round(Math.Sqrt(db.RV_KNNCategory.Count()), 0);
                    // remove the mail from list
                    var newList = db.RV_KNNCategory.Where(i => i.email != email);
                    foreach (RV_KNNCategory item in newList)
                    {
                        double sum = 0;
                        sum += Math.Pow(Convert.ToDouble((single.red - item.red)), 2);
                        sum += Math.Pow(Convert.ToDouble((single.white - item.white)), 2);
                        sum += Math.Pow(Convert.ToDouble((single.rose - item.rose)), 2);
                        sum += Math.Pow(Convert.ToDouble((single.bubble - item.bubble)), 2);
                        sum += Math.Pow(Convert.ToDouble((single.Galil - item.Galil)), 2);
                        sum += Math.Pow(Convert.ToDouble((single.Golan - item.Golan)), 2);
                        sum += Math.Pow(Convert.ToDouble((single.negev - item.negev)), 2);
                        sum += Math.Pow(Convert.ToDouble((single.shomron - item.shomron)), 2);
                        sum += Math.Pow(Convert.ToDouble((single.yehuda - item.yehuda)), 2);
                        sum += Math.Pow(Convert.ToDouble((single.Carmel - item.Carmel)), 2);

                        kList.Add(item.email, Math.Sqrt(sum));
                    }
                    k = k < 10 ? 10 : k;
                    return  kList.Where(i=>i.Key != email)
                                    .OrderBy(i => i.Value).Take(Convert.ToInt32(k));
                }
                else
                {
                    throw new Exception("Not enough data! minimum 9 record in table");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }



}