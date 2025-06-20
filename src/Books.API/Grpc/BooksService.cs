using Grpc.Core;

namespace Books.API.Grpc;

public class BooksService : BookService.BookServiceBase
{
    public override Task<BookList> GetBooks(GetBooksRequest request, ServerCallContext context)
    {
        var result = new BookList();
        result.Books.Add(new Book { Id = 1, Title = "Dune" });
        result.Books.Add(new Book { Id = 2, Title = "Clean Code" });
        return Task.FromResult(result);
    }
}
