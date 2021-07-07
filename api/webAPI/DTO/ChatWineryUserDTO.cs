using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webAPI.DTO
{
    public class ChatWineryUserDTO
    {
        public int id { get; set; }
        public int wineryId { get; set; }
        public string userEmail { get; set; }
        public string text { get; set; }
        public DateTime dateTime { get; set; }
        public string userName { get; set; }
        public string userPicture { get; set; }
    }
}