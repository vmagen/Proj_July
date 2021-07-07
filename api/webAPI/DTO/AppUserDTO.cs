using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webAPI.DTO
{
    public class AppUserDTO
    {
        public string email { get; set; }
        public string password { get; set; }
        public string Name { get; set; }
        public string picture { get; set; }
        public bool isOlder { get; set; }
        public int? typeId { get; set; }
        public double rank { get; set; }
        public bool? isPremium { get; set; }
        public List<WineDTO> wineList {get;set;}
        public List<WineryDTO> wineryList { get; set; }

    }
}