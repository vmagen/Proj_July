using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using DATA.EF;
using webAPI.Models;
using webAPI.DTO;

namespace webAPI.Controllers
{
    public class GroupController : ApiController
    {
        public static ArvinoDbContext db = new ArvinoDbContext();

        /// <summary>
        /// https://localhost:44370/api/Group
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(GroupModel.GetAllGroups(db));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }


        /// <summary>
        /// https://localhost:44370/api/Group/PostGroup
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult PostGroup([FromBody] RV_Group value)
        {
            try
            {
                //does group exists?
                RV_Group u = db.RV_Group.SingleOrDefault(i=>i.groupName == value.groupName);

                if (u == null)
                {
                    if (value.groupName != null 
                        && value.groupDescription != null 
                            && value.ImgPath != null
                                && value.creatorEmail != null)
                    {
                        RV_User single = db.RV_User.SingleOrDefault(i => i.email == value.creatorEmail);
                        if (single != null)
                        {
                            RV_Group group = new RV_Group()
                            {
                                groupName = value.groupName,
                                groupDescription = value.groupDescription,
                                ImgPath = value.ImgPath,
                                creatorEmail = value.creatorEmail,
                                creationDate = DateTime.Now
                        };
                            db.RV_Group.Add(group);
                            db.SaveChanges();
                            return Ok();
                        }
                        else
                        {
                            return Content(HttpStatusCode.BadRequest, "משתמש לא קיים!");
                        }
                    }
                    else
                    {
                        return Content(HttpStatusCode.BadRequest, "אחד מהפרטים שהתבקשת למלא חסר");
                    }
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, " קבוצה קיימת במערכת");
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}