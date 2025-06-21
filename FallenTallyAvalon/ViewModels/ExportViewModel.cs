using Avalonia.Controls;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using FallenTally.Controller;
using FallenTally.Controller.Forms;
using FallenTally.Database.Models;
using FallenTally.Helper;
using FallenTally.Views;
using FallenTallyAvalon.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FallenTally.ViewModels;

public partial class ExportViewModel : ObservableObject
{
    private GameController _gameController;
    private LocationController _locationController;
    private MarkerController _markerController;
    private ExportController _exportController;
    private DeathController _deathController;

    #region constructor
    public ExportViewModel(GameController gameController, LocationController locationController, DeathController deathController, MarkerController markerController, ExportController exportController)
    {
        this._gameController = gameController;
        this._locationController = locationController;
        this._markerController = markerController;
        this._exportController = exportController;
        this._deathController = deathController;

        // Initialize properties or load data if necessary
        Init();
    }

    public void Init()
    {
        InitializeExportTypes();
        InitializeDeathGames();
        InitializeDeathLocations();
        InitializeMarkerGames();
        InitializeMarkerTypes();
        InitializeLabels();
    }

    private void InitializeLabels()
    {
        MaxDeathEntries = _deathController.InitFilter().Count();
        ActualDeathEntries = _deathController.InitFilter().Count();

        MaxMarkerEntries = _markerController.InitFilter().Count();
        ActualMarkerEntries = _markerController.InitFilter().Count();
    }

    private void InitializeMarkerTypes()
    {
        MarkerTypes = new ObservableCollectionCopy<string>(
            GetDefaultMarkerType(),
            new ObservableCollection<string>(_markerController.InitFilter().Select(x => x.categorie).Distinct()));
        SelectedMarkerType = MarkerTypes.FirstOrDefault() ?? string.Empty;
    }

    private void InitializeMarkerGames()
    {
        MarkerGames = new ObservableCollectionCopy<GameStatsModel>(
            GetDefaultDeathGame()
            , new ObservableCollection<GameStatsModel>(_gameController.GetGameStats()));
        SelectedMarkerGame = MarkerGames.FirstOrDefault();
    }



    private void InitializeDeathLocations()
    {
        DeathLocations = new ObservableCollectionCopy<DeathLocationModel>(
            GetDefaultDeathLocation(),
            new ObservableCollection<DeathLocationModel>(_locationController.GetListOfLocations(null)));
        SelectedDeathLocation = DeathLocations.FirstOrDefault();
    }

    private void InitializeDeathGames()
    {
        DeathGames = new ObservableCollectionCopy<GameStatsModel>(
            GetDefaultDeathGame()
            , new ObservableCollection<GameStatsModel>(_gameController.GetGameStats()));
        SelectedDeathGame = DeathGames.FirstOrDefault();
    }

    private void InitializeExportTypes()
    {
        ExportTypes = new ObservableCollection<string>
        {
            "CSV",
            "EXCEL",
            "FTSTAMPS"
        };
        SelectedDeathExportType = ExportTypes.FirstOrDefault() ?? string.Empty;
        SelectedMarkerExportType = ExportTypes.FirstOrDefault() ?? string.Empty;
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
    private GameStatsModel selectedDeathGame;

    [ObservableProperty]
    private ObservableCollection<DeathLocationModel> deathLocations = new();

    [ObservableProperty]
    private DeathLocationModel selectedDeathLocation;

    [ObservableProperty]
    private DateTime? selectedDeathFromDate;

    [ObservableProperty]
    private DateTime? selectedDeathToDate;

    [ObservableProperty]
    private ObservableCollection<string> exportTypes = new();

    [ObservableProperty]
    private string selectedDeathExportType;

    [RelayCommand]
    private async Task ExportDeath()
    {
        // Get top level from the current control. Alternatively, you can use Window reference instead.
        var topLevel = TopLevel.GetTopLevel(MainWindow.Instance);

        List<FilePickerFileType> fileTypes = new();

        if(SelectedDeathExportType == "CSV")
        {
            fileTypes.Add(FileTypes.CSV);
        }
        else if (SelectedDeathExportType == "EXCEL")
        {
            fileTypes.Add(FileTypes.EXCEL);
        }
        else if (SelectedDeathExportType == "FTSTAMPS")
        {
            fileTypes.Add(FileTypes.FTSTAMPS);
        }

        // Start async operation to open the dialog.
        var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = "Save Text File",
            FileTypeChoices = fileTypes
        });

