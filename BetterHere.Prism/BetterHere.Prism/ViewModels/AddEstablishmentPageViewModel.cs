using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BetterHere.Prism.ViewModels
{
    public class AddEstablishmentPageViewModel : ViewModelBase
    {
        public AddEstablishmentPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Add Establishment";
        }
    }
}
