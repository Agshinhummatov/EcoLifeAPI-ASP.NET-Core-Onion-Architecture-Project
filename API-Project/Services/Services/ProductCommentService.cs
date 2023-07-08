using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Repository.Repositories.Interfaces;
using Services.DTOs.Comment;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ProductCommentService : IProdcutCommentService
    {
        private readonly IProdcutCommentRepository _repo;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        public ProductCommentService(IProdcutCommentRepository repo, IMapper mapper, UserManager<AppUser> userManager)
        {
            _repo = repo;
            _mapper = mapper;
            _userManager = userManager;
        }


        public async Task AddCommentToProductAsync(int productId, string comment)
        {
            await _repo.AddCommentToProductAsync(productId, comment);

        }


        public async Task Add(CommentCreateDto commentCreateDto)
        {
            AppUser dbUser = await _userManager.FindByNameAsync(commentCreateDto.UserName);
           
            ProdcutComment comment = new()
            {

                AppUserId = dbUser.Id,
                Context = commentCreateDto.Context,
                ProductId = commentCreateDto.PordicutId,
            };
            
             await _repo.CreateAsync(comment);

        }



        public async Task Delete(int commentId, string userName)
        {
            // Get the comment by ID
            ProdcutComment comment = await _repo.GetByIdAsync(commentId);

            if (comment == null)
            {
                throw new Exception("Comment not found."); // Throw an exception if the comment is not found
            }

            // Find the user by username
            AppUser dbUser = await _userManager.FindByNameAsync(userName);

            if (dbUser == null)
            {
                throw new Exception("User not found."); // Throw an exception if the user is not found
            }

            // Check if the comment belongs to the current user
            if (comment.AppUserId != dbUser.Id)
            {
                throw new Exception("You are not authorized to delete this comment."); // Throw an exception if the comment does not belong to the current user
            }

            // Delete the comment
            await _repo.DeleteAsync(comment);

        }


        public async Task<List<CommentListDto>> GetComments(int productId)
        {
            var comments = await _repo.FindAllAsync(c => c.ProductId == productId);
            List<CommentListDto> commentListDtos = new();
            foreach (var comment in comments)
            {
                CommentListDto commentListDto = new();
                commentListDto.CreatedTime = comment.CreatedAt.ToString(" dd MMMM yyyy HH:mm:ss");
                commentListDto.Context = comment.Context;
                AppUser dbUser = await _userManager.FindByIdAsync(comment.AppUserId);
                commentListDto.UserName = dbUser.UserName;
                commentListDto.ProductCommentId = comment.Id;
                commentListDtos.Add(commentListDto);
            }
            return commentListDtos;
        }


    }
}
