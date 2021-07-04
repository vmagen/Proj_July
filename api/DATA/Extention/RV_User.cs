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

        //[MinLength(8, ErrorMessage = "על הסיסמא להכיל לפחות 8 תווים")]
        //public string password;

        //[MinLength(2, ErrorMessage = "שם משתמש חייב להכיל לפחות 2 תווים")]
        //public string Name;
    }
}
