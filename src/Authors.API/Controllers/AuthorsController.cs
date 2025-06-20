using Authors.API.Models;
using Books;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Authors.API.Controllers;

[ApiController]
[Route("api/authors")]
public class AuthorsController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly BookService.BookServiceClient _bookServiceClient;
    private readonly ILogger<AuthorsController> _logger;

    public AuthorsController(
        HttpClient httpClient,
        BookService.BookServiceClient bookServiceClient,
        ILogger<AuthorsController> logger)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _bookServiceClient = bookServiceClient ?? throw new ArgumentNullException(nameof(bookServiceClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet("with-books-http")]
    public async Task<ActionResult<IEnumerable<Author>>> GetAuthorsWithBooksViaHttp()
    {
        try
        {
            // Get sample authors
            var authors = GetSampleAuthors();

            // Get books from Books.API via HTTP
            var response = await _httpClient.GetAsync("api/books");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var books = JsonSerializer.Deserialize<List<Models.Book>>(content, 
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (books != null)
                {
                    // Assign books to authors (simple demo logic)
                    foreach (var author in authors)
                    {
                        // For demonstration purposes, assign books to authors
                        // For author 1, assign book 1
                        // For author 2, assign book 2
                        author.Books = books
                            .Where(b => b.Id == author.Id)
                            .ToList();
                    }
                }
            }
            else
            {
                _logger.LogWarning("Failed to get books from Books.API via HTTP. Status code: {StatusCode}", 
                    response.StatusCode);
            }

            return Ok(authors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting authors with books via HTTP");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("with-books-grpc")]
    public async Task<ActionResult<IEnumerable<Author>>> GetAuthorsWithBooksViaGrpc()
    {
        try
        {
            // Get sample authors
            var authors = GetSampleAuthors();

            // Get books from Books.API via gRPC
            var bookList = await _bookServiceClient.GetBooksAsync(new GetBooksRequest());

            if (bookList != null && bookList.Books.Any())
            {
                // Assign books to authors (simple demo logic)
                foreach (var author in authors)
                {
                    // For demonstration purposes, assign books to authors
                    // For author 1, assign book 1
                    // For author 2, assign book 2
                    author.Books = bookList.Books
                        .Where(b => b.Id == author.Id)
                        .Select(b => new Models.Book { Id = b.Id, Title = b.Title })
                        .ToList();
                }
            }

            return Ok(authors);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting authors with books via gRPC");
            return StatusCode(500, "Internal server error");
        }
    }

    private List<Author> GetSampleAuthors()
    {
        return new List<Author>
        {
            new Author { Id = 1, Name = "Frank Herbert" },
            new Author { Id = 2, Name = "Robert C. Martin" }
        };
    }
}