using Services.DTOs.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interfaces
{
    public interface IProdcutCommentService
    {
       Task AddCommentToProductAsync(int productId, string comment);

        Task Add(CommentCreateDto commentCreateDto);

        Task Delete(int commentId, string userName);

        Task<List<CommentListDto>> GetComments(int productId);

    }
}
