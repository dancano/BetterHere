using BetterHere.Common.Helpers;
using BetterHere.Common.Models;
using BetterHere.Prism.Views;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BetterHere.Prism.ViewModels
{
    public class EstablishmentLocationItemViewModel : EstablishmentLocationResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectLocationCommand;

        public EstablishmentLocationItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectLocationCommand => _selectLocationCommand ??
            (_selectLocationCommand = new DelegateCommand(SelectLocationAsync));

        private async void SelectLocationAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                {"location", this }
            };

            Settings.EstablishmentLocation = JsonConvert.SerializeObject(this);
            await _navigationService.NavigateAsync(nameof(LocationDetailPage), parameters);

        }
    }
}
