using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using DATA.EF;
using webAPI.Models;
using webAPI.DTO;
using System.Web.Http.Cors;
using System.Data.Entity.Validation;
using System.Threading.Tasks;

namespace webAPI.Controllers
{
    public class UserPrefrenceController : ApiController
    {
        public static ArvinoDbContext db = new ArvinoDbContext();

        /// <summary>
        /// https://localhost:44370/api/UserPrefrence
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IHttpActionResult Post([FromBody] RV_UserPrefrences value)
        {
            try
            {
                RV_UserPrefrences up = new RV_UserPrefrences()
                {
                    email = value.email,
                    PrefrenceID = value.PrefrenceID,
                    FreeText = value.FreeText,
                    registerAt = DateTime.Now
                };
                db.RV_UserPrefrences.Add(up);
                db.SaveChanges();
                
                //ASYNC UPDATE of KNN TABLE.
                new Task(() =>
                {
                    UserPrefrencesModel.updateKnnTablesAsync(up.email, up.FreeText, up.PrefrenceID);
                }).Start();

                return Ok();
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }


    }
}
