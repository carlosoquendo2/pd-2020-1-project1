using TimeRecord.Prism.Resources;
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

        public static string Culture { get; set; }

        public static string Error => Resource.Error;
    }
}
