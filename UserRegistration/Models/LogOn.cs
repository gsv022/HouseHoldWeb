using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UserRegistration.Models
{
    public class LogOn
    {


        [DisplayName("Username")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username required.")]
        public string Username { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Password required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }



        [DisplayName("Remember Me")]
        public bool RememberMe { get; set; }




    }

}