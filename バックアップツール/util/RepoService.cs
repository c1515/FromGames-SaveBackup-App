namespace DS3BackupApp.util {
    internal static class RepoService {
        internal static (string, string) GetLevelAndLocation(string saveFolderPath) {
            string completedLevel = "0";
            string currentLocation = Properties.RepoResources.CurrentLocation_Unknown;

            string[] savedatas = FileSystemHelper.GetFiles(saveFolderPath);

            if (savedatas != null && savedatas.Length > 0) {
                int completedLevelInt = (savedatas.Length + 2) / 3;
                completedLevel = completedLevelInt.ToString();

                int rem = savedatas.Length % 3;
                switch (rem) {
                    case 0:
                        string nextLevel = (completedLevelInt + 1).ToString();
                        currentLocation = string.Format(Properties.RepoResources.CurrentLocation_LevelStart, nextLevel);
                        break;
                    case 1:
                        currentLocation = Properties.RepoResources.CurrentLocation_Shop;
                        break;
                    case 2:
                        currentLocation = Properties.RepoResources.CurrentLocation_Intermission;
                        break;
                }
            }

            return (completedLevel, currentLocation);
        }

        internal static (string, string) GetBackupDir(string backupPath) {
            string backupSavePath = "";
            string saveDirName = "";

            string[] dirs = FileSystemHelper.GetDirectories(backupPath);
            bool hasSaveFile = false;
            if (dirs.Length > 0) {
                foreach (string dir in dirs) {
                    string dirName = Path.GetFileName(dir);
                    if (dirName.StartsWith(AppConstants.RepoSaveDirPrefix)) {
                        string[] saveFiles = FileSystemHelper.GetFiles(dir);
                        foreach (string file in saveFiles) {
                            string fileName = Path.GetFileName(file);
                            string fileExt = Path.GetExtension(fileName);
                            if (fileExt == AppConstants.RepoSaveFileExtension) {
                                backupSavePath = Path.Combine(backupPath, dirName);
                                hasSaveFile = true;
                                saveDirName = dirName;
                                break;
                            }
                        }

                    }
                    if (hasSaveFile) {
                        break;
                    }
                }
            }

            if (!hasSaveFile) {
                MessageHepler.Error(Properties.Resources.Error_NotFoundSavedata);
            }

            return (backupSavePath, saveDirName);
        }

        internal static string[] GetSelectableLevels(string saveFolderPath) {
            int total = FileSystemHelper.GetFiles(saveFolderPath).Length;
            int maxLevel = (total / 3) + 1;

            return Enumerable.Range(1, maxLevel)
                             .Select(i => i.ToString())
                             .Reverse()
                             .ToArray();
        }

        internal static string[] GetLocationsForLevel(string saveFolderPath, int level) {
            int total = FileSystemHelper.GetFiles(saveFolderPath).Length;
            var locations = new List<string>();

            if (level == 1) {
                // レベル1だけ開始地点のセーブが存在しない
                if (total >= 1)
                    locations.Add(Properties.RepoResources.CurrentLocation_Shop);
                if (total >= 2)
                    locations.Add(Properties.RepoResources.CurrentLocation_Intermission);
            } else {
                int startIdx = 3 * level - 3;
                int shopIdx = 3 * level - 2;
                int intIdx = 3 * level - 1;

                if (total >= startIdx)
                    locations.Add(string.Format(Properties.RepoResources.CurrentLocation_LevelStart, level.ToString()));
                if (total >= shopIdx)
                    locations.Add(Properties.RepoResources.CurrentLocation_Shop);
                if (total >= intIdx)
                    locations.Add(Properties.RepoResources.CurrentLocation_Intermission);
            }

            return locations.ToArray();
        }

        internal static int GetRestoreCountForLevelAndLocation(string saveFolderPath, int level, string location) {
            int total = FileSystemHelper.GetFiles(saveFolderPath).Length;
            int restoreCount = 0;
            if (level == 1) {
                if (location == Properties.RepoResources.CurrentLocation_Shop && total >= 1)
                    restoreCount = 1;
                else if (location == Properties.RepoResources.CurrentLocation_Intermission && total >= 2)
                    restoreCount = 2;
            } else {
                int startIdx = 3 * level - 3;
                int shopIdx = 3 * level - 2;
                int intIdx = 3 * level - 1;
                if (location == string.Format(Properties.RepoResources.CurrentLocation_LevelStart, level.ToString()) && total >= startIdx)
                    restoreCount = startIdx;
                else if (location == Properties.RepoResources.CurrentLocation_Shop && total >= shopIdx)
                    restoreCount = shopIdx;
                else if (location == Properties.RepoResources.CurrentLocation_Intermission && total >= intIdx)
                    restoreCount = intIdx;
            }
            return restoreCount;
        }
    }
}
