using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<BookDto>> GetAllBooksAsync(bool trackChanges);

        Task<IEnumerable<BookDto>> GetBooksByFilterAsync(bool trackChanges, string filter,
            string searchTerm, string maxValue);

        Task<BookDto> GetBookByIdAsync(string bookId, bool trackChanges);

        Task<BookDto> CreateBookAsync(BookForCreationDto book);

        Task UpdateBookAsync(string bookid, BookForUpdateDto bookForUpdate, bool trackChanges);

        Task DeleteBookAsync(string bookid, bool trackChanges);
    }
}
