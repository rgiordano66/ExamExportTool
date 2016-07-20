namespace ExamExportTool.Tables
{
    public class Template : Table
    {
        public Template()
        {
            OutputContent = false;

            ConstraintTables.Add("[Common].[dbo].[Utility_Complaints]");
            ConstraintTables.Add("[Common].[dbo].[TemplatePackage_TempateLink]");

            DisableConstraints = true;
            EnableIdentityInsert = true;

            Database = "Common";
            Schema = "dbo";

            ContentField = "TemplateXML";
            UniqueIDField = "TemplateID";

            Name = "Template";

            DescriptionField = "Description";

            InsertString = "INSERT INTO [Common].[dbo].[Template] ([TemplateID],[TemplateTypeID],[VersionNumber],[Description],[ReleaseStatus],[TemplateXML],[TemplateAttribute],[CreatedOn],[CreatedBy],[LastUpdatedOn],[LastUpdatedBy],[TemplatePK],[PracticePK],[TemplateTypePK],[IsExtraTemplateRequired])" + "VALUES ([@TemplateID],[@TemplateTypeID],[@VersionNumber],'[@Description]',[@ReleaseStatus],'[@TemplateXML]','[@TemplateAttribute]','[@CreatedOn]','[@CreatedBy]','[@LastUpdatedOn]','[@LastUpdatedBy]','[@TemplatePK]','[@PracticePK]','[@TemplateTypePK]',[@IsExtraTemplateRequired])";

        }
    }
}
