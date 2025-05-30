namespace DS3BackupApp.util {
    internal static class AccountHelper {
        internal static string[] GetAccounFolders(string gameFolder) {
            string[] accountFolders = FileSystemHelper.GetDirectories(gameFolder);
            if (accountFolders.Length == 0) {
                MessageHepler.Error(Properties.Resources.Error_NotfoundSavefolder);
            }
            return accountFolders;
        }

        internal static void SetAccount(string[] accountFolders, ComboBox cmbAccount, string gameFolder) {
            if (cmbAccount == null) {
                throw new ArgumentNullException(nameof(cmbAccount), "ComboBoxが未設定");
            }

            cmbAccount.Items.Clear();

            foreach (var folder in accountFolders) {
                int accountStart = gameFolder.Length + 1;
                string account = folder[accountStart..];
                cmbAccount.Items.Add(account);
            }

            bool isAccountFound = false;
            foreach (var account in cmbAccount.Items) {
                if (string.IsNullOrEmpty(account.ToString())) {
                    continue;
                }
                if (Properties.Settings.Default.SelectedAccount == account.ToString()) {
                    cmbAccount.SelectedItem = account.ToString();
                    isAccountFound = true;
                }
            }
            if (cmbAccount.Items.Count > 0 && !isAccountFound) {
                cmbAccount.SelectedIndex = 0;
            }
        }
    }
}
