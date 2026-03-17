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
        public async Task<IActionResult> GetCurrencyByNameAndDesc([FromRoute] string name, [FromQuery] string? desc)
        {
            var result= await _context.Currency.FirstOrDefaultAsync(x=> x.Title==name && (string.IsNullOrEmpty(desc) || x.Description==desc));
            return Ok (result);
        }
        [HttpGet("allcurrency/{name}/desc")]
        public async Task<IActionResult> GetAllCurrencyByNameAndDesc([FromRoute] string name, [FromQuery] string? desc)
        {
            var result = await _context.Currency.Where
                (x=> x.Title == name && (string.IsNullOrEmpty(desc) || x.Description==desc))
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCurrencyByIds()
        {
            var ids = new List<int> { 1,2,3};
            var result = await _context.Currency.Where
                (x => ids.Contains(x.Id)).ToListAsync();
            return Ok(result);
        }

        [HttpPost("all")]
        public async Task<IActionResult> getAllcurrencybyidsfrombody([FromBody] List<int> ids)
        {
            var result = await _context.Currency.Where(x => ids.Contains(x.Id)).ToListAsync();
            return Ok(result);
        }

        [HttpGet("currency/few")]
        public async Task<IActionResult> getFewColumnsOfCurrency()
        {
            //var result = await _context.Currency
            //    .Select(x=> new Currency { 
            //        Id = x.Id,
            //        Title = x.Title,
            //    })
            //    .ToListAsync();

            var result = await (from currencies in _context.Currency
                                select new
                                {
                                    ID = currencies.Id,
                                    Name = currencies.Title,
                                }).ToListAsync();
            return Ok(result);
        }
    }
}
