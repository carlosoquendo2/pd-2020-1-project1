using System;
using System.Collections.Generic;
using System.Text;

namespace TimeRecord.Common.Models
{
    public class ExpenseTypeResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }
    }
}
