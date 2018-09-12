using System;
using Microsoft.AspNetCore.Mvc;

namespace PalTracker
{
    [Route("/time-entries")]
    public class TimeEntryController : ControllerBase
    {
        private readonly ITimeEntryRepository _timeEntryRepository;
        private readonly IOperationCounter<TimeEntry, TrackedOperation> _operationCounter;

        public TimeEntryController(ITimeEntryRepository timeEntryRepository, IOperationCounter<TimeEntry, TrackedOperation> operationCounter)
        {
            this._timeEntryRepository = timeEntryRepository;    
            this._operationCounter = operationCounter;
        }

        [HttpGet("{id}", Name = "GetTimeEntry")]
        public IActionResult Read(long id)
        {
            this._operationCounter.Increment(TrackedOperation.Read);
            if (this._timeEntryRepository.Contains(id))
            {
                return Ok(this._timeEntryRepository.Find(id));                
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Create([FromBody]TimeEntry timeEntry)
        {
            this._operationCounter.Increment(TrackedOperation.Create);
            var addedTimeEntry = this._timeEntryRepository.Create(timeEntry);
            return CreatedAtRoute("GetTimeEntry", new {id = addedTimeEntry.Id}, addedTimeEntry);
        }

        [HttpGet]
        public IActionResult List()
        {
            this._operationCounter.Increment(TrackedOperation.List);
            return Ok(this._timeEntryRepository.List());
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody]TimeEntry timeEntry)
        {
            this._operationCounter.Increment(TrackedOperation.Update);
            if (this._timeEntryRepository.Contains(id))
            {
                return Ok(this._timeEntryRepository.Update(id, timeEntry));                
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            this._operationCounter.Increment(TrackedOperation.Delete);
            if (this._timeEntryRepository.Contains(id))
            {
                this._timeEntryRepository.Delete(id);
                return NoContent();             
            }
            return NotFound();
        }
    }
}