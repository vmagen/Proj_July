using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.EF
{
    [MetadataType(typeof(WineryMetaData))]
    public partial class RV_Winery
    {
    }
    public class WineryMetaData
    {
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "כתובת מייל אינה חוקית")]
        public string wineryEmail;

        [MinLength(2, ErrorMessage = "שם משתמש חייב להכיל לפחות 2 תווים")]
        public string wineryName;

        [MinLength(5, ErrorMessage = "כתובת חייבת להכיל לפחות 5 תווים")]
        public string wineryAddress;

        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "טלפון בנוי מ-10 מספרים")]
        public string phone;
    }
}
