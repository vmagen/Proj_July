using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webAPI.DTO
{
    public class GMDTO
    {
        public int _id { get; set; }
        public DateTime? createdAt { get; set; }
        public string text { get; set; }
        public int groupId { get; set; }
        public GMUserDTO user { get; set; }
    }
}