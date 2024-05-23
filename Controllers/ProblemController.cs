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

    [HttpGet("getProblem/{id}", Name = "getProblem")]
    public IActionResult GetProblem(int id)
    {
        var problem = _context.Problem.Where(p => p.pro_id == id).ToList();
        var problem_address = _context.Problem_Address.Where(pa => pa.pa_problem_id == id).ToList();

        var result = new 
        {
            Problem = problem,
            ProblemAddress = problem_address
        };
        return Ok(result);
    }

    [HttpGet("getProblemsUser/{id}", Name = "getProblemsUser")]
    public IActionResult GetProblemsUser(int id)
    {
        var problems = _context.Problem.Where(p => p.pro_user == id).ToList();
        return Ok(problems);
    }

    [HttpPost("createProblem", Name = "createProblem")]
    public async Task<IActionResult> CreateProblem([FromBody] ProblemCreationData newProblem)
    {
        _context.Problem.Add(newProblem.Problem);
        await _context.SaveChangesAsync();

        var problemId = newProblem.Problem.pro_id;
        newProblem.ProblemAddress.pa_problem_id = problemId;
        _context.Problem_Address.Add(newProblem.ProblemAddress);
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

    [HttpPut("changeOwner/{id}/{userId}", Name = "changeOwner")]
    public async Task<IActionResult> ChangeOwner(int id, int userId)
    {

        var problem = _context.Problem.FirstOrDefault(x => x.pro_id == id);
        if (problem == null)
        {
            return NotFound();
        }

        problem.pro_admin = userId;
        problem.pro_status = "ATRIBUIDO";
        await _context.SaveChangesAsync();

        return Ok(problem);
    }

    [HttpPut("closeProblem/{id}", Name = "closeProblem")]
    public async Task<IActionResult> CloseProblem(int id, int userId)
    {

        var problem = _context.Problem.FirstOrDefault(x => x.pro_id == id);
        if (problem == null)
        {
            return NotFound();
        }

        problem.pro_status = "FINALIZADO";
        await _context.SaveChangesAsync();

        return Ok(problem);
    }
}

    

public class ProblemCreationData
{
    public Problem Problem { get; set; }
    public Problem_address ProblemAddress { get; set; }
}