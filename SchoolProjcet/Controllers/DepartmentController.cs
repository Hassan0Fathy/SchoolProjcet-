using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.AppContext;
using School.Models;
using School.Models;
using SchoolProjcet.Controllers;
using SchoolProjcet.DTOs;
using SchoolProjcet.IGenericRepo;

[ApiController]
[Route("api/[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IGeneric<Department> _departmentRepo;

    public DepartmentController(AppDbContext context)
    {
        _context = context;
        this._departmentRepo = _departmentRepo;
    }


    // GET: api/Department
    [HttpGet]
    public  IActionResult GetAll()
    {
        var departments = _context.Departments.ToList();
        var deptDto = new List<DepartmentDto>();
        foreach (var department in departments)
        {
            var dto = new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
                Description = department.Description
            };
            deptDto.Add(dto);
        }


        return Ok(deptDto);
    }

    // GET: api/Department/5
    [HttpGet("{id}")]
    public  IActionResult GetById(int id)
    {
        var department = _context.Departments.Find(id);



        if (department == null)
            return NotFound();

        var DTos = new DepartmentDto
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description
        };

        return Ok(DTos);
    }

    // POST: api/Department
    [HttpPost]
    public  IActionResult Create(DepartmentCreateDto dto)
    {
        var department = new Department
        {
            Name = dto.Name,
            Description = dto.Description
        };

        _context.Departments.Add(department);
        _context.SaveChanges();

        var resultDto = new DepartmentDto
        {
            Id = department.Id,
            Name = department.Name,
            Description = department.Description
        };

        return Ok(resultDto);
    }

    // PUT: api/Department/5
    [HttpPut("{id}")]
    public  IActionResult Edit(int id, DepartmentCreateDto dto)
    {
        var department = _context.Departments.Find(id);
        if (department == null)
            return NotFound();

        department.Name = dto.Name;
        department.Description = dto.Description;

        _context.SaveChanges();

        return Ok();
    }

    // DELETE: api/Department/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var department = await _context.Departments.FindAsync(id);
        if (department == null)
            return NotFound();

        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
