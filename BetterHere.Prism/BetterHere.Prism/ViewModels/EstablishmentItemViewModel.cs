using BetterHere.Common.Helpers;
using BetterHere.Common.Models;
using BetterHere.Prism.Views;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Navigation;

namespace BetterHere.Prism.ViewModels
{
    public class EstablishmentItemViewModel : EstablishmentResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectEstablishmentCommand;

        public EstablishmentItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectEstablishmentCommand => _selectEstablishmentCommand ??
            (_selectEstablishmentCommand = new DelegateCommand(SelectEstablishmentAsync));

        private async void SelectEstablishmentAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                {"establishment", this }
            };

            Settings.Establishment = JsonConvert.SerializeObject(this);
            await _navigationService.NavigateAsync(nameof(EstablishmentLocationPage), parameters);

        }
    }
}
