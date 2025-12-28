using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobsHund.Models
{
    public class PostJobMv
    {
        public int PostJobId { get; set; }
        public int UserId { get; set; }

        public int CompanyId { get; set; }
        public int JobCategoryId { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(500, ErrorMessage = "Do not enter more than 500 characters")]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(2000, ErrorMessage = "Do not enter more than 2000 characters")]
        public string JobDescription { get; set; }

        [Required(ErrorMessage = "Required")]
        public int MinSalary { get; set; }

        [Required(ErrorMessage = "Required")]
        public int MaxSalary { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Required")]
        public int Vecancey { get; set; }

        public int JobNatureId { get; set; }

        public System.DateTime PostDate { get; set; }

        [DataType(DataType.Date)]
        public System.DateTime ApplicationLastdate { get; set; } = DateTime.Now.AddDays(15);

        public int JobStatusId { get; set; }

        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Url)]
        public string WebUrl { get; set; }

    }
}