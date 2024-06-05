using infraAlerta.Data;
using infraAlerta.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace infraAlerta.Controllers
{
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

        // Relatório de bairros com mais chamados
        [HttpGet("report/neighborhoods", Name = "neighborhoodReport")]
        public IActionResult GetNeighborhoodReport()
        {
            var neighborhoods = _context.Problem
                .Join(_context.Problem_Address,
                      problem => problem.pro_id,
                      address => address.pa_problem_id,
                      (problem, address) => new { problem, address })
                .GroupBy(pa => pa.address.pa_neighborhood)
                .Select(g => new
                {
                    Neighborhood = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .ToList();

            var result = new
            {
                Neighborhoods = neighborhoods
            };
            return Ok(result);
        }

        // Relatório de usuários que mais reportaram
        [HttpGet("report/users", Name = "userReport")]
        public IActionResult GetUserReport()
        {
            var users = _context.Problem
                .GroupBy(p => p.pro_user)
                .Select(g => new
                {
                    User = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .ToList();

            var result = new
            {
                Users = users
            };
            return Ok(result);
        }

        // Relatório de tipos de problemas mais comuns
        [HttpGet("report/types", Name = "typeReport")]
        public IActionResult GetTypeReport()
        {
            var types = _context.Problem
                .GroupBy(p => p.pro_classification)
                .Select(g => new
                {
                    Type = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .ToList();

            var result = new
            {
                Types = types
            };
            return Ok(result);
        }

        // Relatório de status dos chamados
        [HttpGet("report/status", Name = "statusReport")]
        public IActionResult GetStatusReport()
        {
            var statuses = _context.Problem
                .GroupBy(p => p.pro_status)
                .Select(g => new
                {
                    Status = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .ToList();

            var result = new
            {
                Statuses = statuses
            };
            return Ok(result);
        }

        // Relatório de chamados ao longo do tempo (Chamados ao Longo do Tempo)
        [HttpGet("report/line-data")]
        public IActionResult GetLineData()
        {
            var data = _context.Problem
                .GroupBy(p => new { p.pro_status, p.pro_id })
                .Select(g => new
                {
                    Year = g.Key.pro_id,
                    Month = g.Key.pro_status,
                    Count = g.Count()
                })
                .OrderBy(d => d.Year).ThenBy(d => d.Month)
                .ToList();

            var labels = data.Select(d => $"{d.Month}/{d.Year}").ToArray();
            var counts = data.Select(d => d.Count).ToArray();

            return Ok(new
            {
                labels,
                datasets = new[]
                {
                    new
                    {
                        label = "Chamados ao Longo do Tempo",
                        data = counts,
                        fill = false,
                        backgroundColor = "rgba(75, 192, 192, 0.2)",
                        borderColor = "rgba(75, 192, 192, 1)"
                    }
                }
            });
        }

        // Relatório de tipos de problemas mais comuns (Tipos de Problemas)
        [HttpGet("report/pie-data")]
        public IActionResult GetPieData()
        {
            var data = _context.Problem
                .GroupBy(p => p.pro_classification)
                .Select(g => new
                {
                    Type = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .ToList();

            var result = new
            {
                labels = data.Select(d => d.Type).ToArray(),
                datasets = new[]
                {
                    new
                    {
                        label = "Problemas",
                        data = data.Select(d => d.Count).ToArray(),
                        backgroundColor = new[]
                        {
                            "rgba(255, 99, 132, 0.2)",
                            "rgba(54, 162, 235, 0.2)",
                            "rgba(255, 206, 86, 0.2)"
                        },
                        borderColor = new[]
                        {
                            "rgba(255, 99, 132, 1)",
                            "rgba(54, 162, 235, 1)",
                            "rgba(255, 206, 86, 1)"
                        },
                        borderWidth = 1
                    }
                }
            };
            return Ok(result);
        }

        // Relatório de bairros com mais chamados (Chamados por Bairro)
        [HttpGet("report/bar-data")]
        public IActionResult GetBarData()
        {
            var data = _context.Problem
                .Join(_context.Problem_Address,
                      problem => problem.pro_id,
                      address => address.pa_problem_id,
                      (problem, address) => new { problem, address })
                .GroupBy(pa => pa.address.pa_neighborhood)
                .Select(g => new
                {
                    Neighborhood = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .ToList();

            var result = new
            {
                labels = data.Select(d => d.Neighborhood).ToArray(),
                datasets = new[]
                {
                    new
                    {
                        label = "Chamados",
                        data = data.Select(d => d.Count).ToArray(),
                        backgroundColor = "rgba(75, 192, 192, 0.2)",
                        borderColor = "rgba(75, 192, 192, 1)",
                        borderWidth = 1
                    }
                }
            };
            return Ok(result);
        }
    }

    public class ProblemCreationData
    {
        public Problem Problem { get; set; }
        public Problem_address ProblemAddress { get; set; }
    }
}
