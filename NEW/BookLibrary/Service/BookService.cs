using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    internal sealed class BookService : IBookService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public BookService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync(bool trackChanges)
        {
            var books = await _repository.Book.GetAllBooksAsync(trackChanges);

            var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);

            return booksDto;
        }

        public async Task<IEnumerable<BookDto>> GetBooksByFilterAsync(bool trackChanges, string filter,
            string searchTerm, string maxValue)
        {
            var books = await _repository.Book.GetBooksByFilterAsync(trackChanges, filter, searchTerm, maxValue);

            var booksDto = _mapper.Map<IEnumerable<BookDto>>(books);

            return booksDto;
        }

        public async Task<BookDto> GetBookByIdAsync(string bookId, bool trackChanges)
        {
            var book = await _repository.Book.GetBookByIdAsync(bookId, trackChanges);

            if (book is null)
            {
                throw new BookNotFoundException(bookId);
            }

            var bookDto = _mapper.Map<BookDto>(book);

            return bookDto;
        }

        public async Task<BookDto> CreateBookAsync(BookForCreationDto book)
        {
            var bookEntity = _mapper.Map<Book>(book);

            var allBooks = await GetAllBooksAsync(trackChanges: false);
            var nextId = GenerateNextBookId(allBooks);

            bookEntity.Id = nextId;

            _repository.Book.CreateBook(bookEntity);
            await _repository.SaveAsync();

            var bookToReturn = _mapper.Map<BookDto>(bookEntity);

            return bookToReturn;
        }

        private string GenerateNextBookId(IEnumerable<BookDto> allBooks)
        {
            int maxBookId = 0;
            foreach (var book in allBooks)
            {
                var bookId = int.TryParse(book.Id.Substring(1), out var id) ? id : 0;
                maxBookId = Math.Max(maxBookId, bookId);
            }

            return "B" + (maxBookId + 1);
        }

        public async Task UpdateBookAsync(string bookId, BookForUpdateDto bookForUpdate, bool trackChanges)
        {
            var bookyEntity = await _repository.Book.GetBookByIdAsync(bookId, trackChanges);

            if (bookyEntity is null)
            {
                throw new BookNotFoundException(bookId);
            }

            _mapper.Map(bookForUpdate, bookyEntity);
            await _repository.SaveAsync();
        }

        public async Task DeleteBookAsync(string bookId, bool trackChanges)
        {
            var book = await _repository.Book.GetBookByIdAsync(bookId, trackChanges);

            if (book is null)
            {
                throw new BookNotFoundException(bookId);
            }

            _repository.Book.DeleteBook(book);
            await _repository.SaveAsync();
        }
    }
}
