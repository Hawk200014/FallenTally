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
            Settings settings = new Settings();
            settings.LoadSettings();
            settings.LoadLanguage();
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm(settings));
        }
        catch (Exception ex)
        {
            DebugLogger.Debug(ex.ToString());
        }
    }
}