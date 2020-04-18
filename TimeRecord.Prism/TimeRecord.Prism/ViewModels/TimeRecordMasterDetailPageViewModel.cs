using Newtonsoft.Json;
using Prism.Navigation;
using TimeRecord.Common.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TimeRecord.Prism.ViewModels
{
    public class TimeRecordMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public TimeRecordMasterDetailPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
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
                    Title = "Login"
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
