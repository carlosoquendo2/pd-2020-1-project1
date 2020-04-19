using System;
using System.Collections.Generic;
using System.Text;

namespace TimeRecord.Common.Models
{
    public class TripRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public String UserEmail { get; set; }
    }
}
