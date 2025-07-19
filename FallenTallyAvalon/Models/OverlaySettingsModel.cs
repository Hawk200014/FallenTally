using Avalonia.Media;

namespace FallenTally.Models
{
    public class OverlaySettingsModel
    {
        public string? SelectedFontFamily { get; set; }
        public double FontSize { get; set; }
        public string? SelectedFontStyle { get; set; }
        public string? SelectedFontWeight { get; set; }
        public double BorderSize { get; set; }
        public double ShadowSize { get; set; }
        public Color TextColor { get; set; }
        public Color OutlineColor { get; set; }
        public Color ShadowColor { get; set; }

        public string TemplateText { get; set; }
    }
}
