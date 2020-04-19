using Newtonsoft.Json;
using Prism.Navigation;
using TimeRecord.Common.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TimeRecord.Common.Helpers;
using TimeRecord.Prism.Helpers;

namespace TimeRecord.Prism.ViewModels
{
    public class TimeRecordMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private UserResponse _user;

        public TimeRecordMasterDetailPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            LoadUser();
            LoadMenus();
        }

        public UserResponse User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        private void LoadUser()
        {
            if (Settings.IsLogin)
            {
                User = JsonConvert.DeserializeObject<UserResponse>(Settings.User);
            }
        }


        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "trips",
                    PageName = "TripsPage",
                    Title = "My trips"
                },
                new Menu
                {
                    Icon = "user",
                    PageName = "ModifyUserPage",
                    Title = "Modify user"
                },
                new Menu
                {
                    Icon = "login",
                    PageName = "LoginPage",
                    Title = Settings.IsLogin ? Languages.Logout : Languages.Login
                }
            };

            Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title
                }).ToList());
        }
    }
}
