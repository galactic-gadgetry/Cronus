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
    public static class BookService
    {
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
    }
}
