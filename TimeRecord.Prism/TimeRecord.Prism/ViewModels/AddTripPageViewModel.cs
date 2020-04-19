using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeRecord.Common.Helpers;
using TimeRecord.Common.Models;
using TimeRecord.Common.Services;
using TimeRecord.Prism.Helpers;

namespace TimeRecord.Prism.ViewModels
{
    public class AddTripPageViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private IApiService _apiService;
        private bool _isRunning;
        private DelegateCommand _registerCommand;
        private bool _isEnabled;
        private TripRequest _trip;
        private UserRequest _user;
        private DateTime _startDate;
        private DateTime _endDate;

        public AddTripPageViewModel(INavigationService navegationService, IApiService apiService)
            : base(navegationService)
        {
            _navigationService = navegationService;
            _apiService = apiService;
            Title = Languages.AddTrip;
            User = new UserRequest();
            Trip = new TripRequest();         
        }

        public DelegateCommand RegisterCommand => _registerCommand ?? (_registerCommand = new DelegateCommand(RegisterTripAsync));

        public DateTime StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }

        public DateTime EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }

        public UserRequest User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public TripRequest Trip
        {
            get => _trip;
            set => SetProperty(ref _trip, value);
        }

        public DateTime MinDate { get => DateTime.Now; }

        public DateTime MaxDate { get => DateTime.Now.AddYears(1); }

        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        private async void RegisterTripAsync()
        {
            bool isValid = await ValidateDataAsync();
            if (!isValid)
            {
                return;
            }

            Trip.StartDate = StartDate;
            Trip.EndDate = EndDate;

            IsRunning = true;
            IsEnabled = false;
            string url = App.Current.Resources["UrlAPI"].ToString();

            UserResponse user = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            if (user != null)
            {
                Trip.UserEmail = user.Email;
            }
            TokenResponse token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);

            Response response = await _apiService.RegisterTripAsync(url, "/api", "/Trips/Add", "bearer", token.Token, Trip);
            IsRunning = false;
            IsEnabled = true;
            /*
            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }
            */
            //await App.Current.MainPage.DisplayAlert(Languages.Ok, response.Message, Languages.Accept);
            //await _navigationService.GoBackAsync();

            await _navigationService.NavigateAsync("/TimeRecordMasterDetailPage/NavigationPage/TripsPage");
        }

        private async Task<bool> ValidateDataAsync()
        {

            if (string.IsNullOrEmpty(Trip.Name))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, "Title missing", Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(StartDate.ToString()))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, "Start date missing", Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(EndDate.ToString()))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.LastNameError, Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(Trip.Description))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, Languages.AddressError, Languages.Accept);
                return false;
            }

            return true;
        }
    }
}
