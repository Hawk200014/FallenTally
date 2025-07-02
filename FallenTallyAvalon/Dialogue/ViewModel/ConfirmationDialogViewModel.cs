using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace FallenTally.Dialogue.ViewModel
{
    public class ConfirmationDialogViewModel : INotifyPropertyChanged
    {
        public bool? DialogResult { get; private set; }
        public ICommand YesCommand { get; }
        public ICommand NoCommand { get; }
        public event PropertyChangedEventHandler? PropertyChanged;
        private Action _closeWindowAction;




        public ConfirmationDialogViewModel(Action closeWindowAction)
        {
            YesCommand = new RelayCommand(OnYes);
            NoCommand = new RelayCommand(OnNo);
            _closeWindowAction = closeWindowAction;
        }

        private void OnYes()
        {
            DialogResult = true;
            OnPropertyChanged(nameof(DialogResult));
            _closeWindowAction?.Invoke();
        }

        private void OnNo()
        {
            DialogResult = false;
            OnPropertyChanged(nameof(DialogResult));
            _closeWindowAction?.Invoke();
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
