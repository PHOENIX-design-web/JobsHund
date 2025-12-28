using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobsHund.Models
{
    public class ForgotPasswordMv
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}