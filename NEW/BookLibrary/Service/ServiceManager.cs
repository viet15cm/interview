using AutoMapper;
using Contracts;
using Service.Contracts;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IBookService> _bookService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
        {
            _bookService = new Lazy<IBookService>(() =>
            new BookService(repositoryManager, logger, mapper));
        }

        public IBookService BookService => _bookService.Value;
    }
}
