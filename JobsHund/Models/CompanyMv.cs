using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobsHund.Models
{
    public class CompanyMv
    {
        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public string CompanyName { get; set; }
        public string ContactNo { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }
 
    }
}