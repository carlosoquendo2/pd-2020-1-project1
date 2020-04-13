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

        public string AttachmentFullPath => string.IsNullOrEmpty(AttachmentPath)
                                        ? "https://timerecord.azurewebsites.net/images/Vouchers/VoucherTest.png"
                                        : $"https://timerecord.azurewebsites.net{AttachmentPath.Substring(1)}";

        public DateTime Date { get; set; }
    }
}
