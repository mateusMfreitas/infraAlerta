using infraAlerta.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace infraAlerta.Repositories
{
    public interface ICommentRepository
    {
        Task AddComment(Comment comment);
        Task<IEnumerable<Comment>> GetCommentsByProblemId(int problemId);
    }
}
