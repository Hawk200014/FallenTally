using DeathCounterHotkey.Controller.Forms;
using DeathCounterHotkey.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Controller
{
    public class TextController
    {
        private OptionsController _optionsController;

        public TextController(OptionsController optionsController)
        { 
            this._optionsController = optionsController;
        }

        public void CreateDirectory()
        {
            string path = Path.Combine(GLOBALVARS.PATHTOEXE, "OBSOverlay");
            Directory.CreateDirectory(path);
        }

        public void WriteDeaths(string prefix, int allDeaths, string location, int locDeaths)
        {
            string path = Path.Combine(GLOBALVARS.PATHTOEXE, "OBSOverlay");
            path = Path.Combine(path, "Overlay.txt");

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

            File.WriteAllText(path, overlayText);
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

    }
}
