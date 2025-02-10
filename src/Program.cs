using DeathCounterHotkey.Controller;
using DeathCounterHotkey.Controller.Forms;
using DeathCounterHotkey.Database;
using FallenTally.Utility.Singletons;

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
            singleton.Add(SQLiteDBContext.GetSingletonName(), db);
            db.TryMigrate();
            GameController gameController = new GameController();
            singleton.Add(GameController.GetSingletonName(), gameController);
            LocationController locationController = new LocationController();
            singleton.Add(LocationController.GetSingletonName(), locationController);
            TimerController streamTimerController = new TimerController();
            singleton.Add("StreamTimerController", streamTimerController);
            TimerController recordTimerController = new TimerController();
            singleton.Add("RecordTimerController", recordTimerController);
            EditController editController = new EditController();
            singleton.Add(EditController.GetSingletonName(), editController);
            OptionsController optionsController = new OptionsController();
            singleton.Add(OptionsController.GetSingletonName(), optionsController);
            TextController textController = new TextController();
            singleton.Add(TextController.GetSingletonName(), textController);
            textController.CreateDirectory();
            TwitchTokenController tokenController = new TwitchTokenController();
            singleton.Add(TwitchTokenController.GetSingletonName(), tokenController);
            RecordingController recordingController = new RecordingController();
            singleton.Add(RecordingController.GetSingletonName(), recordingController);
            MarkerController markerController = new MarkerController();
            singleton.Add(MarkerController.GetSingletonName(), markerController);
            ExportController exportController = new ExportController();
            singleton.Add(ExportController.GetSingletonName(), exportController);
            MainController mainController = new MainController();
            singleton.Add(MainController.GetSingletonName(), mainController);
            Application.Run(new MainForm(mainController));
        }
        catch (Exception ex)
        {
            DebugLogger.Debug(ex.ToString());
        }
    }
}