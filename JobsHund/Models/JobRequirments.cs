using jobhundClassLibrary1;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace JobsHund.Models
{
    public class JobRequirments
    {
        public JobRequirments()
        {
            Details = new List<JobRequirementDetail>();
        }
        [Required(ErrorMessage="Required")]
        public int JobRequirementId { get; set; }
        [Required(ErrorMessage = "Required")]

        public string JobRequirementDetails { get; set; }
        public int PostJobId { get; set; }
        public List<JobRequirementDetail> Details { get; set; }
    }
}