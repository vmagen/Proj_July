using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DATA.EF;
using webAPI.DTO;
using System.Data.Entity;
using System.Web.Http.Cors;
using webAPI.Models;

namespace webAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class WineryController : ApiController
    {
        public static ArvinoDbContext db = new ArvinoDbContext();

        /// <summary>
        /// https://localhost:44370/api/Winery/PostNewWinery
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult PostNewWinery([FromBody] RV_Winery value)
        {
            try
            {
                RV_Winery w = WineryModel.GetWineryByUser(db, value.wineryManagerEmail);
                if (w != null)
                {
                    RV_Winery winery = new RV_Winery()
                    {
                        wineryName = value.wineryName,
                        wineryAddress = value.wineryAddress,
                        wineryEmail = value.wineryEmail,
                        phone = value.phone,
                        statusType = value.statusType,
                        IconImgPath = value.IconImgPath,
                        wineryManagerEmail = value.wineryManagerEmail,
                        areaId = value.areaId
                    };
                    db.RV_Winery.Add(winery);
                    db.SaveChanges();
                    return Ok();
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, "יקב קיים במערכת");
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/Winery 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Winery")]
        public IHttpActionResult Get()
        {
            try
            {
                return Ok(WineryModel.GetWinery(db));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/Winery/wineryManagerEmail?wineryManagerEmail=asaf@gmail.com
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Winery/wineryManagerEmail")]
        public IHttpActionResult GetWineryByUser(string wineryManagerEmail)
        {
            try
            {
                return Ok(WineryModel.GetWineryByUser(db, wineryManagerEmail));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        ///  https://localhost:44370/api/Winery/area?areaID=2
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Winery/area/")]
        public IHttpActionResult GetWineryByArea(int areaID)
        {
            try
            {
                return Ok(WineryModel.GetWineryByArea(db, areaID));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/Winery/PutWinery?id=1
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IHttpActionResult PutWinery(int id, [FromBody] RV_Winery value)
        {
            try
            {
                RV_Winery w = db.RV_Winery.SingleOrDefault(x => x.wineryId == id);
                if (w != null)
                {
                    w.wineryName = value.wineryName;
                    w.wineryAddress = value.wineryAddress;
                    w.wineryEmail = value.wineryEmail;
                    w.phone = value.phone;
                    w.IconImgPath = value.IconImgPath;
                    w.areaId = value.areaId;
                    db.SaveChanges();
                    return Ok(w);
                }
                return Content(HttpStatusCode.NotFound,
                    $"wine with email {id} was not found to update!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}