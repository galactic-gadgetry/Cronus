using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Cronus.Models;
using Cronus.Models.DTOs;
using Cronus.Stores;
using Cronus.UIComponents.Dialogs;

namespace Cronus.Services
{
    public static class BookService
    {
        /// <summary>
        /// Adds the project to the current book's
        /// <see cref="Book.Projects"/> collection.
        /// </summary>
        /// <param name="bookStore"></param>
        /// <param name="project"></param>
        /// <returns>True if successful, false otherwise</returns>
        public static (bool, string?) AddProjectToCurrentBook(
            BookStore bookStore, Project project)
        {
            ArgumentNullException.ThrowIfNull(bookStore, nameof(bookStore));
            ArgumentNullException.ThrowIfNull(project, nameof(project));

            // Check if the project already exists in the collection.
            //Book book = bookStore.CurrentBook;
            //if (book.Projects.Any(p => p.Wbs == project.Wbs))
            //{
            //    throw new NotImplementedException();
            //}

            // Validate that the project properties and return false
            // if invalid.
            Book book = bookStore.CurrentBook;
            (bool result, string? detail) = ValidateProjectIsUnique(book, project);
            if  (result == false)
            {
                return (false, detail);
            }

            book.Projects.Add(project);
            return (true, detail);
        }

        /// <summary>
        /// Sets the book store's
        /// <see cref="BookStore.CurrentBook"/> property to null.
        /// </summary>
        /// <param name="bookStore"></param>
        /// <returns></returns>
        public static bool CloseCurrentBook(BookStore bookStore)
        {
            // If the book has unsaved changes, prompt the user
            // to save the book before closing.
            Book book = bookStore.CurrentBook;
            if (!book.IsBookVoid && book.HasUnsavedChanges)
            {
                // The dialog service's PromptUserWithSaveChangeDialog
                // method returns the SaveChanges dialog window. The
                // SaveBook property of the dialog is true if the user
                // wishes to save the book before closing, false if
                // they wish to discard changes.
                SaveChangesDialog dlg =
                    DialogService.PromptUserWithSaveChangesDialog(bookStore);

                // If the dialog result is false, the user has
                // cancelled the operation.
                // If the dialog result is true, the user has requested
                // to save the book before closing.
                if (dlg.DialogResult == false)
                {
                    return false;
                }
                else if (dlg.DialogResult == true)
                {
                    if (dlg.SaveBook)
                    {
                        BookService.SaveBookToJson(book);
                    }
                }
            }
            
            // Set the book store's current book to a void-state book.
            SetBookStoreCurrentBookToVoidState(bookStore);

            return true;
        }

        /// <summary>
        /// Creates a new initialized <see cref="BookHeader"/>
        /// instance.
        /// </summary>
        /// <param name="book">Book instance used to initialize
        /// the book header</param>
        /// <returns></returns>
        public static BookHeader CreateBookHeader(Book book)
        {
            return new BookHeader()
            {
                BookSaveFilePath = book.SaveFilePath,
                CreatedDateTime = book.CreatedDateTime,
                ID = book.ID,
                Name = book.Name,
                SaveFilePath = book.HeaderSaveFilePath,
                Status = book.StatusText,
            };
        }


        public static Book CreateNewBook(BookDTO dto)
        {
            Book book = new()
            {
                Name = dto.Name,
            };

            return book;
        }

        /// <summary>
        /// Creates a new <see cref="Book"/> and sets it as the book
        /// store's current book.
        /// </summary>
        /// <param name="bookStore">Book data transfer object used to
        /// initialize the book</param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static Book CreateNewCurrentBook(BookStore bookStore,
            BookDTO dto)
        {
            ArgumentNullException.ThrowIfNull(bookStore, nameof(bookStore));
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            // Create a new book from the DTO.
            Book book = CreateNewBook(dto);

            // Set the newly created book as the book store's current
            // book.
            SetBookStoreCurrentBook(bookStore, book);

            return book;
        }

        /// <summary>
        /// Deletes the book and book header files.
        /// </summary>
        /// <param name="book"></param>
        public static void DeleteBook(Book book)
        {
            ArgumentNullException.ThrowIfNull(book, nameof(book));

            // Delete the book and book header files.
            FileService.DeleteFile(book.SaveFilePath);
            FileService.DeleteFile(book.HeaderSaveFilePath);
        }

        /// <summary>
        /// Deletes the book header and associated book files and
        /// sets the book store's current book to a void-state
        /// book.
        /// </summary>
        /// <param name="bookStore"></param>
        /// <param name="header"></param>
        public static void DeleteBook(BookStore bookStore, BookHeader header)
        {
            ArgumentNullException.ThrowIfNull(header, nameof(header));

            FileService.DeleteFile(header.BookSaveFilePath);
            FileService.DeleteFile(header.SaveFilePath);
            SetBookStoreCurrentBookToVoidState(bookStore);
        }

        /// <summary>
        /// Deletes the book store's current book file and sets
        /// a void state book as the new current book.
        /// </summary>
        /// <param name="bookStore"></param>
        public static void DeleteCurrentBook(BookStore bookStore)
        {
            ArgumentNullException.ThrowIfNull(bookStore, nameof(bookStore));

            DeleteBook(bookStore.CurrentBook);

            // Set the book store's current book to a void state
            // book.
            SetBookStoreCurrentBookToVoidState(bookStore);
        }

