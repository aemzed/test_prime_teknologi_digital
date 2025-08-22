using System.Diagnostics;
using System.Text.Json;
using HrCrud.Data;
using HrCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HrCrud.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _db;
        public EmployeeController(AppDbContext db) => _db = db;

        public IActionResult Index() => View();

        [HttpGet]
        public async Task<IActionResult> List(string? q)
        {
            var query = _db.Employee.AsQueryable();
            if (!string.IsNullOrWhiteSpace(q))
                query = query.Where(e => e.NIK.Contains(q) || e.Name.Contains(q));
            var data = await query
                .OrderBy(e => e.NIK)
                .Select(e => new {
                    e.Id, e.NIK, e.Name, e.PlaceOfBirth, dob = e.DateOfBirth.ToString("yyyy-MM-dd"),
                    e.BasicSalary, gender = e.Gender.ToString(), marital = e.MaritalStatus.ToString()
                })
                .ToListAsync();
            return Json(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Employee vm)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .Select(e => new 
                    { 
                        Field = e.Key, 
                        Error = e.Value.Errors.First().ErrorMessage 
                    })
                    .ToList();

                return BadRequest(errors);
            }

            _db.Add(vm);
            await _db.SaveChangesAsync();
            return Ok(new { message = "Employee created successfully" });
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm] Employee vm)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .Select(e => new
                    {
                        Field = e.Key,
                        Error = e.Value.Errors.First().ErrorMessage
                    })
                    .ToList();

                return BadRequest(errors);
            }

            _db.Update(vm);
            await _db.SaveChangesAsync();
            return Ok(new { message = "Employee updated successfully" });
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var e = await _db.Employee.FindAsync(id);
            if (e == null) return NotFound();
            return Json(new {
                e.Id, e.NIK, e.Name, e.PlaceOfBirth,
                dob = e.DateOfBirth.ToString("yyyy-MM-dd"),
                e.BasicSalary, e.Gender, e.MaritalStatus
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var e = await _db.Employee.FindAsync(id);
            if (e == null) return NotFound();
            _db.Remove(e);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
