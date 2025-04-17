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

        public static Book CreateNewBook(BookDTO dto)
        {
            Book book = new()
            {
                Name = dto.Name,
            };

            return book;
        }


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



        private static void SetBookStoreCurrentBook(BookStore bookStore,
            Book book)
        {
            ArgumentNullException.ThrowIfNull(book, nameof(book));

            bookStore.CurrentBook = book;
        }
    }
}
