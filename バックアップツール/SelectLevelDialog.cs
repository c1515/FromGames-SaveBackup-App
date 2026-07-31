using DS3BackupApp.util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DS3BackupApp {
    public partial class SelectLevelDialog : Form {
        private readonly string saveDirPath;
        private string SelectedLevel => cmbLevel.Text.Trim();
        private string SelectedLocation => cmbLocation.Text.Trim();
        public int restoreCount;

        public SelectLevelDialog(string saveDirPath) {
            InitializeComponent();

            this.saveDirPath = saveDirPath;

            string[] levels = RepoService.GetSelectableLevels(saveDirPath);
            string[] locations = RepoService.GetLocationsForLevel(saveDirPath, levels.Length);

            cmbLevel.Items.AddRange(levels);
            cmbLocation.Items.AddRange(locations);

            cmbLevel.SelectedIndex = 0;
            cmbLocation.SelectedIndex = 0;
        }

        private void cmbLevel_SelectedIndexChanged(object sender, EventArgs e) {
            cmbLocation.Items.Clear();
            string[] locations = RepoService.GetLocationsForLevel(saveDirPath, int.Parse(cmbLevel.Text.Trim()));
            cmbLocation.Items.AddRange(locations);
            cmbLocation.SelectedIndex = 0;
        }

        private void btnOk_Click(object sender, EventArgs e) {
            restoreCount = RepoService.GetRestoreCountForLevelAndLocation(saveDirPath, int.Parse(SelectedLevel), SelectedLocation);
        }
    }
}
