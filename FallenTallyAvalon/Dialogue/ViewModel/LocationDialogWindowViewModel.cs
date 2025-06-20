using CommunityToolkit.Mvvm.Input;
using FallenTally.Database.Models;
using DocumentFormat.OpenXml.Wordprocessing;
using FallenTally.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FallenTally.Dialogue.ViewModel
{
    internal class LocationDialogWindowViewModel : INotifyPropertyChanged
    {
        private string _locationName = string.Empty;
        private readonly LocationController _locationController;
        private readonly GameStatsModel _activeGame;
        private Action _closeWindow;


        public event PropertyChangedEventHandler? PropertyChanged;

        public LocationDialogWindowViewModel(LocationController locationController, GameStatsModel activeGame, Action closeWindow)
        {
            _closeWindow = closeWindow;
            _locationController = locationController;
            _activeGame = activeGame;
            SaveCommand = new RelayCommand(OnSave, CanSave);
            CloseCommand = new RelayCommand(OnClose);
        }

        public string LocationName
        {
            get => _locationName;
            set
            {
                if (_locationName != value)
                {
                    _locationName = value;
                    OnPropertyChanged();
                    SaveCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public RelayCommand SaveCommand { get; }
        public RelayCommand CloseCommand { get; }

        public DeathLocationModel? Result { get; private set; }
        public bool? DialogResult { get; private set; }

        private void OnSave()
        {
            Result = new DeathLocationModel
            {
                Name = LocationName,
                GameID = _activeGame.GameId,
                Finish = false
            };
            DialogResult = true;
            _closeWindow.Invoke();
        }

        private void OnClose()
        {
            Result = null;
            DialogResult = false;
            _closeWindow.Invoke();
        }

        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(LocationName)
                && !_locationController.IsDupeName(_activeGame, LocationName);
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
