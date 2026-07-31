namespace DS3BackupApp {
    internal static class AppConstants {

        public const string DefaultProfile = "default";
        public const string AutosaveProfile = "Autosaves";
        public const string MemoFile = "memo.txt";
        public const string TopBackupFolder = "Backups";
        public const string DarkSoulsIII = "DarkSoulsIII";
        public const string DefaultBackupFolderSuffix = "_Backup";
        public const string AutosaveSaveName = "Autosave";
        public const string SavedataListFormat = "{0} - {1:yyyy/MM/dd HH:mm:ss}";
        public const string SaveFileDS3 = "DS30000.sl2";
        public const string AutosaveFormat = AutosaveSaveName + " ";
        public const string FromGames = "FromGames";
        public const string EldenRing = "EldenRing";
        public const string DarkSoulsII = "DarkSoulsII";
        public const string DarkSoulsIISotFS = "DarkSoulsII SotFS";
        public const string DarkSoulsRjp = "DarkSouls Remastered (日本語)";
        public const string DarkSoulsRen = "DarkSouls Remastered (English)";
        public const string DarkSoulsRPath = "DARK SOULS REMASTERED";
        public const string Sekiro = "Sekiro";
        public const string ArmoredCore6 = "ArmoredCore6";
        public const string FromSoftware = "FromSoftware";
        public const string NBGI = "NBGI";
        public const string SaveFileER = "ER0000.sl2";
        public const string SaveFileDS2 = "DS2SOFS0000.sl2";
        public const string SaveFileDSR = "DRAKS0005.sl2";
        public const string SaveFileDSekiro = "S0000.sl2";
        public const string SaveFileAC6 = "AC60000.sl2";
        public const string NightreignPath = "Nightreign";
        public const string Nightreign = "EldenRing Nightreign";
        public const string SaveFileNR = "NR0000.sl2";
        public const string Repo = "R.E.P.O.";
        public const string RepoSaveDirPrefix = "REPO_SAVE_";
        public const string RepoSaveFileExtension = ".es3";
        public static readonly string DefaultBackupPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), FromGames + DefaultBackupFolderSuffix);
        public static readonly string DefaultBackupPathDS3 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), FromGames + DefaultBackupFolderSuffix, TopBackupFolder, DarkSoulsIII);
        public static readonly string SavePathDSIII = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), DarkSoulsIII);
        public static readonly string SavePathDSII = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), DarkSoulsII);
        public static readonly string SavePathDSRjp = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), FromSoftware, DarkSoulsRPath);
        public static readonly string SavePathDSRen = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), NBGI, DarkSoulsRPath);
        public static readonly string SavePathER = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), EldenRing);
        public static readonly string SavePathSekiro = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Sekiro);
        public static readonly string SavePathAC6 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ArmoredCore6);
        public static readonly string SavePathNR = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), NightreignPath);
        // LocalLow は SpecialFolder に無いためユーザープロファイルから組立てています (例: %USERPROFILE%\AppData\LocalLow)
        public static readonly string LocalLowPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData", "LocalLow");
        public static readonly string SavePathRepo = Path.Combine(LocalLowPath, "semiwork", "Repo", "saves");
        public static readonly string RepoSavedataListFormat = "{0} : " + Properties.RepoResources.SavedataListFormat_CompletedLevels + " {1}, " + Properties.RepoResources.SavedataListFormat_CurrentLocation + " {2} - {3:yyyy/MM/dd HH:mm:ss}";

    }
}
