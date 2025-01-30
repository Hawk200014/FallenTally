using DeathCounterHotkey.Controller.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeathCounterHotkey.Forms
{
    public partial class ExportForm : Form
    {
        private ExportController _exportController;
        public ExportForm(ExportController exportController)
        {
            InitializeComponent();
            _exportController = exportController;

            LoadValues();
            CalcEntries();
            CalcEntriesMarker();
        }

        private void CalcEntries()
        {
            string? date = filterDeathDateCombo.Text == "" ? null : filterDeathDateCombo.Text;
            string? game = filterDeathGameCombo.Text == "" ? null : filterDeathGameCombo.Text;
            string? location = filterDeathLocationCombo.Text == "" ? null : filterDeathLocationCombo.Text;
            this.deathEntriesTB.Text = _exportController.GetDeathCount(game, location, date).ToString();
        }

        private void CalcEntriesMarker()
        {
            string? date = filterMarkerDateCombo.Text == "" ? null : filterMarkerDateCombo.Text;
            string? game = filterMarkerGameCombo.Text == "" ? null : filterMarkerGameCombo.Text;
            int? session = filterMarkerSessionCombo.Text == "" ? null : (int?)int.Parse(filterMarkerSessionCombo.Text);
            this.MarkerEntriesTB.Text = _exportController.GetMarkerCount(game, session, date).ToString();
        }

        private void LoadValues()
        {
            filterDeathGameCombo.Items.Clear();
            filterDeathGameCombo.Items.Add("");
            filterDeathGameCombo.Items.AddRange(_exportController.GetGameDeathList());
            filterDeathDateCombo.Items.Clear();
            filterDeathDateCombo.Items.Add("");
            filterDeathDateCombo.Items.AddRange(_exportController.GetDistinctDeathDates());
            deathExportFormatCombo.Items.Clear();
            deathExportFormatCombo.Items.AddRange(_exportController.GetExportFormats());
            markerExportFormatCombo.Items.Clear();
            markerExportFormatCombo.Items.AddRange(_exportController.GetExportFormats());
            filterMarkerGameCombo.Items.Clear();
            filterMarkerGameCombo.Items.Add("");
            filterMarkerGameCombo.Items.AddRange(_exportController.GetGameMarkerList());
            filterMarkerDateCombo.Items.Clear();
            filterMarkerDateCombo.Items.Add("");
            filterMarkerDateCombo.Items.AddRange(_exportController.GetDistinctMarkerDates());
            filterMarkerSessionCombo.Items.Clear();
            filterMarkerSessionCombo.Items.Add("");
            filterMarkerSessionCombo.Items.AddRange(_exportController.GetDistinctMarkerSessions());
        }

        private void filterDeathGameCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateLocationList();
            UpdateDateList();
            CalcEntries();
        }

        private void UpdateDateList()
        {
            string[] dates = _exportController.GetDistinctDeathDates(filterDeathGameCombo.Text, filterDeathLocationCombo.Text);
            filterDeathDateCombo.Items.Clear();
            filterDeathDateCombo.Items.Add("");
            filterDeathDateCombo.Items.AddRange(dates);
            filterDeathDateCombo.SelectedIndex = 0;
        }

        private void UpdateLocationList()
        {
            string[] locations = _exportController.GetLocationDeathList(filterDeathGameCombo.Text);
            filterDeathLocationCombo.Items.Clear();
            filterDeathLocationCombo.Items.Add("");
            filterDeathLocationCombo.Items.AddRange(locations);
            filterDeathLocationCombo.SelectedIndex = 0;
        }

        private void filterDeathDateCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcEntries();
        }

        private void filterDeathLocationCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDateList();
            CalcEntries();
        }

        private void DeathsExportBtn_Click(object sender, EventArgs e)
        {
            FileDialog fileDialog = new SaveFileDialog();
            if (deathExportFormatCombo.Text == "")
            {
                MessageBox.Show("Please select a format to export", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (deathEntriesTB.Text == "0")
            {
                MessageBox.Show("No entries to export", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ExportController.ExportType exportType = (ExportController.ExportType)Enum.Parse(typeof(ExportController.ExportType), deathExportFormatCombo.Text);
            switch (exportType)
            {
                case ExportController.ExportType.CSV:
                    fileDialog.Filter = "CSV files (*.csv)|*.csv";
                    break;
                case ExportController.ExportType.EXCEL:
                    fileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                    break;
                default:
                    break;
            }
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                _exportController.Export(exportType, filterDeathGameCombo.Text, filterDeathLocationCombo.Text, filterDeathDateCombo.Text, fileDialog.FileName);
            }

        }

        private void UpdateMarkerDateList()
        {
            string[] dates = _exportController.GetDistinctMarkerDates(filterMarkerGameCombo.Text);
            filterMarkerDateCombo.Items.Clear();
            filterMarkerDateCombo.Items.Add("");
            filterMarkerDateCombo.Items.AddRange(dates);
            filterMarkerDateCombo.SelectedIndex = 0;
        }

        private void UpdateMarkerSessionList()
        {
            string[] sessions = _exportController.GetDistinctMarkerSessions(filterMarkerGameCombo.Text, filterMarkerDateCombo.Text);
            filterMarkerSessionCombo.Items.Clear();
            filterMarkerSessionCombo.Items.Add("");
            filterMarkerSessionCombo.Items.AddRange(sessions);
            filterMarkerSessionCombo.SelectedIndex = 0;
        }

        private void filterMarkerGameCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMarkerDateList();
            CalcEntriesMarker();
        }

        private void filterMarkerDateCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMarkerSessionList();
            CalcEntriesMarker();
        }

        private void filterMarkerSessionCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcEntriesMarker();
        }

        private void markerExportBtn_Click(object sender, EventArgs e)
        {
            FileDialog fileDialog = new SaveFileDialog();
            if (markerExportFormatCombo.Text == "")
            {
                MessageBox.Show("Please select a format to export", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (MarkerEntriesTB.Text == "0")
            {
                MessageBox.Show("No entries to export", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ExportController.ExportType exportType = (ExportController.ExportType)Enum.Parse(typeof(ExportController.ExportType), markerExportFormatCombo.Text);
            switch (exportType)
            {
                case ExportController.ExportType.CSV:
                    fileDialog.Filter = "CSV files (*.csv)|*.csv";
                    break;
                case ExportController.ExportType.EXCEL:
                    fileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                    break;
                default:
                    break;
            }
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                _exportController.ExportMarker(exportType, filterMarkerGameCombo.Text, filterMarkerDateCombo.Text, filterMarkerSessionCombo.Text, fileDialog.FileName);
            }
        }
    }
}
