using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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


        public static (bool, string?) EditTimeEntryDetails(BookStore bookStore,
            TimeEntry timeEntry, TimeEntryDTO dto)
        {
            ArgumentNullException.ThrowIfNull(bookStore, nameof(bookStore));
            ArgumentNullException.ThrowIfNull(timeEntry, nameof(timeEntry));
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            // Validate that the DTO's details will be valid within
            // the current book's TimeEntries collection as the
            // time entry's details will be changed to that fo the
            // DTO's.
            // A copy of the book's entries for the date is created
            // and the time entry that is to be edited is removed so
            // that the DTO's details aren't checked against the
            // time entry that is wanting to be changed.
            Book book = bookStore.CurrentBook;
            List<TimeEntry> timeEntriesCopy =
                new List<TimeEntry>(book.TimeEntries.Where(t => t.Date == timeEntry.Date));
            timeEntriesCopy.Remove(timeEntry);
            (bool result, string? detail) =
                ValidateTimeEntryInCollection(timeEntriesCopy, dto);

            // If the ValidateTimeEntry method returns false, the
            // time entry details are not valid, therefore return
            // false.
            if (!result)
            {
                return (false, detail);
            }

            // Update time entry details.
            timeEntry.AssignedProject = dto.AssignedProject;
            timeEntry.Date = dto.Date;
            timeEntry.Description = dto.Description;
            timeEntry.EndTime = dto.EndTime;
            timeEntry.StartTime = dto.StartTime;
            timeEntry.Title = dto.Title;

            // Set the book's HasUnsavedChanges property to
            // indicate that the book has changed.
            BookService.SetCurrentBookHasUnsavedChanges(bookStore, true);

            return (true, detail);
        }



        public static (bool, string?) ValidateTimeEntryInCollection(
            IEnumerable<TimeEntry> collection, TimeEntryDTO dto)
        {
            ArgumentNullException.ThrowIfNull(collection, nameof(collection));
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            TimeEntry timeEntry = CreateNewTimeEntry(dto);

            return ValidateTimeEntryInCollection(collection, timeEntry);
        }


        public static (bool, string?) ValidateTimeEntryInCollection(
            IEnumerable<TimeEntry> collection, TimeEntry timeEntry)
        {
            ArgumentNullException.ThrowIfNull(collection, nameof(collection));
            ArgumentNullException.ThrowIfNull(timeEntry, nameof(timeEntry));

            // If the time entry's timeframe overlaps an existing
            // time entry's timeframe, return false.
            foreach (TimeEntry t in collection)
            {
                (bool result, string? title) = t.ContainsTimeframeConflict(timeEntry);
                if (result == true)
                {
                    return (false, title);
                }
            }

            return (true, null);
        }
    }
}
