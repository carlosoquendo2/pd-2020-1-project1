using System.Collections.Generic;
using TimeRecord.Common.Models;
using TimeRecord.Web.Data.Entities;
using TimeRecord.Web.Models;

namespace TimeRecord.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public TripDetailEntity ToTripDetailEntity(TripDetailViewModel model, string path, bool isNew)
        {
            return new TripDetailEntity
            {
                Id = isNew ? 0 : model.Id,
                Expense = model.Expense,
                ExpenseType = model.ExpenseType,
                Currency = model.Currency,
                Comment = model.Comment,
                Date = model.Date.ToUniversalTime(),
                AttachmentPath = path,
                Name = model.Name
            };
        }

        public TripDetailViewModel ToTripDetailModel(TripDetailEntity tripDetailEntity)
        {
            return new TripDetailViewModel
            {
                Id = tripDetailEntity.Id,
                AttachmentPath = tripDetailEntity.AttachmentPath,
                Expense = tripDetailEntity.Expense,
                ExpenseType = tripDetailEntity.ExpenseType,
                Currency = tripDetailEntity.Currency,
                Comment = tripDetailEntity.Comment,
                Date = tripDetailEntity.Date,
                Name = tripDetailEntity.Name
            };
        }

        public TripEntity ToTriEntity(TripResponse tripResponse, UserEntity user)
        {
            return new TripEntity
            {
                Id = tripResponse.Id,
                Name = tripResponse.Name,
                Description = tripResponse.Description,
                StartDate = tripResponse.StartDate.ToUniversalTime(),
                EndDate = tripResponse.EndDate.ToUniversalTime(),
                User = user
            };
        }

        public TripResponse ToTripResponse(TripEntity tripEntity)
        {
            List<TripDetailResponse> tripDetailsResponse = new List<TripDetailResponse>();
            if (tripEntity.TripDetails != null)
            {
                foreach (TripDetailEntity tripDetail in tripEntity.TripDetails)
                {
                    tripDetailsResponse.Add(ToTripDetailResponse(tripDetail));
                }
            }
            return new TripResponse
            {
                Id = tripEntity.Id,
                Name = tripEntity.Name,
                Description = tripEntity.Description,
                StartDate = tripEntity.StartDateLocal,
                EndDate = tripEntity.EndDateLocal,
                TripDetails = tripDetailsResponse,
                User = ToUserResponse(tripEntity.User)
            };
        }

        public List<TripResponse> ToTripsResponse(List<TripEntity> tripsEntity)
        {
            List<TripResponse> tripsResponse = new List<TripResponse>();
            foreach (TripEntity tripEntity in tripsEntity)
            {
                tripsResponse.Add(ToTripResponse(tripEntity));
            }
            return tripsResponse;
        }

        public TripDetailEntity ToTriDetailEntity(TripDetailResponse tripDetailResponse, TripEntity tripEntity)
        {
            return new TripDetailEntity
            {
                Id = tripDetailResponse.Id,
                Trip = tripEntity,
                ExpenseType = ToExpenseTypeEntity(tripDetailResponse.ExpenseType),
                Name = tripDetailResponse.Name,
                Expense = tripDetailResponse.Expense,
                Currency = tripDetailResponse.Currency,
                Comment = tripDetailResponse.Comment,
                AttachmentPath = tripDetailResponse.AttachmentPath,
                Date = tripDetailResponse.Date.ToUniversalTime(),
    };
        }

        public TripDetailResponse ToTripDetailResponse(TripDetailEntity tripDetailEntity)
        {
            return new TripDetailResponse
            {
                Id = tripDetailEntity.Id,
                ExpenseType = ToExpenseTypeResponse(tripDetailEntity.ExpenseType),
                Name = tripDetailEntity.Name,
                Expense = tripDetailEntity.Expense,
                Currency = tripDetailEntity.Currency,
                Comment = tripDetailEntity.Comment,
                AttachmentPath = tripDetailEntity.AttachmentPath,
                Date = tripDetailEntity.DateLocal
            };
        }

        public List<TripDetailResponse> ToTripDetailsResponse(List<TripDetailEntity> tripDetailsEntity)
        {
            List<TripDetailResponse> tripDetailResponses = new List<TripDetailResponse>();

            foreach (TripDetailEntity tripDetailEntity in tripDetailsEntity)
            {
                tripDetailResponses.Add(ToTripDetailResponse(tripDetailEntity));
            }
            return tripDetailResponses;
        }

        public ExpenseTypeEntity ToExpenseTypeEntity(ExpenseTypeResponse expenseTypeResponse)
        {
            return new ExpenseTypeEntity
            {
                Id = expenseTypeResponse.Id,
                Name = expenseTypeResponse.Name,
                Active = expenseTypeResponse.Active
            };
        }

        public ExpenseTypeResponse ToExpenseTypeResponse(ExpenseTypeEntity expenseTypeEntity)
        {
            return new ExpenseTypeResponse
            {
                Id = expenseTypeEntity.Id,
                Name = expenseTypeEntity.Name,
                Active = expenseTypeEntity.Active
            };
        }

        public UserResponse ToUserResponse(UserEntity user)
        {
            if (user == null)
            {
                return null;
            }

            return new UserResponse
            {
                Address = user.Address,
                Document = user.Document,
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                PicturePath = user.PicturePath,
                UserType = user.UserType
            };
        }
    }
}
