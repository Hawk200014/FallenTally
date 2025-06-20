using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FallenTally.Controller;
using FallenTally.Controller.Forms;
using FallenTally.Database.Models;
using System;
using System.Collections.ObjectModel;

namespace FallenTally.ViewModels
{
    public partial class ExportViewModel : ObservableObject
    {
        private GameController _gameController;
        private LocationController _locationController;
        private MarkerController _markerController;
        private ExportController _exportController;

        #region constructor
        public ExportViewModel(GameController gameController, LocationController locationController, MarkerController markerController, ExportController exportController)
        {
            this._gameController = gameController;
            this._locationController = locationController;
            this._markerController = markerController;
            this._exportController = exportController;

            // Initialize properties or load data if necessary
            InitializeExportTypes();
            InitializeDeathGames();
            InitializeDeathLocations();
            InitializeMarkerGames();
            InitializeMarkerTypes();

        }

        private void InitializeMarkerTypes()
        {
            MarkerTypes = new ObservableCollection<string>
            {
                "Normal",
                "Funny",
                "Game",
                "Talk",
                "Pause"
            };
        }

        private void InitializeMarkerGames()
        {
            MarkerGames = new ObservableCollection<GameStatsModel>(_gameController.GetGameStats());
        }

        private void InitializeDeathLocations()
        {
            DeathLocations = new ObservableCollection<DeathLocationModel>(_locationController.GetListOfLocations(null)); // Pass the appropriate game model if needed
        }

        private void InitializeDeathGames()
        {
            DeathGames = new ObservableCollection<GameStatsModel>(_gameController.GetGameStats());
        }

        private void InitializeExportTypes()
        {
            ExportTypes = new ObservableCollection<string>
            {
                "CSV",
                "EXCEL",
                "FTSTAMPS"
            };
        }

        #endregion

        #region Deaths Tab Bindings

        [ObservableProperty]
        private bool deathExportButtonEnabled;

        [ObservableProperty]
        private int actualDeathEntries;

        [ObservableProperty]
        private int maxDeathEntries;

        [ObservableProperty]
        private ObservableCollection<GameStatsModel> deathGames = new();

        [ObservableProperty]
        private string selectedDeathGame;

        [ObservableProperty]
        private ObservableCollection<DeathLocationModel> deathLocations = new();

        [ObservableProperty]
        private string selectedDeathLocation;

        [ObservableProperty]
        private DateTime? selectedDeathFromDate;

        [ObservableProperty]
        private DateTime? selectedDeathToDate;

        [ObservableProperty]
        private ObservableCollection<string> exportTypes = new();

        [ObservableProperty]
        private string selectedDeathExportType;

        [RelayCommand]
        private void ExportDeath()
        {
            // Implement export logic here
        }
        #endregion


        #region Markers Tab Bindings

        [ObservableProperty]
        private bool markerExportButtonEnabled;

        [ObservableProperty]
        private int actualMarkerEntries;

        [ObservableProperty]
        private int maxMarkerEntries;

        [ObservableProperty]
        private ObservableCollection<GameStatsModel> markerGames = new();

        [ObservableProperty]
        private string selecteMarkerGame;

        [ObservableProperty]
        private ObservableCollection<string> markerTypes = new();

        [ObservableProperty]
        private string selectedMarkerType;

        [ObservableProperty]
        private DateTime? selectedMarkerFromDate;

        [ObservableProperty]
        private DateTime? selectedMarkerToDate;

        [ObservableProperty]
        private string selectedMarkerExportType;


        [RelayCommand]
        private void ExportMarker()
        {
            // Implement export logic here
        }

        #endregion
    }
}
