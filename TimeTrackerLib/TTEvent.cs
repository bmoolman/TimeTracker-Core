using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTrackerLib
{
    public class TTEvent
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string StartUTC { get; set; }
        public string EndUTC { get; set; }
        public string Comments { get; set; }

    }
}
