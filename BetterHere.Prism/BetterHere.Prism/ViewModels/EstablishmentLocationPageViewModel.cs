using BetterHere.Common.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace BetterHere.Prism.ViewModels
{
    public class EstablishmentLocationPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private EstablishmentResponse _establishment;
        private bool _isRunning;
        private bool _isEnabled;

        public EstablishmentLocationPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            Title = "Establishments Locations";
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

        public EstablishmentResponse Establishment
        {
            get => _establishment;
            set => SetProperty(ref _establishment, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("establishment"))
            {
                Establishment = parameters.GetValue<EstablishmentResponse>("establishment");
            }
        }
    }
}
