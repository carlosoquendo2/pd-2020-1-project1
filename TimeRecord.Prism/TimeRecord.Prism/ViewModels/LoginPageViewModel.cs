using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using TimeRecord.Common.Services;

namespace TimeRecord.Prism.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private INavigationService _navegationService;
        private IApiService _apiService;

        public LoginPageViewModel(INavigationService navegationService, IApiService apiService)
            : base(navegationService)
        {
            _navegationService = navegationService;
            _apiService = apiService;
            Title = "Login";
        }
    }
}
