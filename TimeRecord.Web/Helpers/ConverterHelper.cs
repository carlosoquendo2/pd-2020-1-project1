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
    }
}
