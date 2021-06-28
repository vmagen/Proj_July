using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DATA.EF;

namespace webAPI.DTO
{
    public class EventDTO
    {
        public int eventId;
        public string eventName;
        public string content;
        public int price;
        public int participantsAmount;
        public DateTime eventDate;
        public TimeSpan startTime;
        public string eventImgPath;
        public int categoryId;
        public int wineryId;
        public int ticketsPurchased;
        public string categoryName;
        public string wineryName { get; set; }
        public string wineryImage { get; set; }
        public List<LikesDTO> likes { get; set; }

    }
}