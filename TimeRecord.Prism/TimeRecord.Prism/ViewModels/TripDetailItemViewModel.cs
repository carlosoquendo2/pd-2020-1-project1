using Prism.Commands;
using Prism.Navigation;
using System;
using TimeRecord.Common.Models;
using TimeRecord.Prism.Views;

namespace TimeRecord.Prism.ViewModels
{
    public class TripDetailItemViewModel : TripDetailResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectTripDetailCommand;

        public TripDetailItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectTripDetailCommand => _selectTripDetailCommand ?? (_selectTripDetailCommand = new DelegateCommand(SelectTripDetailAsync));

        private async void SelectTripDetailAsync()
        {
            var parameters = new NavigationParameters
            {
                {"TripDetail", this}
            };
            await _navigationService.NavigateAsync(nameof(VoucherPage), parameters);
        }
    }
}
