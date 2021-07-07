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
    public class ChatWineryUserController : ApiController
    {
        public static ArvinoDbContext db = new ArvinoDbContext();

        /// <summary>
        /// https://localhost:44370/api/ChatWineryUser 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/ChatWineryUser")]
        public IHttpActionResult GetAllUsersChat(int id)
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
    }
}