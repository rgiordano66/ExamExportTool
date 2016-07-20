using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ExamExportTool.Tables
{
    public class ExamItem : Table
    {
        public ExamItem()
        {
            EnableIdentityInsert = false;
            
            Database = "Common";

            Schema = "dbo";

            WhereClause = "B WHERE EXISTS (SELECT 'x' FROM [Common].[dbo].[Exam_ItemLink] A WHERE A.[ExamItemPK] = B.[ExamItemPK] AND A.[ExamPK] = ";
            
            UniqueIDField = "ExamItemPK";

            Name = "Exam_Item";

            InsertString = "INSERT INTO [Common].[dbo].[Exam_Item] ([ExamItemPK],[PracticePK],[HasRightLeft],[Description],[BodySystem],[AbnormalTranscription],[NormalTranscription],[DetailQuestionId],[DetailQuestionDescription],[Comments],[CreatedOn],[CreatedBy],[LastUpdatedOn],[LastUpdatedBy])" + " VALUES ([@ExamItemPK],[@PracticePK],[@HasRightLeft],'[@Description]','[@BodySystem]','[@AbnormalTranscription]','[@NormalTranscription]',[@DetailQuestionId],'[@DetailQuestionDescription]','Inserted via content update script','[@CreatedOn]','[@CreatedBy]','[@LastUpdatedOn]','[@LastUpdatedBy]')";
        }

        public Dictionary<string, string> GetIndividualScripts(Guid crit)
        {
            Dictionary<string, string> scripts = new Dictionary<string, string>();
            
            string strSQL = "SELECT * FROM " + FullName;
            
            if (UseWhereClause)
            {
                strSQL += (" " + WhereClause + "'" + crit + "')");
            }

            DataTable dtData = MSQL.GetDataTable(strSQL);

            StringBuilder sbUpdate = new StringBuilder();

            sbUpdate.AppendLine("DECLARE @description VARCHAR(MAX)");
            sbUpdate.AppendLine("DECLARE @hasRightLeft BIT");
            sbUpdate.AppendLine("DECLARE @bodysystem VARCHAR(MAX)");
            sbUpdate.AppendLine("DECLARE @abDesc VARCHAR(MAX)");
            sbUpdate.AppendLine("DECLARE @normDesc VARCHAR(MAX)");
            sbUpdate.AppendLine("DECLARE @questionId INT");
            sbUpdate.AppendLine("DECLARE @comments VARCHAR(MAX)");
            sbUpdate.AppendLine();

            foreach (DataRow row in dtData.Rows)
            {
                string ID = row[UniqueIDField].ToString();
                string description = row["Description"].ToString().Replace("'","''");
                int hasRightLeft = (bool)row["HasRightLeft"] == true ? 1 : 0;
                string bodysystem = row["Bodysystem"].ToString();
                string abTran = row["AbnormalTranscription"].ToString().Replace("'", "''");
                string normTran = row["Normaltranscription"].ToString().Replace("'", "''");
                int questionId = 0;
                int.TryParse(row["DetailQuestionId"].ToString(), out questionId);
                string comments = row["Comments"].ToString().Replace("'", "''");

                sbUpdate.AppendLine("SET @description = '" + description + "'");
                sbUpdate.AppendLine("SET @hasRightLeft = " + hasRightLeft + "");
                sbUpdate.AppendLine("SET @bodysystem = '" + bodysystem + "'");
                sbUpdate.AppendLine("SET @abDesc = '" + abTran + "'");
                sbUpdate.AppendLine("SET @normDesc = '" + normTran + "'");
                sbUpdate.AppendLine("SET @questionId = " + questionId + "");
                sbUpdate.AppendLine("SET @comments = '" + comments + "'");
                sbUpdate.AppendLine();
                
                //   Exists and is correct, no change needed
                sbUpdate.AppendLine("IF EXISTS(SELECT 1 FROM " + FullName + " WHERE " + UniqueIDField + " = '" + ID +
                    "' AND Description = @description AND HasRightLeft = @hasRightLeft AND BodySystem = @bodySystem AND AbnormalTranscription = @abDesc AND NormalTranscription = @normDesc AND DetailQuestionId = @questionId AND Comments = @comments)");
                sbUpdate.AppendLine("BEGIN");
                sbUpdate.AppendLine("\t" + "PRINT('Exam Item up to date, no changes needed.')");
                sbUpdate.AppendLine("END");

                //   Exists and is not correct, run update
                sbUpdate.AppendLine("ELSE IF EXISTS(SELECT 1 FROM " + FullName + " WHERE " + UniqueIDField + " = '" + ID +
                    "' AND (Description <> @description OR HasRightLeft <> @hasRightLeft OR BodySystem <> @bodySystem OR AbnormalTranscription <> @abDesc OR NormalTranscription <> @normDesc OR DetailQuestionId <> @questionId OR Comments <> @comments))");
                sbUpdate.AppendLine("BEGIN");
                
                sbUpdate.AppendLine("\t" + MSQL.GetDebugPrint("Exam Item for ID ''" + ID + "'' out of date, running update..."));
                sbUpdate.AppendLine("\t" + "UPDATE " + FullName + " SET Description = @description, HasRightLeft = @hasRightLeft, BodySystem = @bodySystem, AbnormalTranscription = @abDesc, NormalTranscription = @normDesc, DetailQuestionId = @questionId, Comments = @comments WHERE " + UniqueIDField + " = '" + ID + "'");
                sbUpdate.AppendLine("\t" + MSQL.GetDebugPrint("Exam Item update for ID ''" + ID + "'' complete."));
                sbUpdate.AppendLine("END");

                //   Does not exist, needs to be inserted
                sbUpdate.AppendLine("ELSE IF NOT EXISTS(SELECT * FROM " + FullName + " WHERE " + UniqueIDField + " = '" + ID + "')");
                sbUpdate.AppendLine("BEGIN");
                

                sbUpdate.AppendLine("\t" + MSQL.GetDebugPrint("Inserting new content into " + FullName));
                sbUpdate.AppendLine();

                if (EnableIdentityInsert)
                {
                    sbUpdate.AppendLine("\t" + MSQL.GetDebugPrint("Enabling identity insert."));
                    sbUpdate.AppendLine("\t" + GetEnableIdentityInsert());
                    sbUpdate.AppendLine();
                }
                
                sbUpdate.AppendLine("\t" + GetInsertFromData(row));
                sbUpdate.AppendLine();
                
                if (EnableIdentityInsert)
                {
                    sbUpdate.AppendLine("\t" + MSQL.GetDebugPrint("Disabling identity insert."));
                    sbUpdate.AppendLine("\t" + GetDisableIdentityInsert());
                    sbUpdate.AppendLine();
                }

                sbUpdate.AppendLine("\t" + MSQL.GetDebugPrint("Insert complete."));
                sbUpdate.AppendLine("END");

                scripts.Add(ID, sbUpdate.ToString());
                sbUpdate.Clear();
            }

            return scripts;
        }
    }
}