        /// <summary>
        /// Deletes the <see cref="Project"/> isntance from the
        /// book's <see cref="Book.Projects"/> collection.
        /// </summary>
        /// <param name="book"></param>
        /// <param name="project"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public static void DeleteProjectFromBook(Book book,
            Project project)
        {
            ArgumentNullException.ThrowIfNull(book, nameof(book));
            ArgumentNullException.ThrowIfNull(project, nameof(project));

            ObservableCollection<Project> projects = book.Projects;
            if (!projects.Remove(project))
            {
                throw new InvalidOperationException("Removal of the " +
                    "Project instance from the collection failed");
            }
        }

        /// <summary>
        /// Deletes the <see cref="Project"/> instance from the
        /// book store's current book's
        /// <see cref="Book.Projects"/> collection.
        /// </summary>
        /// <param name="bookStore"></param>
        /// <param name="project"></param>
        public static void DeleteProjectFromCurrentBook(
            BookStore bookStore, Project project)
        {
            ArgumentNullException.ThrowIfNull(bookStore, nameof(bookStore));
            ArgumentNullException.ThrowIfNull(project, nameof(project));

            DeleteProjectFromBook(bookStore.CurrentBook, project);
        }

        /// <summary>
        /// Retrieves the saved book headers on file.
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<BookHeader> GetSavedBookHeaders()
        {
            string[] files = FileService.GetSaveFiles();

            IEnumerable<string> headerFiles =
                files.Where<string>(f => f.Contains("_head"));

            ObservableCollection<BookHeader> headers = new();
            foreach (string filePath in headerFiles)
            {
                headers.Add(LoadBookHeaderFromJson(filePath));
            }

            return headers;
        }


        public static void EditBookDetails(BookDTO dto, Book book)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));
            ArgumentNullException.ThrowIfNull(book, nameof(book));

            book.Name = dto.Name;
        }

        /// <summary>
        /// Loads a <see cref="BookHeader"/> instance from a JSON file.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static BookHeader LoadBookHeaderFromJson(string filePath)
        {
            return JsonService.LoadBookHeaderFromJsonFile(filePath);
        }

        /// <summary>
        /// Saves the book and its associated header to file.
        /// </summary>
        /// <param name="book"></param>
        /// <exception cref="InvalidOperationException">Thrown if
        /// the book is void state</exception>
        public static void SaveBookToJson(Book book)
        {
            // Check for null or invalid book state.
            ArgumentNullException.ThrowIfNull(book, nameof(book));
            if (book.IsBookVoid)
            {
                throw new InvalidOperationException("Book cannot be " +
                    "void state");
            }

            // Create a book header for saving.
            BookHeader header = CreateBookHeader(book);

            // Set the Book instance's HasUnsavedChanges property to false
            // to indicate that the book has no unsaved changes.
            book.HasUnsavedChanges = false;

            JsonService.SaveObject(header, header.SaveFilePath);
            JsonService.SaveObject(book, book.SaveFilePath);
        }

        /// <summary>
        /// Loads a <see cref="Book"/> instance from a JSON file and
        /// sets it as the book store's current book.
        /// </summary>
        /// <param name="bookStore"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static BookStore LoadBookToBookStoreFromJson(
            BookStore bookStore, string filePath)
        {
            Book book = JsonService.LoadBookFromJsonFile(filePath);
            SetBookStoreCurrentBook(bookStore, book);

            return bookStore;
        }

        /// <summary>
        /// Saves the book store's current book to JSON file.
        /// </summary>
        /// <param name="bookStore"></param>
        public static void SaveCurrentBookToJson(BookStore bookStore)
        {
            // Check for null book store.
            ArgumentNullException.ThrowIfNull(bookStore, nameof(bookStore));

            SaveBookToJson(bookStore.CurrentBook);
        }

        /// <summary>
        /// Sets the book store's
        /// <see cref="BookStore.CurrentFocusedProject"/> property
        /// to the project.
        /// </summary>
        /// <param name="bookStore"></param>
        /// <param name="project"></param>
        public static void SetBookStoreCurrentFocusedProject(
            BookStore bookStore, Project project)
        {
            ArgumentNullException.ThrowIfNull(bookStore, nameof(bookStore));
            ArgumentNullException.ThrowIfNull(project, nameof(project));

            bookStore.CurrentFocusedProject = project;
        }

        /// <summary>
        /// Validates the project's properties.
        /// </summary>
        /// <param name="book">Book from which the collection will be
        /// compared</param>
        /// <param name="project"></param>
        /// <returns>True if the project is valid, false otherwise</returns>
        public static (bool, string?) ValidateProjectIsUnique(Book book, Project project)
        {
            ArgumentNullException.ThrowIfNull(book, nameof(book));
            ArgumentNullException.ThrowIfNull(project, nameof(project));

            // If any details in any of the book's existing projects
            // conflict with the passed project return false.
            foreach (Project p in book.Projects)
            {
                (bool result, string? detail) = p.ContainsDetailConflict(project);
                if (result == true)
                {
                    return (false, detail);
                }
            }

            return (true, null);
        }


        /// <summary>
        /// Sets the book store's
        /// <see cref="BookStore.CurrentBook"/> property to the
        /// book instance.
        /// </summary>
        /// <param name="bookStore"></param>
        /// <param name="book"></param>
        private static void SetBookStoreCurrentBook(BookStore bookStore,
            Book book)
        {
            ArgumentNullException.ThrowIfNull(book, nameof(book));

            bookStore.CurrentBook = book;
        }


        private static void SetBookStoreCurrentBookToVoidState(BookStore bookStore)
        {
            SetBookStoreCurrentBook(bookStore, new Book() { IsBookVoid = true });
        }
    }
}
