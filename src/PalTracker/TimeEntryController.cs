using System;
using Microsoft.AspNetCore.Mvc;

namespace PalTracker
{
    [Route("/time-entries")]
    public class TimeEntryController : ControllerBase
    {
        private readonly ITimeEntryRepository _timeEntryRepository;

        public TimeEntryController(ITimeEntryRepository timeEntryRepository)
        {
            this._timeEntryRepository = timeEntryRepository;    
        }

        [HttpGet("{id}", Name = "GetTimeEntry")]
        public IActionResult Read(long id)
        {
            if (this._timeEntryRepository.Contains(id))
            {
                return Ok(this._timeEntryRepository.Find(id));                
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Create([FromBody]TimeEntry timeEntry)
        {
            var addedTimeEntry = this._timeEntryRepository.Create(timeEntry);
            return CreatedAtRoute("GetTimeEntry", new {id = addedTimeEntry.Id}, addedTimeEntry);
        }

        [HttpGet]
        public IActionResult List()
        {
            return Ok(this._timeEntryRepository.List());
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody]TimeEntry timeEntry)
        {
            if (this._timeEntryRepository.Contains(id))
            {
                return Ok(this._timeEntryRepository.Update(id, timeEntry));                
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (this._timeEntryRepository.Contains(id))
            {
                this._timeEntryRepository.Delete(id);
                return NoContent();             
            }
            return NotFound();
        }
    }
}