using FallenTally.Controller;
using CommunityToolkit.Mvvm.ComponentModel;
using FallenTally.Views;

namespace FallenTally.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public OverlayViewModel OverlayViewModel { get; }
    public TallyViewModel TallyViewModel { get; }
    public ExportViewModel ExportViewModel { get; }
    public SettingsViewModel? SettingsViewModel { get; }
    public HotkeyController? HotkeyController { get; }

    [ObservableProperty]
    private int selectedTabIndex;

    public MainViewModel()
    {
        // Initialize the OverlayViewModel and set it as the DataContext for the OverlayView
        OverlayViewModel = ServiceLocator.Provider.GetService(typeof(OverlayViewModel)) as OverlayViewModel;
        TallyViewModel = ServiceLocator.Provider.GetService(typeof(TallyViewModel)) as TallyViewModel;
        ExportViewModel = ServiceLocator.Provider.GetService(typeof(ExportViewModel)) as ExportViewModel;
        SettingsViewModel = ServiceLocator.Provider.GetService(typeof(SettingsViewModel)) as SettingsViewModel;
        HotkeyController = ServiceLocator.Provider.GetService(typeof(HotkeyController)) as HotkeyController;
    }

    partial void OnSelectedTabIndexChanged(int value)
    {
        if(value == 0)
        {
            HotkeyController.StartHotkeys();
            HotkeyController.ReloadKeysFromOptions();
        }
        else
        {
            HotkeyController.StopHotkeys();
        }
        if(value != 3)
        {
            SettingsView.StopGlobalHook();
        }
        // Overlay tab is at index 2 (zero-based)
        if (value == 2 && OverlayViewModel is not null)
        {
            OverlayViewModel.SaveOverlaySettings();
        }
        if(value == 1 && TallyViewModel is not null)
        {
            ExportViewModel.Init();
        }

    }
}
