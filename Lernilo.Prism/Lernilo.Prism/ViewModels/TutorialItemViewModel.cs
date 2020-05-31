using Lernilo.Common.Models;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lernilo.Prism.ViewModels
{
    public class TutorialItemViewModel : TutorialResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectTutorialCommand;

        public TutorialItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectTutorialCommand => _selectTutorialCommand ?? (_selectTutorialCommand = new DelegateCommand(SelectTutorialAsync));

        private async void SelectTutorialAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "tutorial", this }
            };

            await _navigationService.NavigateAsync("TutorialDetailsPage", parameters);
        }
    }

}

