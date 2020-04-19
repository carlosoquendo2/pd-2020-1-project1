using System.Collections.Generic;
using TimeRecord.Common.Models;
using TimeRecord.Web.Data.Entities;
using TimeRecord.Web.Models;

namespace TimeRecord.Web.Helpers
{
    public interface IConverterHelper
    {
        TripDetailEntity ToTripDetailEntity(TripDetailViewModel model, string path, bool isNew);

        TripDetailViewModel ToTripDetailModel(TripDetailEntity tripDetailEntity);

        TripEntity ToTriEntity(TripResponse tripResponse, UserEntity user);

        TripResponse ToTripResponse(TripEntity tripEntity);

        List<TripResponse> ToTripsResponse(List<TripEntity> tripsEntity);

        TripDetailEntity ToTriDetailEntity(TripDetailResponse tripResponse, TripEntity tripEntity);

        TripDetailResponse ToTripDetailResponse(TripDetailEntity tripDetailEntity);

        List<TripDetailResponse> ToTripDetailsResponse(List<TripDetailEntity> tripDetailsEntity);

        ExpenseTypeEntity ToExpenseTypeEntity(ExpenseTypeResponse expenseTypeResponse);

        ExpenseTypeResponse ToExpenseTypeResponse(ExpenseTypeEntity expenseTypeEntity);

        UserResponse ToUserResponse(UserEntity user);

        TripEntity tripRequestToEntity(TripRequest tripRequest);
    }

}
