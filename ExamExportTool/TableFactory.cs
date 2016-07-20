using ExamExportTool.Tables;

namespace ExamExportTool
{
    public class TableFactory
    {
        public Table GetTableObj(string tableName)
        {
            switch (tableName)
            {

                case "Template":

                    return new Template();
                case "Template_Question":

                    return new TemplateQuestion();
                case "Exam":

                    return new Exam();
                case "Exam_Item":

                    return new ExamItem();
                case "Exam_ItemLink":

                    return new ExamItemLink();
                default:

                    throw new TableNotImplementedException();
            }
        }
    }
}
