using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ExamExportTool
{
    public class Table
    {

        public string Name { get; set; }
        public string Schema { get; set; }
        public string WhereClause { get; set; }
        public bool DisableConstraints { get; set; }
        public List<string> ConstraintTables { get; set; }
        public bool EnableIdentityInsert { get; set; }
        public string Database { get; set; }
        public string UniqueIDField { get; set; }
        public string UniqueIDField2 { get; set; }
        public string DescriptionField { get; set; }
        public bool DeleteExists { get; set; }
        public bool UsesContentFieldAlias
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ContentFieldAlias))
                {
                    return false;
                }
                else {
                    return true;
                }
            }
        }

        public bool UseUniqueIDField2
        {
            get
            {
                if (string.IsNullOrWhiteSpace(UniqueIDField2))
                {
                    return false;
                }
                else {
                    return true;
                }
            }
        }

        public bool UseContentField2
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ContentField2))
                {
                    return false;
                }
                else {
                    return true;
                }
            }
        }

        public bool UseWhereClause
        {
            get
            {
                if (string.IsNullOrWhiteSpace(WhereClause))
                {
                    return false;
                }
                else {
                    return true;
                }
            }
        }

        public string ContentField { get; set; }
        public string ContentFieldAlias { get; set; }
        public string ContentField2 { get; set; }
        public string ContentExtension { get; set; }
        public bool OutputContent { get; set; }
        public string InsertString { get; set; }
        public string DeleteString { get; set; }
        public string FullName => "[" + Database + "].[" + Schema + "].[" + Name + "]";

        //public string GetEnableConstraintsString()
        //{
        //    if (DisableConstraints)
        //    {
        //        StringBuilder sb = new StringBuilder();

        //        sb.AppendLine("ALTER TABLE " + FullName + " WITH CHECK CHECK CONSTRAINT ALL");


        //        foreach (string ConstraintTable in ConstraintTables)
        //        {
        //            sb.AppendLine("\t" + "ALTER TABLE " + ConstraintTable + " WITH CHECK CHECK CONSTRAINT ALL");

        //        }

        //        return sb.ToString();


        //    }
        //    else {
        //        return string.Empty;

        //    }

        //}

        //public string GetDisableConstraintsString()
        //{


        //    if (DisableConstraints)
        //    {
        //        StringBuilder sb = new StringBuilder();

        //        sb.AppendLine("ALTER TABLE " + FullName + " NOCHECK CONSTRAINT ALL");


        //        foreach (string ConstraintTable in ConstraintTables)
        //        {
        //            sb.AppendLine("\t" + "ALTER TABLE " + ConstraintTable + " NOCHECK CONSTRAINT ALL");

        //        }

        //        return sb.ToString();


        //    }
        //    else {
        //        return string.Empty;

        //    }

        //}

        public string GetEnableIdentityInsert()
        {
            if (EnableIdentityInsert)
            {
                return "SET IDENTITY_INSERT " + FullName + " ON";
            }
            else
            {
                return string.Empty;
            }
        }
        public string GetDisableIdentityInsert()
        {
            if (EnableIdentityInsert)
            {
                return "SET IDENTITY_INSERT " + FullName + " OFF";
            }
            else {
                return string.Empty;
            }
        }

        public string GetInsertData()
        {
            DataTable dtInserts = new DataTable();
            StringBuilder sbInserts = new StringBuilder();

            dtInserts = MSQL.ExecuteSQLScript("SQL\\GetInserts.sql", Name);


            foreach (DataRow row in dtInserts.Rows)
            {
                sbInserts.AppendLine("\t" + row.ItemArray[0].ToString());

            }

            return sbInserts.ToString();
        }

        public string GetDeleteString()
        {
            return "DELETE FROM " + FullName;
        }
        
        public Dictionary<string, string> GetIndividualScripts(Guid crit)
        {
            Dictionary<string, string> scripts = new Dictionary<string, string>();
            DataTable dtData = default(DataTable);
            string strSQL = "SELECT " + UniqueIDField + ", " + ContentField + (UsesContentFieldAlias ? " AS " + ContentFieldAlias : string.Empty) + ", * FROM " + FullName;
            string strContentField = (UsesContentFieldAlias ? ContentFieldAlias : ContentField);
            string strExistsContentField = ContentField;

            if (UseWhereClause)
            {
                strSQL += (" " + WhereClause + "'" + crit + "'");
            }

            dtData = MSQL.GetDataTable(strSQL);

            foreach (DataRow row in dtData.Rows)
            {
                StringBuilder sbUpdate = new StringBuilder();
                string ID = row[UniqueIDField].ToString();
                string Description = "";
                string Content = row[strContentField].ToString();
                string Content2 = (UseContentField2 ? row[strContentField].ToString() : string.Empty);

                if (!string.IsNullOrEmpty(DescriptionField))
                {
                    Description = row[DescriptionField].ToString().Replace(":", "").Replace("&", "-").Replace("\\", "-").Replace("/", "-").Replace("\"", "");
                }
                sbUpdate.AppendLine("/*");
                sbUpdate.AppendLine("\t" + "Automatically generated exam export script");
                sbUpdate.AppendLine("\t" + "Table: " + Name);
                sbUpdate.AppendLine("\t" + "ContentField: " + strContentField);
                if (UseContentField2)
                {
                    sbUpdate.AppendLine("\t" + "ContentField2: " + ContentField2);
                }
                sbUpdate.AppendLine("\t" + "ID: " + UniqueIDField);
                sbUpdate.AppendLine("*/");

                sbUpdate.AppendLine();

                sbUpdate.AppendLine();
                sbUpdate.AppendLine("DECLARE @Content VARCHAR(MAX) = '" + Content.Replace("'", "''") + "'");
                sbUpdate.AppendLine();

                if (UseContentField2)
                {
                    sbUpdate.AppendLine();
                    sbUpdate.AppendLine("DECLARE @Content2 VARCHAR(MAX) = '" + Content2.Replace("'", "''") + "'");
                    sbUpdate.AppendLine();
                }

                //   Exists and is correct, no change needed
                sbUpdate.AppendLine("IF EXISTS(SELECT * FROM " + FullName + " WHERE " + UniqueIDField + " = '" + ID + "' AND " + strExistsContentField + " = @Content" + (UseContentField2 ? " AND " + ContentField2 + " = @Content2" : string.Empty) + ")");
                sbUpdate.AppendLine("BEGIN");
                sbUpdate.AppendLine("\t" + "PRINT('Content up to date, no changes needed.')");
                sbUpdate.AppendLine("END");

                //   Exists and is not correct, run update
                sbUpdate.AppendLine("ELSE IF EXISTS(SELECT * FROM " + FullName + " WHERE " + UniqueIDField + " = '" + ID + "' AND " + strExistsContentField + " <> @Content)");
                sbUpdate.AppendLine("BEGIN");
                if (DeleteExists)
                {
                    sbUpdate.AppendLine("\t" + MSQL.GetDebugPrint("Deleteing record from " + FullName));
                    sbUpdate.AppendLine("\t" + "DELETE FROM " + FullName + " WHERE " + UniqueIDField + " = '" + ID + "')");
                }
                else {
                    sbUpdate.AppendLine("\t" + MSQL.GetDebugPrint("Content for ID ''" + ID + "'' out of date, running update..."));
                    sbUpdate.AppendLine("\t" + "UPDATE " + FullName + " SET " + strContentField + " = @Content WHERE " + UniqueIDField + " = '" + ID + "'");
                    sbUpdate.AppendLine("\t" + MSQL.GetDebugPrint("Content update for ID ''" + ID + "'' complete."));
                    sbUpdate.AppendLine("END");

                    //   Does not exist, needs to be inserted
                    sbUpdate.AppendLine("ELSE IF NOT EXISTS(SELECT * FROM " + FullName + " WHERE " + UniqueIDField + " = '" + ID + "')");
                    sbUpdate.AppendLine("BEGIN");
                }

                sbUpdate.AppendLine("\t" + MSQL.GetDebugPrint("Inserting new content into " + FullName));

                if (EnableIdentityInsert)
                {
                    sbUpdate.AppendLine("\t" + MSQL.GetDebugPrint("Enabling identity insert."));
                    sbUpdate.AppendLine("\t" + GetEnableIdentityInsert());
                    sbUpdate.AppendLine();
                }
                
                sbUpdate.AppendLine("\t" + GetInsertFromData(row));
                
                if (EnableIdentityInsert)
                {
                    sbUpdate.AppendLine("\t" + MSQL.GetDebugPrint("Disabling identity insert."));
                    sbUpdate.AppendLine("\t" + GetDisableIdentityInsert());
                    sbUpdate.AppendLine();
                }

                sbUpdate.AppendLine("\t" + MSQL.GetDebugPrint("Insert complete."));
                sbUpdate.AppendLine("END");

                if (!string.IsNullOrEmpty(Description))
                {
                    scripts.Add(Description + "-" + ID, sbUpdate.ToString());
                }
                else
                {
                    scripts.Add(ID, sbUpdate.ToString());
                }
            }

            return scripts;
        }

        protected string GetInsertFromData(DataRow Data)
        {
            string strInsert = InsertString;

            foreach (DataColumn col in Data.Table.Columns)
            {
                //TypeCode colType = Type.GetTypeCode(col.DataType);
                
                switch (col.DataType.Name.ToUpper())
                {
                    case "GUID":
                        strInsert = strInsert.Replace("[@" + col.ColumnName + "]", Data[col.ColumnName] == DBNull.Value ? "NULL" : "'" + Data[col.ColumnName].ToString().Replace("'", "''") + "'");
                        break;
                    case "BOOLEAN":
                        strInsert = strInsert.Replace("[@" + col.ColumnName + "]", ((bool)Data[col.ColumnName] ? "1" : "0"));
                        break;
                    case "STRING":
                        strInsert = strInsert.Replace("[@" + col.ColumnName + "]", Data[col.ColumnName] == DBNull.Value ? "NULL" : Data[col.ColumnName].ToString().Replace("'", "''"));
                        break;
                    case "DATETIME":
                        strInsert = strInsert.Replace("[@" + col.ColumnName + "]", Data[col.ColumnName] == DBNull.Value ? "NULL" : Data[col.ColumnName].ToString());
                        break;
                    case "BYTE[]":
                    //case TypeCode.SByte:
                    case "INT16":
                    //case TypeCode.UInt16:
                    case "INT32":
                    //case TypeCode.UInt32:
                    //case TypeCode.Int64:
                    //case TypeCode.UInt64:
                    //case TypeCode.Single:
                    //case TypeCode.Double:
                    //case TypeCode.Decimal:
                    default:
                        strInsert = strInsert.Replace("[@" + col.ColumnName + "]", Data[col.ColumnName] == DBNull.Value ? "NULL" : Data[col.ColumnName].ToString());
                        break;
                }
            }

            return strInsert;

        }
    }
}
