using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cronus.Models;
using Cronus.Models.DTOs;
using Cronus.Stores;

namespace Cronus.Services
{
    public static class TimeEntryService
    {

        public static TimeEntry CreateNewTimeEntry(TimeEntryDTO dto)
        {
            TimeEntry timeEntry = new()
            {
                AssignedProject = dto.AssignedProject,
                Date = dto.Date,
                Description = dto.Description,
                EndTime = dto.EndTime,
                StartTime = dto.StartTime,
                Title = dto.Title,
            };

            return timeEntry;
        }

        /// <summary>
        /// Creates a new <see cref="TimeEntry"/> instance and adds
        /// it to the current book's
        /// <see cref="Book.TimeEntries"/> collection.
        /// </summary>
        /// <param name="bookStore"></param>
        /// <param name="dto">TimeEntry data transfer object used
        /// to initialize the time entry</param>
        /// <returns>True if the new time entry was added to the
        /// current book's time entries collection, false
        /// otherwise</returns>
        public static (bool, string?) CreateNewTimeEntryInCurrentBook(
            BookStore bookStore, TimeEntryDTO dto)
        {
            ArgumentNullException.ThrowIfNull(bookStore, nameof(bookStore));
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            // Create the new TimeEntry from the DTO.
            TimeEntry timeEntry = CreateNewTimeEntry(dto);

            // Add the time entry to the current book's TimeEntries
            // collection.
            return BookService.AddTimeEntryToCurrentBook(bookStore, timeEntry);
        }
    }
}
