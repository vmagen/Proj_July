using DATA.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webAPI.DTO;

namespace webAPI.Controllers
{
    //nir stam
    public class MessagesController : ApiController
    {
        public static ArvinoDbContext db = new ArvinoDbContext();
        // GET api/Messages
        public IHttpActionResult Get(int id)
        {
            try
            {
                List<GMDTO> messagesByGroup = new List<GMDTO>();
                var messages = db.RV_GroupMessages.Where(i => i.GroupID == id);

                foreach (var item in messages.OrderByDescending(i => i.createdAt))
                {
                    try
                    {
                        GMDTO single = new GMDTO();
                        single._id = item.ID;
                        single.text = item.text;
                        single.createdAt = item.createdAt;
                        GMUserDTO gmUser = new GMUserDTO();
                        gmUser._id = item.userEmail;

                        var singleUser = db.RV_User.Single(i => i.email == item.userEmail);
                        gmUser.name = singleUser.Name;
                        gmUser.avatar = singleUser.picture;
                        single.user = gmUser;
                        messagesByGroup.Add(single);

                    }
                    catch (Exception ex)
                    {
                        return Content(HttpStatusCode.BadRequest, ex.Message);

                    }
                }

                return Content(HttpStatusCode.OK, messagesByGroup);
            }


            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        public IHttpActionResult Post([FromBody] GMDTO value)
        {
            try
            {
                RV_GroupMessages gm = new RV_GroupMessages()
                {
                  createdAt=DateTime.Now,
                  userEmail= value.user._id,
                  text=value.text,
                  GroupID =value.groupId

                };
                db.RV_GroupMessages.Add(gm);
                db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}