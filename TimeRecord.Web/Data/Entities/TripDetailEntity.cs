using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeRecord.Web.Data.Entities
{
    public class TripDetailEntity
    {
        public int Id { get; set; }

        public TripEntity Trip { get; set; }

        public ExpenseTypeEntity ExpenseType { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public double Expense { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Currency { get; set; }

        [MaxLength(255, ErrorMessage = "The field {0} can not have more than {1} characters.")]
        public string Comment { get; set; }

        [Display(Name = "Voucher")]
        public string AttachmentPath { get; set; }

        [Display(Name = "Full path voucher")]
        public string AttachmentFullPath => string.IsNullOrEmpty(AttachmentPath)
                                        ? "https://timerecord.azurewebsites.net/images/Vouchers/VoucherTest.jpg"
                                        : $"https://timerecord.azurewebsites.net{AttachmentPath.Substring(1)}";

        [DataType(DataType.DateTime)]
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime Date { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = false)]
        public DateTime DateLocal => Date.ToLocalTime();
    }
}
