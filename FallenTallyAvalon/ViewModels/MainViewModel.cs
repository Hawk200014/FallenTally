using FallenTallyAvalon.Controller;
using CommunityToolkit.Mvvm.ComponentModel;

namespace FallenTallyAvalon.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public OverlayViewModel OverlayViewModel { get; }
    public TallyViewModel TallyViewModel { get; }

    [ObservableProperty]
    private int selectedTabIndex;

    public MainViewModel()
    {
        // Initialize the OverlayViewModel and set it as the DataContext for the OverlayView
        OverlayViewModel = ServiceLocator.Provider.GetService(typeof(OverlayViewModel)) as OverlayViewModel;
        TallyViewModel = ServiceLocator.Provider.GetService(typeof(TallyViewModel)) as TallyViewModel;
    }

    partial void OnSelectedTabIndexChanged(int value)
    {
        // Overlay tab is at index 2 (zero-based)
        if (value == 2 && OverlayViewModel is not null)
        {
            OverlayViewModel.SaveOverlaySettings();
        }
    }
}
