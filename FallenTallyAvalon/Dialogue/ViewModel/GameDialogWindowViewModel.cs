
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.Input;
using DeathCounterHotkey.Database.Models;
using FallenTallyAvalon.Controller;

namespace FallenTallyAvalon.Dialogue.ViewModel
{
    public class GameDialogWindowViewModel : INotifyPropertyChanged
    {
        private string _gameName = string.Empty;
        private string _gamePrefix = string.Empty;
        private readonly GameController _gameController;
        private Action _closeWindow;

        public event PropertyChangedEventHandler? PropertyChanged;

        public GameDialogWindowViewModel(GameController gameController, Action closeWindow)
        {
            _closeWindow = closeWindow;
            _gameController = gameController;
            SaveCommand = new RelayCommand(OnSave, CanSave);
            CloseCommand = new RelayCommand(OnClose);
        }

        public string GameName
        {
            get => _gameName;
            set
            {
                if (_gameName != value)
                {
                    _gameName = value;
                    OnPropertyChanged();
                    SaveCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public string GamePrefix
        {
            get => _gamePrefix;
            set
            {
                if (_gamePrefix != value)
                {
                    _gamePrefix = value;
                    OnPropertyChanged();
                    SaveCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public RelayCommand SaveCommand { get; }
        public RelayCommand CloseCommand { get; }

        public GameStatsModel? Result { get; private set; }
        public bool? DialogResult { get; private set; }

        private void OnSave()
        {
            Result = new GameStatsModel
            {
                GameName = GameName,
                Prefix = GamePrefix
            };
            DialogResult = true;
            _closeWindow.Invoke();
        }

        private void OnClose()
        {
            Result = null;
            DialogResult = false;
        }

        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(GameName)
                && !string.IsNullOrWhiteSpace(GamePrefix)
                && !_gameController.IsDupeName(GameName);
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
