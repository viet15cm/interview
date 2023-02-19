using AutoMapper;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Shared.DataTransferObjects;

namespace Repository.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            var books = ReadDataFromJsonFile();
            builder.HasData(books);
        }

        private Book[] ReadDataFromJsonFile()
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Files", "books.json");

            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                var booksDto = JsonConvert.DeserializeObject<BookDto[]>(json);
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookDto, Book>()).CreateMapper();
                var books = mapper.Map<Book[]>(booksDto);
                return books;
            }
        }
    }
}