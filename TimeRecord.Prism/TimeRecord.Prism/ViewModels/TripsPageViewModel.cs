
using Newtonsoft.Json;
using TimeRecord.Common.Models;
using TimeRecord.Common.Services;
using TimeRecord.Prism.Helpers;
using System;
using System.Threading.Tasks;
using Prism.Navigation;
using System.Collections.Generic;
using System.Linq;

namespace TimeRecord.Prism.ViewModels
{
    public class TripsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navegationService;
        private readonly IApiService _apiService;
        private List<TripItemViewModel> _trips;

        public TripsPageViewModel(INavigationService navegationService, IApiService apiService)
            : base(navegationService)
        {
            _navegationService = navegationService;
            _apiService = apiService;
            Title = "Trip ";
            LoadTripsAsync();
        }

        public List<TripItemViewModel> Trips
        {
            get => _trips;
            set => SetProperty(ref _trips, value);
        }

        private async void LoadTripsAsync()
        {
            //UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            //TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            string url = App.Current.Resources["UrlAPI"].ToString();
            Response response = await _apiService.GetListAsync<TripResponse>(
                url,
                "/api",
                "/Trips/User/16c8adf4-c128-4109-9d7f-f6c6c6313da4",
                "bearer",
                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJjYXJsb3NvcXVlbmRvMTIwNkBnbWFpbC5jb20iLCJqdGkiOiJkODk0OGQ2Ni00ZjdiLTRhOGQtODEzNS1lZWIxZjU3MzIxMzkiLCJleHAiOjE1OTU3MzU2MDIsImlzcyI6ImxvY2FsaG9zdCIsImF1ZCI6InVzZXJzIn0.scoZzCoY-56n_MdMGnIFjfTiTIUWfwTYtE78OWStTeA");

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
    }
}
