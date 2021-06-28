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
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class WineryCommentController : ApiController
    {
        public static ArvinoDbContext db = new ArvinoDbContext();

        /// <summary>
        /// https://localhost:44370/api/WineryComment/PostWineryComment
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult PostWineryComment([FromBody] RV_WineryCommand value)
        {
            try
            {
                RV_WineryCommand comment = new RV_WineryCommand()
                {
                    text = value.text,
                    wineryId = value.wineryId,
                    wineId = value.wineId,
                    date = DateTime.Now,
                    commendOn = value.commendOn
                };
                db.RV_WineryCommand.Add(comment);
                db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/WineryComment/GetWineryComments?Id=1
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetWineryComments(int Id)
        {
            try
            {
                return Ok(WineryCommentModel.GetAllWineryComments(Id, db));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}