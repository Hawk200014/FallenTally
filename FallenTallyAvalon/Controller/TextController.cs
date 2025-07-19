using Avalonia.Media;
using FallenTally.Controller.Forms;
using FallenTally.Database.Models;
using FallenTally.Models;
using FallenTally.Resources;
using FallenTally.Services;
using FallenTally.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FallenTally.Controller
{
    public class TextController
    {
        private OptionsController _optionsController;

        private OverlaySettingsModel? _overlaySettingsModel;
        private static GameController? _gameController;
        private static LocationController? _locationController;
        private static DeathController? _deathController;

        public TextController(OptionsController optionsController, GameController gameController, LocationController locationController, DeathController deathController)
        {
            this._optionsController = optionsController;
            this._gameController = gameController;
            this._locationController = locationController;
            this._deathController = deathController;
        }

        public void CreateDirectory()
        {
            string path = Path.Combine(GLOBALVARS.PATHTOEXE, "OBSOverlay");
            Directory.CreateDirectory(path);
        }

        public void WriteOverlay()
        {
            // Use Overlay folder inside the program folder where the executable is
            string overlayDir = Path.Combine(GLOBALVARS.PATHTOEXE, "Overlay");
            Directory.CreateDirectory(overlayDir);

            var htmlFileName = $"overlay.html";
            var htmlPath = Path.Combine(overlayDir, htmlFileName);

            Write(TallyViewModel tvm, htmlPath, true);
        }

        public static void Write(GameStatsModel game, DeathLocationModel location, OverlaySettingsModel overlaySettingsModel, string path, bool replace = true)
        {

            string replaceText = overlaySettingsModel.TemplateText;
            if (replace)
                replaceText = ReplaceTemplate(replaceText, game, location);

            // Build the HTML content from properties
            var html = $@"
        <!DOCTYPE html>
        <html>
        <head>
        <meta charset='UTF-8'>
        <meta http-equiv='refresh' content='1'>
        <title>FallenTallyCounterOverlay</title>
        <style>
        body {{
            font-family: '{overlaySettingsModel.SelectedFontFamily}';
            font-size: {overlaySettingsModel.FontSize}px;
            font-style: {overlaySettingsModel.SelectedFontStyle?.ToLower()};
            font-weight: {overlaySettingsModel.SelectedFontWeight?.ToLower()};
            color: {ToCssRgba(overlaySettingsModel.TextColor)};
            -webkit-text-stroke: {overlaySettingsModel.BorderSize.ToString().Replace(",", ".")}px {ToCssRgba(overlaySettingsModel.OutlineColor)};
            text-shadow: {overlaySettingsModel.ShadowSize.ToString().Replace(",", ".")}px {overlaySettingsModel.ShadowSize.ToString().Replace(",", ".")}px {ToCssRgba(overlaySettingsModel.ShadowColor)};
            text-align: center;
        }}
        </style>
        </head>
        <body>
        <p>{overlaySettingsModel.TemplateText.Replace(Environment.NewLine, "<br>")}</p>
        </body>
        </html>
        ";


            // Write the HTML file to the Overlay folder
            File.WriteAllText(path, html);
        }

        private static string ReplaceTemplate(string templateText, GameStatsModel game, DeathLocationModel location)
        {
            return templateText
                .Replace("[GAMEPREFIX]", game.Prefix)
                .Replace("[GAMEDEATHS]", "" + _gameController?.GetAllDeaths(game))
                .Replace("[LOCATIONNAME]", location.Name)
                .Replace("[LOCATIONDEATHS]", "" + _deathController?.GetDeaths(location));
        }

        public static string ToCssRgba(Color color)
        {
            // color.A is 0-255, CSS expects 0-1 for alpha
            var alpha = color.A / 255.0;
            return $"rgba({color.R},{color.G},{color.B},{alpha.ToString(System.Globalization.CultureInfo.InvariantCulture)})";
        }


    }
}
