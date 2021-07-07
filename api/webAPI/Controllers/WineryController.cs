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
using System.Data.Entity.Validation;

namespace webAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class WineryController : ApiController
    {
        public static ArvinoDbContext db = new ArvinoDbContext();

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
        [HttpPut]
        [Route("api/Winery/PutWinery/")]
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

        /// <summary>
        /// https://localhost:44370/api/Winery/GetWineryUser?email=asaf@gmail.com
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Winery/GetWineryUser/")]
        public IHttpActionResult GetWineryUser(string email)
        {
            try
            {
                if (email != null)
                {
                    return Ok(WineryModel.GetWineryUser(email, db));
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, "לא הוזנה כתובת מייל");
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/Winery/PostNewWinery
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Winery/PostNewWinery")]
        public IHttpActionResult PostNewWinery([FromBody] RV_Winery value)
        {
            try
            {
                RV_User user = db.RV_User.SingleOrDefault(u => u.email == value.wineryManagerEmail);
                if (user != null)
                {
                    if (value.areaId != null & value.wineryName != null & value.wineryEmail != null & value.phone != null & value.IconImgPath != null & value.wineryAddress != null & value.areaId != null)
                    {
                        RV_Winery winery = new RV_Winery()
                        {
                            wineryName = value.wineryName,
                            wineryAddress = value.wineryAddress,
                            wineryEmail = value.wineryEmail,
                            phone = value.phone,
                            statusType = "פעיל",
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
                        return Content(HttpStatusCode.BadRequest, "אחד מהפרטים שהתבקשת למלא חסר");
                    }
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, "לא ניתן להוסיף יקב אם לא קיים מנהל יקב רשום במערכת");
                }

            }
            catch (DbEntityValidationException ex)
            {
                string errors = "";
                foreach (DbEntityValidationResult vr in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError er in vr.ValidationErrors)
                    {
                        errors += $"PropertyName - {er.PropertyName }, Error {er.ErrorMessage} <br/>";
                    }
                }
                return Content(HttpStatusCode.BadRequest, errors);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/Winery/isEmailExists?email=...
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Winery/isEmailExists")]
        public IHttpActionResult isEmailExists(string email)
        {
            try
            {
                RV_Winery w = db.RV_Winery.SingleOrDefault(e => e.wineryEmail == email);
                if (w != null)
                {
                    return null;
                }
                return Ok(1);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}