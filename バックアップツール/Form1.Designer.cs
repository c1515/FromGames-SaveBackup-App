namespace DS3BackupApp {
    partial class FormBackupApp {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBackupApp));
            btnBackup = new Button();
            txtSelectedBackupFolderPath = new TextBox();
            btnSelectBackupFolder = new Button();
            timerBackup = new System.Windows.Forms.Timer(components);
            chkAutoBackup = new CheckBox();
            numBackupInterval = new NumericUpDown();
            lblBackupInterval = new Label();
            lblProfile = new Label();
            cmbProfile = new ComboBox();
            lstSavedata = new ListBox();
            txtMemo = new TextBox();
            numMaxAutosave = new NumericUpDown();
            lblMaxAutosave = new Label();
            lblBackupFolderPath = new Label();
            cmbSavename = new ComboBox();
            lblSavename = new Label();
            cmbSaveprofile = new ComboBox();
            lblSaveprofile = new Label();
            btnDeleteSavedata = new Button();
            btnLordSavedata = new Button();
            btnDeleteProfile = new Button();
            cmbAccount = new ComboBox();
            lblAccount = new Label();
            btnChangeName = new Button();
            cmbGame = new ComboBox();
            lblGame = new Label();
            lblBackupFolderPathDisplay = new Label();
            lblCompletedLevels = new Label();
            lblCompletedLevelsCount = new Label();
            lblCurrentLocation = new Label();
            lblCurrentLocationDisplay = new Label();
            btnBackupFolderOpen = new Button();
            chkSelectLevel = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)numBackupInterval).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMaxAutosave).BeginInit();
            SuspendLayout();
            // 
            // btnBackup
            // 
            resources.ApplyResources(btnBackup, "btnBackup");
            btnBackup.Name = "btnBackup";
            btnBackup.UseVisualStyleBackColor = true;
            btnBackup.Click += btnBackaup_Click;
            // 
            // txtSelectedBackupFolderPath
            // 
            resources.ApplyResources(txtSelectedBackupFolderPath, "txtSelectedBackupFolderPath");
            txtSelectedBackupFolderPath.Name = "txtSelectedBackupFolderPath";
            // 
            // btnSelectBackupFolder
            // 
            resources.ApplyResources(btnSelectBackupFolder, "btnSelectBackupFolder");
            btnSelectBackupFolder.Name = "btnSelectBackupFolder";
            btnSelectBackupFolder.UseVisualStyleBackColor = true;
            btnSelectBackupFolder.Click += btnSelectBackupFolder_Click;
            // 
            // timerBackup
            // 
            timerBackup.Interval = 300000;
            timerBackup.Tick += timerBackup_Tick;
            // 
            // chkAutoBackup
            // 
            resources.ApplyResources(chkAutoBackup, "chkAutoBackup");
            chkAutoBackup.Name = "chkAutoBackup";
            chkAutoBackup.UseVisualStyleBackColor = true;
            chkAutoBackup.CheckedChanged += chkAutoBackup_CheckedChanged;
            // 
            // numBackupInterval
            // 
            resources.ApplyResources(numBackupInterval, "numBackupInterval");
            numBackupInterval.Maximum = new decimal(new int[] { 1440, 0, 0, 0 });
            numBackupInterval.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numBackupInterval.Name = "numBackupInterval";
            numBackupInterval.Value = new decimal(new int[] { 5, 0, 0, 0 });
            numBackupInterval.ValueChanged += numBackupInterval_ValueChanged;
            // 
            // lblBackupInterval
            // 
            resources.ApplyResources(lblBackupInterval, "lblBackupInterval");
            lblBackupInterval.Name = "lblBackupInterval";
            // 
            // lblProfile
            // 
            resources.ApplyResources(lblProfile, "lblProfile");
            lblProfile.Name = "lblProfile";
            // 
            // cmbProfile
            // 
            resources.ApplyResources(cmbProfile, "cmbProfile");
            cmbProfile.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProfile.FormattingEnabled = true;
            cmbProfile.Name = "cmbProfile";
            cmbProfile.SelectedIndexChanged += cmbProfile_SelectedIndexChanged;
            // 
            // lstSavedata
            // 
            resources.ApplyResources(lstSavedata, "lstSavedata");
            lstSavedata.FormattingEnabled = true;
            lstSavedata.Name = "lstSavedata";
            lstSavedata.SelectedIndexChanged += lstSavedata_SelectedIndexChanged;
            lstSavedata.DoubleClick += lstSavedata_DoubleClick;
            // 
            // txtMemo
            // 
            resources.ApplyResources(txtMemo, "txtMemo");
            txtMemo.Name = "txtMemo";
            txtMemo.Leave += txtMemo_Leave;
            // 
            // numMaxAutosave
            // 
            resources.ApplyResources(numMaxAutosave, "numMaxAutosave");
            numMaxAutosave.Name = "numMaxAutosave";
            numMaxAutosave.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // lblMaxAutosave
            // 
            resources.ApplyResources(lblMaxAutosave, "lblMaxAutosave");
            lblMaxAutosave.Name = "lblMaxAutosave";
            // 
            // lblBackupFolderPath
            // 
            resources.ApplyResources(lblBackupFolderPath, "lblBackupFolderPath");
            lblBackupFolderPath.Name = "lblBackupFolderPath";
            // 
            // cmbSavename
            // 
            resources.ApplyResources(cmbSavename, "cmbSavename");
            cmbSavename.FormattingEnabled = true;
            cmbSavename.Name = "cmbSavename";
            // 
            // lblSavename
            // 
            resources.ApplyResources(lblSavename, "lblSavename");
            lblSavename.Name = "lblSavename";
            // 
            // cmbSaveprofile
            // 
            resources.ApplyResources(cmbSaveprofile, "cmbSaveprofile");
            cmbSaveprofile.FormattingEnabled = true;
            cmbSaveprofile.Name = "cmbSaveprofile";
            cmbSaveprofile.SelectedIndexChanged += cmbSaveprofile_SelectedIndexChanged;
            cmbSaveprofile.KeyPress += cmbSaveprofile_KeyPress;
            // 
            // lblSaveprofile
            // 
            resources.ApplyResources(lblSaveprofile, "lblSaveprofile");
            lblSaveprofile.Name = "lblSaveprofile";
            // 
            // btnDeleteSavedata
            // 
            resources.ApplyResources(btnDeleteSavedata, "btnDeleteSavedata");
            btnDeleteSavedata.Name = "btnDeleteSavedata";
            btnDeleteSavedata.UseVisualStyleBackColor = true;
            btnDeleteSavedata.Click += btnDeleteSavedata_Click;
            // 
            // btnLordSavedata
            // 
            resources.ApplyResources(btnLordSavedata, "btnLordSavedata");
            btnLordSavedata.Name = "btnLordSavedata";
            btnLordSavedata.UseVisualStyleBackColor = true;
            btnLordSavedata.Click += btnLordSavedata_Click;
            // 
            // btnDeleteProfile
            // 
            resources.ApplyResources(btnDeleteProfile, "btnDeleteProfile");
            btnDeleteProfile.Name = "btnDeleteProfile";
            btnDeleteProfile.UseVisualStyleBackColor = true;
            btnDeleteProfile.Click += btnDeleteProfile_Click;
            // 
            // cmbAccount
            // 
            resources.ApplyResources(cmbAccount, "cmbAccount");
            cmbAccount.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAccount.FormattingEnabled = true;
            cmbAccount.Name = "cmbAccount";
            cmbAccount.SelectedIndexChanged += cmbAccount_SelectedIndexChanged;
            // 
            // lblAccount
            // 
            resources.ApplyResources(lblAccount, "lblAccount");
            lblAccount.Name = "lblAccount";
            // 
            // btnChangeName
            // 
            resources.ApplyResources(btnChangeName, "btnChangeName");
            btnChangeName.Name = "btnChangeName";
            btnChangeName.UseVisualStyleBackColor = true;
            btnChangeName.Click += btnChangeName_Click;
            // 
            // cmbGame
            // 
            resources.ApplyResources(cmbGame, "cmbGame");
            cmbGame.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGame.FormattingEnabled = true;
            cmbGame.Name = "cmbGame";
            cmbGame.SelectedIndexChanged += cmbGame_SelectedIndexChanged;
            // 
            // lblGame
            // 
            resources.ApplyResources(lblGame, "lblGame");
            lblGame.Name = "lblGame";
            // 
            // lblBackupFolderPathDisplay
            // 
            resources.ApplyResources(lblBackupFolderPathDisplay, "lblBackupFolderPathDisplay");
            lblBackupFolderPathDisplay.Name = "lblBackupFolderPathDisplay";
            // 
            // lblCompletedLevels
            // 
            resources.ApplyResources(lblCompletedLevels, "lblCompletedLevels");
            lblCompletedLevels.Name = "lblCompletedLevels";
            // 
            // lblCompletedLevelsCount
            // 
            resources.ApplyResources(lblCompletedLevelsCount, "lblCompletedLevelsCount");
            lblCompletedLevelsCount.Name = "lblCompletedLevelsCount";
            // 
            // lblCurrentLocation
            // 
            resources.ApplyResources(lblCurrentLocation, "lblCurrentLocation");
            lblCurrentLocation.Name = "lblCurrentLocation";
            // 
            // lblCurrentLocationDisplay
            // 
            resources.ApplyResources(lblCurrentLocationDisplay, "lblCurrentLocationDisplay");
            lblCurrentLocationDisplay.Name = "lblCurrentLocationDisplay";
            // 
            // btnBackupFolderOpen
            // 
            resources.ApplyResources(btnBackupFolderOpen, "btnBackupFolderOpen");
            btnBackupFolderOpen.Name = "btnBackupFolderOpen";
            btnBackupFolderOpen.UseVisualStyleBackColor = true;
            btnBackupFolderOpen.Click += btnBackupFolderOpen_Click;
            // 
            // chkSelectLevel
            // 
            resources.ApplyResources(chkSelectLevel, "chkSelectLevel");
            chkSelectLevel.Name = "chkSelectLevel";
            chkSelectLevel.UseVisualStyleBackColor = true;
            // 
            // FormBackupApp
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(chkSelectLevel);
            Controls.Add(btnBackupFolderOpen);
            Controls.Add(lblCurrentLocationDisplay);
            Controls.Add(lblCurrentLocation);
            Controls.Add(lblCompletedLevelsCount);
            Controls.Add(lblCompletedLevels);
            Controls.Add(lblBackupFolderPathDisplay);
            Controls.Add(lblGame);
            Controls.Add(cmbGame);
            Controls.Add(btnChangeName);
            Controls.Add(lblAccount);
            Controls.Add(cmbAccount);
            Controls.Add(btnDeleteProfile);
            Controls.Add(btnLordSavedata);
            Controls.Add(btnDeleteSavedata);
            Controls.Add(lblSaveprofile);
            Controls.Add(cmbSaveprofile);
            Controls.Add(lblSavename);
            Controls.Add(cmbSavename);
            Controls.Add(lblBackupFolderPath);
            Controls.Add(lblMaxAutosave);
            Controls.Add(numMaxAutosave);
            Controls.Add(txtMemo);
            Controls.Add(lstSavedata);
            Controls.Add(cmbProfile);
            Controls.Add(lblProfile);
            Controls.Add(lblBackupInterval);
            Controls.Add(numBackupInterval);
            Controls.Add(chkAutoBackup);
            Controls.Add(btnSelectBackupFolder);
            Controls.Add(txtSelectedBackupFolderPath);
            Controls.Add(btnBackup);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormBackupApp";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)numBackupInterval).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMaxAutosave).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnBackup;
        private TextBox txtSelectedBackupFolderPath;
        private Button btnSelectBackupFolder;
        private System.Windows.Forms.Timer timerBackup;
        private CheckBox chkAutoBackup;
        private NumericUpDown numBackupInterval;
        private Label lblBackupInterval;
        private Label lblProfile;
        private ComboBox cmbProfile;
        private ListBox lstSavedata;
        private TextBox txtMemo;
        private NumericUpDown numMaxAutosave;
        private Label lblMaxAutosave;
        private Label lblBackupFolderPath;
        private ComboBox cmbSavename;
        private Label lblSavename;
        private ComboBox cmbSaveprofile;
        private Label lblSaveprofile;
        private Button btnDeleteSavedata;
        private Button btnLordSavedata;
        private Button btnDeleteProfile;
        private ComboBox cmbAccount;
        private Label lblAccount;
        private Button btnChangeName;
        private ComboBox cmbGame;
        private Label lblGame;
        private Label lblBackupFolderPathDisplay;
        private Label lblCompletedLevels;
        private Label lblCompletedLevelsCount;
        private Label lblCurrentLocation;
        private Label lblCurrentLocationDisplay;
        private Button btnBackupFolderOpen;
        private CheckBox chkSelectLevel;
    }
}
