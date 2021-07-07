using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.EF
{
    [MetadataType(typeof(EventMetaData))]
    public partial class RV_Event
    {
    }

    public class EventMetaData
    {
        [MinLength(2, ErrorMessage = "שם של אירוע חייב להכיל לפחות 2 תווים")]
        public string eventName;

        [MinLength(2, ErrorMessage = "תיאור של אירוע חייב להכיל לפחות 2 תווים")]
        public string content;

        [Range(1, int.MaxValue, ErrorMessage = "מחיר מחיר חייב להיות מספר חיובי מעל 1")]
        public string price;
    }
}
