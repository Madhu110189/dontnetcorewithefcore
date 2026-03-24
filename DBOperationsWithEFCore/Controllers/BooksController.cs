using DBOperationsWithEFCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpPut("updatebook/{bookid}")]
        public async Task<IActionResult> updateBooks([FromRoute] int bookid, [FromBody] Book bookmodel)
        {
            var objbook = appDb.Books.FirstOrDefault(x=> x.Id == bookid);
            if (objbook == null)
            {
                return BadRequest("Not found");
            }

            objbook.Title = bookmodel.Title;
            objbook.Description = bookmodel.Description;

            await appDb.SaveChangesAsync();
            return Ok(bookmodel);
        }

        [HttpPut("updatebulk")]
        public async Task<IActionResult> updateBooksInBulk()
        {
            await appDb.Books.
                Where(x => x.NoOfPages == 10).
                ExecuteUpdateAsync(x => x
            .SetProperty(p => p.Title, p => p.Title + " updated.")
            .SetProperty(p => p.Description, p => p.Description + " updated"));
            return Ok();
        }
    }
}
