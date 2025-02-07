using DeathCounterHotkey.Controller.Forms;
using DeathCounterHotkey.Resources;
using FallenTally.Utility.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace DeathCounterHotkey.Controller
{
    public class TextController : ISingleton
    {
        private readonly Singleton _singleton = Singleton.GetInstance();
        private OptionsController? _optionsController;

        public TextController()
        { 
            this._optionsController = _singleton.GetValue(OptionsController.GetSingletonName()) as OptionsController;
        }

        public void CreateDirectory()
        {
            string path = Path.Combine(GLOBALVARS.PATHTOEXE, "OBSOverlay");
            Directory.CreateDirectory(path);
        }

        public void WriteDeaths(string prefix, int allDeaths, string location, int locDeaths)
        {
            

            if (!TemplaceExists()) CreateTemplate();

            List<string> templateContent = File.ReadAllLines(GetTemplatePath()).ToList();

            List<string> overlayContent = templateContent.Where(x => !x.StartsWith("#")).ToList();

            string overlayText = "";
            foreach (string content in overlayContent)
            {
                overlayText += content + "\n";
            }

            string setting = _optionsController.GetSetting(nameof(OptionsController.OPTIONS.WORLD_AS_ALL));
            if(string.IsNullOrEmpty(setting))
            {
                setting = "No";
            }
            bool worldAllDeaths = setting == "Yes";

            overlayText = overlayText.Replace("[GAMEPREFIX]", prefix)
                .Replace("[GAMEDEATHS]", "" + allDeaths);

            if (worldAllDeaths && location.Equals(GLOBALVARS.DEFAULT_LOCATION))
            {
                overlayText = overlayText
                    .Replace("[LOCATIONDEATHS]", "" + allDeaths)
                    .Replace("[LOCATIONNAME]", prefix);
            }
            else
            {
                overlayText = overlayText
                    .Replace("[LOCATIONDEATHS]", "" + locDeaths)
                    .Replace("[LOCATIONNAME]", location);
            }
            overlayText = overlayText.Replace("\n", "<br>");



            CreateHtml(overlayText);

        }

        private string GetTemplatePath()
        {
            string path = Path.Combine(GLOBALVARS.PATHTOEXE, "OBSOverlay");
            return Path.Combine(path, "OverlayTemplate.txt");


        }

        public bool TemplaceExists()
        {
            return File.Exists(GetTemplatePath());
        }

        public void CreateHtml(string paragraph)
        {
            string path = Path.Combine(GLOBALVARS.PATHTOEXE, "OBSOverlay");
            path = Path.Combine(path, "Overlay.html");

            


            string html = "";
            html += "<!DOCTYPE html>";
            html += "<html lang = \"en\">";
            html += "<head>";
            html += "<meta charset = \"UTF -8\">";
            html += "<meta http-equiv=\"refresh\" content=\"1\" >";
            html += "<title>DeathCounterOverlay</title>";
            html += "<style>";
            html += "body {";
            html += "width: fit-content;";
            html += "height: fit-content;";
            html += "text-align: left;";
            html += "font-family: " + GetFont() + ";";
            html += "color: " + GetFontColor() + ";";
            html += "font-size: " + GetFontSize() + "px;";
            html += "font-weight: " + GetFontWeight() + ";";
            html += "font-style: " + GetFontStyle() + ";";
            html += "-webkit-text-stroke: " + GetBorderSize() + "px " + GetBorderColor() + ";";
            html += "text-shadow: " + GetShadowSize() + "px " + GetShadowSize() + "px " + GetShadowColor() + ";";
            html += "}";
            html += "</style>";
            html += "</head>";
            html += "<body>";
            html += "<p>";
            html += paragraph;
            html += "</p>";
            html += "</body>";
            html += "</html>";

            File.WriteAllText(path, html);
        }

        private string GetShadowColor()
        {
            string tmp = _optionsController.GetSetting(nameof(OptionsController.OPTIONS.SHADOWCOLOR));
            if (tmp == "")
            {
                tmp = "#000000";
            }
            return tmp;
        }

        private string GetShadowSize()
        {
            string tmp = _optionsController.GetSetting(nameof(OptionsController.OPTIONS.SHADOWSIZE));
            if (tmp == "")
            {
                tmp = "1";
            }
            return tmp;
        }

        private string GetBorderColor()
        {
            string tmp = _optionsController.GetSetting(nameof(OptionsController.OPTIONS.BORDERCOLOR));
            if (tmp == "")
            {
                tmp = "#000000";
            }
            return tmp;
        }

        private string GetBorderSize()
        {
            string tmp = _optionsController.GetSetting(nameof(OptionsController.OPTIONS.BORDERSIZE));
            tmp = tmp.Replace(",", ".");
            if (tmp == "")
            {
                tmp = "1";
            }
            return tmp;
        }

        private string GetFontStyle()
        {
            string tmp = _optionsController.GetSetting(nameof(OptionsController.OPTIONS.FONTSTYLE));
            if (tmp == "")
            {
                tmp = "normal";
            }
            return tmp;
        }

        private string GetFontWeight()
        {
            string tmp = _optionsController.GetSetting(nameof(OptionsController.OPTIONS.FONTWEIGHT));
            if (tmp == "")
            {
                tmp = "normal";
            }
            return tmp;
        }

        private string GetFontSize()
        {
            string tmp = _optionsController.GetSetting(nameof(OptionsController.OPTIONS.FONTSIZE));
            if (tmp == "")
            {
                tmp = "1";
            }
            return tmp;
        }

        private string GetFontColor()
        {
            string tmp = _optionsController.GetSetting(nameof(OptionsController.OPTIONS.TEXTCOLOR));
            if (tmp == "")
            {
                tmp = "#000000";
            }
            return tmp;
        }

        private string GetFont()
        {
            string fontname = _optionsController.GetSetting(nameof(OptionsController.OPTIONS.FONTFAMILY));
            if (fontname == "")
            {
                fontname = "Arial";
            }
            return fontname;
        }

        public void CreateTemplate()
        {
            string path = GetTemplatePath();

            if (File.Exists(path)) { return; }

            string textContent = "# Overlay template to display inside obs\n";
            textContent += "# '#' at the beginning of a line comments out the line\n";
            textContent += "# there are a few replacer to customize the text:\n";
            textContent += "# [GAMEPREFIX] => Displays the prefix or display name of a game\n";
            textContent += "# [GAMEDEATHS] => Displays the number of deaths in a game\n";
            textContent += "# [LOCATIONNAME] => Displays the name of the location\n";
            textContent += "# [LOCATIONDEATHS] => Displays the number of deaths in a location\n";
            textContent += "# The next rows shows an example to display the numbers\n";
            textContent += "[GAMEPREFIX]: [GAMEDEATHS]\n";
            textContent += "[LOCATIONNAME]: [LOCATIONDEATHS]";

            File.WriteAllText(path, textContent);
        }

        public static string GetSingletonName()
        {
            return "TextController";
        }
    }
}
