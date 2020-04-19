using Prism;
using Prism.Ioc;
using Syncfusion.Licensing;
using TimeRecord.Common.Services;
using TimeRecord.Prism.ViewModels;
using TimeRecord.Prism.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TimeRecord.Prism
{
    public partial class App
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            SyncfusionLicenseProvider.RegisterLicense("MjQyNzM4QDMxMzgyZTMxMmUzMGpxdWdIWDJDbmdjNFNXcElOdHc4WnIxK2w1bUdMVXBTZFlaWG9rUjlYa289");
            InitializeComponent();
            await NavigationService.NavigateAsync("/TimeRecordMasterDetailPage/NavigationPage/TripsPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<TripsPage, TripsPageViewModel>();
            containerRegistry.RegisterForNavigation<TripDetailPage, TripDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<TimeRecordMasterDetailPage, TimeRecordMasterDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<VoucherPage, VoucherPageViewModel>();
            containerRegistry.RegisterForNavigation<ModifyUserPage, ModifyUserPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
        }
    }
}
