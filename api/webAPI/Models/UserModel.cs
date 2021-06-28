using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DATA.EF;
using webAPI.DTO;
using System.Data.Entity;
using System.Threading.Tasks;

namespace webAPI.Models
{
    public class UserModel
    {
        public static RV_User GetUser(string email, ArvinoDbContext db)
        {
            return db.RV_User.SingleOrDefault(x => x.email == email);
        }

        public static int AddLikeToEventEntity(ArvinoDbContext db, POSTLikeDTO like)
        {
            try
            {
                string userEmail = db.RV_User.SingleOrDefault(i => i.email == like.userEmail).email;
                int eventID = db.RV_Event.SingleOrDefault(i => i.eventId == like.entityId).eventId;
                RV_LikesUsers lu = new RV_LikesUsers()
                {
                    entityType = 1,
                    entityId = eventID,
                    email = userEmail,
                    Date= DateTime.Now
                };
                db.RV_LikesUsers.Add(lu);
                db.SaveChanges();

                //Update userPrefrnces prefrence ID 6
                RV_UserPrefrences up = new RV_UserPrefrences()
                {
                    email = lu.email,
                    PrefrenceID = 6,  //Extract WineryID from event to update KNN Tables
                    FreeText = db.RV_Event.SingleOrDefault(i => i.eventId == lu.entityId).wineryId.ToString(),
                    registerAt = DateTime.Now
                };
                db.RV_UserPrefrences.Add(up);
                db.SaveChanges();

                //ASYNC UPDATE of KNN TABLE.
                new Task(() =>
                {
                    UserPrefrencesModel.updateKnnTablesAsync(up.email, up.FreeText, up.PrefrenceID);
                }).Start();
                return lu.ID;
            }
            catch
            {
                return -1;
            }
        }

        public static int AddLikeToWineryEntity(ArvinoDbContext db, POSTLikeDTO like)
        {
            try
            {
                string userEmail = db.RV_User.SingleOrDefault(i => i.email == like.userEmail).email;
                int wineryID = like.entityId;
                
                RV_LikesUsers lu = new RV_LikesUsers()
                {
                    entityType = 3,
                    entityId = wineryID,
                    email = userEmail,
                    Date = DateTime.Now
                };
                db.RV_LikesUsers.Add(lu);
                db.SaveChanges();

                //Update userPrefrnces prefrence ID 6
                RV_UserPrefrences up = new RV_UserPrefrences()
                {
                    email = lu.email,
                    PrefrenceID = 7,  // WineryID 
                    FreeText = wineryID.ToString(),
                    registerAt = DateTime.Now
                };
                db.RV_UserPrefrences.Add(up);
                db.SaveChanges();

                //ASYNC UPDATE of KNN TABLE.
                new Task(() =>
                {
                    UserPrefrencesModel.updateKnnTablesAsync(up.email, up.FreeText, up.PrefrenceID);
                }).Start();
                return lu.ID;
            }
            catch
            {
                return -1;
            }
        }

        public static int AddLikeToWineEntity(ArvinoDbContext db, POSTLikeDTO like)
        {
            try
            {
                string userEmail = db.RV_User.SingleOrDefault(i => i.email == like.userEmail).email;
                int wineID = like.entityId;

                RV_LikesUsers lu = new RV_LikesUsers()
                {
                    entityType = 2,
                    entityId = wineID,
                    email = userEmail,
                    Date = DateTime.Now
                };
                db.RV_LikesUsers.Add(lu);
                db.SaveChanges();

                //Update userPrefrnces prefrence ID 6
                RV_UserPrefrences up = new RV_UserPrefrences()
                {
                    email = lu.email,
                    PrefrenceID = 5,  // WineryID 
                    FreeText = wineID.ToString(),
                    registerAt = DateTime.Now
                };
                db.RV_UserPrefrences.Add(up);
                db.SaveChanges();

                //ASYNC UPDATE of KNN TABLE.
                new Task(() =>
                {
                    UserPrefrencesModel.updateKnnTablesAsync(up.email, up.FreeText, up.PrefrenceID);
                }).Start();
                return lu.ID;
            }
            catch
            {
                return -1;
            }
        }
    }
}