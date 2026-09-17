namespace DS3BackupApp.util {
    internal static class ProfileService {
        internal static void RemoveSavedata(string savename, ListBox listBox) {
            if (listBox == null || listBox.Items == null) {
                throw new ArgumentNullException(nameof(listBox), "ListBoxか、そのアイテムが未設定");
            }

            string prefix = savename ?? throw new ArgumentNullException(nameof(savename), "Savenameがnull");
            for (int i = listBox.Items.Count - 1; i >= 0; i--) {
                var item = listBox.Items[i]?.ToString();
                if (item != null && item.StartsWith(prefix)) {
                    listBox.Items.RemoveAt(i);
                    break;
                }
            }
        }

        internal static void RemoveSavename(string savename, ComboBox comboBox) {
            if (comboBox == null || comboBox.Items == null) {
                throw new ArgumentNullException(nameof(comboBox), "comboBoxか、そのアイテムが未設定");
            }

            string prefix = savename ?? throw new ArgumentNullException(nameof(savename), "Savenameがnull");
            for (int i = comboBox.Items.Count - 1; i >= 0; i--) {
                var item = comboBox.Items[i]?.ToString();
                if (item != null && item.StartsWith(prefix)) {
                    comboBox.Items.RemoveAt(i);
                    break;
                }
            }
        }

        internal static void RemoveProfile(string profile, ComboBox comboBox) {
            if (comboBox == null || comboBox.Items == null) {
                throw new ArgumentNullException(nameof(comboBox), "comboBoxか、そのアイテムが未設定");
            }

            for (int i = comboBox.Items.Count - 1; i >= 0; i--) {
                var item = comboBox.Items[i]?.ToString();
                if (item != null && item.Equals(profile)) {
                    comboBox.Items.RemoveAt(i);

                    // 削除後の選択アイテムを安全に設定
                    if (comboBox.Items.Count > 0) {
                        int nextIndex = Math.Min(i, comboBox.Items.Count - 1);
                        comboBox.SelectedItem = comboBox.Items[nextIndex];
                    } else {
                        comboBox.SelectedItem = null; // アイテムが空の場合
                    }

                    break;
                }
            }
        }

        internal static void SetSavedata(string backupPath, string profile, ListBox lstSavedata) {
            if (lstSavedata == null || lstSavedata.Items == null) {
                throw new ArgumentNullException(nameof(lstSavedata), "ListBoxか、そのアイテムが未設定");
            }

            lstSavedata.Items.Clear();

            if (!Directory.Exists(backupPath)) {
                return;
            }

            string profileParh = Path.Combine(backupPath, profile);
            if (!Directory.Exists(profileParh)) {
                return;
            }

            string[] saveDirs = FileSystemHelper.GetDirectories(profileParh);
            if (profile == AppConstants.AutosaveProfile) {
                Array.Sort(saveDirs, (x, y) => {
                    string xName = Path.GetFileName(x).Replace(AppConstants.AutosaveFormat, "");
                    string yName = Path.GetFileName(y).Replace(AppConstants.AutosaveFormat, "");

                    if (int.TryParse(xName, out int xIndex) && int.TryParse(yName, out int yIndex)) {
                        return xIndex.CompareTo(yIndex); // 数値として比較
                    }

                    return string.Compare(xName, yName, StringComparison.Ordinal); // 数値でない場合は文字列として比較
                });
            }

            bool isRepo = backupPath.Contains(AppConstants.Repo);
            foreach (var saveDir in saveDirs) {
                string? saveName = Path.GetFileName(saveDir);
                if (!string.IsNullOrEmpty(saveName) && FileSystemHelper.GetLastWriteTime(saveDir, out DateTime lastWriteTime)) {
                    if (isRepo) {
                        var (backupDir, _) = RepoService.GetBackupDir(saveDir);
                        var (completedLevel, currentLocation) = RepoService.GetLevelAndLocation(backupDir);
                        lstSavedata.Items.Add(string.Format(AppConstants.RepoSavedataListFormat, saveName, completedLevel, currentLocation, lastWriteTime));
                    } else {
                        lstSavedata.Items.Add(string.Format(AppConstants.SavedataListFormat, saveName, lastWriteTime));
                    }
                }
            }
        }

        internal static void GetSavename(ListBox lstSavedata, out string saveName) {
            saveName = string.Empty;
            if (lstSavedata == null || lstSavedata.Items == null) {
                throw new ArgumentNullException(nameof(lstSavedata), "ListBoxか、そのアイテムが未設定");
            }

            string? selectedItem = lstSavedata.SelectedItem?.ToString(); // Null条件演算子を使用してnull参照を防止
            if (selectedItem != null) {
                int savenameEnd = selectedItem.IndexOf(" - ");
                if (savenameEnd < 0) {
                    MessageHepler.Error(Properties.Resources.Error_InvalidFormat);
                    return;
                }
                saveName = selectedItem[..savenameEnd];

                if (saveName.Contains(':')) {
                    savenameEnd = selectedItem.IndexOf(" : ");
                    if (savenameEnd < 0) {
                        MessageHepler.Error(Properties.Resources.Error_InvalidFormat);
                        return;
                    }
                    saveName = selectedItem[..savenameEnd];
                }
            } else {
                MessageHepler.Error(Properties.Resources.Error_NoItemSelected);
            }
        }

        internal static bool Delete(string backupPath, string profile) {
            if (!Directory.Exists(backupPath)) {
                MessageHepler.Error(Properties.Resources.Error_NotfoundBackupfolder);
                return false;
            }

            if (!MessageHepler.Confirm(string.Format(Properties.Resources.Confirm_DeleteProfile, profile))) {
                return false;
            }

            return FileSystemHelper.DeleteDirectory(backupPath, true);
        }

        internal static void SetProfile(string backupPath, ComboBox cmbSaveprofile, ComboBox cmbProfile, bool IsLoading) {
            if (cmbSaveprofile == null || cmbSaveprofile.Items == null) {
                throw new ArgumentNullException(nameof(cmbSaveprofile), "cmbSaveprofileか、そのアイテムが未設定");
            }
            if (cmbProfile == null || cmbProfile.Items == null) {
                throw new ArgumentNullException(nameof(cmbProfile), "cmbProfileか、そのアイテムが未設定");
            }

            cmbProfile.Items.Clear();
            cmbSaveprofile.Items.Clear();

            string[] profiles = Directory.Exists(backupPath) ? FileSystemHelper.GetDirectories(backupPath) : [];
            bool IsSaveprofileMatch = false;
            bool IsProfileMatch = false;
            if (profiles.Length > 0) {
                foreach (var dirPath in profiles) {
                    string? profileName = Path.GetFileName(dirPath); // Nullable 型に変更
                    if (!string.IsNullOrEmpty(profileName)) { // Null チェックを追加
                        if (profileName != AppConstants.AutosaveProfile) {
                            cmbSaveprofile.Items.Add(profileName);
                            if (IsLoading && Properties.Settings.Default.SelectedSaveprofile == profileName) {
                                cmbSaveprofile.SelectedItem = profileName; // 選択されたアイテムを設定
                                IsSaveprofileMatch = true;
                            }
                        }
                        cmbProfile.Items.Add(profileName);
                        if (IsLoading && Properties.Settings.Default.SelectedProfile == profileName) {
                            cmbProfile.SelectedItem = profileName;
                            IsProfileMatch = true;
                        }
                    }
                }
                if (!IsSaveprofileMatch && cmbSaveprofile.Items.Count > 0) {
                    cmbSaveprofile.SelectedIndex = 0;
                }
                if (!IsProfileMatch) {
                    cmbProfile.SelectedIndex = 0;
                    if (cmbProfile.Items.Count > 1 && cmbProfile.Text == AppConstants.AutosaveProfile) {
                        cmbProfile.SelectedIndex = 1;
                    }
                }
            } else {
                cmbProfile.Items.Add(AppConstants.DefaultProfile);
                cmbSaveprofile.Items.Add(AppConstants.DefaultProfile);
                // 初期値を設定
                if (cmbProfile.Items.Count > 0 && cmbSaveprofile.Items.Count > 0) {
                    cmbProfile.SelectedIndex = 0; // 最初の項目を選択
                    cmbSaveprofile.SelectedIndex = 0;
                }
            }
        }

        internal static void SetSavename(string backupPath, string profile, ComboBox cmbSavename) {
            if (cmbSavename == null || cmbSavename.Items == null) {
                throw new ArgumentNullException(nameof(cmbSavename), "cmbSavenameか、そのアイテムが未設定");
            }

            cmbSavename.Items.Clear();

            if (string.IsNullOrEmpty(backupPath) || string.IsNullOrEmpty(profile) || !Directory.Exists(backupPath)) {
                return;
            }

            string profileParh = Path.Combine(backupPath, profile);
            if (!PathHelper.ValidatePath(profileParh, true)) {
                return;
            }

            foreach (var dirPath in FileSystemHelper.GetDirectories(profileParh)) {
                string? savename = Path.GetFileName(dirPath);
                if (!string.IsNullOrEmpty(savename)) {
                    cmbSavename.Items.Add(savename);
                }
            }
        }
    }
}
