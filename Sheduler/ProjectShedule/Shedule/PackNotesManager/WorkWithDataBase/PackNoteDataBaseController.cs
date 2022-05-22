using ProjectShedule.DataBase;
using ProjectShedule.DataBase.Entities;
using ProjectShedule.DataBase.Entities.Base;
using ProjectShedule.Shedule.DataBase;
using ProjectShedule.Shedule.DataBase.Interfaces;
using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.Models;
using ProjectShedule.Shedule.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProjectShedule.Shedule.PackNotesManager.WorkWithDataBase
{
    public class PackNoteDataBaseController : IPackNoteDataBaseController
    {
        private readonly ApplicationContext _packNoteData;

        private readonly IPackNoteParsControl _parsRemove;
        private readonly IBuilderPackNote _builderPackNote;

        private readonly IBuilderSmallTaskViewModel _builderSmallTaskViewModel;
        public PackNoteDataBaseController(ApplicationContext packNoteData)
        {
            _builderSmallTaskViewModel = new BuilderSmallTaskViewModel();
            _builderPackNote = new BuilderPackNoteModel(_builderSmallTaskViewModel);
            _packNoteData = packNoteData;
            _parsRemove = new PackNoteParsRemove(_packNoteData);
        }

        public IPackNoteParsControl PartsControl => _parsRemove;

        public void Save(IPackNote packNote)
        {
            Note note = packNote.Note as Note;
            CorrectionNote correction = new CorrectionNote(note);
            correction.Сorrect();

            _parsRemove.SaveInDataBase(note);
            IEnumerable<IHasModel<BaseSmallTask>> hasSmallTasks = packNote.SmallTasks;
            IEnumerable<SmallTask> smallTasks = GetTaskModels(hasSmallTasks);

            if (packNote.SmallTasks.Count() > 0)
            {
                if (note.Id == 0)
                    note = _parsRemove.GetLastSavedNote() as Note;
                _parsRemove.SaveInDataBase(smallTasks, noteId: note.Id);
            }
        }
        public void Delete(IPackNote packNote)
        {
            _parsRemove.DeleteInDataBase(packNote.Note as Note);
            IEnumerable<IHasModel<BaseSmallTask>> hasSmallTasks = packNote.SmallTasks;
            IEnumerable<SmallTask> smallTasks = GetTaskModels(hasSmallTasks);
            _parsRemove.DeleteInDataBase(smallTasks);
        }
        public IPackNote GetItem(int id)
        {
            BaseNote baseNote = _packNoteData.Note.GetItem(id);
            IPackNote tempPackNote = _builderPackNote
                .SetNote(baseNote)
                .SetSmallTasks(GetTasks(baseNote.Id))
                .Build();
            return tempPackNote;
        }
        public List<IPackNote> GetItems()
        {
            List<IPackNote> tempList = new List<IPackNote>();

            IEnumerable<IPackNote> packNotes = _packNoteData.Note
                .GetItems()
                .Select(N => _builderPackNote
                            .SetNote(N)
                            .SetSmallTasks(GetTasks(N.Id))
                            .Build());

            tempList.AddRange(packNotes);

            return tempList;
        }

        public List<IPackNote> GetForDates(IEnumerable<DateTime> dateTimes)
        {
            List<IPackNote> packNotes = new List<IPackNote>();

            foreach (DateTime dateTime in dateTimes)
                packNotes.AddRange(GetForDate(dateTime));

            return packNotes;
        }
        public List<IPackNote> GetForDate(DateTime dateTime)
        {
            DateTime minDateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
            DateTime maxDateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day).AddDays(1);
            return GetForRangeDate(minDateTime, maxDateTime);
        }
        public List<IPackNote> GetForRangeDate(DateTime first, DateTime second)
        {
            List<IPackNote> resultPackNotes = new List<IPackNote>();

            IEnumerable<IPackNote> packNotes = _packNoteData.Note
                .GetForRangeDate(first, second)
                .Select(N => _builderPackNote
                            .SetNote(N)
                            .SetSmallTasks(GetTasks(N.Id))
                            .Build());

            resultPackNotes.AddRange(packNotes);

            return resultPackNotes;
        }
        private IEnumerable<SmallTask> GetTaskModels(IEnumerable<IHasModel<BaseSmallTask>> typers)
        {
            return typers.Select(s => s.Model as SmallTask);
        }
        private IEnumerable<BaseSmallTask> GetTasks(int noteId)
        {
            return _packNoteData.Tasks.Query(noteId);
        }

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
}
