using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.EF
{
    [MetadataType(typeof(UserMetaData))]
    public partial class RV_User
    {
    }

    public class UserMetaData
    {
        //[RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "כתובת מייל אינה חוקית")]
        //public string email;

        //[RegularExpression("(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,15})$", ErrorMessage = "על הסיסמא להכיל לפחות אות אחת קטנה, אחת גדולה, מספר, תו מיוחד ולהיות באורך 8 תווים")]
        //public string password;
    }
}
