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
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace webAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class WineController : ApiController
    {
        public static ArvinoDbContext db = new ArvinoDbContext();

        /// <summary>
        /// https://localhost:44370/api/Wine/GetAllWines
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Wine/GetAllWines")]
        public IHttpActionResult GetAllWines()
        {
            try
            {
                return Ok(WineModel.GetAllWines(db));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        ///  https://localhost:44370/api/Wine/Get?CategoryId=2
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get(int CategoryId)
        {
            try
            {
                return Ok(WineModel.GetWineByCategory(CategoryId, db));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.InnerException);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        [Route("api/RandomWines")]
        public IHttpActionResult GetRandomWines(int numOfWines)
        {
            try
            {
                List<WineDTO> RWines = new List<WineDTO>();
                List<RV_Wine> list = db.RV_Wine.ToList() ;
                
                for (int i = 0; i < numOfWines; i++)
                {
                    var rand = new Random().Next(list.Count);
                    RV_Wine temp = list[rand];
                    list.Remove(list[rand]);
                    WineDTO sWineDTO = new WineDTO()
                    {
                        wineId=temp.wineId,
                        wineImgPath=temp.wineImgPath,
                        wineName=temp.wineName,
                        content=temp.content,
                        price=temp.price,
                        wineryName= db.RV_Winery.FirstOrDefault(j=>j.wineryId== temp.wineryId).wineryName,
                        wineryImage = db.RV_Winery.FirstOrDefault(j => j.wineryId == temp.wineryId).IconImgPath,
                        wineryId = db.RV_Winery.FirstOrDefault(j => j.wineryId == temp.wineryId).wineryId

                    };

                    RWines.Add(sWineDTO);
                }
                return Ok(RWines);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //NEW SECTION 14/6/2021
        /// <summary>
        ///  https://localhost:44370/api/getWine?wineId=2
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/getWine")]
        public IHttpActionResult GetWineByID(int wineId)
        {
            try
            {
                return Ok(WineModel.getWIneByID(wineId, db));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.InnerException);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/Wine/GetWineryWines?id=1
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Wine/GetWineryWines")]
        public IHttpActionResult GetWineryWines(int id)
        {
            try
            {
                List<WineDTO> w = WineModel.GetWineryWines(id, db);
                if (w == null)
                {
                    return Content(HttpStatusCode.BadRequest, "אין יינות ביקב");
                }
                return Ok(w);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/Wine/GetCategory
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Wine/GetCategory")]
        public IHttpActionResult GetCategory()
        {
            try
            {
                return Ok(WineModel.GetCategory(db));
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/Wine/GetSortCategory?id=1
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Wine/GetSortCategory")]
        public IHttpActionResult GetSortCategory(int id, int wineryId)
        {
            try
            {
                List<WineDTO> w = WineModel.sortCategory(id, wineryId, db);
                if (w == null)
                {
                    return Content(HttpStatusCode.BadRequest, "אין יינות בקטגוריה");
                }
                return Ok(w);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// להתיחס לליקיםםםםםםםםםםםםםםםםםםםםםםםםםםםםםםםםםםםםםםםםםםםםםםםםםםםםםםםםםםםםםם
        /// https://localhost:44370/api/Wine/DeleteWine?id=1
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("api/Wine/DeleteWine")]
        public IHttpActionResult DeleteWine(int id)
        {
            try
            {
                List<RV_Rate> r = db.RV_Rate.Where(wine => wine.wineId == id).ToList();
                List<RV_WineComment> c = db.RV_WineComment.Where(wine => wine.wineId == id).ToList();
                List<RV_Competition> cm = db.RV_Competition.Where(wine => wine.wineId == id).ToList();
                List<RV_WineryCommand> wc = db.RV_WineryCommand.Where(wine => wine.wineId == id).ToList();
                RV_Wine w = db.RV_Wine.SingleOrDefault(wine => wine.wineId == id);
                if (w != null)
                {
                    if (r != null)
                    {
                        foreach (var item in r)
                        {
                            db.RV_Rate.Remove(item);
                        }
                    }
                    if (c != null)
                    {
                        foreach (var item in c)
                        {
                            db.RV_WineComment.Remove(item);
                        }
                    }
                    if (cm != null)
                    {
                        foreach (var item in cm)
                        {
                            db.RV_Competition.Remove(item);
                        }
                    }
                    if (wc != null)
                    {
                        foreach (var item in wc)
                        {
                            db.RV_WineryCommand.Remove(item);
                        }
                    }
                    db.RV_Wine.Remove(w);
                    db.SaveChanges();
                    return Ok();
                }
                return Content(HttpStatusCode.NotFound,
                    $"wine with id {id} was not found to delete!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/Wine/PostWine
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Wine/PostWine")]
        public IHttpActionResult PostWine([FromBody] RV_Wine value)
        {
            try
            {
                RV_Wine wine = new RV_Wine()
                {
                    wineName = value.wineName,
                    content = value.content,
                    price = value.price,
                    wineImgPath = value.wineImgPath,
                    wineLabelPath = value.wineLabelPath,
                    categoryId = value.categoryId,
                    wineryId = value.wineryId
                };
                db.RV_Wine.Add(wine);
                db.SaveChanges();
                return Ok();
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
        /// https://localhost:44370/api/Wine/PutWine?id=1
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/Wine/PutWine")]
        public IHttpActionResult PutWine(int id, [FromBody] WineDTO value)
        {
            try
            {
                RV_Wine w = db.RV_Wine.SingleOrDefault(x => x.wineId == id);

                if (w != null)
                {
                    w.wineName = value.wineName;
                    w.content = value.content;
                    w.price = value.price;
                    w.wineImgPath = value.wineImgPath;
                    w.wineLabelPath = value.wineLabelPath;
                    w.categoryId = value.categoryId;
                    w.wineryId = value.wineryId;
                    db.SaveChanges();
                    return Ok(w);
                }
                return Content(HttpStatusCode.NotFound,
                    $"wine with id {id} was not found to update!");
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
    }
}