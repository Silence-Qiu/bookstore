using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.Data;
using BookStore.DTO.Books;
using BookStore.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MQuery;
using NSwag.Annotations;

namespace BookStore.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookStoreDbContext _db;
        private readonly IMapper _mapper;

        public BooksController(BookStoreDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<BookVM>> Create(BookUM um)
        {
            var model = _mapper.Map<Book>(um);

            _db.Add(model);

            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { model.Id }, _mapper.Map<BookVM>(model));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookVM>> Update(int id, BookUM um)
        {
            var model = await _db.Books.FirstOrDefaultAsync(x => x.Id == id);

            if (model is null)
            {
                return NotFound();
            }

            _mapper.Map(um, model);

            await _db.SaveChangesAsync();

            return _mapper.Map<BookVM>(model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _db.Books.FindAsync(id);

            if (model is null)
                return NotFound();

            _db.Remove(model);

            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var model = await _db.Books
                .Where(x => x.Id == id)
                .ProjectTo<BookVM>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (model is null)
                return NotFound();

            return Ok(model);
        }

        [HttpGet]
        public async Task<ActionResult> GetByQuery(
             [OpenApiIgnore] Query<BookVM> query, 
             [FromQuery(Name = "$search")] string? search = null)
        {
            var mqy = _db.Books
                .ProjectTo<BookVM>(_mapper.ConfigurationProvider);

            mqy = query.FilterTo(mqy);


            if (!string.IsNullOrWhiteSpace(search))
            {
                mqy = mqy.Where(x => x.Name.Contains(search!));
            }

            mqy = query.FilterTo(mqy);

            Response.Headers.Add("X-Count", mqy.Count().ToString());

            if (Request.Method.ToLower() == "head")
                return Ok();

            mqy = query.SortTo(mqy);
            mqy = query.SliceTo(mqy);

            return Ok(await mqy.ToListAsync());
        }
    }
}
