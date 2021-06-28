using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DATA.EF;
using webAPI.Models;
using webAPI.DTO;
using System.Web.Http.Cors;
using System.Data.Entity.Validation;

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
        /// https://localhost:44370/api/EventCategory/PostEventCategoey
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult PostEventCategoey([FromBody] EventCategoryDTO value)
        {
            try
            {
                RV_EventCategory eventCategory = new RV_EventCategory()
                {
                    categoryName = value.categoryName

                };
                db.RV_EventCategory.Add(eventCategory);
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