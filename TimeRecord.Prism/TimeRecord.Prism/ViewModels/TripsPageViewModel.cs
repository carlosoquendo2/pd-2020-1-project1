
using Newtonsoft.Json;
using TimeRecord.Common.Models;
using TimeRecord.Common.Services;
using TimeRecord.Prism.Helpers;
using System;
using System.Threading.Tasks;
using Prism.Navigation;
using System.Collections.Generic;
using System.Linq;
using TimeRecord.Common.Helpers;
using TimeRecord.Prism.Views;
using Prism.Commands;

namespace TimeRecord.Prism.ViewModels
{
    public class TripsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navegationService;
        private readonly IApiService _apiService;
        private List<TripItemViewModel> _trips;
        private DelegateCommand _registerCommand;

        public TripsPageViewModel(INavigationService navegationService, IApiService apiService)
            : base(navegationService)
        {
            _navegationService = navegationService;
            _apiService = apiService;
            Title = "Trip ";
            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            if (user != null)
            {
                LoadTripsAsync(user);
            }
        }

        public DelegateCommand RegisterCommand => _registerCommand ?? (_registerCommand = new DelegateCommand(AddTrip));

        public List<TripItemViewModel> Trips
        {
            get => _trips;
            set => SetProperty(ref _trips, value);
        }

        private async void LoadTripsAsync(UserResponse user)
        {
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<TripResponse>(
                url,
                "/api",
                "/Trips/User/" + user.Id,
                "bearer",
                token.Token);

            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            var trips = (List<TripResponse>)response.Result;
            Trips = trips.Select(t => new TripItemViewModel(_navegationService)
            {
                Id = t.Id,
                User = t.User,
                EndDate = t.EndDate,
                StartDate = t.StartDate,
                Name = t.Name,
                TripDetails = t.TripDetails,
                Description = t.Description
            }).ToList();
        }

        private async void AddTrip()
        {
            await _navegationService.NavigateAsync(nameof(AddTripPage));
        }
    }
}
