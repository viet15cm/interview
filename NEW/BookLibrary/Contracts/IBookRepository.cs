using Entities.Models;

namespace Contracts
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync(bool trackChanges);

        Task<IEnumerable<Book>> GetBooksByFilterAsync(bool trackChanges, string filter,
            string searchTerm, string maxValue);

        Task<Book> GetBookByIdAsync(string bookId, bool trackChanges);

        void CreateBook(Book book);

        void DeleteBook(Book book);
    }
}
