using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.DTOs.AboutInfo;
using Services.DTOs.Advertising;
using Services.DTOs.Benefit;
using Services.DTOs.Comment;
using Services.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace App.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProdcutCommentController : ControllerBase
    {
        private readonly IProdcutCommentService _service;

        public ProdcutCommentController(IProdcutCommentService service)
        {
            _service = service;
        }


        [Authorize]
        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> AddComment(int id, [FromBody] string comment)
        {
            await _service.AddCommentToProductAsync(id, comment);
            return Ok();
        }


        [HttpPost]
        [ProducesResponseType(statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromForm] CommentCreateDto comment)
        {
            try
            {
                await _service.Add(comment);
                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK, Type = typeof(CommentListDto))]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById([FromRoute][Required] int id)
        {
            try
            {
                return Ok(await _service.GetComments(id));
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);

            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }

        }



        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete([FromRoute] int id, string userName)
        {
            try
            {
                // Get the current user
             

                // Call the delete method in your service passing the comment ID and the current user's username
                await _service.Delete(id, userName);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





    }
}
