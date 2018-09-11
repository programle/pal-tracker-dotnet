using System.Collections.Generic;
using System.Linq;

namespace PalTracker
{
    public class InMemoryTimeEntryRepository : ITimeEntryRepository
    {
        private readonly Dictionary<long, TimeEntry> _timeEntryDictionary = new Dictionary<long, TimeEntry>();

        public bool Contains(long id) => _timeEntryDictionary.ContainsKey(id);

        public TimeEntry Create(TimeEntry timeEntry)
        {
            var id = _timeEntryDictionary.Count + 1;
            timeEntry.Id = id;
            _timeEntryDictionary.Add(id, timeEntry);
            return timeEntry;
        }

        public void Delete(long id) => _timeEntryDictionary.Remove(id);

        public TimeEntry Find(long id) => _timeEntryDictionary[id];

        public IEnumerable<TimeEntry> List() => _timeEntryDictionary.Values.ToList();

        public TimeEntry Update(long id, TimeEntry timeEntry)
        {
            timeEntry.Id = id;
            _timeEntryDictionary[id] = timeEntry;
            return timeEntry;
        }
    }
}