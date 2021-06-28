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
    public class AreaCategoryController : ApiController
    {
        public static ArvinoDbContext db = new ArvinoDbContext();

        /// <summary>
        /// https://localhost:44370/api/AreaCategory/GetAllAreaCategories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/AreaCategory/GetAllAreaCategories")]
        public IHttpActionResult GetAllAreaCategories()
        {
            try
            {
                return Ok(AreaCategoryModel.GetAreaCategories(db));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}