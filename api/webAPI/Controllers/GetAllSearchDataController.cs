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
    public class GetAllSearchDataController : ApiController
    {

        public static ArvinoDbContext db = new ArvinoDbContext();

        /// <summary>
        /// https://localhost:44370/api/GetAllSearchData
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/GetAllSearchData")]
        public IHttpActionResult GetAllSearchData()
        {
            try
            {
                return Ok(GetAllSearchDataModel.GetAllData(db));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }


    }
}

