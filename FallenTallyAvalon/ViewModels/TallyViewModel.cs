using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FallenTally.Database.Models;

namespace FallenTallyAvalon.ViewModels
{
    public class TallyViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<string> _counterList1 = new();
        private ObservableCollection<string> _counterList2 = new();
        private string? _selectedCounter1;
        private string? _selectedCounter2;
        private string _counterValue = string.Empty;
        private int counterValueInt = 0;
        private ObservableCollection<MarkerModel> _markers = new();

        public ObservableCollection<string> CounterList1
        {
            get => _counterList1;
            set { _counterList1 = value; OnPropertyChanged(); }
        }

        public ObservableCollection<string> CounterList2
        {
            get => _counterList2;
            set { _counterList2 = value; OnPropertyChanged(); }
        }

        public string? SelectedCounter1
        {
            get => _selectedCounter1;
            set { _selectedCounter1 = value; OnPropertyChanged(); }
        }

        public string? SelectedCounter2
        {
            get => _selectedCounter2;
            set { _selectedCounter2 = value; OnPropertyChanged(); }
        }

        public string CounterValue
        {
            get => _counterValue;
            set { _counterValue = value; OnPropertyChanged(); }
        }

        public ObservableCollection<MarkerModel> Markers
        {
            get => _markers;
            set { _markers = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
