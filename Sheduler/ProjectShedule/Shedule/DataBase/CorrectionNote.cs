using ProjectShedule.DataBase.Entities.Base;
using System.Text.RegularExpressions;

namespace ProjectShedule.Shedule.PackNotesManager.WorkWithDataBase
{
    internal class CorrectionNote
    {
        private readonly BaseNote _note;
        public CorrectionNote(BaseNote note)
        {
            _note = note;
        }
        public void Сorrect(bool replaceEmptyHeader = true, bool reduceGaps = true)
        {
            string header = _note.Header;
            string dopText = _note.DopText;

            if (reduceGaps)
            {
                if (!string.IsNullOrWhiteSpace(dopText))
                    dopText = ReduceGaps(dopText);
                if (!string.IsNullOrWhiteSpace(header))
                    header = ReduceGaps(header);
            }
            if (replaceEmptyHeader && string.IsNullOrWhiteSpace(header))
            {
                header = AssignPartText(dopText, length: 22);
            }

            _note.Header = header;
            _note.DopText = dopText;
        }
        private string AssignPartText(string text, int length = 15)
        {
            return text.Substring(0, text.Length >= length ? length : text.Length);
        }
        private string ReduceGaps(string text)
        {
            return new Regex(@"\s+").Replace(text, " ");
        }
    }
}