        if (file is not null)
        {
            ExportController.ExportType type = ExportController.ExportType.CSV;
            if (SelectedDeathExportType == "CSV")
            {
                type = ExportController.ExportType.CSV;
            }
            else if (SelectedDeathExportType == "EXCEL")
            {
                type = ExportController.ExportType.EXCEL;
            }
            else if (SelectedDeathExportType == "FTSTAMPS")
            {
                type = ExportController.ExportType.FTSTAMPS;
            }

            string gamename = (SelectedDeathGame?.GameName ?? "All Games") == "All Games" ? "" : SelectedDeathGame?.GameName ?? "";
            string locationName = (SelectedDeathLocation?.Name ?? "All Locations") == "All Locations" ? "" : SelectedDeathLocation?.Name ?? "";
            DateOnly? fromDate = SelectedDeathFromDate.HasValue ? DateOnly.FromDateTime(SelectedDeathFromDate.Value) : null;
            DateOnly? toDate = SelectedDeathToDate.HasValue ? DateOnly.FromDateTime(SelectedDeathToDate.Value) : null;

            _exportController.Export(type, file.Path.AbsolutePath, gamename, locationName, fromDate, toDate);
        }
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
    private GameStatsModel selectedMarkerGame;

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
    private async Task ExportMarker()
    {
        // Get top level from the current control. Alternatively, you can use Window reference instead.
        var topLevel = TopLevel.GetTopLevel(MainWindow.Instance);

        List<FilePickerFileType> fileTypes = new();

        if (SelectedDeathExportType == "CSV")
        {
            fileTypes.Add(FileTypes.CSV);
        }
        else if (SelectedDeathExportType == "EXCEL")
        {
            fileTypes.Add(FileTypes.EXCEL);
        }
        else if (SelectedDeathExportType == "FTSTAMPS")
        {
            fileTypes.Add(FileTypes.FTSTAMPS);
        }

        // Start async operation to open the dialog.
        var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = "Save Text File",
            FileTypeChoices = fileTypes
        });

        if (file is not null)
        {
            ExportController.ExportType type = ExportController.ExportType.CSV;
            if (SelectedDeathExportType == "CSV")
            {
                type = ExportController.ExportType.CSV;
            }
            else if (SelectedDeathExportType == "EXCEL")
            {
                type = ExportController.ExportType.EXCEL;
            }
            else if (SelectedDeathExportType == "FTSTAMPS")
            {
                type = ExportController.ExportType.FTSTAMPS;
            }

            string gamename = (SelectedDeathGame?.GameName ?? "All Games") == "All Games" ? "" : SelectedDeathGame?.GameName ?? "";
            string markerType = (SelectedMarkerType ?? "All") == "All" ? "" : SelectedMarkerType ?? "";
            DateOnly? fromDate = SelectedDeathFromDate.HasValue ? DateOnly.FromDateTime(SelectedDeathFromDate.Value) : null;
            DateOnly? toDate = SelectedDeathToDate.HasValue ? DateOnly.FromDateTime(SelectedDeathToDate.Value) : null;

            _exportController.ExportMarker(type, file.Path.AbsolutePath, gamename, markerType, fromDate, toDate);
        }
    }

    #endregion

    private DeathLocationModel GetDefaultDeathLocation()
    {
        return new DeathLocationModel
        {
            LocationId = -1,
            Name = "All Locations",
            GameID = -1,
            Finish = false
        };
    }

    private GameStatsModel GetDefaultDeathGame()
    {
        return new GameStatsModel
        {
            GameId = -1,
            GameName = "All Games",
            Prefix = "All"
        };
    }

    private string GetDefaultMarkerType()
    {
        return "All"; // Default marker type
    }

    private void UpdateDeathEntriesCount()
    {
        IQueryable<DeathModel> deathsQuery = _deathController.InitFilter();
        if (SelectedDeathGame != null && SelectedDeathGame.GameId != -1)
        {
            deathsQuery = _deathController.Filter(SelectedDeathGame);
        }
        if (SelectedDeathLocation != null && SelectedDeathLocation.LocationId != -1)
        {
            deathsQuery = _deathController.Filter(SelectedDeathLocation);
        }
        if (SelectedDeathFromDate.HasValue)
        {
            deathsQuery = _deathController.Filter(fromDate: DateOnly.FromDateTime(SelectedDeathFromDate.Value));
        }
        if (SelectedDeathToDate.HasValue)
        {
            deathsQuery = _deathController.Filter(toDate: DateOnly.FromDateTime(SelectedDeathToDate.Value));
        }
        ActualDeathEntries = deathsQuery.Count();
        if (ActualDeathEntries != 0)
        {
            DeathExportButtonEnabled = true;
        }
        else
        {
            DeathExportButtonEnabled = false;
        }
    }

    private void UpdateMarkerEntriesCount()
    {
        IQueryable<MarkerModel> markersQuery = _markerController.InitFilter();
        if (SelectedMarkerGame != null && SelectedMarkerGame.GameId != -1)
        {
            markersQuery = _markerController.Filter(SelectedMarkerGame);
        }
        if (!string.IsNullOrEmpty(SelectedMarkerType) && SelectedMarkerType != "All")
        {
            markersQuery = _markerController.Filter(SelectedMarkerType);
        }
        if (SelectedMarkerFromDate.HasValue)
        {
            markersQuery = _markerController.Filter(fromDate: DateOnly.FromDateTime(SelectedMarkerFromDate.Value));
        }
        if (SelectedMarkerToDate.HasValue)
        {
            markersQuery = _markerController.Filter(toDate: DateOnly.FromDateTime(SelectedMarkerToDate.Value));
        }
        ActualMarkerEntries = markersQuery.Count();
        if (ActualMarkerEntries != 0)
        {
            MarkerExportButtonEnabled = true;
        }
        else
        {
            MarkerExportButtonEnabled = false;
        }
    }

    partial void OnSelectedDeathGameChanged(GameStatsModel value)
    {
        if (value is null || value.GameId == -1)
        {
            DeathLocations = new ObservableCollectionCopy<DeathLocationModel>(GetDefaultDeathLocation(), new ObservableCollection<DeathLocationModel>(_locationController.GetListOfLocations()));
            SelectedDeathLocation = DeathLocations.FirstOrDefault();
        }
        else
        {
            List<DeathLocationModel> locations = _locationController.GetListOfLocations(value);
            if (locations != null && locations.Count > 0)
            {
                DeathLocations = new ObservableCollectionCopy<DeathLocationModel>(GetDefaultDeathLocation(), new ObservableCollection<DeathLocationModel>(locations));
                SelectedDeathLocation = DeathLocations.FirstOrDefault();
            }
            else
            {
                DeathLocations = new ObservableCollection<DeathLocationModel>();
                SelectedDeathLocation = null;
            }
        }
        UpdateDeathEntriesCount();
    }

    partial void OnSelectedDeathLocationChanged(DeathLocationModel value)
    {
        if (value == null) return;
        UpdateDeathEntriesCount();
    }

    partial void OnSelectedDeathFromDateChanged(DateTime? value)
    {
        UpdateDeathEntriesCount();
    }

    partial void OnSelectedDeathToDateChanged(DateTime? value)
    {
        UpdateDeathEntriesCount();
    }

    partial void OnSelectedMarkerGameChanged(GameStatsModel value)
    {
        if (value == null || value.GameId == -1)
        {
            MarkerTypes = new ObservableCollectionCopy<string>(
                GetDefaultMarkerType(),
                new ObservableCollection<string>(_markerController.InitFilter().Select(x => x.categorie).Distinct()));
            SelectedMarkerType = GetDefaultMarkerType();
        }
        else
        {
            MarkerTypes = new ObservableCollectionCopy<string>(
                GetDefaultMarkerType(),
                new ObservableCollection<string>(_markerController.InitFilter().Where(x => x.GameId == value.GameId).Select(x => x.categorie).Distinct()));
            SelectedMarkerType = GetDefaultMarkerType();
        }
        UpdateMarkerEntriesCount();
    }

    partial void OnSelectedMarkerTypeChanged(string value)
    {

        UpdateMarkerEntriesCount();
    }

    partial void OnSelectedMarkerFromDateChanged(DateTime? value)
    {
        UpdateMarkerEntriesCount();
    }

    partial void OnSelectedMarkerToDateChanged(DateTime? value)
    {
        UpdateMarkerEntriesCount();
    }


}
