using FallenTally.Controller;

namespace FallenTally.Controller.Forms
{
    public class EditController
    {
        //private GameController _gameController;
        //private LocationController _locationController;

        //public EditController(GameController gameController, LocationController locationController)
        //{
        //    this._gameController = gameController;
        //    this._locationController = locationController;
        //}

        //internal bool AddEdit(string editText, EDITCATEGORIE editCat, bool? finished = null)
        //{
        //    if (string.IsNullOrEmpty(editText)) return false;
        //    switch (editCat)
        //    {
        //        case EDITCATEGORIE.GAME:
        //            if (_gameController.IsDupeName(editText)) return false;
        //            return _gameController.EditName(editText);
        //        case EDITCATEGORIE.LOCATION:
        //            //if (_locationController.IsDupeName(editText)) return false;
        //            _locationController.SetFinish(finished);
        //            _locationController.EditName(editText);
        //            return true;
        //        case EDITCATEGORIE.PREFIX:
        //            break;
        //    }
        //    return false;
            
        //}

        //internal bool GetCheckedState(string oldValue)
        //{
        //    return _locationController.GetFinishState(oldValue);
        //}

        //public enum EDITCATEGORIE
        //{
        //    GAME,
        //    LOCATION,
        //    PREFIX
        //}
    }
}
