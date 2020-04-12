using Prism.Navigation;
using System.Collections.Generic;
using System.Linq;
using TimeRecord.Common.Models;
using TimeRecord.Common.Services;

namespace TimeRecord.Prism.ViewModels
{
    public class TripsPageViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;
        private List<TripResponse> _trips;

        public TripsPageViewModel(INavigationService navegationService, IApiService apiService)
            : base(navegationService)
        {
            _apiService = apiService;
            Title = "Trip ";
            LoadTripsAsync();
        }

        public List<TripResponse> Trips
        {
            get => _trips;
            set => SetProperty(ref _trips, value);
        }

        private async void LoadTripsAsync()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<TripResponse>(
                url,
                "/api",
                "/Trips/User/16c8adf4-c128-4109-9d7f-f6c6c6313da4");

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            Trips = (List<TripResponse>)response.Result;
        }
    }
}
