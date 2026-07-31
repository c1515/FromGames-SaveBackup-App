using System.Text.RegularExpressions;

namespace DS3BackupApp.util {
    internal static class SavedataService {
        internal static bool Backup(string backupPath, string saveFolderPath) {
            bool isSuccess = false;
            foreach (var filePath in FileSystemHelper.GetFiles(saveFolderPath)) {
                string fileName = Path.GetFileName(filePath);
                string destPath = Path.Combine(backupPath, fileName);
                isSuccess = FileSystemHelper.CopyFile(filePath, destPath);
                if (!isSuccess) {
                    break;
                }
            }


            if (isSuccess) {
                MemoService.Create(backupPath);
            }

            return isSuccess;
        }

        internal static bool BackupRepo(string backupPath, string saveFolderPath) {
            string backupSavePath = Path.Combine(backupPath, Path.GetFileName(saveFolderPath));
            bool isSuccess = FileSystemHelper.CopyDirectory(saveFolderPath, backupSavePath);

            if (isSuccess) {
                MemoService.Create(backupPath);
            }

            return isSuccess;
        }

        internal static void Restore(string backupPath, string saveFolderPath, string gameName) {
            if (!File.Exists(Path.Combine(backupPath, GetSavefile(gameName)))) {
                MessageHepler.Error(Properties.Resources.Error_NotFoundSavedata);
                return;
            }
            bool isSuccess = false;
            foreach (var filePath in FileSystemHelper.GetFiles(backupPath)) {
                string fileName = Path.GetFileName(filePath);
                if (fileName != AppConstants.MemoFile) {
                    string destPath = Path.Combine(saveFolderPath, fileName);
                    isSuccess = FileSystemHelper.CopyFile(filePath, destPath);
                    if (!isSuccess) {
                        break;
                    }
                }
            }

            if (isSuccess) {
                MessageHepler.Info(Properties.Resources.Complite_Restore);
            }
        }

        internal static void RestoreRepo(string backupPath, string saveDirName) {
            bool isSuccess = FileSystemHelper.CopyDirectory(backupPath, Path.Combine(AppConstants.SavePathRepo, saveDirName));
            if (isSuccess) {
                MessageHepler.Info(Properties.Resources.Complite_Restore);
            }
        }

        internal static void RestoreRepoSelectedLevel(string backupFolderPath, string saveName, int restoreCount) {
            string saveFolderPath = Path.Combine(AppConstants.SavePathRepo, saveName);

            string[] orderedFiles = GetOrderedSaveFiles(backupFolderPath); // 古い→新しい
            string[] filesToRestore = orderedFiles.Take(restoreCount).ToArray();

            if (filesToRestore.Length == 0) {
                throw new InvalidOperationException("復元対象のファイルが見つかりません。");
            }

            string tempFolderPath = Path.Combine(Path.GetTempPath(), "RepoSaveRestore_" + Guid.NewGuid());
            FileSystemHelper.CreateDirectory(tempFolderPath);

            try {
                for (int i = 0; i < filesToRestore.Length; i++) {
                    bool isLatest = (i == filesToRestore.Length - 1);
                    string newFileName = isLatest ? $"{saveName}.es3" : $"{saveName}_BACKUP{i + 1}.es3";

                    string destPath = Path.Combine(tempFolderPath, newFileName);
                    File.Copy(filesToRestore[i], destPath);
                }

                bool liveFolderExisted = Directory.Exists(saveFolderPath);
                string backupOfLive = saveFolderPath.TrimEnd(Path.DirectorySeparatorChar) + "_old_" + Guid.NewGuid();

                if (liveFolderExisted) {
                    Directory.Move(saveFolderPath, backupOfLive);
                }

                try {
                    Directory.Move(tempFolderPath, saveFolderPath);
                } catch {
                    // 失敗時のロールバック
                    if (liveFolderExisted) {
                        Directory.Move(backupOfLive, saveFolderPath);
                    }
                    throw;
                }

                if (liveFolderExisted) {
                    Directory.Delete(backupOfLive, true);
                }
            } finally {
                if (Directory.Exists(tempFolderPath)) {
                    Directory.Delete(tempFolderPath, true);
                }
            }

            MessageHepler.Info(Properties.Resources.Complite_Restore);
        }

