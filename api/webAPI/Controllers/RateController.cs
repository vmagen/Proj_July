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
    public class RateController : ApiController
    {
        public static ArvinoDbContext db = new ArvinoDbContext();

        /// <summary>
        /// https://localhost:44370/api/Rate/GetWineryInfoRates?Id=1
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Rate/GetWineryInfoRates")]
        public IHttpActionResult GetWineryInfoRates(int Id)
        {
            try
            {
                return Ok(RateModel.GetWineryInfoRates(Id, db));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/Rate/GetWineRate?Id=1
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Rate/GetWineRate")]
        public IHttpActionResult GetWineRate(int Id)
        {
            try
            {
                return Ok(RateModel.GetWineRateByWinery(Id, db));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/Rate/PostComment
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Rate/PostComment")]
        public IHttpActionResult PostNewWineComment([FromBody] WineCommentDTO value)
        {
            try
            {
                RV_WineComment wineComment = new RV_WineComment()
                {
                   email=value.email,
                   text=value.text,
                   date=DateTime.Now,
                   rate=value.rate,
                   wineId=value.wineId
                };
                db.RV_WineComment.Add(wineComment);
                db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }


        /// <summary>
        /// https://localhost:44370/api/Rate/UserAllowed?wineId=1&email=vmagen@gmail.com&days=7
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Rate/UserAllowed")]
        public IHttpActionResult GetIsUserAllowedToPublishComment(int wineId, string email, int days)
        {
            try
            {
                return Ok(RateModel.IsUserAllowedToPublish(wineId, email, days,  db));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}