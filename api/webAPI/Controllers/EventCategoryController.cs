using DATA.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using webAPI.DTO;
using webAPI.Models;

namespace webAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EventCategoryController : ApiController
    {
        public static ArvinoDbContext db = new ArvinoDbContext();

        /// <summary>
        /// https://localhost:44370/api/EventCategory/GetAllEventsCategories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/EventCategory/GetAllEventsCategories")]
        public IHttpActionResult GetAllEventsCategories()
        {
            try
            {
                return Ok(EventCategoryModel.GetAllCategories(db));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/EventCategory/GetCategoryId
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/EventCategory/GetCategoryId")]
        public IHttpActionResult GetCategoryId(string name)
        {
            try
            {
                RV_EventCategory e = EventCategoryModel.GetCategoryId(name, db);
                if (e != null)
                {
                    return Ok(e);
                }
                else
                {
                    return Content(HttpStatusCode.BadRequest, $"{name} לא נמצאה קטגוריה בשם");
                }
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/EventCategory/PostEventCategoey
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/EventCategory/PostEventCategoey")]
        public IHttpActionResult PostEventCategoey([FromBody] EventCategoryDTO value)
        {
            try
            {
                if (EventCategoryModel.GetCategoryId(value.categoryName, db) == null)
                {
                    RV_EventCategory eventCategory = new RV_EventCategory()
                    {
                        categoryName = value.categoryName
                    };
                    db.RV_EventCategory.Add(eventCategory);
                    db.SaveChanges();
                    return Ok();
                }
                return Content(HttpStatusCode.BadRequest, "קטגוריה כבר קיימת");

            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}