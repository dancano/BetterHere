using BetterHere.Common.Helpers;
using BetterHere.Common.Models;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace BetterHere.Prism.ViewModels
{
    public class EstablishmentItemViewModelViewModel : EstablishmentResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectEstablishmentCommand;

        public EstablishmentItemViewModelViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectEstablishmentCommand => _selectEstablishmentCommand ??
            (_selectEstablishmentCommand = new DelegateCommand(SelectEstablishmentAsync));

        private async void SelectEstablishmentAsync()
        {
            var parameters = new NavigationParameters
            {
                {"establishmentList", this }
            };

            Settings.Establishment = JsonConvert.SerializeObject(this);
            await _navigationService.NavigateAsync(nameof(EstablishmentLocationPageViewModel), parameters);

        }
    }
}
