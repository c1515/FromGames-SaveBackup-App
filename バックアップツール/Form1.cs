using DS3BackupApp.util;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace DS3BackupApp {
    public partial class FormBackupApp : Form {
        private string saveFolderPath = "";
        private bool IsLoading = true;
        private bool IsAccuountChanged = false;
        private bool IsBackupPathChanged = false;

        public FormBackupApp() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            cmbGame.Items.Add(AppConstants.DarkSoulsRjp);
            cmbGame.Items.Add(AppConstants.DarkSoulsRen);
            cmbGame.Items.Add(AppConstants.DarkSoulsIISotFS);
            cmbGame.Items.Add(AppConstants.DarkSoulsIII);
            cmbGame.Items.Add(AppConstants.Sekiro);
            cmbGame.Items.Add(AppConstants.EldenRing);
            cmbGame.Items.Add(AppConstants.Nightreign);
            cmbGame.Items.Add(AppConstants.ArmoredCore6);
            cmbGame.Items.Add(AppConstants.Repo);

            if (Properties.Settings.Default.IsFirstRun) {
                txtSelectedBackupFolderPath.Text = AppConstants.DefaultBackupPath;
                lblBackupFolderPathDisplay.Text = AppConstants.DefaultBackupPathDS3;
                int minutes = (int)numBackupInterval.Value;
                timerBackup.Interval = minutes * 60 * 1000; // 分をミリ秒に変換

                cmbGame.SelectedItem = AppConstants.DarkSoulsIII;

                Properties.Settings.Default.IsFirstRun = false;
                Properties.Settings.Default.Save();
            } else {
                txtSelectedBackupFolderPath.Text = Properties.Settings.Default.BackupFolder;//設定ファイルの見直し
                numBackupInterval.Value = Properties.Settings.Default.BackupInterval;
                timerBackup.Interval = (int)numBackupInterval.Value * 60 * 1000;
                saveFolderPath = Properties.Settings.Default.SaveFolder;
                numMaxAutosave.Value = Properties.Settings.Default.MaxAutosave;
                cmbGame.SelectedItem = Properties.Settings.Default.SelectedGame;
            }

            IsLoading = false;
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            bool retry = false;
            do {
                try {
                    Properties.Settings.Default.BackupFolder = txtSelectedBackupFolderPath.Text.Trim();
                    Properties.Settings.Default.BackupInterval = numBackupInterval.Value;
                    Properties.Settings.Default.SaveFolder = saveFolderPath;
                    Properties.Settings.Default.MaxAutosave = numMaxAutosave.Value;
                    Properties.Settings.Default.SelectedProfile = cmbProfile.Text.Trim();
                    Properties.Settings.Default.SelectedSaveprofile = cmbSaveprofile.Text.Trim();
                    Properties.Settings.Default.SelectedAccount = cmbAccount.Text.Trim();
                    Properties.Settings.Default.SelectedGame = cmbGame.Text.Trim();

                    Properties.Settings.Default.Save();
                    retry = false; // 成功した場合はリトライしない
                } catch (Exception ex) {
                    retry = MessageHepler.Warning(string.Format(Properties.Resources.Warning_Close, ex.Message));
                }
            } while (retry);
        }

        private void btnBackaup_Click(object sender, EventArgs e) {
            if (!PathHelper.ValidatePath(lblBackupFolderPathDisplay.Text.Trim(), false, saveFolderPath)) {
                return;
            }

            if (string.IsNullOrEmpty(cmbSaveprofile.Text.Trim())) {
                MessageHepler.Error(Properties.Resources.Error_EmptyProfile);
                return;
            }

            if (string.IsNullOrEmpty(cmbSavename.Text.Trim())) {
                MessageHepler.Error(Properties.Resources.Error_EmptySavename);
                return;
            }

            string backupPath = Path.Combine(lblBackupFolderPathDisplay.Text.Trim(), cmbSaveprofile.Text.Trim(), cmbSavename.Text.Trim());
            if (!PathHelper.ValidatePath(backupPath, false)) {
                return;
            }

            if (!SavedataService.PrepareForBackup(backupPath, cmbSavename, cmbSaveprofile, cmbProfile)) {
                return;
            }

            bool isBackupSuccess = false;
            if (cmbGame.Text.Trim() == AppConstants.Repo) {
                isBackupSuccess = SavedataService.BackupRepo(backupPath, saveFolderPath);
            } else {
                isBackupSuccess = SavedataService.Backup(backupPath, saveFolderPath);
            }

            if (isBackupSuccess) {
                MessageHepler.Info(Properties.Resources.Complite_Backup);
                if (cmbProfile.Text.Trim() == cmbSaveprofile.Text.Trim()) {
                    ProfileService.SetSavedata(lblBackupFolderPathDisplay.Text.Trim(), cmbProfile.Text.Trim(), lstSavedata);
                }
            }
        }

        private void btnSelectBackupFolder_Click(object sender, EventArgs e) {
            using (FolderBrowserDialog dialog = new()) {
                if (dialog.ShowDialog() == DialogResult.OK) {
                    lblBackupFolderPathDisplay.Text = Path.Combine(dialog.SelectedPath, AppConstants.TopBackupFolder, cmbGame.Text.Trim(), cmbAccount.Text.Trim());
                    txtSelectedBackupFolderPath.Text = dialog.SelectedPath;
                }
            }

            if (!PathHelper.ValidatePath(lblBackupFolderPathDisplay.Text.Trim(), false)) {
                txtSelectedBackupFolderPath.Clear();
                lblBackupFolderPathDisplay.Text = "";
                return;
            }

            IsBackupPathChanged = true;
            ProfileService.SetProfile(lblBackupFolderPathDisplay.Text.Trim(), cmbSaveprofile, cmbProfile, IsLoading);
            IsBackupPathChanged = false;
        }

        private void lstSavedata_DoubleClick(object sender, EventArgs e) {
            if (cmbGame.Text.Trim() == AppConstants.Repo) {
                if (!PathHelper.ValidatePath(lblBackupFolderPathDisplay.Text.Trim(), true, AppConstants.SavePathRepo)) {
                    return;
                }
            } else {
                if (!PathHelper.ValidatePath(lblBackupFolderPathDisplay.Text.Trim(), true, saveFolderPath)) {
                    return;
                }
            }

            ProfileService.GetSavename(lstSavedata, out string saveName);
            if (string.IsNullOrEmpty(saveName)) {
                return;
            }

            PathHelper.GetBackupPathFromList(saveName, lblBackupFolderPathDisplay.Text, cmbProfile.Text.Trim(), out string backupPath);
            if (!PathHelper.ValidatePath(backupPath, true)) {
                return;
            }

            if (cmbGame.Text.Trim() == AppConstants.Repo) {
                var (backupSavePath, saveDirName) = RepoService.GetBackupDir(backupPath);
                if (string.IsNullOrEmpty(backupSavePath)) {
                    return;
                }

                if (chkSelectLevel != null && chkSelectLevel.Checked) {
                    using var dlg = new SelectLevelDialog(backupSavePath);
                    if (dlg.ShowDialog(this) != DialogResult.OK) {
                        return;
                    }
                    int restoreCount = dlg.restoreCount;

                    if (!MessageHepler.Confirm(string.Format(Properties.Resources.Confirm_Restore, saveName))) {
                        return;
                    }

                    SavedataService.RestoreRepoSelectedLevel(backupSavePath, saveDirName, restoreCount);
                } else {
                    if (!MessageHepler.Confirm(string.Format(Properties.Resources.Confirm_Restore, saveName))) {
                        return;
                    }

                    SavedataService.RestoreRepo(backupSavePath, saveDirName);
                }
            } else {
                if (!MessageHepler.Confirm(string.Format(Properties.Resources.Confirm_Restore, saveName))) {
                    return;
                }
                SavedataService.Restore(backupPath, saveFolderPath, cmbGame.Text.Trim());
            }
        }

        private void timerBackup_Tick(object sender, EventArgs e) {
            if (InvokeRequired) {
                Invoke(new Action(() => timerBackup_Tick(sender, e)));
                return;
            }

            if (!PathHelper.ValidatePathForAuto(lblBackupFolderPathDisplay.Text.Trim(), saveFolderPath)) {
                return;
            }

            bool isBackupSuccess = SavedataService.AutoBackup(lblBackupFolderPathDisplay.Text.Trim(), saveFolderPath, numMaxAutosave.Value, cmbGame.Text.Trim());
            if (isBackupSuccess) {
                if (!cmbProfile.Items.Contains(AppConstants.AutosaveProfile)) {
                    cmbProfile.Items.Add(AppConstants.AutosaveProfile);
                }
                if (cmbProfile.Text.Trim() == AppConstants.AutosaveProfile) {
                    ProfileService.SetSavedata(lblBackupFolderPathDisplay.Text.Trim(), cmbProfile.Text.Trim(), lstSavedata);
                }
            }
        }

        private void chkAutoBackup_CheckedChanged(object? sender, EventArgs e) {
            if (InvokeRequired) {
                Invoke(new Action(() => chkAutoBackup_CheckedChanged(sender, e)));
                return;
            }

            if (chkAutoBackup.Checked && !PathHelper.ValidatePath(lblBackupFolderPathDisplay.Text.Trim(), false, saveFolderPath)) {
                // チェックボックスの状態を元に戻す  
                chkAutoBackup.CheckedChanged -= chkAutoBackup_CheckedChanged; // イベントを一時的に解除
                chkAutoBackup.Checked = !chkAutoBackup.Checked;
                chkAutoBackup.CheckedChanged += chkAutoBackup_CheckedChanged; // イベントを再登録
                return;
            }

            // タイマーの開始または停止  
            if (chkAutoBackup.Checked) {
                timerBackup.Start();
            } else {
                timerBackup.Stop();
            }
        }

        private void numBackupInterval_ValueChanged(object sender, EventArgs e) {
            int minutes = (int)numBackupInterval.Value;
            timerBackup.Interval = minutes * 60 * 1000;
        }

        private void cmbSaveprofile_SelectedIndexChanged(object sender, EventArgs e) {
            if (!IsLoading && !IsAccuountChanged && !IsBackupPathChanged && !PathHelper.ValidatePath(lblBackupFolderPathDisplay.Text.Trim(), true)) {
                cmbSavename.Items.Clear();
                return;
            }
            ProfileService.SetSavename(lblBackupFolderPathDisplay.Text.Trim(), cmbSaveprofile.Text.Trim(), cmbSavename);
        }

        private void cmbProfile_SelectedIndexChanged(object sender, EventArgs e) {
            if (!IsLoading && !IsAccuountChanged && !IsBackupPathChanged && !PathHelper.ValidatePath(lblBackupFolderPathDisplay.Text.Trim(), true)) {
                cmbProfile.Items.Clear();
                lstSavedata.Items.Clear();
                return;
            }

            ProfileService.SetSavedata(lblBackupFolderPathDisplay.Text.Trim(), cmbProfile.Text.Trim(), lstSavedata);

            txtMemo.Text = Properties.Resources.Info_SelectSavedata;
            txtMemo.Enabled = false;
        }

        private void txtMemo_Leave(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(txtSelectedBackupFolderPath.Text.Trim())) {
                MessageHepler.Error(Properties.Resources.Error_MemoWriteEmptyBackupPath);
                return;
            }

            ProfileService.GetSavename(lstSavedata, out string saveName);
            if (string.IsNullOrEmpty(saveName)) {
                return;
            }

            PathHelper.GetBackupPathFromList(saveName, lblBackupFolderPathDisplay.Text.Trim(), cmbProfile.Text.Trim(), out string profilePath);
            if (!PathHelper.ValidatePath(profilePath, true)) {
                MessageHepler.Error(Properties.Resources.Error_MemoWrite);
                return;
            }

            MemoService.Write(profilePath, txtMemo.Text);
        }

        private void btnLordSavedata_Click(object sender, EventArgs e) {
            lstSavedata_DoubleClick(sender, e);
        }

        private void btnDeleteSavedata_Click(object sender, EventArgs e) {
            if (!PathHelper.ValidatePath(lblBackupFolderPathDisplay.Text.Trim(), true)) {
                return;
            }

            ProfileService.GetSavename(lstSavedata, out string saveName);
            if (string.IsNullOrEmpty(saveName)) {
                return;
            }

            PathHelper.GetBackupPathFromList(saveName, lblBackupFolderPathDisplay.Text.Trim(), cmbProfile.Text.Trim(), out string backupPath);
            if (!PathHelper.ValidatePath(backupPath, true)) {
                return;
            }

            if (!MessageHepler.Confirm(string.Format(Properties.Resources.Confirm_DeleteSavedata, saveName))) {
                return;
            }

            bool isDeleteSuccess = SavedataService.Delete(backupPath);
            if (isDeleteSuccess) {
                ProfileService.RemoveSavedata(saveName, lstSavedata);
                ProfileService.RemoveSavename(saveName, cmbSavename);
                txtMemo.Text = Properties.Resources.Info_SelectSavedata;
                txtMemo.Enabled = false;
            }
        }

        private void btnDeleteProfile_Click(object sender, EventArgs e) {
            if (!PathHelper.ValidatePath(lblBackupFolderPathDisplay.Text.Trim(), true)) {
                return;
            }

            string profile = cmbProfile.Text.Trim();
            if (string.IsNullOrEmpty(profile)) {
                return;
            }

            string backupPath = Path.Combine(lblBackupFolderPathDisplay.Text.Trim(), profile);
            if (!PathHelper.ValidatePath(backupPath, true)) {
                return;
            }

            if (ProfileService.Delete(backupPath, profile)) {
                string saveProfile = cmbSaveprofile.Text.Trim();
                ProfileService.RemoveProfile(profile, cmbProfile);
                ProfileService.RemoveProfile(profile, cmbSaveprofile);
                if (profile.Equals(saveProfile)) {
                    cmbSavename.Text = "";
                }
            }
        }

        private void cmbSaveprofile_KeyPress(object sender, KeyPressEventArgs e) {
            cmbSavename.Items.Clear();
        }

        private void cmbAccount_SelectedIndexChanged(object sender, EventArgs e) {
            if (InvokeRequired) {
                Invoke(new Action(() => cmbAccount_SelectedIndexChanged(sender, e)));
                return;
            }

            string gameFolder = PathHelper.GetGamePath(cmbGame.Text.Trim());
            if (string.IsNullOrEmpty(gameFolder)) {
                MessageHepler.Error(Properties.Resources.Error_InvalidGameName);
                return;
            }

            string path = Path.Combine(gameFolder, cmbAccount.Text.Trim());
            if (Directory.Exists(path)) {
                IsAccuountChanged = true;
                saveFolderPath = path;
                lblBackupFolderPathDisplay.Text = Path.Combine(txtSelectedBackupFolderPath.Text.Trim(), AppConstants.TopBackupFolder, cmbGame.Text.Trim());
                if (cmbGame.Text.Trim() != AppConstants.Repo) {
                    lblBackupFolderPathDisplay.Text = Path.Combine(lblBackupFolderPathDisplay.Text.Trim(), cmbAccount.Text.Trim());
                }
                ProfileService.SetProfile(lblBackupFolderPathDisplay.Text.Trim(), cmbSaveprofile, cmbProfile, IsLoading);
                IsAccuountChanged = false;
                cmbSavename.Text = "";
            } else {
                MessageHepler.Error(Properties.Resources.Error_NotFoundAccount);
            }

            if (cmbGame.Text.Trim() == AppConstants.Repo) {
                string saveFolderPath = Path.Combine(AppConstants.SavePathRepo, cmbAccount.Text.Trim());
                var (completedLevel, currentLocation) = RepoService.GetLevelAndLocation(saveFolderPath);
                lblCompletedLevelsCount.Text = completedLevel;
                lblCurrentLocationDisplay.Text = currentLocation;
            }
        }

        private void btnChangeName_Click(object sender, EventArgs e) {
            if (!PathHelper.ValidatePath(lblBackupFolderPathDisplay.Text.Trim(), true)) {
                return;
            }

            if (lstSavedata.SelectedItem == null) {
                MessageHepler.Error(Properties.Resources.Error_NoItemSelected);
                return;

            }
            ProfileService.GetSavename(lstSavedata, out string oldName);
            if (string.IsNullOrEmpty(oldName)) {
                return;
            }

            while (true) {
                using (var inputDialog = new InputDialog(Properties.Resources.Prompt_EnterNewName, Properties.Resources.Form_ChangeName, oldName)) {
                    if (inputDialog.ShowDialog() == DialogResult.OK) {
                        string newName = inputDialog.InputText.Trim();
                        if (string.IsNullOrEmpty(newName)) {
                            MessageHepler.Error(Properties.Resources.Error_EmptySavename);
                            continue;
                        }

                        PathHelper.GetBackupPathFromList(oldName, lblBackupFolderPathDisplay.Text.Trim(), cmbProfile.Text.Trim(), out string oldPath);
                        if (!PathHelper.ValidatePath(oldPath, true)) {
                            return;
                        }

                        string newPath = Path.Combine(Path.GetDirectoryName(oldPath) ?? string.Empty, newName);
                        if (!PathHelper.ValidatePath(newPath, false)) {
                            return;
                        }

                        if (Directory.Exists(newPath)) {
                            MessageHepler.Error(Properties.Resources.Error_NameAlreadyExists);
                            oldName = newName;
                            continue;
                        }

                        if (FileSystemHelper.MoveDirectory(oldPath, newPath)) {
                            ProfileService.SetSavedata(lblBackupFolderPathDisplay.Text.Trim(), cmbProfile.Text.Trim(), lstSavedata);
                            MessageHepler.Info(string.Format(Properties.Resources.Info_Rename, oldName, newName));
                            break;
                        } else {
                            break;
                        }
                    } else {
                        break;
                    }
                }
            }

        }

        private void lstSavedata_SelectedIndexChanged(object sender, EventArgs e) {
            if (lstSavedata.SelectedItem == null) {
                return;
            }

            txtMemo.Clear();
            if (!txtMemo.Enabled) {
                txtMemo.Enabled = true;
                txtMemo.ScrollBars = ScrollBars.Vertical;
            }

            if (string.IsNullOrEmpty(txtSelectedBackupFolderPath.Text.Trim())) {
                MessageHepler.Error(Properties.Resources.Error_MemoReadNoBackupPath);
                return;
            }

            ProfileService.GetSavename(lstSavedata, out string saveName);
            if (string.IsNullOrEmpty(saveName)) {
                return;
            }

            PathHelper.GetBackupPathFromList(saveName, lblBackupFolderPathDisplay.Text.Trim(), cmbProfile.Text.Trim(), out string profilePath);
            if (!PathHelper.ValidatePath(profilePath, true)) {
                return;
            }

            MemoService.Read(profilePath, txtMemo);
        }

        private void cmbGame_SelectedIndexChanged(object sender, EventArgs e) {
            string gameFolder = PathHelper.GetGamePath(cmbGame.Text.Trim());
            if (string.IsNullOrEmpty(gameFolder)) {
                MessageHepler.Error(Properties.Resources.Error_InvalidGameName);
                return;
            }
            if (!Directory.Exists(gameFolder)) {
                ToggleElement(false);
                MessageHepler.Error(Properties.Resources.Error_NotfoundSavefolder);
                return;
            }

            string[] subFolders = PathHelper.GetSubFolders(gameFolder);
            if (subFolders.Length == 0) {
                ToggleElement(false);
                return;
            }

            ToggleElement(true);
            saveFolderPath = subFolders[0];
            AccountHelper.SetAccount(subFolders, cmbAccount, gameFolder);

            // ここで見た目をゲームに応じて動的に変更する
            UpdateAppearanceForGame(cmbGame.Text.Trim());
        }

        private void UpdateAppearanceForGame(string game) {
            // 例: ゲームごとにフォーム背景色・txtMemo の色・一部コントロールの有効/無効を切り替える
            bool isRepo = game == AppConstants.Repo;
            lblCompletedLevels.Visible = isRepo;
            lblCompletedLevelsCount.Visible = isRepo;
            lblCurrentLocation.Visible = isRepo;
            lblCurrentLocationDisplay.Visible = isRepo;
            lblAccount.Text = isRepo ? Properties.Resources.AccountLabel_BackupTarget : Properties.Resources.AccountLabel_Account;
            chkSelectLevel.Visible = isRepo;

            // 複数コントロールの更新をまとめて行う場合はレイアウトを一時停止してから再開
            this.SuspendLayout();
            // ここでさらに: cmbProfile.Visible = (game != AppConstants.Repo);
            this.ResumeLayout();
        }

        private void ToggleElement(bool isEnabled) {
            btnBackup.Enabled = isEnabled;
            btnChangeName.Enabled = isEnabled;
            btnDeleteProfile.Enabled = isEnabled;
            btnDeleteSavedata.Enabled = isEnabled;
            btnLordSavedata.Enabled = isEnabled;
            btnSelectBackupFolder.Enabled = isEnabled;
            chkAutoBackup.Enabled = isEnabled;
            lstSavedata.Enabled = isEnabled;
            cmbProfile.Enabled = isEnabled;
            txtSelectedBackupFolderPath.Enabled = isEnabled;
            cmbAccount.Enabled = isEnabled;
        }

        private void btnBackupFolderOpen_Click(object sender, EventArgs e) {
            var path = txtSelectedBackupFolderPath.Text?.Trim() ?? string.Empty;

            if (!PathHelper.ValidatePath(path, true)) {
                MessageBox.Show(this, "指定されたフォルダパスが無効です。パスを確認してください。", "フォルダを開けません", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try {
                // エクスプローラーでフォルダを開く
                var psi = new System.Diagnostics.ProcessStartInfo {
                    FileName = "explorer.exe",
                    Arguments = $"\"{path}\"",
                    UseShellExecute = true
                };
                System.Diagnostics.Process.Start(psi);
            } catch (Exception ex) {
                MessageBox.Show(this, $"フォルダを開く際にエラーが発生しました:\n{ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
