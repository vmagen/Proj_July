using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.EF
{
    [MetadataType(typeof(WineMetaData))]
    public partial class RV_Wine
    {
    }

    public class WineMetaData
    {
        [MinLength(2, ErrorMessage = "שם של יין חייב להכיל לפחות 2 תווים")]
        public string wineName;

        [MinLength(2, ErrorMessage = "תיאור של יין חייב להכיל לפחות 2 תווים")]
        public string content;

        [Range(1, int.MaxValue, ErrorMessage = "מחיר יין חייב להיות מספר חיובי מעל 1")]
        public string price;
    }
}
