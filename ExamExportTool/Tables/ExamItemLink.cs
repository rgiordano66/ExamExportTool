using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ExamExportTool.Tables
{
    public class ExamItemLink : Table
    {
        public ExamItemLink()
        {
            EnableIdentityInsert = false;
            DeleteExists = true;

            Database = "Common";

            Schema = "dbo";

            WhereClause = " WHERE [ExamPk] = ";

            UniqueIDField = "ExamPK";
            UniqueIDField2 = "ExamItemPK";

            Name = "Exam_ItemLink";

            InsertString = "INSERT INTO [Common].[dbo].[Exam_ItemLink] ([ExamPK],[ExamItemPK],[CountsAsDetail],[SortOrder])" + " VALUES ([@ExamPK],[@ExamItemPK],[@CountsAsDetail],[@SortOrder])";
            DeleteString = "DELETE FROM [Common].[dbo].[Exam_ItemLink]";
        }

        public Dictionary<string, string> GetIndividualScripts(Guid crit)
        {
            Dictionary<string, string> scripts = new Dictionary<string, string>();
            DataTable dtData = default(DataTable);
            string strSQL = "SELECT * FROM " + FullName;

            if (UseWhereClause)
            {
                strSQL += (" " + WhereClause + "'" + crit + "'");
            }

            dtData = MSQL.GetDataTable(strSQL);

            StringBuilder sbUpdate = new StringBuilder();

            sbUpdate.AppendLine("DECLARE @examPk UNIQUEIDENTIFIER");
            sbUpdate.AppendLine("DECLARE @examItemPk UNIQUEIDENTIFIER");
            sbUpdate.AppendLine("DECLARE @countsAsDetail BIT");
            sbUpdate.AppendLine("DECLARE @sortOrder INT");

            bool isFirstRow = true;

            foreach (DataRow row in dtData.Rows)
            {
                string ID = row[UniqueIDField].ToString();
                string ID2 = string.Empty;
                int countsAsDetail = (bool)row["CountsAsDetail"] ? 1 : 0;
                int sortOrder;
                int.TryParse(row["SortOrder"].ToString(), out sortOrder);

                if (UseUniqueIDField2)
                {
                    ID2 = row[UniqueIDField2].ToString();
                }

                if (isFirstRow)
                {
                    sbUpdate.AppendLine();
                    sbUpdate.AppendLine(DeleteString + (UseWhereClause ? (WhereClause + "'" + ID + "'"): string.Empty));
                    sbUpdate.AppendLine();
                    isFirstRow = false;
                }

                sbUpdate.AppendLine("SET @countsAsDetail = '" + countsAsDetail + "'");
                sbUpdate.AppendLine("SET @sortOrder = " + sortOrder + "");
                sbUpdate.AppendLine();

                ////   Exists and is correct, no change needed
                //sbUpdate.AppendLine("IF EXISTS(SELECT 1 FROM " + FullName + " WHERE "
                //    + UniqueIDField + " = '" + ID + "' AND " + UniqueIDField2 + " = '" + ID2 + "'"  +
                //    " AND SortOrder = @sortOrder AND CountsAsDetail = @countsAsDetail)");
                //sbUpdate.AppendLine("BEGIN");
                //sbUpdate.AppendLine("\t" + "PRINT('Exam Item Link up to date, no changes needed.')");
                //sbUpdate.AppendLine("END");

                ////   Exists and is not correct, run update
                //sbUpdate.AppendLine("ELSE IF EXISTS(SELECT 1 FROM " + FullName + " WHERE "
                //    + UniqueIDField + " = '" + ID + "' AND " + UniqueIDField2 + " = '" + ID2 + "'" +
                //    " AND (SortOrder <> @sortOrder OR CountsAsDetail <> @countsAsDetail))");
                //sbUpdate.AppendLine("BEGIN");

                //sbUpdate.AppendLine("\t" + MSQL.GetDebugPrint("Exam Item Link for ID ''" + ID + "'' out of date, running update..."));
                //sbUpdate.AppendLine("\t" + "UPDATE " + FullName + " SET SortOrder = @sortOrder, CountsAsDetail = @countsAsDetail WHERE " + UniqueIDField + " = '" + ID + "' AND " + UniqueIDField2 + " = '" + ID2 + "'");
                //sbUpdate.AppendLine("\t" + MSQL.GetDebugPrint("Exam Item Link update for ID ''" + ID + "'' complete."));
                //sbUpdate.AppendLine("END");

                ////   Does not exist, needs to be inserted
                //sbUpdate.AppendLine("ELSE IF NOT EXISTS(SELECT * FROM " + FullName + " WHERE " + UniqueIDField + " = '" + ID + "' AND " + UniqueIDField2 + " = '" + ID2 + "')");
                sbUpdate.AppendLine("IF NOT EXISTS(SELECT * FROM " + FullName + " WHERE " + UniqueIDField + " = '" + ID + "' AND " + UniqueIDField2 + " = '" + ID2 + "')");
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

                scripts.Add(ID2, sbUpdate.ToString());
                sbUpdate.Clear();
            }

            return scripts;
        }
    }
}
