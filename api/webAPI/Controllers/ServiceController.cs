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
    public class ServiceController : ApiController
    {
        public static ArvinoDbContext db = new ArvinoDbContext();

        /// <summary>
        /// https://localhost:44370/api/Service/GetAllServices
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Service/GetAllServices")]
        public IHttpActionResult GetAllServices()
        {
            try
            {
                //Vereda
                return Ok(ServiceModel.GetServices(db));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/Service/GetWineryService?id=1
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Service/GetWineryService")]
        public IHttpActionResult GetWineryService(int id, ArvinoDbContext db)
        {
            try
            {
                return Ok(ServiceModel.GetAllServices(id, db));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/Service/PostService
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Service/PostService")]
        public IHttpActionResult PostService([FromBody] RV_Service value)
        {
            try
            {
                RV_Service service = new RV_Service()
                {
                    serviceName = value.serviceName,
                    serviceCategory = value.serviceCategory,
                    price = value.price,
                    content = value.content,
                    wineryId = value.wineryId,
                };
                db.RV_Service.Add(service);
                db.SaveChanges();
                return Ok(db.RV_Service.Where(x => x.serviceName == value.serviceName && x.wineryId == value.wineryId));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/Service/DeleteService?id=1
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult DeleteService(int id)
        {
            try
            {
                RV_Service s = db.RV_Service.SingleOrDefault(service => service.serviceId == id);
                if (s != null)
                {
                    db.RV_Service.Remove(s);
                    db.SaveChanges();
                    return Ok();
                }
                return Content(HttpStatusCode.NotFound,
                    $"service with id {id} was not found to delete!");

                RV_ServiceImage[] img = db.RV_ServiceImage.Where(i => i.serviceId == id).Select(i => new RV_ServiceImage { }).ToArray();
                for (int i = 0; i < img.Length; i++)
                {
                    if (img[i] != null)
                    {
                        db.RV_ServiceImage.Remove(img[i]);
                        db.SaveChanges();
                        return Ok();
                    }
                    return Content(HttpStatusCode.NotFound,
                        $"img was not found to delete!");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/Service/PutService?id=1
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult PutService(int id, [FromBody] RV_Service value)
        {
            try
            {
                RV_Service s = db.RV_Service.SingleOrDefault(service => service.serviceId == id);
                if (s != null)
                {
                    s.serviceName = value.serviceName;
                    s.serviceCategory = value.serviceCategory;
                    s.content = value.content;
                    s.price = value.price;
                    db.SaveChanges();
                    return Ok(s);
                }
                return Content(HttpStatusCode.NotFound,
                    $"sevice was not found to update!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}