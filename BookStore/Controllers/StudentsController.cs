using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStore.Data;
using BookStore.DTO.Students;
using BookStore.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MQuery;
using NSwag.Annotations;

namespace BookStore.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly BookStoreDbContext _db;
        private readonly IMapper _mapper;

        public StudentsController(BookStoreDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<StudentVM>> Create(StudentUM um)
        {
            var model = _mapper.Map<Student>(um);

            _db.Add(model);

            await _db.SaveChangesAsync();


            return CreatedAtAction(nameof(GetById), new { model.Id }, _mapper.Map<StudentVM>(model));

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StudentVM>> Update(int id, StudentUM um)
        {
            var model = await _db.Students.FirstOrDefaultAsync(x => x.Id == id);

            if (model is null)
            {
                return NotFound();
            }

            _mapper.Map(um, model);

            await _db.SaveChangesAsync();

            return _mapper.Map<StudentVM>(model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _db.Students.FindAsync(id);

            if (model is null)
                return NotFound();

            _db.Remove(model);
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var model = await _db.Students
                .Where(x => x.Id == id)
                .ProjectTo<StudentVM>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (model is null)
                return NotFound();

            return Ok(model);
        }

        [HttpGet]
        public async Task<ActionResult> GetByQuery(
             [OpenApiIgnore] Query<StudentVM> query,
             [FromQuery(Name = "$search")] string? search = null)
        {
            var mqy = _db.Books
                .ProjectTo<StudentVM>(_mapper.ConfigurationProvider);

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
