using infraAlerta.DTOs;
using infraAlerta.Models;
using infraAlerta.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace infraAlerta.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _repository;

        public CommentController(ICommentRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("{pro_id}")]
        public async Task<IActionResult> AddComment(int pro_id, [FromBody] AddCommentDto addCommentDto)
        {
            var comment = new Comment
            {
                pro_id = pro_id,
                user_id = addCommentDto.user_id,
                comment_text = addCommentDto.Content,
                created_at = DateTime.UtcNow
            };

            await _repository.AddComment(comment);
            return CreatedAtAction(nameof(GetCommentsByProblemId), new { pro_id = comment.pro_id }, comment);
        }

        [HttpGet("{pro_id}")]
        public async Task<IActionResult> GetCommentsByProblemId(int pro_id)
        {
            var comment = await _repository.GetCommentsByProblemId(pro_id);
            return Ok(comment);
        }
    }
}
