using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDev.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class LogonModel
    {
        [Required]
        [Display(Name = "Username")]
        public string username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        public string ValidationMessage { get; set; }
    }
}