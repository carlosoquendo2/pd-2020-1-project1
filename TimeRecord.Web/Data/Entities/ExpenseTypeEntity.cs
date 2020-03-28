using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeRecord.Web.Data.Entities
{
    public class ExpenseTypeEntity
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "The field {0} can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        public string Name { get; set; }

        public bool Active { get; set; }

        public ICollection<TripDetailEntity> TripDetail { get; set; }
    }
}
