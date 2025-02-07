

using FallenTally.Utility.Singletons;

namespace DeathCounterHotkey.Controller.Forms
{
    public class EditController : ISingleton
    {
        private readonly Singleton _singleton = Singleton.GetInstance();
        private GameController? _gameController;
        private LocationController? _locationController;

        public EditController(GameController gameController, LocationController locationController)
        {
            this._gameController = _singleton.GetValue(GameController.GetSingletonName()) as GameController;
            this._locationController = _singleton.GetValue(LocationController.GetSingletonName()) as LocationController;
        }

        public static string GetSingletonName()
        {
            return "EditController";
        }

        internal bool AddEdit(string editText, EDITCATEGORIE editCat, bool? finished = null)
        {
            if (string.IsNullOrEmpty(editText)) return false;
            switch (editCat)
            {
                case EDITCATEGORIE.GAME:
                    if (_gameController.IsDupeName(editText)) return false;
                    return _gameController.EditName(editText);
                case EDITCATEGORIE.LOCATION:
                    //if (_locationController.IsDupeName(editText)) return false;
                    _locationController.SetFinish(finished);
                    _locationController.EditName(editText);
                    return true;
                case EDITCATEGORIE.PREFIX:
                    break;
            }
            return false;
            
        }

        internal bool GetCheckedState(string oldValue)
        {
            return _locationController.GetFinishState(oldValue);
        }

        public enum EDITCATEGORIE
        {
            GAME,
            LOCATION,
            PREFIX
        }
    }
}
