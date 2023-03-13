using Microsoft.AspNetCore.Mvc;

using mongodb_example.Model;
using mongodb_example.Interface;

namespace mongodb_example.Controllers;

[ApiController]
[Route("articles")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService) {
        _commentService = commentService;
    }

    [HttpGet("{articleId}/comments")]
    public async Task<ActionResult> Get(string articleId) {
        var result = await _commentService.Get(articleId);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost("{articleId}/comments")]
    public async Task<ActionResult> Create(string articleId, Comment request) {
        var result = await _commentService.Create(articleId, request);
        
        return CreatedAtAction(nameof(Get), new { articleId }, result);
    }

    [HttpPut("{articleId}/comments/{commentId}")]
    public async Task<ActionResult> Update(string articleId, string commentId, Comment request) {
        var result = await _commentService.Update(articleId, commentId, request);
        
        return Ok(result);
    }

    [HttpDelete("{articleId}/comments/{commentId}")]
    public async Task<ActionResult> Delete(string articleId, string commentId) {
        await _commentService.Delete(articleId, commentId);
        
        return NoContent();
    }
}