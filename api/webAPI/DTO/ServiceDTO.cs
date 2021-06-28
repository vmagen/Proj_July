using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DATA.EF;

namespace webAPI.DTO
{
    public class ServiceDTO
    {
        public int serviceId;
        public string serviceName;
        public string serviceCategory;
        public string content;
        public int price;
        public int wineryId;
        public string wineryName;
        public string wineryImg;
        public List<ServiceImageDTO> images;
        public List<LikesDTO> likes;

    }
}