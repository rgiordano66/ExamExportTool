namespace ExamExportTool.Tables
{
    public class TemplateQuestion : Table
    {
        public TemplateQuestion()
        {
            DisableConstraints = false;
            EnableIdentityInsert = true;

            Database = "Common";
            Schema = "dbo";

            WhereClause = "A WHERE EXISTS(SELECT * FROM[Common].[dbo].[Exam_Item] B JOIN[Common].[dbo].[Exam_ItemLink] C ON C.[ExamItemPK] = B.[ExamItemPK] WHERE B.[DetailQuestionId] = A.[TemplateQuestionID] AND C.[ExamPK] = '{042c7f0b-1f4a-4ffe-b73c-9ba0153b9133}')";

            UniqueIDField = "TemplateQuestionPK";

            Name = "Template_Question";

            InsertString = "INSERT INTO [Common].[dbo].[Template_Question] ([TemplateQuestionID],[TemplateTypeID],[QuestionName],[QuestionXML],[CreatedOn],[CreatedBy],[LastUpdatedOn],[LastUpdatedBy],[TemplateQuestionPK],[PracticePK])" + "VALUES ([@TemplateQuestionID],[@TemplateTypeID],'[@QuestionName]','[@QuestionXML]','[@CreatedOn]','[@CreatedBy]','[@LastUpdatedOn]','[@LastUpdatedBy]',[@TemplateQuestionPK],[@PracticePK])";
        }
    }
}
