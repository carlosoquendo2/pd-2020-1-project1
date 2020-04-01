using TimeRecord.Web.Data.Entities;
using TimeRecord.Web.Models;

namespace TimeRecord.Web.Helpers
{
    public interface IConverterHelper
    {
        TripDetailEntity ToTripDetailEntity(TripDetailViewModel model, string path, bool isNew);

        TripDetailViewModel ToTripDetailModel(TripDetailEntity tripDetailEntity);
    }

}
