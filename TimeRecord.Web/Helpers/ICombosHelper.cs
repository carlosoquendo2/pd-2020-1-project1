using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace TimeRecord.Web.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboTrips();
    }
}
