using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using DATA.EF;
using webAPI.Models;
using webAPI.DTO;
using System.Net;
using System.Web.Http.Cors;

namespace webAPI.Controllers
{
    public class LikesController : ApiController
    {
        public static ArvinoDbContext db = new ArvinoDbContext();

        /// <summary>
        /// https://localhost:44370/api/Likes/PostLikeToEntity
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult PostLikeToEntity([FromBody] POSTLikeDTO value)
        {
            try
            {
                RV_EntityTypes type =  db.RV_EntityTypes.SingleOrDefault(i => i.ID == value.entityType);
                int retID = -1;
                if (type != null)
                {
                    switch (type.ID)
                    {
                        case 1: //Evnet
                            retID = UserModel.AddLikeToEventEntity(db, value);
                            break;
                        case 2: //Wine
                            retID = UserModel.AddLikeToWineEntity(db, value);
                            break;
                        case 3: //Winery
                            retID = UserModel.AddLikeToWineryEntity(db, value);
                            break;
                    }
                }


                return Content(HttpStatusCode.OK, retID);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}