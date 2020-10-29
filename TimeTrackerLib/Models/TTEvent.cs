using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTrackerLib.Models
{
    public class TTEvent
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime StartUTC { get; set; }
        public DateTime EndUTC { get; set; }
        public string Comments { get; set; }

       
    }
}
