using DeathCounterHotkey.Controller;
using DeathCounterHotkey.Controller.Forms;
using DeathCounterHotkey.Database.Models;
using DeathCounterHotkey.Forms;
using DeathCounterHotkey.Resources;
using System.Web;

namespace DeathCounterHotkey;

public partial class MainForm : Form
{
    private EventHandler<KeyPressedEventArgs> keyEvent;

    
    private MainController _mainController;



    public MainForm(MainController mainController)
    {
        InitializeComponent();
        _mainController = mainController;
        _mainController.SetForm(this);


        UpdateGameList();
        //keyEvent = new EventHandler<KeyPressedEventArgs>(IncreaseHotKeyPressed);
        //increaseHook.KeyPressed += keyEvent;
        //increaseHook.RegisterHotKey(Keys.F13);
    }

    public void IncreaseHotKeyPressed(object sender, KeyPressedEventArgs e)
    {

    }



    private void AddGamesToCombo(List<string> games)
    {
        gameSelectCombo.Items.Clear();
        gameSelectCombo.Items.AddRange(games.ToArray());
    }

    private void addGameBtn_Click(object sender, EventArgs e)
    {
        string methodPrefix = "MainForm.addGameBtn_Click: ";
        DebugLogger.Debug(methodPrefix + "Add Game Button Pressed");
        AddGameForm gameForm = new AddGameForm(_mainController.GetGameController(), UpdateAndSelectGame, AddDefaultLocation);
        gameForm.Show();
    }

    public void AddDefaultLocation()
    {
        this._mainController.GetLocationController().AddLocation(GLOBALVARS.DEFAULT_LOCATION);
        this._mainController.GetLocationController().SetActiveLocation(GLOBALVARS.DEFAULT_LOCATION);
        UpdateLocationList();
        SetLocation(GLOBALVARS.DEFAULT_LOCATION);
    }

    public void SetLocation(string location)
    {
        this.locationCombo.SelectedIndex = this.locationCombo.Items.IndexOf(location);
    }



    private void removeGameBtn_Click(object sender, EventArgs e)
    {
        this._mainController.RemoveGame();
        UpdateGameList();
    }

    private void editGameBtn_Click(object sender, EventArgs e)
    {
        new EditForm(_mainController.GetGameController().GetActiveGame().GameName,this._mainController.GetEditController(), EditController.EDITCATEGORIE.GAME, UpdateGameList).Show(this);
    }

    private void UpdateAndSelectGame(GameStatsModel? model)
    {
        List<string> games = this._mainController.GetGameNames();
        AddGamesToCombo(games);
        if (model != null)
        {
            gameSelectCombo.SelectedIndex = this.gameSelectCombo.Items.IndexOf(model.GameName);
            _mainController.GetGameController().SetActiveGame(model.GameName);
        }
    }

    private void UpdateGameList()
    {
        List<string> games = this._mainController.GetGameNames();
        AddGamesToCombo(games);
    }

    private string GetSelectedGame()
    {
        int selectedIndex = gameSelectCombo.SelectedIndex;
        return (string)gameSelectCombo.Items[selectedIndex];
    }

    private void gameSelectCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
        string gameName = GetSelectedGame();
        string prefix = _mainController.GameChanged(gameName);
        pretextTxtb.Text = prefix;
        UpdateLocationList();
        UpdateDeaths();
    }



    public void UpdateDeaths()
    {
        int allDeaths = this._mainController.GetAllDeaths();
        deathCountTxtb.Text = "" + allDeaths;
        int locationDeaths = this._mainController.GetLocationDeaths();
        locationDeathCountTxtb.Text = "" + locationDeaths;
    }




    private void increaseBtn_Click(object sender, EventArgs e)
    {
        this._mainController.IncreaseDeaths();
        UpdateDeaths();
    }

    private void decreaseBtn_Click(object sender, EventArgs e)
    {
        this._mainController.DecreaseDeaths();
        UpdateDeaths();
    }

    private void resetBtn_Click(object sender, EventArgs e)
    {
        this._mainController.ResetDeahts();
        UpdateDeaths();
    }

    private void optionsBtn_Click(object sender, EventArgs e)
    {

        new OptionsForm(this._mainController.GetOptionController(), this._mainController.OptionsChangedAction).Show();
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        this._mainController.UnregisterHotkeys();
    }

    private void addLocationBtn_Click(object sender, EventArgs e)
    {
        new AddLocation(this._mainController.GetLocationController(), UpdateLocationList).Show(this);
    }

    public void UpdateLocationList()
    {
        List<string> locations = this._mainController.GetLocationNames();
        AddLocationsToCombo(locations);
    }



    private void AddLocationsToCombo(List<string> locations)
    {
        locationCombo.Items.Clear();
        locationCombo.Items.AddRange(locations.ToArray());
    }

    private void editLocationBtn_Click(object sender, EventArgs e)
    {
        if (_mainController.GetLocationController().GetActiveLocation().Name.Equals(GLOBALVARS.DEFAULT_LOCATION)) return;
        new EditForm(_mainController.GetLocationController().GetActiveLocation().Name, this._mainController.GetEditController(), EditController.EDITCATEGORIE.LOCATION, UpdateLocationList).Show(this);
    }

    private string GetSelectedLocation()
    {
        int selectedIndex = locationCombo.SelectedIndex;
        if (selectedIndex == -1) return "";
        return (string)locationCombo.Items[selectedIndex];
    }


    private void removeLocationbtn_Click(object sender, EventArgs e)
    {
        _mainController.RemoveLocation();
        UpdateLocationList();
        UpdateDeaths();
    }

    private void locationCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
        string locationName = GetSelectedLocation();
        if(string.IsNullOrEmpty(locationName)) return;
        _mainController.LocationChanged(locationName);
        UpdateDeaths();
    }

    internal int GetLocationIndex()
    {
        return locationCombo.SelectedIndex;
    }
}