using DeathCounterHotkey.Controller;
using DeathCounterHotkey.Controller.Forms;
using DeathCounterHotkey.Database.Models;
using DeathCounterHotkey.Forms;
using DeathCounterHotkey.Resources;
using FallenTally.Controller.Forms;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Web;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace DeathCounterHotkey;

public partial class MainForm : Form, IFormUpdate
{


    private MainController _mainController;
    private PeriodicTimer? _periodicTimer;


    public MainForm(MainController mainController)
    {
        InitializeComponent();
        _mainController = mainController;
        _mainController.SetForm(this);

        locationCombo.DrawItem += LocationCombo_DrawItem;
        locationCombo.DrawMode =
          System.Windows.Forms.DrawMode.OwnerDrawVariable;

        UpdateGameList();
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
        string message = "Are you sure you want to delete the game?";
        string caption = "Delete Game";
        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
        DialogResult result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Warning);

        if (result == DialogResult.Yes)
        {
            this._mainController.RemoveGame();
            UpdateGameList();
            UpdateLocationList();
            UpdateDeaths();
        }
    }

    private void editGameBtn_Click(object sender, EventArgs e)
    {
        string gameName = _mainController.GetGameController().GetActiveGame()?.GameName ?? "";
        if (string.IsNullOrEmpty(gameName)) return;
        new EditForm(gameName, this._mainController.GetEditController(), EditController.EDITCATEGORIE.GAME, UpdateGameList).Show(this);
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

    private void UpdateGameList(string? game = null)
    {
        List<string> games = this._mainController.GetGameController().GetAllGameNames();
        AddGamesToCombo(games);
    }

    private string GetSelectedGame()
    {

        int selectedIndex = gameSelectCombo.SelectedIndex;
        if (selectedIndex == -1) return "";
        return (string)gameSelectCombo.Items[selectedIndex];
    }

    private void gameSelectCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
        string gameName = GetSelectedGame();
        if (string.IsNullOrEmpty(gameName)) return;
        string prefix = _mainController.GameChanged(gameName);
        pretextTxtb.Text = prefix;
        UpdateLocationList();
        SetLocation(GLOBALVARS.DEFAULT_LOCATION);
        UpdateDeaths();

    }



    public void UpdateDeaths()
    {
        int allDeaths = this._mainController.GetAllDeaths();
        deathCountTxtb.Text = "" + allDeaths;
        int locationDeaths = this._mainController.GetLocationDeaths();
        locationDeathCountTxtb.Text = "" + locationDeaths;
        string gamePrefix = _mainController.GetGameController().GetActiveGame()?.Prefix ?? "";
        string locationName = _mainController.GetLocationController().GetActiveLocation()?.Name ?? "";
        _mainController.GetTextController().WriteDeaths(gamePrefix, allDeaths, locationName, locationDeaths);
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
        string message = "Are you sure you want to reset / delete all deaths?";
        string caption = "Delete Deaths";
        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
        DialogResult result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Warning);

        if (result == DialogResult.Yes)
        {
            this._mainController.ResetDeahts();
            UpdateDeaths();
        }
    }

    private void optionsBtn_Click(object sender, EventArgs e)
    {
        new OptionsForm(this._mainController.GetOptionController(), this._mainController.OptionsChangedAction, this._mainController.GetExportController()).Show();
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        this._mainController.UnregisterHotkeys(); 
    }

    private void addLocationBtn_Click(object sender, EventArgs e)
    {
        if (_mainController.GetGameController().GetActiveGame() is null) return;
        new AddLocation(this._mainController.GetLocationController(), UpdateLocationList).Show(this);
    }

    public void UpdateLocationList(string? locationName = null)
    {
        List<string> locations = this._mainController.GetLocationNames();
        AddLocationsToCombo(locations);
        locationCombo.Refresh();
        if (string.IsNullOrEmpty(locationName)) return;
        SetLocation(locationName);
    }



    private void AddLocationsToCombo(List<string> locations)
    {
        locationCombo.Items.Clear();
        locationCombo.Items.AddRange(locations.ToArray());
    }

    private void editLocationBtn_Click(object sender, EventArgs e)
    {
        DeathLocationModel? active = _mainController.GetLocationController().GetActiveLocation();
        if (active is null) return;
        if (active.Name.Equals(GLOBALVARS.DEFAULT_LOCATION)) return;
        new EditForm(active.Name, this._mainController.GetEditController(), EditController.EDITCATEGORIE.LOCATION, UpdateLocationList).Show(this);
    }

    private string GetSelectedLocation()
    {
        int selectedIndex = locationCombo.SelectedIndex;
        if (selectedIndex == -1) return "";
        return (string)locationCombo.Items[selectedIndex];
    }


    private void removeLocationbtn_Click(object sender, EventArgs e)
    {
        string message = "Are you sure you want to delete the location?";
        string caption = "Delete Location";
        MessageBoxButtons buttons = MessageBoxButtons.YesNo;
        DialogResult result = MessageBox.Show(message, caption, buttons, MessageBoxIcon.Warning);

        if (result == DialogResult.Yes)
        {
            if (!_mainController.RemoveLocation()) return;
            UpdateLocationList();
            SetLocation(GLOBALVARS.DEFAULT_LOCATION);
            UpdateDeaths();
        }
    }

    private void locationCombo_SelectedIndexChanged(object sender, EventArgs e)
    {
        string locationName = GetSelectedLocation();
        if (string.IsNullOrEmpty(locationName)) return;
        _mainController.LocationChanged(locationName);
        UpdateDeaths();
    }

    internal int GetLocationIndex()
    {
        return locationCombo.SelectedIndex;
    }

    public void UpdateStreamTime(string time)
    {
        if (streamTimeLbl.InvokeRequired)
        {
            streamTimeLbl.Invoke(UpdateStreamTime, time);
        }
        else
        {
            this.streamTimeLbl.Text = "Stream Time: " + time;
            
        }
    }


    private CancellationTokenSource? _cts;

    private async Task BackgroundTimerRunner(Action<string> updateAction, TimerController controller, CancellationTokenSource token)
    {
        var periodicTimer = new PeriodicTimer(TimeSpan.FromSeconds(1));
        while (await periodicTimer.WaitForNextTickAsync())
        {

            updateAction(TimerController.ConvertTimeToReadableTime(controller.GetTime()));
            controller.AddTime(1);
            //UpdateStreamTime(_mainController.GetStreamTimeController().ConvertTimeToReadableTime(_streamtime));

            if (token.IsCancellationRequested)
            {
                controller.SetTime(0);
                updateAction(TimerController.ConvertTimeToReadableTime(controller.GetTime()));
                // another thread decided to cancel
                Console.WriteLine("task canceled");
                break;
            }
        }
    }

    private void syncTimerBtn_Click(object sender, EventArgs e)
    {
        if (_periodicTimer != null)
        {
            _periodicTimer.Dispose();
            _periodicTimer = null;
        }

        TwitchTokenController tokenController = _mainController.GetTwitchTokenController();
        double time = tokenController.GetStreamTime();
        TimerController controller = new TimerController();
        controller.SetTime((int)time);
        
        if (time > 0)
        {
            _mainController.SetStreamTimeController(controller);
            if (_cts != null)
            {
                _cts.Cancel();
            }
            _cts = new CancellationTokenSource();
            _mainController.SetStreaming(true);
            Task.Run(() =>
            {
                BackgroundTimerRunner(UpdateStreamTime, _mainController.GetStreamTimeController(), _cts);
            }, _cts.Token);


        }

        if (time == -1)
        {
            if (_cts != null)
            {
                _cts.Cancel();
            }

            MessageBox.Show("No Twitch name set!\nCan't retrieve streamtime", "DeathCounterHotkey", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.streamTimeLbl.Text = "Streamtime: 00:00:00";
        }
        else if (time == -2)
        {
            if (_cts != null)
            {
                _cts.Cancel();
            }

            MessageBox.Show("No Twitch stream found!\nCan't retrieve streamtime", "DeathCounterHotkey", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.streamTimeLbl.Text = "Streamtime: 00:00:00";
        }


    }

    public void UpdateRecordingTime(string time)
    {
        if (recordingTimeLbl.InvokeRequired)
        {
            recordingTimeLbl.Invoke(UpdateRecordingTime, time);
        }
        else
        {
            this.recordingTimeLbl.Text = "Recording Time: " + time;
        }
    }


    private CancellationTokenSource? _ctsRecording;

    public void StartRecordingTimer()
    {

        if (_ctsRecording != null)
        {
            _ctsRecording.Cancel();
        }
        _ctsRecording = new CancellationTokenSource();
        Task.Run(() =>
        {
            BackgroundTimerRunner(UpdateRecordingTime, _mainController.GetRecordTimeController(), _ctsRecording);
        }, _ctsRecording.Token);

    
    }

    public void StopRecordTimer()
    {
        _ctsRecording?.Cancel();
        TimerController recordTimerController = new TimerController();
        recordingTimeLbl.Text = "Recording Time: " + TimerController.ConvertTimeToReadableTime(0);

        _mainController.SetRecordTimeController(recordTimerController);
    }


    private void LocationCombo_DrawItem(object sender, DrawItemEventArgs e)
    {
        // Draw the background 


        e.DrawBackground();

        if (e.Index < 0) return;
        // Get the item text    
        string text = ((ComboBox)sender).Items[e.Index].ToString();

        bool finished = this._mainController.GetLocationController().GetFinishState(text);
        // Determine the forecolor based on whether or not the item is selected    
        Brush brush;
        if (finished)// compare  date with your list.  
        {
            brush = Brushes.Green;
        }
        else
        {
            brush = SystemBrushes.ControlDark;
        }

        // Draw the text
        //e.DrawBackground(brush);

        //e.DrawFocusRectangle();


        // change background color
        e.Graphics.FillRectangle(brush, e.Bounds);


        e.Graphics.DrawString(text, ((Control)sender).Font, Brushes.Black, e.Bounds.X, e.Bounds.Y);
        e.DrawFocusRectangle();

        //e.Graphics.DrawString(comboBox1.Items[index].ToString(), e.Font, brush, e.Bounds, StringFormat.GenericDefault);



    }

    internal void UpdateMarkers()
    {
        string[] markers = _mainController.GetMarkerController().GetLatestMarkers();
        markerRTB.Lines = markers;
    }

    public void UpdateForm()
    {
        throw new NotImplementedException();
    }
}