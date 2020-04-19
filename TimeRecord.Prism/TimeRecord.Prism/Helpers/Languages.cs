﻿using TimeRecord.Prism.Resources;
using System.Globalization;
using Xamarin.Forms;
using TimeRecord.Common.Interfaces;

namespace TimeRecord.Prism.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            CultureInfo ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            Culture = ci.Name;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Ok => Resource.Ok;

        public static string DocumentError => Resource.DocumentError;

        public static string FirstNameError => Resource.FirstNameError;

        public static string LastNameError => Resource.LastNameError;

        public static string Address => Resource.Address;

        public static string AddressError => Resource.AddressError;

        public static string AddressPlaceHolder => Resource.AddressPlaceHolder;

        public static string Phone => Resource.Phone;

        public static string PhoneError => Resource.PhoneError;

        public static string PhonePlaceHolder => Resource.PhonePlaceHolder;

        public static string FavoriteTeam => Resource.FavoriteTeam;

        public static string FavoriteTeamError => Resource.FavoriteTeamError;

        public static string FavoriteTeamPlaceHolder => Resource.FavoriteTeamPlaceHolder;

        public static string PasswordConfirm => Resource.PasswordConfirm;

        public static string PasswordConfirmError1 => Resource.PasswordConfirmError1;

        public static string PasswordConfirmError2 => Resource.PasswordConfirmError2;

        public static string PasswordConfirmPlaceHolder => Resource.PasswordConfirmPlaceHolder;
        
        public static string Logout => Resource.Logout;

        public static string ConnectionError => Resource.ConnectionError;

        public static string LoginError => Resource.LoginError;

        public static string Culture { get; set; }

        public static string Login => Resource.Login; 

        public static string Accept => Resource.Accept;

        public static string Email => Resource.Email;

        public static string EmailPlaceHolder => Resource.EmailPlaceHolder;

        public static string EmailError => Resource.EmailError;

        public static string Password => Resource.Password;

        public static string PasswordError => Resource.PasswordError;

        public static string PasswordPlaceHolder => Resource.PasswordPlaceHolder;

        public static string Register => Resource.Register;

        public static string Error => Resource.Error;
    }
}