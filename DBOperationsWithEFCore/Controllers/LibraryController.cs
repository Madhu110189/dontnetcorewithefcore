using DBOperationsWithEFCore.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DBOperationsWithEFCore.Controllers
{
    [Route("api/library")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly AppDbContext _context;
        public LibraryController(AppDbContext appDb) 
        { 
            this._context = appDb;
        }

        [HttpGet("currency")]
        public async Task<IActionResult> GetAllCurrencies()
        {
            //var result = await _context.Currency.ToListAsync();
            var result = await (from currencies in _context.Currency
                                select currencies).ToListAsync();
            return Ok(result);
        }

        [HttpGet("language")]
        public async Task<IActionResult> GetAllLanguage()
        {
            //var result = await _context.Languages.ToListAsync();
            var result = await (from languages in _context.Languages
                                select  languages).ToListAsync();
            return Ok(result);
        }

        [HttpGet("currency/{id:int}")]
        public async Task<IActionResult> GetCurrencyByID([FromRoute] int id)
        {
            var result = await _context.Currency.FindAsync(id);
            return Ok(result);
        }

        [HttpGet("currency/{name}")]
        public async Task<IActionResult> GetCurrencyByName([FromRoute] string name)
        {
            var result = await _context.Currency.FirstOrDefaultAsync(x=> x.Title==name);
            return Ok(result);
        }
        [HttpGet("currency/{name}/desc")]
        public async Task<IActionResult> GetCurrencyByNameAndDesc([FromRoute] string name, [FromQuery] string desc)
        {
            var result= await _context.Currency.FirstOrDefaultAsync(x=> x.Title==name && (string.IsNullOrEmpty(x.Description) || x.Description==desc));
            return Ok (result);
        }
    }
}
