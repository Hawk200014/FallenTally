using FallenTally.Enums;
using FallenTally.Utility.Singletons;

namespace DeathCounterHotkey.Controller.Forms
{
    /// <summary>
    /// Controller for editing game and location details.
    /// </summary>
    public class EditController : ISingleton
    {
        private readonly Singleton _singleton = Singleton.GetInstance();
        private readonly GameController? _gameController;
        private readonly LocationController? _locationController;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditController"/> class.
        /// </summary>
        public EditController()
        {
            _gameController = _singleton.GetValue(GameController.GetSingletonName()) as GameController;
            _locationController = _singleton.GetValue(LocationController.GetSingletonName()) as LocationController;
        }

        /// <summary>
        /// Gets the singleton name for the <see cref="EditController"/>.
        /// </summary>
        /// <returns>The singleton name.</returns>
        public static string GetSingletonName() => "EditController";

        /// <summary>
        /// Adds or edits a game or location based on the provided category.
        /// </summary>
        /// <param name="editText">The text to edit.</param>
        /// <param name="editCat">The category to edit (GAME, LOCATION, PREFIX).</param>
        /// <param name="finished">Optional parameter to set the finished state for a location.</param>
        /// <returns>True if the edit was successful, otherwise false.</returns>
        internal bool AddEdit(string editText, EDITCATEGORIE editCat, bool? finished = null)
        {
            if (string.IsNullOrEmpty(editText)) return false;

            return editCat switch
            {
                EDITCATEGORIE.GAME => !_gameController.IsDupeName(editText) && _gameController.EditName(editText),
                EDITCATEGORIE.LOCATION => EditLocation(editText, finished),
                _ => false,
            };
        }

        /// <summary>
        /// Edits the location details.
        /// </summary>
        /// <param name="editText">The text to edit.</param>
        /// <param name="finished">Optional parameter to set the finished state for a location.</param>
        /// <returns>True if the edit was successful, otherwise false.</returns>
        private bool EditLocation(string editText, bool? finished)
        {
            _locationController.SetFinish(finished);
            return _locationController.EditName(editText);
        }

        /// <summary>
        /// Gets the checked state of a location based on its old value.
        /// </summary>
        /// <param name="oldValue">The old value of the location.</param>
        /// <returns>True if the location is finished, otherwise false.</returns>
        internal bool GetCheckedState(string oldValue) => _locationController.GetFinishState(oldValue);
    }
}
