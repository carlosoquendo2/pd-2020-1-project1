using Prism.Commands;
using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using TimeRecord.Common.Models;
using TimeRecord.Common.Services;
using TimeRecord.Prism.Helpers;
using System;
using Newtonsoft.Json;
using TimeRecord.Common.Helpers;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace TimeRecord.Prism.ViewModels
{
    public class AddTripDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private readonly IFilesHelper _filesHelper;
        private DelegateCommand _registerCommand;
        private DelegateCommand _changeImageCommand;
        private TripDetailRequest _tripDetail;
        private DateTime _date;
        private bool _isRunning;
        private ImageSource _image;
        private bool _isEnabled;
        private ObservableCollection<ExpenseTypeResponse> _expenseTypes;
        private ExpenseTypeResponse _expenseType;
        private List<string> _currencyTypes;
        private string _currencyType;
        private MediaFile _file;
        private String _tripId;

        public AddTripDetailPageViewModel(
            INavigationService navegationService, 
            IApiService apiService,
            IFilesHelper filesHelper)
            : base(navegationService)
        {
            _navigationService = navegationService;
            _apiService = apiService;
            Title = Languages.AddVoucher;
            TripDetail = new TripDetailRequest();
            Date = DateTime.Now;
            IsRunning = false;
            IsEnabled = false;
            _filesHelper = filesHelper;
            Image = App.Current.Resources["UrlNoImage"].ToString();
            LoadPikersAsync();
        }

        public DelegateCommand ChangeImageCommand => _changeImageCommand ?? (_changeImageCommand = new DelegateCommand(ChangeImageAsync));

        public DelegateCommand RegisterCommand => _registerCommand ?? (_registerCommand = new DelegateCommand(RegisterTripDetailAsync));

        public TripDetailRequest TripDetail
        {
            get => _tripDetail;
            set => SetProperty(ref _tripDetail, value);
        }

        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

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

        public ImageSource Image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        public UserResponse user
        {
            get => JsonConvert.DeserializeObject<UserResponse>(Settings.User);
        }
        
        public TokenResponse token
        {
            get => JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
        }

        public ObservableCollection<ExpenseTypeResponse> ExpenseTypes
        {
            get => _expenseTypes;
            set => SetProperty(ref _expenseTypes, value);
        }

        public ExpenseTypeResponse ExpenseType
        {
            get => _expenseType;
            set => SetProperty(ref _expenseType, value);
        }

        public List<String> CurrencyTypes
        {
            get => _currencyTypes;
            set => SetProperty(ref _currencyTypes, value);
        }

        public String CurrencyType
        {
            get => _currencyType;
            set => SetProperty(ref _currencyType, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            _tripId = parameters["TripId"].ToString();
        }

        private async void RegisterTripDetailAsync()
        {
            bool isValid = await ValidateDataAsync();
            if (!isValid)
            {
                return;
            }

            byte[] imageArray = null;
            if (_file != null)
            {
                imageArray = _filesHelper.ReadFully(_file.GetStream());
            }

            TripDetail.Currency = CurrencyType;
            TripDetail.VoucherArray = imageArray;
            TripDetail.Date = Date;
            TripDetail.Day = Date.Day;
            TripDetail.Mounth = Date.Month;
            TripDetail.Year = Date.Year;
            TripDetail.ExpenseTypeId = ExpenseType.Id.ToString();
            TripDetail.TripId = _tripId;

            IsRunning = true;
            IsEnabled = false;
            string url = App.Current.Resources["UrlAPI"].ToString();

            Response response = await _apiService.RegisterTripDetailAsync(url, "/api", "/Trips/AddDetail", "bearer", token.Token, TripDetail);
            IsRunning = false;
            IsEnabled = true;
            /*
            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }
            */
            await _navigationService.NavigateAsync("/TimeRecordMasterDetailPage/NavigationPage/TripsPage");
        }

        private async Task<bool> ValidateDataAsync()
        {

            if (string.IsNullOrEmpty(TripDetail.Name))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, "Title missing", Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(Date.ToString()))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, "Date missing", Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(TripDetail.Comment))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, "Comment missing", Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(CurrencyType))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, "Currency missing", Languages.Accept);
                return false;
            }

            if (string.IsNullOrEmpty(TripDetail.Expense.ToString()))
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, "Expense missing", Languages.Accept);
                return false;
            }

            if (_tripId == null)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, "Trip missing", Languages.Accept);
                return false;
            }

            if (ExpenseType == null)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, "Expense type missing", Languages.Accept);
                return false;
            }

            return true;
        }

        private async void LoadPikersAsync()
        {
            string url = App.Current.Resources["UrlAPI"].ToString();

            Response currencyResponse = await _apiService.GetListAsync<String>(url, "/api", "/Currencies", "bearer", token.Token);
            Response expenseTypeResponse = await _apiService.GetListAsync<ExpenseTypeResponse>(url, "/api", "/ExpenseTypes", "bearer", token.Token);

            if (!currencyResponse.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, currencyResponse.Message, Languages.Accept);
                return;
            }
            if (!expenseTypeResponse.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(Languages.Error, expenseTypeResponse.Message, Languages.Accept);
                return;
            }

            CurrencyTypes = (List<String>)currencyResponse.Result;
            List<ExpenseTypeResponse> listExpenseTypes = (List<ExpenseTypeResponse>)expenseTypeResponse.Result;
            ExpenseTypes = new ObservableCollection<ExpenseTypeResponse>(listExpenseTypes.OrderBy(t => t.Name));
        }

        private async void ChangeImageAsync()
        {
            await CrossMedia.Current.Initialize();

            string source = await Application.Current.MainPage.DisplayActionSheet(
                Languages.PictureSource,
                Languages.Cancel,
                null,
                Languages.FromGallery,
                Languages.FromCamera);

            if (source == Languages.Cancel)
            {
                _file = null;
                return;
            }

            if (source == Languages.FromCamera)
            {
                _file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                    }
                );
            }
            else
            {
                _file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (_file != null)
            {
                Image = ImageSource.FromStream(() =>
                {
                    System.IO.Stream stream = _file.GetStream();
                    return stream;
                });
            }
        }
    }
}
