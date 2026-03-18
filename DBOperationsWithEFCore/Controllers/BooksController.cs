using DBOperationsWithEFCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DBOperationsWithEFCore.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext appDb;
        public BooksController(AppDbContext appDbContext)
        {
            this.appDb = appDbContext;
        }

        [HttpPost("addbook")]
        public async Task<IActionResult> InsertBooks([FromBody] Book bookmodel)
        {
            appDb.Books.Add(bookmodel);
            await appDb.SaveChangesAsync();

            return Ok(bookmodel);   
        }

        [HttpPost("AddManyBooks")]
        public async Task<IActionResult> AddMultipleBooks([FromBody] List<Book> booksmodel)
        {
            appDb.Books.AddRange(booksmodel);
            await appDb.SaveChangesAsync();

            return Ok(booksmodel);
        }
    }
}
