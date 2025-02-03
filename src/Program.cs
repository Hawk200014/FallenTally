using DeathCounterHotkey.Controller;
using DeathCounterHotkey.Controller.Forms;
using DeathCounterHotkey.Database;
using FallenTally.Utility;

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
            Singleton singleton = Singleton.GetInstance();
            SQLiteDBContext db = new SQLiteDBContext();
            singleton.Add("SQLiteDBContext", db);
            db.TryMigrate();
            GameController gameController = new GameController(db);
            singleton.Add(gameController.GetType().ToString(), gameController);
            LocationController locationController = new LocationController(gameController, db);
            singleton.Add(locationController.GetType().ToString(), locationController);
            TimerController streamTimerController = new TimerController();
            singleton.Add("StreamTimerController", streamTimerController);
            TimerController recordTimerController = new TimerController();
            singleton.Add("RecordTimerController", recordTimerController);
            DeathController deathController = new DeathController(db);
            singleton.Add(deathController.GetType().ToString(), deathController);
            EditController editController = new EditController(gameController, locationController);
            singleton.Add(editController.GetType().ToString(), editController);
            OptionsController optionsController = new OptionsController(db);
            singleton.Add(optionsController.GetType().ToString(), optionsController);
            TextController textController = new TextController(optionsController);
            singleton.Add(textController.GetType().ToString(), textController);
            textController.CreateDirectory();
            TwitchTokenController tokenController = new TwitchTokenController(optionsController);
            singleton.Add(tokenController.GetType().ToString(), tokenController);
            RecordingController recordingController = new RecordingController(db);
            singleton.Add("", recordingController);
            MarkerController markerController = new MarkerController(db, recordingController);
            singleton.Add(markerController.GetType().ToString(), markerController);
            ExportController exportController = new ExportController(db, markerController);
            singleton.Add(exportController.GetType().ToString(), exportController);
            MainController mainController = new MainController(gameController, locationController, deathController, editController, optionsController, streamTimerController, tokenController, textController, exportController, markerController, recordTimerController, recordingController);
            singleton.Add(mainController.GetType().ToString(), mainController);
            Application.Run(new MainForm(mainController));
        }
        catch (Exception ex)
        {
            DebugLogger.Debug(ex.ToString());
        }
    }
}