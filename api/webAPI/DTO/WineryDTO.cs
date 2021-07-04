using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webAPI.DTO
{
    public class WineryDTO
    {
        public int wineryId { get; set; }
        public string wineryName { get; set; }
        public string wineryManagerEmail { get; set; }
        public string wineryAddress { get; set; }
        public string wineryEmail { get; set; }
        public string phone { get; set; }
        public string wineryImage { get; set; }
        public string wineryAreaName { get; set; }
        public List<EventDTO> eventList { get; set; }
        public List<WineDTO> wineList { get; set; }
        public List<ServiceDTO> serviceList { get; set; }
        public List<LikesDTO> likes { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string Name { get; set; }
        public string userphone { get; set; }
        public string statusType { get; set; }
        public string picture { get; set; }

    }
}
