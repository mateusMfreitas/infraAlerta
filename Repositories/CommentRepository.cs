using infraAlerta.Data;
using infraAlerta.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace infraAlerta.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApiDbContext _context;

        public CommentRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsByProblemId(int pro_id)
        {
            return await _context.Comments
                                 .Where(c => c.pro_id == pro_id)
                                 .ToListAsync();
        }
    }
}
