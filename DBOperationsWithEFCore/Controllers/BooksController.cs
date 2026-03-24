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

        [HttpDelete("deletebook/{id}")]
        public async Task<IActionResult> deleteFromBook([FromRoute] int id)
        {
            #region delete method 1 - by 2 db hits
            //var book = appDb.Books.FirstOrDefault(x=> x.Id ==id);
            //if (book==null)
            //{
            //    return NotFound();
            //}
            //appDb.Books.Remove(book);
            //await appDb.SaveChangesAsync();
            #endregion

            #region delete method 2 - by single db hit
            var book = new Book { Id = id };
            appDb.Entry(book).State = EntityState.Deleted;
            await appDb.SaveChangesAsync();
            #endregion

            return Ok();
        }
    }
}
