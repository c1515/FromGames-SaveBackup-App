namespace DS3BackupApp.util {
    internal static class SavedataService {
        internal static void Backup(string backupPath, string saveFolderPath) {
            foreach (var filePath in FileSystemHelper.GetFiles(saveFolderPath)) {
                string fileName = Path.GetFileName(filePath);
                string destPath = Path.Combine(backupPath, fileName);
                FileSystemHelper.CopyFile(filePath, destPath);
            }

            MemoService.Create(backupPath);
        }

        internal static void Restore(string backupPath, string saveFolderPath, string gameName) {
            if (!File.Exists(Path.Combine(backupPath, GetSavefile(gameName)))) {
                MessageHepler.Error(Properties.Resources.Error_NotFoundSavedata);
                return;
            }

            foreach (var filePath in FileSystemHelper.GetFiles(backupPath)) {
                string fileName = Path.GetFileName(filePath);
                if (fileName != AppConstants.MemoFile) {
                    string destPath = Path.Combine(saveFolderPath, fileName);
                    FileSystemHelper.CopyFile(filePath, destPath);
                }
            }

            MessageHepler.Info(Properties.Resources.Complite_Restore);
        }

        internal static void Delete(string backupPath) {
            FileSystemHelper.DeleteDirectory(backupPath, true);
        }

        internal static bool PrepareForBackup(string backupPath, ComboBox cmbSavename, ComboBox cmbSaveprofile, ComboBox cmbProfile) {
            if (!Directory.Exists(backupPath)) {
                FileSystemHelper.CreateDirectory(backupPath);

                cmbSavename.Items.Add(cmbSavename.Text.Trim());

                if (!cmbSaveprofile.Items.Contains(cmbSaveprofile.Text.Trim())) {
                    cmbSaveprofile.Items.Add(cmbSaveprofile.Text.Trim());
                }

                if (!cmbProfile.Items.Contains(cmbSaveprofile.Text.Trim())) {
                    cmbProfile.Items.Add(cmbSaveprofile.Text.Trim());
                }
            } else {
                if (!MessageHepler.Confirm(string.Format(Properties.Resources.Confirm_Overwrite, cmbSavename.Text.Trim()))) {
                    return false;
                }

                FileSystemHelper.SetLastWriteTime(backupPath, DateTime.Now);

            }
            return true;
        }

        internal static void AutoBackup(string backupFolderPath, string saveFolderPath, decimal maxAutosave) {
            string backupPath = Path.Combine(backupFolderPath, AppConstants.AutosaveProfile, AppConstants.AutosaveFormat + Properties.Settings.Default.AutosaveIndex);
            if (!PathHelper.ValidatePath(backupPath, false)) {
                return;
            }

            FileSystemHelper.CreateDirectory(backupPath);

            Backup(backupPath, saveFolderPath);

            Properties.Settings.Default.AutosaveIndex++;
            Properties.Settings.Default.Save();

            if (maxAutosave > 0) {
                string[] autosaveFolders = FileSystemHelper.GetDirectories(Path.Combine(backupFolderPath, AppConstants.AutosaveProfile));
                if (autosaveFolders.Length > maxAutosave) {
                    Array.Sort(autosaveFolders, (x, y) => {
                        string xName = Path.GetFileName(x).Replace(AppConstants.AutosaveFormat, "");
                        string yName = Path.GetFileName(y).Replace(AppConstants.AutosaveFormat, "");

                        if (int.TryParse(xName, out int xIndex) && int.TryParse(yName, out int yIndex)) {
                            return xIndex.CompareTo(yIndex); // 数値として比較
                        }

                        return string.Compare(xName, yName, StringComparison.Ordinal); // 数値でない場合は文字列として比較
                    });

                    for (int i = 0; i < autosaveFolders.Length - maxAutosave; i++) {
                        FileSystemHelper.DeleteDirectory(autosaveFolders[i], true);
                    }
                }
            }
        }

        internal static string GetSavefile(string gameName) {
            return gameName switch {
                AppConstants.DarkSoulsIII => AppConstants.SaveFileDS3,
                AppConstants.DarkSoulsIISotFS => AppConstants.SaveFileDS2,
                AppConstants.DarkSoulsRjp => AppConstants.SaveFileDSR,
                AppConstants.DarkSoulsRen => AppConstants.SaveFileDSR,
                AppConstants.EldenRing => AppConstants.SaveFileER,
                AppConstants.Sekiro => AppConstants.SaveFileDSekiro,
                AppConstants.ArmoredCore6 => AppConstants.SaveFileAC6,
                AppConstants.Nightreign => AppConstants.SaveFileNR,
                _ => "不正な値",
            };
        }
    }
}
