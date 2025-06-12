using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using DeathCounterHotkey.Database.Models;
using FallenTallyAvalon.Controller;
using FallenTallyAvalon.Views;

namespace FallenTallyAvalon.Dialogue;

public partial class GameDialogWindow : Window, IDialog<GameStatsModel>
{

    private GameStatsModel? _data;
    private GameController _gameController;

    public GameDialogWindow(GameController gameController)
    {
        InitializeComponent();
        _gameController = gameController;
    }

    public void CloseDialogue()
    {
        this.Close();
    }

    public GameStatsModel? GetData()
    {
        if (!IsValidGameName()) return null;
        if (_data == null)
        {
            _data = new GameStatsModel();
        }
        _data.GameName = txtGameName.Text ?? "";
        _data.Prefix = txtGamePrefix.Text ?? "";
        return _data;
    }

    private void TxtGameName_TextInput(object? sender, Avalonia.Input.TextInputEventArgs e)
    {
        if (IsValidGameName())
        {
            btnSave.IsEnabled = false;
            return;
        }
        btnSave.IsEnabled = true;
    }

    private bool IsValidGameName()
    {
        return !string.IsNullOrEmpty(txtGameName?.Text) && !string.IsNullOrEmpty(txtGamePrefix?.Text) && !IsDupeGame();
    }

    private bool IsDupeGame()
    {
        return _gameController.IsDupeName(txtGameName.Text ?? "");
    }

    public void Init(GameStatsModel? data = null)
    {
        if (data != null)
        {
            txtGameName.Text = data.GameName;
            txtGamePrefix.Text = data.Prefix;
            this._data = data;
        }
    }

    public void ShowDialogue()
    {
        this.ShowDialog(MainWindow.Instance);
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }



}