        private static string[] GetOrderedSaveFiles(string backupFolderPath) {
            string[] files = FileSystemHelper.GetFiles(backupFolderPath);
            return files.OrderBy(f => GetBackupOrderKey(f)).ToArray();
        }

        private static int GetBackupOrderKey(string filePath) {
            string fileName = Path.GetFileName(filePath);
            Match match = Regex.Match(fileName, @"_BACKUP(\d+)\.es3$", RegexOptions.IgnoreCase);
            return match.Success ? int.Parse(match.Groups[1].Value) : int.MaxValue;
        }

        internal static bool Delete(string backupPath) {
            return FileSystemHelper.DeleteDirectory(backupPath, true);
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

        internal static bool AutoBackup(string backupFolderPath, string saveFolderPath, decimal maxAutosave, string gameName) {
            int autosaveIndex = 0;
            switch (gameName) {
                case AppConstants.DarkSoulsIII:
                    autosaveIndex = Properties.Settings.Default.AutosaveIndexDS3;
                    break;
                case AppConstants.DarkSoulsIISotFS:
                    autosaveIndex = Properties.Settings.Default.AutosaveIndexDS2;
                    break;
                case AppConstants.DarkSoulsRjp:
                    autosaveIndex = Properties.Settings.Default.AutosaveIndexDSRjp;
                    break;
                case AppConstants.DarkSoulsRen:
                    autosaveIndex = Properties.Settings.Default.AutosaveIndexDSRen;
                    break;
                case AppConstants.EldenRing:
                    autosaveIndex = Properties.Settings.Default.AutosaveIndexER;
                    break;
                case AppConstants.Sekiro:
                    autosaveIndex = Properties.Settings.Default.AutosaveIndexSekiro;
                    break;
                case AppConstants.ArmoredCore6:
                    autosaveIndex = Properties.Settings.Default.AutosaveIndexAC6;
                    break;
                case AppConstants.Nightreign:
                    autosaveIndex = Properties.Settings.Default.AutosaveIndexNR;
                    break;
                case AppConstants.Repo:
                    autosaveIndex = Properties.Settings.Default.AutosaveIndexRepo;
                    break;
            }

            string backupPath = Path.Combine(backupFolderPath, AppConstants.AutosaveProfile, AppConstants.AutosaveFormat + autosaveIndex);
            if (!PathHelper.ValidatePath(backupPath, false)) {
                return false;
            }

            FileSystemHelper.CreateDirectory(backupPath);

            bool isSuccess = false;
            if (gameName == AppConstants.Repo) {
                isSuccess = BackupRepo(backupPath, saveFolderPath);
            } else {
                isSuccess = Backup(backupPath, saveFolderPath);
            }

            if (isSuccess) {
                switch (gameName) {
                    case AppConstants.DarkSoulsIII:
                        Properties.Settings.Default.AutosaveIndexDS3++;
                        break;
                    case AppConstants.DarkSoulsIISotFS:
                        Properties.Settings.Default.AutosaveIndexDS2++;
                        break;
                    case AppConstants.DarkSoulsRjp:
                        Properties.Settings.Default.AutosaveIndexDSRjp++;
                        break;
                    case AppConstants.DarkSoulsRen:
                        Properties.Settings.Default.AutosaveIndexDSRen++;
                        break;
                    case AppConstants.EldenRing:
                        Properties.Settings.Default.AutosaveIndexER++;
                        break;
                    case AppConstants.Sekiro:
                        Properties.Settings.Default.AutosaveIndexSekiro++;
                        break;
                    case AppConstants.ArmoredCore6:
                        Properties.Settings.Default.AutosaveIndexAC6++;
                        break;
                    case AppConstants.Nightreign:
                        Properties.Settings.Default.AutosaveIndexNR++;
                        break;
                    case AppConstants.Repo:
                        Properties.Settings.Default.AutosaveIndexRepo++;
                        break;
                }
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
            } else {
                FileSystemHelper.DeleteDirectory(backupPath, true);
            }

            return isSuccess;
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
