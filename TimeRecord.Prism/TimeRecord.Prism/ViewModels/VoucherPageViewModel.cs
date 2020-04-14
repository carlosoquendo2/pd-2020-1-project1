using Prism.Navigation;
using System.Collections.Generic;
using TimeRecord.Common.Models;

namespace TimeRecord.Prism.ViewModels
{
    public class VoucherPageViewModel : ViewModelBase
    {
        private TripDetailResponse _tripDetail;
        private string _textExpense;
        public VoucherPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Trip detail";
        }

        public TripDetailResponse TripDetail
        {
            get => _tripDetail;
            set => SetProperty(ref _tripDetail, value);
        }

        public string TextExpense
        { 
            get => _textExpense;
            set => SetProperty(ref _textExpense, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            TripDetail = parameters.GetValue<TripDetailResponse>("TripDetail");
            Title = TripDetail.Name;
            TextExpense = TripDetail.Expense + " " + TripDetail.Currency;
        }
    }
}
