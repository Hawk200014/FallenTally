using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;

namespace FallenTallyAvalon.ViewModels
{
    public partial class OverlayViewModel : ObservableObject
    {
        // ComboBox sources
        public ObservableCollection<string> FontFamilies { get; } 
        public ObservableCollection<string> FontStyles { get; } = new() { "Normal", "Italic", "Oblique" };
        public ObservableCollection<string> FontWeights { get; } = new() { "Normal", "Bold", "Light" };

        // Selected/entered values
        [ObservableProperty]
        private string? selectedFontFamily = "Arial";

        [ObservableProperty]
        private double fontSize = 12;

        [ObservableProperty]
        private string? selectedFontStyle = "Normal";

        [ObservableProperty]
        private string? selectedFontWeight = "Normal";

        [ObservableProperty]
        private double borderSize = 1;

        [ObservableProperty]
        private double shadowSize = 1;

        [ObservableProperty]
        private Color textColor = Colors.Black;

        [ObservableProperty]
        private Color outlineColor = Colors.Black;

        [ObservableProperty]
        private Color shadowColor = Colors.Black;

        // For the WebView address binding
        [ObservableProperty]
        private string adresse = "https://www.example.com";

        [ObservableProperty]
        private string templateText = "[GAMEPREFIX]: [GAMEDEATHS]\r\n[LOCATIONNAME]: [LOCATIONDEATHS]";

        public OverlayViewModel()
        {
            // Get system font family names (distinct and sorted)
            var systemFonts = FontManager.Current.SystemFonts
                .Select(f => f.Name)
                .Distinct()
                .OrderBy(name => name)
                .ToList();

            FontFamilies = new ObservableCollection<string>(systemFonts);

            // Optionally set a default
            SelectedFontFamily = FontFamilies.FirstOrDefault();
        }
    }
}
