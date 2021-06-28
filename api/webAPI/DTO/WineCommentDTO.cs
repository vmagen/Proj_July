using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webAPI.DTO
{
    public class WineCommentDTO
    {
        public int id { get; set; }
        public string email { get; set; }
        public string text { get; set; }
        public DateTime date { get; set; }
        public int wineId { get; set; }
        public int rate { get; set; }
        public string UserPitcure { get; set; }
        public string userName { get; set; }
        public string wineImgPath { get; set; }
    }
}