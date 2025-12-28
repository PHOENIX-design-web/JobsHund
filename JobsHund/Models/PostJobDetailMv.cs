using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobsHund.Models
{
    public class PostJobDetailMv
    {

        public PostJobDetailMv()
            {
            Requirments=new  List<JobRequirementMv>();
            }
        public int PostJobId { get; set; }
        public String Company { get; set; }
        public String JobCategory { get; set; }
        public string JobTitle { get; set; }
        public string JobDescription { get; set; }
        public int MinSalary { get; set; }
        public int Maxsalary { get; set; }
        public int Vecancey { get; set; }
        public string Location { get; set; }
        public System.DateTime PostDate { get; set; }
        public System.DateTime ApplicationLastdate { get; set; }
        public String JobNature { get; set; }
        public string WebUrl { get; set; }

        public List<JobRequirementMv>Requirments{get; set;}
    }
}