using FallenTally.Controller;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FallenTally.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public OverlayViewModel OverlayViewModel { get; }
    public TallyViewModel TallyViewModel { get; }
    public ExportViewModel ExportViewModel { get; }

    [ObservableProperty]
    private int selectedTabIndex;

    public MainViewModel()
    {
        // Initialize the OverlayViewModel and set it as the DataContext for the OverlayView
        OverlayViewModel = ServiceLocator.Provider.GetService(typeof(OverlayViewModel)) as OverlayViewModel;
        TallyViewModel = ServiceLocator.Provider.GetService(typeof(TallyViewModel)) as TallyViewModel;
        ExportViewModel = ServiceLocator.Provider.GetService(typeof(ExportViewModel)) as ExportViewModel;
    }

    partial void OnSelectedTabIndexChanged(int value)
    {
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
