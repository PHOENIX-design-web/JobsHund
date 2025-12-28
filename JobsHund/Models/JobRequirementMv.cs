using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobsHund.Models
{
    public class JobRequirementMv
    {
        public JobRequirementMv()
        {
            Details = new List<JobRequirementDetailMv>();
        }
        public int JobRequirementId { get; set; }
        public string JobRequirementTitle { get; set; }
        public List<JobRequirementDetailMv> Details {get;set;}
    }
}