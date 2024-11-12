using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeathCounterHotkey.Controller.Forms
{
    public class EditController
    {
        private GameController _gameController;
        private LocationController _locationController;

        public EditController(GameController gameController, LocationController locationController)
        {
            this._gameController = gameController;
            this._locationController = locationController;
        }

        internal bool AddEdit(string editText, string editCat)
        {
            if (string.IsNullOrEmpty(editText)) return false;
            switch (editCat.ToUpper())
            {
                case "GAME":
                    if (_gameController.IsDupeName(editText)) return false;
                    return _gameController.EditName(editText);
                case "LOCATION":
                    if (_locationController.IsDupeName(editText)) return false;
                    return _locationController.EditName(editText);
                case "PREFIX":
                    break;
            }
            return false;
            
        }
    }
}
