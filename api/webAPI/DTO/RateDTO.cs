using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webAPI.DTO
{
    public class RateDTO
    {
        public int rateId { get; set; }
        public DateTime rateDate { get; set; }
        public int wineId { get; set; }
        public int score { get; set; }
        public string ratedByUserEmail { get; set; }
        public string UserPitcure { get; set; }
        public string userName { get; set; }
        public string winePitcure { get; set; }
    }
}