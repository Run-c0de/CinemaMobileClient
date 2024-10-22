﻿

using CinemaMobileClient.Services;
using CinemaMobileClient.Views;

namespace CinemaMobileClient.ViewModels
{
  public class PaymentViewModel : ViewModelBase
  {
    private readonly IStoreService _storeService;
    private string _cardCvv;
    private string _cardExpirationDate;
    private string _cardNumber;
    private string _purchaseText;

    private string _totalCharge;

    public PaymentViewModel(INavigationService navService, IStoreService store)
      : base(navService)
    {
      _storeService = store;
      PurchaseText = $"Purchase for ${_storeService.TotalCost.ToString("##.00")}";

    }
    public string TotalCharge
    {
      get => _totalCharge;
      set => SetProperty(ref _totalCharge, value);
    }
    

    public string CardCvv
    {
      get => _cardCvv;
      set => SetProperty(ref _cardCvv, value);
    }

    public string CardExpirationDate
    {
      get => _cardExpirationDate;
      set => SetProperty(ref _cardExpirationDate, value);
    }

    public string CardNumber
    {
      get => _cardNumber;
      set => SetProperty(ref _cardNumber, value);
    }

    public DelegateCommand CmdGoBack => new DelegateCommand(async () =>
    {
      var args = new NavigationParameters();
      await NavigationService.GoBackAsync(args);
    });

    public DelegateCommand CmdPurchase => new DelegateCommand(async () =>
    {
      if (IsValidPurchase())
      {
        var args = new NavigationParameters
        {
          { "total", _storeService.TotalCost.ToString("##.00") }
        };

        await NavigationService.NavigateAsync(nameof(ReceiptView), args);
      }
    });


    public string PurchaseText
    {
      get => _purchaseText;
      set => SetProperty(ref _purchaseText, value);
    }

    public override void OnNavigatedTo(INavigationParameters parameters)
    {
    }


    private bool IsValidPurchase()
    {
      bool isCondition = string.IsNullOrEmpty(CardCvv) && string.IsNullOrEmpty(CardExpirationDate);

      return _storeService.TotalCost > 0;
    }
  }
}
