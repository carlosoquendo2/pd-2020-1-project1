using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using TimeRecord.Web.Data.Entities;

namespace TimeRecord.Web.Models
{
    public class TripDetailViewModel : TripDetailEntity
    {
        [Display(Name = "Voucher")]
        public IFormFile VoucherFile { get; set; }
    }
}
