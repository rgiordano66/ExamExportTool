using DevExpress.XtraGrid.Localization;

namespace ExamExportTool
{
    public class CustomGridLocalizer : GridLocalizer
    {
        public override string GetLocalizedString(GridStringId id)
        {
            if (id == GridStringId.CheckboxSelectorColumnCaption)
            {
                return "Export Exam";
            }
            return base.GetLocalizedString(id);
        }
    }
}
