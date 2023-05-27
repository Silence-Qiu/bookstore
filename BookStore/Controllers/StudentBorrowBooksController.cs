using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.Data;
using BookStore.DTO.StudentBooks;
using BookStore.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MQuery;
using NSwag.Annotations;

namespace BookStore.Controllers
{
    [Route("api/student-borrow-books")]
    [ApiController]
    public class StudentBorrowBooksController : ControllerBase
    {
        private readonly BookStoreDbContext _db;
        private readonly IMapper _mapper;

        public StudentBorrowBooksController(BookStoreDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<StudentBorrowBookVM>> Create(StudentBorrowBookCM um)
        {
            var model = _mapper.Map<StudentBorrowBooks>(um);

            _db.Add(model);

            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { model.Id }, _mapper.Map<StudentBorrowBookVM>(model));
        }

        [HttpPut("{id}/return")]
        public async Task<ActionResult<StudentBorrowBookVM>> Return(int id)
        {
            var model = await _db.StudentBorrowBooks.FirstOrDefaultAsync(x => x.Id == id);

            if (model is null)
            {
                return NotFound();
            }

            model.ReturnTime = DateTime.Now;
            model.Status = StudentBorrowBooksStatus.已归还;

            await _db.SaveChangesAsync();

            return _mapper.Map<StudentBorrowBookVM>(model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _db.StudentBorrowBooks.FindAsync(id);

            if (model is null)
                return NotFound();

            _db.Remove(model);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var model = await _db.StudentBorrowBooks
                .Where(x => x.Id == id)
                .ProjectTo<StudentBorrowBookVM>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (model is null)
                return NotFound();

            return Ok(model);
        }

        [HttpGet]
        public async Task<ActionResult> GetByQuery(
             [OpenApiIgnore] Query<StudentBorrowBookVM> query,
             [FromQuery(Name = "$search")] string? search = null)
        {
            var mqy = _db.StudentBorrowBooks
                .ProjectTo<StudentBorrowBookVM>(_mapper.ConfigurationProvider);

            mqy = query.FilterTo(mqy);

            if (!string.IsNullOrWhiteSpace(search))
            {
                mqy = mqy.Where(x => x.Book!.Name.Contains(search!) || x.Student!.Name.Contains(search!));
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
