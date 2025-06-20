using Microsoft.AspNetCore.Mvc;
using Books.API.Models;

namespace Books.API.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        var books = new List<Book>();
        books.Add(new Book { Id = 1, Title = "Dune" });
        books.Add(new Book { Id = 2, Title = "Clean Code" });

        await Task.Delay(100);
        
        return Ok(books);
    }
}
