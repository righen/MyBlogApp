using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyBlogApp.Application.Commands.CreatePost;
using MyBlogApp.Application.Commands.DeletePost;
using MyBlogApp.Application.Commands.UpdatePost;
using MyBlogApp.Application.Queries.GetAllPosts;
using MyBlogApp.Application.Queries.GetPostById;

namespace MyBlogApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly ILogger<PostsController> _logger;
        private readonly IMediator _mediator;

        public PostsController(IMediator mediator, ILogger<PostsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            _logger.LogInformation("Getting post with ID {PostId}", id);
            var post = await _mediator.Send(new GetPostByIdQuery(id));
            if (post == null)
            {
                _logger.LogWarning("Post with ID {PostId} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Post with ID {PostId} retrieved successfully", id);
            return Ok(post);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            _logger.LogInformation("Getting all posts");
            var posts = await _mediator.Send(new GetAllPostsQuery());
            _logger.LogInformation("{PostCount} posts retrieved successfully", posts.Count());
            return Ok(posts);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostCommand command)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for CreatePostCommand");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating a new post");
            var postId = await _mediator.Send(command);
            _logger.LogInformation("Post with ID {PostId} created successfully", postId);
            return CreatedAtAction(nameof(GetPostById), new { id = postId }, command);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] UpdatePostCommand command)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for UpdatePostCommand");
                return BadRequest(ModelState);
            }

            // Ensure the command uses the ID from the URL
            command.Id = id;
            _logger.LogInformation("Updating post with ID {PostId}", id);
            await _mediator.Send(command);
            _logger.LogInformation("Post with ID {PostId} updated successfully", id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            _logger.LogInformation("Deleting post with ID {PostId}", id);
            await _mediator.Send(new DeletePostCommand(id));
            _logger.LogInformation("Post with ID {PostId} deleted successfully", id);
            return NoContent();
        }
    }
}