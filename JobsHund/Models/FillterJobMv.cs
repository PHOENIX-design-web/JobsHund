using jobhundClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobsHund.Models
{
    public class FillterJobMv
    {
        public FillterJobMv()
            {
               Result = new List<PostJob>();
            }
        public int JobCategoryId { get; set; }
        public int JobNatureId { get; set; }
        public int NoDays { get; set; }
        public List<PostJob> Result  { get; set; }
        

    }
}