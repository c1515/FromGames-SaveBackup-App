namespace DS3BackupApp.util {
    internal static class PathHelper {
        internal static string GetBackupPathFromList(string savename, string backupFolder, string profile) {
            if (!string.IsNullOrEmpty(savename)) {
                string backupPath = Path.Combine(backupFolder, profile, savename);
                if (!Directory.Exists(backupPath)) {
                    MessageHepler.Error(Properties.Resources.Error_NotfoundBackupfolder);
                    return "";
                }
                return backupPath;
            }
            return "";
        }

        internal static bool ValidatePath(string path, bool isChkExists) {
            return IsValidPath(path,
                 Properties.Resources.Error_EmptyBackupPath,
                 Properties.Resources.Error_InvalidBackupPathChars,
                 Properties.Resources.Error_BackupPathTooLong,
                 Properties.Resources.Error_NotfoundBackupfolder,
                 Properties.Resources.Error_InvalidBackupPath,
                 isChkExists);
        }

        internal static bool ValidatePath(string backupPath, bool isChkExists, string savePath) {
            bool result = IsValidPath(savePath,
                 Properties.Resources.Error_NoSavePath,
                 Properties.Resources.Error_InvalidSavePathChars,
                 null,
                 Properties.Resources.Error_NotfoundSavefolder,
                 Properties.Resources.Error_InvalidSavePath,
                 true);

            if (result) {
                return ValidatePath(backupPath, isChkExists);
            }
            return false;
        }

        internal static bool ValidatePathForAuto(string backupPath, string savePath) {
            bool result = IsValidPath(savePath,
                 Properties.Resources.Error_NoSavePath,
                 Properties.Resources.Error_InvalidSavePathCharsForAuto,
                 null,
                 Properties.Resources.Error_NotfoundSavefolderForAuto,
                 Properties.Resources.Error_InvalidSavePathForAuto,
                 true);

            if (result) {
                return IsValidPath(backupPath,
                 Properties.Resources.Error_EmptyBackupPathForAuto,
                 Properties.Resources.Error_InvalidBackupPathCharsForAuto,
                 Properties.Resources.Error_BackupPathTooLongForAuto,
                 Properties.Resources.Error_NotfoundBackupfolderForAuto,
                 Properties.Resources.Error_InvalidBackupPathForAuto,
                 false);
            }

            return false;

        }

        internal static bool IsValidPath(string path, string emptyMessage, string invalidCharsMessage, string? tooLongMessage, string notFoundMessage, string invalidPathMessage, bool isChkExists) {
            try {
                if (string.IsNullOrEmpty(path)) {
                    MessageHepler.Error(emptyMessage);
                    return false;
                }

                // 無効な文字が含まれていないかチェック
                if (path.IndexOfAny(Path.GetInvalidPathChars()) >= 0) {
                    MessageHepler.Error(invalidCharsMessage);
                    return false;
                }

                // パスの長さが制限を超えていないかチェック
                if (tooLongMessage != null) {
                    if (path.Length > 260) { // Windowsの最大パス長
                        MessageHepler.Error(tooLongMessage);
                        return false;
                    }
                }

                // パスがルートディレクトリとして有効かチェック
                Path.GetFullPath(path);

                if (isChkExists) {
                    if (!Directory.Exists(path)) {
                        MessageHepler.Error(notFoundMessage);
                        return false;
                    }
                }

                return true;
            } catch {
                MessageHepler.Error(invalidPathMessage);
                return false;
            }
        }

        internal static string GetGamePath(string game) {
            return game switch {
                AppConstants.DarkSoulsIII => AppConstants.SavePathDSIII,
                AppConstants.DarkSoulsIISotFS => AppConstants.SavePathDSII,
                AppConstants.DarkSoulsRjp => AppConstants.SavePathDSRjp,
                AppConstants.DarkSoulsRen => AppConstants.SavePathDSRen,
                AppConstants.EldenRing => AppConstants.SavePathER,
                AppConstants.Sekiro => AppConstants.SavePathSekiro,
                AppConstants.ArmoredCore6 => AppConstants.SavePathAC6,
                AppConstants.Nightreign => AppConstants.SavePathNR,
                _ => ""
            };
        }
    }
}
