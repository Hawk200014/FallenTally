using DeathCounterHotkey.Controller;
using DeathCounterHotkey.Controller.Forms;
using DeathCounterHotkey.Database;

namespace DeathCounterHotkey;

static class Program
{

    public static bool debug = false;
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {

        if (debug)
        {
            DebugLogger logger = new DebugLogger();
        }
        try {

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.SetCompatibleTextRenderingDefault(true);
            TextController.CreateDirectory();
            SQLiteDBContext db = new SQLiteDBContext();
            GameController gameController = new GameController(db);
            LocationController locationController = new LocationController(gameController, db);
            StreamTimeController streamTimeController = new StreamTimeController();
            DeathController deathController = new DeathController(db, streamTimeController);
            EditController editController = new EditController(gameController, locationController);
            OptionsController optionsController = new OptionsController(db);
            MainController mainController = new MainController(gameController, locationController, deathController, editController, optionsController, streamTimeController);
            Application.Run(new MainForm(mainController));
        }
        catch (Exception ex)
        {
            DebugLogger.Debug(ex.ToString());
        }
    }
}