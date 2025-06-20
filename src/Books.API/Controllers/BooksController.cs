using Microsoft.AspNetCore.Mvc;
using Books.API.Models;

namespace Books.API.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    [HttpGet]
    public IActionResult GetBooks()
    {
        var books = new[]
        {
            new Book { Id = 1, Title = "Dune" },
            new Book { Id = 2, Title = "Clean Code" }
        };
        
        return Ok(books);
    }
}
