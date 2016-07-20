namespace ExamExportTool.Tables
{
    public class Exam : Table
    {
        public Exam()
        {
            OutputContent = false;

            DisableConstraints = false;
            EnableIdentityInsert = false;

            Database = "Common";

            Schema = "dbo";

            WhereClause = " WHERE [ExamPk] = ";

            ContentExtension = "txt";

            ContentField = "Released";

            UniqueIDField = "ExamPK";

            Name = "Exam";

            DescriptionField = "Description";

            InsertString = "INSERT INTO [Common].[dbo].[Exam] ([ExamPK],[Description],[Version],[DefaultImagePK],[Released],[EditLocked],[NumItemsForDetail],[Comments]) VALUES ([@ExamPK],'[@Description]',[@Version],[@DefaultImagePK],[@Released],[@EditLocked],[@NumItemsForDetail],'Inserted via content update script')";

        }
    }
}
