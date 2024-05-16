using infraAlerta.Data;
using infraAlerta.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace infraAlerta.Controllers;

[ApiController]
[Route("problem")]
public class ProblemController : ControllerBase
{
    private readonly ApiDbContext _context;

    public ProblemController(ApiDbContext context)
    {
        _context = context;
    }

    [HttpGet("getProblems", Name = "getProblems")]
    public IActionResult GetProblems()
    {
        var problems = _context.Problem.ToList();
        return Ok(problems);
    }

    [HttpPost("createProblem", Name = "createProblem")]
    public async Task<IActionResult> CreateProblem([FromBody] Problem newProblem)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _context.Problem.Add(newProblem);
        await _context.SaveChangesAsync();

        return Ok(newProblem);
    }

    [HttpPut("updateProblem/{id}", Name = "updateProblem")]
    public async Task<IActionResult> UpdateProblem(int id, [FromBody] Problem updatedProblem)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var problem = _context.Problem.FirstOrDefault(x => x.pro_id == id);
        if (problem == null)
        {
            return NotFound();
        }

        problem.pro_name = updatedProblem.pro_name;
        problem.pro_classification = updatedProblem.pro_classification;
        problem.pro_photo = updatedProblem.pro_photo;

        await _context.SaveChangesAsync();

        return Ok(problem);
    }

    [HttpDelete("deleteProblem/{id}", Name = "deleteProblem")]
    public async Task<IActionResult> DeleteProblem(int id)
    {
        var problem = _context.Problem.FirstOrDefault(x => x.pro_id == id);
        if (problem == null)
        {
            return NotFound();
        }

        _context.Problem.Remove(problem);
        await _context.SaveChangesAsync();

        return Ok();
    }
}
