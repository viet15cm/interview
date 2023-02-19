using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace BookLibrary
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>();

            CreateMap<BookForCreationDto, Book>();

            CreateMap<BookForUpdateDto, Book>();

        }
    }
}
