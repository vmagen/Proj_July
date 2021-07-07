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
using System.Threading.Tasks;

namespace webAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        public static ArvinoDbContext db = new ArvinoDbContext();

        /// <summary>
        /// https://localhost:44370/api/User/email?email=asaf@gmail.com
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/User/email")]
        public IHttpActionResult GetUserByEmail(string email)
        {
            try
            {
                return Ok(UserModel.GetUser(email, db));
            }
            catch (DbEntityValidationException ex)
            {
                string error = "";
                foreach (DbEntityValidationResult vr in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError er in vr.ValidationErrors)
                    {
                        error += er.ErrorMessage + "\n";
                    }
                }
                return Content(HttpStatusCode.BadRequest, error);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/User/PutUser?email=asaf@gmail.com
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/User/PutUser")]
        public IHttpActionResult PutUser(string email, [FromBody] RV_User value)
        {
            try
            {
                RV_User user = db.RV_User.SingleOrDefault(x => x.email == email);
                if (user != null)
                {
                    user.email = value.email;
                    user.password = value.password;
                    user.Name = value.Name;
                    user.phone = value.phone;
                    user.picture = value.picture;
                    db.SaveChanges();
                    return Ok(user);
                }
                return Content(HttpStatusCode.NotFound,
                    $"wine with email {email} was not found to update!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/User/getProximity?email=vmagen@gmail.com
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/User/getProximity")]
        public IHttpActionResult GetProximityUserList(string email)
        {
            try
            {
                var list = UserPrefrencesModel.CalculateProximity(email);
                if (list != null)
                {
                    List<AppUserDTO> friends = new List<AppUserDTO>();
                    //Generate List of proximity users order by closest first
                    foreach (var item in list)
                    {
                        RV_User user = db.RV_User.SingleOrDefault(i => i.email == item.Key);
                        if (user != null)
                        {
                            friends.Add(new AppUserDTO()
                            {
                                email = user.email,
                                Name = user.Name,
                                picture = user.picture,
                                rank = item.Value
                            });
                        }
                    }

                    return Content(HttpStatusCode.OK, friends);
                }
                else
                {
                    //Genertae RandomList of Users
                    return Content(HttpStatusCode.OK, String.Empty);
                }
            }
            catch (DbEntityValidationException ex)
            {
                string error = "";
                foreach (DbEntityValidationResult vr in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError er in vr.ValidationErrors)
                    {
                        error += er.ErrorMessage + "\n";
                    }
                }
                return Content(HttpStatusCode.BadRequest, error);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/User/getProximity?email=vmagen@gmail.com
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/User/getRecommendedWines")]
        public IHttpActionResult GetRecommendedWines(string email)
        {
            try
            {

                return Content(HttpStatusCode.OK, "");
            }
            catch (DbEntityValidationException ex)
            {
                string error = "";
                foreach (DbEntityValidationResult vr in ex.EntityValidationErrors)
                {
                    foreach (DbValidationError er in vr.ValidationErrors)
                    {
                        error += er.ErrorMessage + "\n";
                    }
                }
                return Content(HttpStatusCode.BadRequest, error);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        /// <summary>
        /// https://localhost:44370/api/User/PostUser
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/User/PostUser")]
        public IHttpActionResult PostUser([FromBody] RV_User value)
        {
            DateTime d = DateTime.Now;
            using (var context = new ArvinoDbContext())
            {
                try
                {
                    //is user exists?
                    RV_User u = UserModel.GetUser(value.email, context);

                    if (u == null)
                    {
                        if (value.email != null & value.password != null & value.Name != null)
                        {
                            RV_User user = new RV_User()
                            {
                                email = value.email,
                                password = value.password,
                                Name = value.Name,
                                phone = Convert.ToString(value.phone),
                                registrationDate = d,
                                picture = value.picture,
                                isOlder = value.isOlder,
                                typeId = value.typeId,
                                token = value.token,
                                isPremium=false
                            };
                            context.RV_User.Add(user);
                            context.SaveChanges();
                            if (user.typeId == 3)//APP USER 
                            {
                                DATA.Extention.RV_KNNCategory.AddNewUser(db, new RV_KNNCategory(), user.email);

                            }
                            return Ok();
                        }
                        else
                        {
                            return Content(HttpStatusCode.BadRequest, "אחד מהפרטים שהתבקשת למלא חסר");
                        }
                    }
                    else
                    {
                        return Content(HttpStatusCode.BadRequest, "משתמש קיים במערכת");
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
        }

        /// <summary>
        /// https://localhost:44370/api/User/isEmailExists?email=...
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/User/isEmailExists")]
        public IHttpActionResult isEmailExists(string email)
        {
            try
            {
                RV_User u = db.RV_User.SingleOrDefault(e => e.email == email);
                if (u != null)
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