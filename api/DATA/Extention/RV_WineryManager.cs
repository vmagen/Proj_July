using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.EF
{
    [MetadataType(typeof(WineryManagerMetaData))]
    public partial class RV_WineryManager
    {
    }
    public class WineryManagerMetaData
    {
        [MinLength(2, ErrorMessage = "שם פרטי חייב להכיל מעל 2 אותיות")]
        public string firstName;

        [MinLength(2, ErrorMessage = "שם משפחה חייב להכיל מעל 2 אותיות")]
        public string lastName;

        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "כתובת מייל אינה חוקית")]
        public string email;

        [RegularExpression(@"^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}$", ErrorMessage = "מספר טלפון אינו חוקי")]
        public string phone;
    }
}
