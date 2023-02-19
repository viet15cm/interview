using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Repository
{
    internal sealed class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<Book>> GetAllBooksAsync(bool trackChanges) =>
            await FindAll(trackChanges).ToListAsync();

        public async Task<IEnumerable<Book>> GetBooksByFilterAsync(bool trackChanges, string filter,
            string searchTerm, string maxValue)
        {
            var books = await FindAll(trackChanges).ToListAsync();

            if (filter == "ids")
            {
                if (searchTerm is null)
                {
                    return books.OrderBy(x => Convert.ToInt32(x.Id.Remove(0, 1)));
                }

                return books.Where(c => c.Id.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(c => Convert.ToInt32(c.Id.Remove(0, 1)));
            }
            else if (filter == "author")
            {
                if (searchTerm is null)
                {
                    return books.OrderBy(c => c.Author);
                }

                return books.Where(c => c.Author.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(c => c.Author);
            }
            else if (filter == "title")
            {
                if (searchTerm is null)
                {
                    return books.OrderBy(c => c.Title);
                }

                return books.Where(c => c.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .OrderBy(c => c.Title);
            }
            else if (filter == "genre")
            {
                if (searchTerm is null)
                {
                    return books.OrderBy(c => c.Genre);
                }

                return books.Where(c => c.Genre.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .OrderBy(c => c.Genre);
            }
            else if (filter == "price")
            {
                if (searchTerm is null)
                {
                    return books.OrderBy(c => double.Parse(c.Price, CultureInfo.InvariantCulture));
                }

                var prices = searchTerm.Split('&');

                if (prices.Length == 1)
                {
                    return books.Where(c => c.Price.Contains(searchTerm))
                        .OrderBy(c => double.Parse(c.Price, CultureInfo.InvariantCulture));
                }
                else if (prices.Length == 2)
                {
                    var min = double.Parse(prices[0], CultureInfo.InvariantCulture);
                    var max = double.Parse(prices[1], CultureInfo.InvariantCulture);
                    return books.Where(x => double.Parse(x.Price, CultureInfo.InvariantCulture) >= min &&
                                        double.Parse(x.Price, CultureInfo.InvariantCulture) <= max)
                                .OrderBy(x => double.Parse(x.Price, CultureInfo.InvariantCulture));
                }
                else
                {
                    return books;
                }
            }
            else if (filter == "date")
            {
                if (searchTerm is null)
                {
                    return books.OrderBy(c => c.Publish_Date);
                }

                return books.Where(c => c.Publish_Date.Contains(searchTerm))
                .OrderBy(c => c.Publish_Date);
            }
            else if (filter == "description")
            {
                if (searchTerm is null)
                {
                    return books.OrderBy(c => c.Description);
                }

                return books.Where(c => c.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .OrderBy(c => c.Description);
            }
            else
            {
                return books;
            }
        }

        public async Task<Book> GetBookByIdAsync(string bookId, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(bookId), trackChanges).SingleOrDefaultAsync();

        public void CreateBook(Book book) => Create(book);

        public void DeleteBook(Book book) => Delete(book);
    }
}
