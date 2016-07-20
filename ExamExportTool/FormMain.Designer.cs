namespace ExamExportTool
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cbSource = new DevExpress.XtraEditors.ComboBoxEdit();
            this.simpleButtonExams = new DevExpress.XtraEditors.SimpleButton();
            this.buttonSelect = new DevExpress.XtraEditors.SimpleButton();
            this.textBoxDirectory = new System.Windows.Forms.TextBox();
            this.folderBrowserDialogLocation = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.gridControlExams = new DevExpress.XtraGrid.GridControl();
            this.bindingSourceExams = new System.Windows.Forms.BindingSource(this.components);
            this.gridViewExams = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnExamPK = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnVersion = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnEditLocked = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnComments = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnLastUpdatedOn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnLastUpdatedBy = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.cbSource.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlExams)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceExams)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewExams)).BeginInit();
            this.SuspendLayout();
            // 
            // cbSource
            // 
            this.cbSource.EditValue = "";
            this.cbSource.Location = new System.Drawing.Point(12, 12);
            this.cbSource.Name = "cbSource";
            this.cbSource.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbSource.Properties.DropDownRows = 4;
            this.cbSource.Properties.Sorted = true;
            this.cbSource.Size = new System.Drawing.Size(163, 20);
            this.cbSource.TabIndex = 0;
            this.cbSource.SelectedIndexChanged += new System.EventHandler(this.cbSource_SelectedIndexChanged);
            // 
            // simpleButtonExams
            // 
            this.simpleButtonExams.Location = new System.Drawing.Point(181, 10);
            this.simpleButtonExams.Name = "simpleButtonExams";
            this.simpleButtonExams.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonExams.TabIndex = 1;
            this.simpleButtonExams.Text = "Get Exams";
            this.simpleButtonExams.Click += new System.EventHandler(this.simpleButtonExams_Click);
            // 
            // buttonSelect
            // 
            this.buttonSelect.Location = new System.Drawing.Point(12, 41);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(160, 23);
            this.buttonSelect.TabIndex = 2;
            this.buttonSelect.Text = "Select File Directory...";
            this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
            // 
            // textBoxDirectory
            // 
            this.textBoxDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDirectory.Enabled = false;
            this.textBoxDirectory.Location = new System.Drawing.Point(181, 43);
            this.textBoxDirectory.Name = "textBoxDirectory";
            this.textBoxDirectory.ReadOnly = true;
            this.textBoxDirectory.Size = new System.Drawing.Size(819, 20);
            this.textBoxDirectory.TabIndex = 4;
            // 
            // buttonExport
            // 
            this.buttonExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExport.Enabled = false;
            this.buttonExport.Location = new System.Drawing.Point(846, 486);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(75, 23);
            this.buttonExport.TabIndex = 10;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(927, 487);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 9;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // gridControlExams
            // 
            this.gridControlExams.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControlExams.DataSource = this.bindingSourceExams;
            this.gridControlExams.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gridControlExams.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gridControlExams.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gridControlExams.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gridControlExams.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gridControlExams.Location = new System.Drawing.Point(13, 77);
            this.gridControlExams.MainView = this.gridViewExams;
            this.gridControlExams.Name = "gridControlExams";
            this.gridControlExams.Size = new System.Drawing.Size(992, 400);
            this.gridControlExams.TabIndex = 11;
            this.gridControlExams.UseEmbeddedNavigator = true;
            this.gridControlExams.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewExams});
            // 
            // gridViewExams
            // 
            this.gridViewExams.ActiveFilterEnabled = false;
            this.gridViewExams.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnExamPK,
            this.gridColumnDescription,
            this.gridColumnVersion,
            this.gridColumnEditLocked,
            this.gridColumnComments,
            this.gridColumnLastUpdatedOn,
            this.gridColumnLastUpdatedBy});
            this.gridViewExams.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.gridViewExams.GridControl = this.gridControlExams;
            this.gridViewExams.Name = "gridViewExams";
            this.gridViewExams.OptionsBehavior.Editable = false;
            this.gridViewExams.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewExams.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gridViewExams.OptionsSelection.MultiSelect = true;
            this.gridViewExams.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridViewExams.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gridViewExams.OptionsView.ShowGroupPanel = false;
            this.gridViewExams.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gridColumnDescription, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // gridColumnExamPK
            // 
            this.gridColumnExamPK.Caption = "ExamPK";
            this.gridColumnExamPK.FieldName = "ExamPK";
            this.gridColumnExamPK.Name = "gridColumnExamPK";
            this.gridColumnExamPK.Width = 225;
            // 
            // gridColumnDescription
            // 
            this.gridColumnDescription.Caption = "Description";
            this.gridColumnDescription.FieldName = "Description";
            this.gridColumnDescription.Name = "gridColumnDescription";
            this.gridColumnDescription.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
            this.gridColumnDescription.Visible = true;
            this.gridColumnDescription.VisibleIndex = 1;
            this.gridColumnDescription.Width = 268;
            // 
            // gridColumnVersion
            // 
            this.gridColumnVersion.Caption = "Version";
            this.gridColumnVersion.FieldName = "Version";
            this.gridColumnVersion.Name = "gridColumnVersion";
            this.gridColumnVersion.OptionsColumn.FixedWidth = true;
            this.gridColumnVersion.Visible = true;
            this.gridColumnVersion.VisibleIndex = 2;
            this.gridColumnVersion.Width = 64;
            // 
            // gridColumnEditLocked
            // 
            this.gridColumnEditLocked.Caption = "Edit Locked";
            this.gridColumnEditLocked.FieldName = "EditLocked";
            this.gridColumnEditLocked.Name = "gridColumnEditLocked";
            this.gridColumnEditLocked.OptionsColumn.FixedWidth = true;
            this.gridColumnEditLocked.Visible = true;
            this.gridColumnEditLocked.VisibleIndex = 6;
            // 
            // gridColumnComments
            // 
            this.gridColumnComments.Caption = "Comments";
            this.gridColumnComments.FieldName = "Comments";
            this.gridColumnComments.Name = "gridColumnComments";
            this.gridColumnComments.Visible = true;
            this.gridColumnComments.VisibleIndex = 3;
            this.gridColumnComments.Width = 274;
            // 
            // gridColumnLastUpdatedOn
            // 
            this.gridColumnLastUpdatedOn.Caption = "Last Updated On";
            this.gridColumnLastUpdatedOn.FieldName = "LastUpdatedOn";
            this.gridColumnLastUpdatedOn.Name = "gridColumnLastUpdatedOn";
            this.gridColumnLastUpdatedOn.Visible = true;
            this.gridColumnLastUpdatedOn.VisibleIndex = 4;
            this.gridColumnLastUpdatedOn.Width = 99;
            // 
            // gridColumnLastUpdatedBy
            // 
            this.gridColumnLastUpdatedBy.Caption = "Last Updated By";
            this.gridColumnLastUpdatedBy.FieldName = "LastUpdatedBy";
            this.gridColumnLastUpdatedBy.Name = "gridColumnLastUpdatedBy";
            this.gridColumnLastUpdatedBy.Visible = true;
            this.gridColumnLastUpdatedBy.VisibleIndex = 5;
            this.gridColumnLastUpdatedBy.Width = 94;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 518);
            this.Controls.Add(this.gridControlExams);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.textBoxDirectory);
            this.Controls.Add(this.buttonSelect);
            this.Controls.Add(this.simpleButtonExams);
            this.Controls.Add(this.cbSource);
            this.Name = "FormMain";
            this.Text = "Exam Export Utility";
            ((System.ComponentModel.ISupportInitialize)(this.cbSource.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlExams)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceExams)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewExams)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.ComboBoxEdit cbSource;
        private DevExpress.XtraEditors.SimpleButton simpleButtonExams;
        private DevExpress.XtraEditors.SimpleButton buttonSelect;
        private System.Windows.Forms.TextBox textBoxDirectory;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogLocation;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonClose;
        private DevExpress.XtraGrid.GridControl gridControlExams;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewExams;
        private System.Windows.Forms.BindingSource bindingSourceExams;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnExamPK;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDescription;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnVersion;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnEditLocked;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnComments;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnLastUpdatedOn;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnLastUpdatedBy;
    }
}

