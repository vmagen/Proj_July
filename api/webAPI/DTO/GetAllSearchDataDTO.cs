using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webAPI.DTO
{
    public class GetAllSearchDataDTO
    {
        public int wineId { get; set; }
        public string wineName { get; set; }
        public string content { get; set; }
        public int price { get; set; }
        public string wineImgPath { get; set; }
        public string wineLabelPath { get; set; }
        public string wineCategoryName { get; set; }
        public string wineryArea { get; set; }
        public int wineryId { get; set; }
        public string wineryName { get; set; }
        public string IconImgPath { get; set; }
        public int eventId { get; set; }
        public string eventName { get; set; }
        public int participantsAmount { get; set; }
        public DateTime eventDate { get; set; }
        public TimeSpan startTime { get; set; }
        public string eventImgPath { get; set; }
        public string eventCategoryName { get; set; }
        public int groupId { get; set; }
        public string groupName { get; set; }
        public string ImgPath { get; set; }
        public string groupDescription { get; set; }

    }
}