using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using TimeRecord.Common.Models;

namespace TimeRecord.Prism.ViewModels
{
    public class TripDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navegationService;
        private TripResponse _trip;
        private ICollection<TripDetailItemViewModel> _tripDetail;
        public TripDetailPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _navegationService = navigationService;
            Title = "Trip Detail";
        }

        public ICollection<TripDetailItemViewModel> TripDetails
        {
            get => _tripDetail;
            set => SetProperty(ref _tripDetail, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            _trip = parameters.GetValue<TripResponse>("Trip");
            Title = _trip.Name;
            var tripDetail = (List<TripDetailResponse>)_trip.TripDetails;
            TripDetails = tripDetail.Select(td => new TripDetailItemViewModel(_navegationService)
            {
                Id = td.Id,
                ExpenseType = td.ExpenseType,
                Name = td.Name,
                Expense = td.Expense,
                Currency = td.Currency,
                Comment = td.Comment,
                AttachmentPath = td.AttachmentPath,
                Date = td.Date
            }).ToList();
        }
    }
}
