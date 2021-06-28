using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DATA.EF;
using webAPI.Models;
using webAPI.DTO;
using System.Threading.Tasks;
using System.IO;
using System.Web;
using System.Web.Http.Cors;

namespace webAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UploadPictureController : ApiController
    {
        public static ArvinoDbContext db = new ArvinoDbContext();

        /// <summary>
        /// https://localhost:44370/uploadpicture/
        /// </summary>
        /// <returns></returns>
        [Route("uploadpicture")]
        public Task<HttpResponseMessage> Post()
        {
            List<string> savedFilePath = new List<string>();
            Dictionary<string, string> attributes = new Dictionary<string, string>();

            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            //Where to put the picture on server  ...MapPath("~/TargetDir")
            string rootPath = System.Web.HttpContext.Current.Server.MapPath("~/uploadFiles");
            var provider = new MultipartFileStreamProvider(rootPath);
            string newFileName = string.Empty;

            Task<HttpResponseMessage> task = Request.Content.ReadAsMultipartAsync(provider).
                ContinueWith<HttpResponseMessage>(t =>
                {
                    if (t.IsCanceled || t.IsFaulted)
                    {
                        Request.CreateErrorResponse(HttpStatusCode.InternalServerError, t.Exception);
                    }

                    foreach (MultipartFileData item in provider.FileData)
                    {

                        try
                        {
                            string name = item.Headers.ContentDisposition.FileName.Replace("\"", "");
                            newFileName = Path.GetFileNameWithoutExtension(name) + "_" + CreateDateTimeWithValidChars() + Path.GetExtension(name);
                            string[] names = Directory.GetFiles(rootPath);
                            foreach (var fileName in names)
                            {
                                if (Path.GetFileNameWithoutExtension(fileName).IndexOf(Path.GetFileNameWithoutExtension(name)) != -1)
                                {
                                    File.Delete(fileName);
                                }
                            }

                            File.Copy(item.LocalFileName, Path.Combine(rootPath, newFileName), true);
                            File.Delete(item.LocalFileName);

                            Uri baseuri = new Uri(Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.PathAndQuery, string.Empty));
                            string fileRelativePath = "~/uploadFiles/" + newFileName;
                            Uri fileFullPath = new Uri(baseuri, VirtualPathUtility.ToAbsolute(fileRelativePath));
                            savedFilePath.Add(fileFullPath.ToString());
                        }
                        catch (Exception ex)
                        {
                            Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
                        }

                    }
                    //Save to DB Here .
                    string filePath = Path.Combine(rootPath, newFileName);
                    //MoveAndSaveFile(filePath);
                    return Request.CreateResponse(HttpStatusCode.Created, MoveAndSaveFile(filePath));
                });
            return task;
        }

        private string CreateDateTimeWithValidChars()
        {
            return DateTime.Now.ToString().Replace('/', '_').Replace(':', '-').Replace(' ', '_');
        }

        private string MoveAndSaveFile(string path)
        {
            string currentDir = Path.GetDirectoryName(path);
            DirectoryInfo info = Directory.GetParent(path);
            string fileName = Path.GetFileName(path);
            DirectoryInfo info2 = Directory.GetParent(info.FullName);
            File.Move(path, info2.FullName + "\\FinalPics\\" + fileName);

            Uri baseuri = new Uri(Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.PathAndQuery, string.Empty));
            string fileRelativePath = "~/FinalPics/" + fileName;
            Uri fileFullPath = new Uri(baseuri, VirtualPathUtility.ToAbsolute(fileRelativePath));
            return fileFullPath.AbsoluteUri;

        }
    }
}