using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webAPI.DTO
{
    public class POSTLikeDTO
    {
        public int entityType { get; set; }
        public int entityId { get; set; }
        public string userEmail { get; set; }
    }
}