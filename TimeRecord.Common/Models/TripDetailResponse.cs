using System;

namespace TimeRecord.Common.Models
{
    public class TripDetailResponse
    {
        public int Id { get; set; }

        public ExpenseTypeResponse ExpenseType { get; set; }

        public string Name { get; set; }

        public double Expense { get; set; }

        public string Currency { get; set; }

        public string Comment { get; set; }

        public string AttachmentPath { get; set; }

        public DateTime Date { get; set; }
    }
}
