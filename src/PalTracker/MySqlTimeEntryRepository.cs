﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PalTracker
{
    public class MySqlTimeEntryRepository : ITimeEntryRepository
    {
        private readonly TimeEntryContext _timeEntryContext;

        public MySqlTimeEntryRepository(TimeEntryContext timeEntryContext)
        {
            this._timeEntryContext = timeEntryContext;    
        }
        public bool Contains(long id) => _timeEntryContext.TimeEntryRecords.AsNoTracking().Any(s => s.Id == id);

        public TimeEntry Create(TimeEntry timeEntry)
        {
            var recToCreate = timeEntry.ToRecord();
            _timeEntryContext.TimeEntryRecords.Add(recToCreate);
            _timeEntryContext.SaveChanges();

            return this.Find(recToCreate.Id.Value);
        }

        public void Delete(long id)
        {
            _timeEntryContext.TimeEntryRecords.Remove(FindRecord(id));
            _timeEntryContext.SaveChanges();
        }

        public TimeEntry Find(long id) => FindRecord(id).ToEntity();

        public IEnumerable<TimeEntry> List() => _timeEntryContext.TimeEntryRecords.AsNoTracking().Select(s => s.ToEntity());

        public TimeEntry Update(long id, TimeEntry timeEntry)
        {
            var recToUpd = timeEntry.ToRecord();
            recToUpd.Id = id;

            _timeEntryContext.Update(recToUpd);
            _timeEntryContext.SaveChanges();

            return Find(id);
        }

        private TimeEntryRecord FindRecord(long id) => _timeEntryContext.TimeEntryRecords.AsNoTracking().Single(s => s.Id == id);
    }
}