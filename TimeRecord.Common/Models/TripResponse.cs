using System;
using System.Collections.Generic;

namespace TimeRecord.Common.Models
{
    public class TripResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ICollection<TripDetailResponse> TripDetails { get; set; }

        public UserResponse User { get; set; }
    }
}
