using DeathCounterHotkey.Models;

namespace DeathCounterHotkey;

public partial class MainForm : Form
{
    private Settings _settings = new Settings();
    private static Dictionary<string, GameStatsModel> GameDic = new Dictionary<string, GameStatsModel>();
    private GameStatsModel _activeGameStats;
    private EventHandler<KeyPressedEventArgs> keyEvent;

    KeyboardHook increaseHook = new KeyboardHook();

    public static bool GameExists(string name)
    {
        name = Utilities.EscapeSpecialChars(name.ToLower());
        return GameDic.ContainsKey(name);
    }

    public MainForm(Settings settings)
    {
        this._settings = settings;
        InitializeComponent();
        AddedGameAction();
        keyEvent = new EventHandler<KeyPressedEventArgs>(IncreaseHotKeyPressed);
        increaseHook.KeyPressed += keyEvent;
        increaseHook.RegisterHotKey(Keys.F13);
    }

    public void IncreaseHotKeyPressed(object sender, KeyPressedEventArgs e)
    {
        IncreaseCounter();
    }

    private void AddedGameAction()
    {
        GameDic.Clear();
        List<GameStatsModel> games = _settings.GetAllGameStats();
        foreach (GameStatsModel game in games)
        {
            string sanitizedName = Utilities.EscapeSpecialChars(game.GameName.ToLower());
            GameDic.Add(sanitizedName, game);
        }
        AddGamesToCombo();
    }

    private void AddGamesToCombo()
    {
        gameSelectCombo.Items.Clear();
        foreach (GameStatsModel game in GameDic.Values)
        {
            gameSelectCombo.Items.Add(game.GameName);
        }
    }

    private void addGameBtn_Click(object sender, EventArgs e)
    {
        string methodPrefix = "MainForm.addGameBtn_Click: ";
        DebugLogger.Debug(methodPrefix + "Add Game Button Pressed");
        AddGameForm gameForm = new AddGameForm(this._settings, AddedGameAction);
        gameForm.Show();
    }

    private void removeGameBtn_Click(object sender, EventArgs e)
    {
        int index = gameSelectCombo.SelectedIndex;
        string gameName = gameSelectCombo.SelectedText;
        gameSelectCombo.Items.RemoveAt(index);
        gameSelectCombo.SelectedIndex = 0;
        string sanitizedName = Utilities.EscapeSpecialChars(gameName.ToLower());
        GameStatsModel model = GameDic[sanitizedName];
        _settings.RemoveGameStats(model);
        GameDic.Remove(sanitizedName);
    }

    private void gameSelectCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
        ComboBox cmb = (ComboBox)sender;
        int selectedIndex = cmb.SelectedIndex;
        string selectedValue = (string)cmb.Items[selectedIndex];
        if (this._activeGameStats != null)
        {
            _settings.SaveGameStats(this._activeGameStats);
        }
        string sanitizedName = Utilities.EscapeSpecialChars(selectedValue.ToLower());
        GameStatsModel model = GameDic[sanitizedName];
        this._activeGameStats = model;
        pretextTxtb.Text = model.Prefix;
        deathCountTxtb.Text = model.Deaths.ToString();
        this._settings.WriteTextFile(model);
    }

    public void UpdateDeaths()
    {
        int deaths = this._activeGameStats.Deaths;
        deathCountTxtb.Text = "" + deaths;
        this._settings.WriteTextFile(this._activeGameStats);
    }

    public void IncreaseCounter()
    {
        this._activeGameStats.Deaths++;
        UpdateDeaths();
    }

    public void DecreaseCounter()
    {
        this._activeGameStats.Deaths--;
        UpdateDeaths();
    }

    public void ResetCounter()
    {
        this._activeGameStats.Deaths = 0;
        UpdateDeaths();
    }



    private void increaseBtn_Click(object sender, EventArgs e)
    {
        IncreaseCounter();
    }

    private void decreaseBtn_Click(object sender, EventArgs e)
    {
        DecreaseCounter();
    }

    private void resetBtn_Click(object sender, EventArgs e)
    {
        ResetCounter();
    }

    private void optionsBtn_Click(object sender, EventArgs e)
    {
        new OptionsForm(this._settings, OptionsChangedAction).Show();
    }

    private void OptionsChangedAction()
    {

    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        increaseHook.KeyPressed -= this.keyEvent;
        if(this._activeGameStats != null)
            _settings.SaveGameStats(this._activeGameStats);
    }


}