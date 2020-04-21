using System;
using System.Collections.Generic;
using System.Text;

namespace TimeRecord.Common.Models
{
    public class TripDetailRequest
    {
        public string TripId { get; set; }

        public String ExpenseTypeId { get; set; }

        public string Name { get; set; }

        public double Expense { get; set; }

        public string Currency { get; set; }

        public string Comment { get; set; }

        public byte[] VoucherArray { get; set; }

        public DateTime Date { get; set; }

        public int Day { get; set; }

        public int Mounth { get; set; }

        public int Year { get; set; }
    }
}
