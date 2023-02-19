using Entities.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Testing
{
    public class BooksControllerTests
    {
        private readonly HttpClient _client;

        public BooksControllerTests()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7278/"),
            };
        }

        [Fact]
        public async Task GetAllBooksAsync_ReturnUnsorted()
        {
            // Act
            var response = await _client.GetAsync("api/books");
            var content = await response.Content.ReadAsStringAsync();
            var books = JsonConvert.DeserializeObject<List<Book>>(content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(books);
            Assert.NotEmpty(books);
        }

        [Fact]
        public async Task GetBooksByFilterAsync_ReturnFilterSorted()
        {
            // Arrange
            string filter = "author";

            // Act
            var response = await _client.GetAsync($"/api/books/{filter}");
            var content = await response.Content.ReadAsStringAsync();
            var books = JsonConvert.DeserializeObject<List<Book>>(content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(books);
            Assert.NotEmpty(books);
        }

        [Fact]
        public async Task GetBooksByFilterAsync_ReturnSearchTermSorted()
        {
            // Arrange
            string filter = "author";
            string searchTerm = "joe";

            // Act
            var response = await _client.GetAsync($"/api/books/{filter}/{searchTerm}");
            var content = await response.Content.ReadAsStringAsync();
            var books = JsonConvert.DeserializeObject<List<Book>>(content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(books);
            Assert.NotEmpty(books);
        }

        [Fact]
        public async Task GetBooksByFilterAsync_ReturnPriceRangeSorted()
        {
            // Arrange
            string filter = "price";
            string searchTerm = "11";
            string maxValue = "36";

            // Act
            var response = await _client.GetAsync($"/api/books/{filter}/{searchTerm}/{maxValue}");
            var content = await response.Content.ReadAsStringAsync();
            var books = JsonConvert.DeserializeObject<List<Book>>(content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(books);
            Assert.NotEmpty(books);
        }

        [Fact]
        public async Task CreateBookAsync_ReturnCreatedBook()
        {
            // Arrange
            var book = new Book
            {
                Author = "Alexandre Dumas",
                Title = "The Count of Monte Cristo",
                Genre = "Adventure",
                Price = "22.95",
                Publish_Date = "1846, 1, 15",
                Description = "The Count of Monte Cristo is an adventure novel written by French author Alexandre Dumas completed in 1844."
            };

            var json = JsonConvert.SerializeObject(book);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("api/books", data);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.True(response.Headers.Location != null);
        }

        [Fact]
        public async Task UpdateBookAsync_ReturnUpdatedBook()
        {
            // Arrange
            var bookId = "B14";
            var book = new Book
            {
                Author = "Alexandre Dumas",
                Title = "The Count of Monte Cristo",
                Genre = "Adventure",
                Price = "31.59",
                Publish_Date = "1846-01-15",
                Description = "The Count of Monte Cristo is an adventure novel written by French author Alexandre Dumas completed in 1844. It is one of the author's most popular works, along with The Three Musketeers."
            };
            var json = JsonConvert.SerializeObject(book);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            // Check if book exists
            var response = await _client.GetAsync($"api/books/{bookId}");
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                // Book not found, PUT should fail
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
                return;
            }

            // Book exists, update it using PUT
            response = await _client.PutAsync($"api/books/{bookId}", data);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task DeleteBookAsync_ReturnDeleted()
        {
            // Arrange
            var bookId = "B15"; // Check if id exist before deleting.
            var deleteRequest = new HttpRequestMessage(HttpMethod.Delete, $"api/books/{bookId}");

            // Act
            var deleteResponse = await _client.SendAsync(deleteRequest);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
        }
    }
}
