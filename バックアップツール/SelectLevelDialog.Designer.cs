namespace DS3BackupApp {
    partial class SelectLevelDialog {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectLevelDialog));
            lblMessage = new Label();
            lblLevel = new Label();
            lblLocation = new Label();
            cmbLocation = new ComboBox();
            cmbLevel = new ComboBox();
            btnCancel = new Button();
            btnOk = new Button();
            SuspendLayout();
            // 
            // lblMessage
            // 
            resources.ApplyResources(lblMessage, "lblMessage");
            lblMessage.Name = "lblMessage";
            // 
            // lblLevel
            // 
            resources.ApplyResources(lblLevel, "lblLevel");
            lblLevel.Name = "lblLevel";
            // 
            // lblLocation
            // 
            resources.ApplyResources(lblLocation, "lblLocation");
            lblLocation.Name = "lblLocation";
            // 
            // cmbLocation
            // 
            cmbLocation.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLocation.FormattingEnabled = true;
            resources.ApplyResources(cmbLocation, "cmbLocation");
            cmbLocation.Name = "cmbLocation";
            // 
            // cmbLevel
            // 
            cmbLevel.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLevel.FormattingEnabled = true;
            resources.ApplyResources(cmbLevel, "cmbLevel");
            cmbLevel.Name = "cmbLevel";
            cmbLevel.SelectedIndexChanged += cmbLevel_SelectedIndexChanged;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            resources.ApplyResources(btnCancel, "btnCancel");
            btnCancel.Name = "btnCancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            btnOk.DialogResult = DialogResult.OK;
            resources.ApplyResources(btnOk, "btnOk");
            btnOk.Name = "btnOk";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += btnOk_Click;
            // 
            // SelectLevelDialog
            // 
            AcceptButton = btnOk;
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            Controls.Add(btnOk);
            Controls.Add(btnCancel);
            Controls.Add(cmbLevel);
            Controls.Add(cmbLocation);
            Controls.Add(lblLocation);
            Controls.Add(lblLevel);
            Controls.Add(lblMessage);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "SelectLevelDialog";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblMessage;
        private Label lblLevel;
        private Label lblLocation;
        private ComboBox cmbLocation;
        private ComboBox cmbLevel;
        private Button btnCancel;
        private Button btnOk;
    }
}