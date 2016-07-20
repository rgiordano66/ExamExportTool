using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Text;
using DevExpress.XtraGrid.Localization;
using ExamExportTool.Tables;

namespace ExamExportTool
{
    public partial class FormMain : Form
    {
        private readonly Dictionary<string, string> CONNECTIONSTRINGS = new Dictionary<string, string>()
        {
            {"DEV", "Server=devdb01;Database=Common;Trusted_Connection=True;"},
            {"PROD", "Server=10.0.1.3;Database=Common;Trusted_Connection=True;"},
            {"ST1", "Server=10.0.11.90;Database=Common;Trusted_Connection=True;"},
            {"St2", "Server=10.0.11.14;Database=Common;Trusted_Connection=True;"}
        };

        private string folderName;

        private DataTable releasedExams;
        private DataTable examData;
        private DataTable examItemData;
        private DataTable examItemLinkData;
        private DataTable templateData;
        private DataTable templateQuestionData;
        
        private string examName = string.Empty;
        private int version;

        public FormMain()
        {
            InitializeComponent();

            GridLocalizer.Active = new CustomGridLocalizer();

            simpleButtonExams.Enabled = false;
            
            cbSource.Properties.Items.AddRange(CONNECTIONSTRINGS.Keys);

        }
        
        private void simpleButtonExams_Click(object sender, EventArgs e)
        {
            string connString;
            CONNECTIONSTRINGS.TryGetValue(cbSource.EditValue.ToString(), out connString);
            MSQL.ConnectToSQL(connString);
            releasedExams = MSQL.GetDataTable("SELECT * FROM [Common]..[Exam] WHERE [Released] = 1");
            bindingSourceExams.DataSource = releasedExams;
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            int[] rows = gridViewExams.GetSelectedRows();

            foreach (int handle in rows)
            {
                Guid examPK = (Guid)gridViewExams.GetDataRow(handle)["ExamPK"];
                examName = gridViewExams.GetDataRow(handle)["Description"].ToString().Replace(":", "").Replace("&", "-").Replace("\\", "-").Replace("/", "-").Replace("\"", "");
                int.TryParse(gridViewExams.GetDataRow(handle)["Version"].ToString(), out version);

                ExportExam(examPK);
            }

            MessageBox.Show("Exam export(s) complete!", "Success");
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            MSQL.CloseConnection();
            this.Close();
        }

        private void cbSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSource.SelectedIndex != -1)
            {
                simpleButtonExams.Enabled = true;
            }
        }

        private void buttonSelect_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialogLocation.ShowDialog();
            if (result == DialogResult.OK)
            {
                folderName = folderBrowserDialogLocation.SelectedPath;

                textBoxDirectory.Text = folderName;
                buttonExport.Enabled = true;
            }
            else if (textBoxDirectory.Text == string.Empty && folderName == string.Empty)
            {
                buttonExport.Enabled = false;
            }
            else
            {
                buttonExport.Enabled = true;
            }
        }

        private void ExportExam(Guid examPK)
        {
            Cursor.Current = Cursors.WaitCursor;
            
            StringBuilder sbSQLMaster = new StringBuilder();

            Exam exam = new Exam();
            Dictionary<string, string> indivScripts = exam.GetIndividualScripts(examPK);

            foreach (KeyValuePair<string,string> record in indivScripts)
            {
                sbSQLMaster.AppendLine(record.Value);
            }

            ExamItem examItem = new ExamItem();
            indivScripts = examItem.GetIndividualScripts(examPK);

            foreach (KeyValuePair<string, string> record in indivScripts)
            {
                sbSQLMaster.AppendLine(record.Value);
            }

            ExamItemLink examItemLink = new ExamItemLink();
            indivScripts = examItemLink.GetIndividualScripts(examPK);

            foreach (KeyValuePair<string, string> record in indivScripts)
            {
                sbSQLMaster.AppendLine(record.Value);
            }

            File.WriteAllText(folderName + "\\" + examName + " Exam_v" + version + ".sql", sbSQLMaster.ToString());

            Cursor.Current = Cursors.Default;
        }
    }
}
