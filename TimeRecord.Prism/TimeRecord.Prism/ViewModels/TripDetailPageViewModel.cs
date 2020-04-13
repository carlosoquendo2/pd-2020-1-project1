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
        private TripResponse _trip;
        private ICollection<TripDetailResponse> _tripDetail;
        public TripDetailPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Trip Detail";
        }

        public ICollection<TripDetailResponse> TripDetails
        {
            get => _tripDetail;
            set => SetProperty(ref _tripDetail, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            _trip = parameters.GetValue<TripResponse>("Trip");
            Title = _trip.Name;
            TripDetails = _trip.TripDetails;
        }
    }
}
