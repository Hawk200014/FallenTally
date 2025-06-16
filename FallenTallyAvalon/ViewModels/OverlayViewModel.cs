using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using FallenTallyAvalon.Models;
using FallenTallyAvalon.Services;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace FallenTallyAvalon.ViewModels
{
    public partial class OverlayViewModel : ObservableObject
    {
        // ComboBox sources
        public ObservableCollection<string> FontFamilies { get; }
        public ObservableCollection<string> FontStyles { get; } = ["Normal", "Italic", "Oblique"];
        public ObservableCollection<string> FontWeights { get; } = ["Normal", "Bold", "Light"];

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

        // Store the last generated file path
        private string? _lastHtmlPath;

        // Debounce timer for color updates
        private Timer? _debounceTimer;
        private readonly TimeSpan _debounceInterval = TimeSpan.FromMilliseconds(100);
        private bool _pendingUpdate = false;

        protected override void OnPropertyChanged(System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            // Only debounce color property changes, update immediately for others
            if (e.PropertyName is nameof(TextColor) or nameof(OutlineColor) or nameof(ShadowColor))
            {
                DebounceWriteOverlayHtml();
            }
            else if (!e.PropertyName.Equals(nameof(Adresse)))
            {
                WriteOverlayHtml();
            }
        }

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

            LoadOverlaySettings();
        }

        private void DebounceWriteOverlayHtml()
        {
            _pendingUpdate = true;
            _debounceTimer?.Dispose();
            _debounceTimer = new Timer(_ =>
            {
                if (_pendingUpdate)
                {
                    _pendingUpdate = false;
                    WriteOverlayHtml();
                }
            }, null, _debounceInterval, Timeout.InfiniteTimeSpan);
        }

        private void WriteOverlayHtml()
        {
            // Get the AppData\Roaming directory and create a subfolder for your app
            var appDataDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "FallenTallyAvalon", "overlay");
            Directory.CreateDirectory(appDataDir);

            // Generate a unique file name
            var htmlFileName = $"overlay_{Guid.NewGuid()}.html";
            var htmlPath = Path.Combine(appDataDir, htmlFileName);

            // Build the HTML content from properties
            var html = $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <title>Overlay</title>
    <style>
        body {{
            font-family: '{SelectedFontFamily}';
            font-size: {FontSize}px;
            font-style: {SelectedFontStyle?.ToLower()};
            font-weight: {SelectedFontWeight?.ToLower()};
            color: {ToCssRgba(TextColor)};
            -webkit-text-stroke: {BorderSize.ToString().Replace(",", ".")}px {ToCssRgba(OutlineColor)};
            text-shadow: {ShadowSize.ToString().Replace(",", ".")}px {ShadowSize.ToString().Replace(",", ".")}px {ToCssRgba(ShadowColor)};
            text-align: center;
        }}
    </style>
</head>
<body>
    <p>{TemplateText.Replace(Environment.NewLine, "<br>")}</p>
</body>
</html>
";
            File.WriteAllText(htmlPath, html);
            // Update the browser address
            var fileUri = new Uri(htmlPath).AbsoluteUri;
            Adresse = fileUri;

            // Delete the previous file if it exists and is not the same as the new one
            if (!string.IsNullOrEmpty(_lastHtmlPath) && File.Exists(_lastHtmlPath) && _lastHtmlPath != htmlPath)
            {
                try
                {
                    File.Delete(_lastHtmlPath);
                }
                catch
                {
                    // Ignore errors (e.g., file in use)
                }
            }

            _lastHtmlPath = htmlPath;
        }

        private static string ToCssRgba(Color color)
        {
            // color.A is 0-255, CSS expects 0-1 for alpha
            var alpha = color.A / 255.0;
            return $"rgba({color.R},{color.G},{color.B},{alpha.ToString(System.Globalization.CultureInfo.InvariantCulture)})";
        }

        public void SaveOverlaySettings()
        {
            OverlaySettingsModel model = new OverlaySettingsModel
            {
                SelectedFontFamily = SelectedFontFamily,
                FontSize = FontSize,
                SelectedFontStyle = SelectedFontStyle,
                SelectedFontWeight = SelectedFontWeight,
                BorderSize = BorderSize,
                ShadowSize = ShadowSize,
                TextColor = TextColor,
                OutlineColor = OutlineColor,
                ShadowColor = ShadowColor
            };
            JsonSettingsService.Save(model, "overlay_settings.json");
        }

        public void LoadOverlaySettings()
        {
            // Load settings from a JSON file or other storage
            // This is a placeholder; implement actual loading logic as needed
            var settings = JsonSettingsService.Load<OverlaySettingsModel>("overlay_settings.json");
            if (settings != null)
            {
                SelectedFontFamily = settings.SelectedFontFamily;
                FontSize = settings.FontSize;
                SelectedFontStyle = settings.SelectedFontStyle;
                SelectedFontWeight = settings.SelectedFontWeight;
                BorderSize = settings.BorderSize;
                ShadowSize = settings.ShadowSize;
                TextColor = settings.TextColor;
                OutlineColor = settings.OutlineColor;
                ShadowColor = settings.ShadowColor;
            }


        }
    }
}
