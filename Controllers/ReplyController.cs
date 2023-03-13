using Microsoft.AspNetCore.Mvc;

using mongodb_example.Model;
using mongodb_example.Interface;

namespace mongodb_example.Controllers;

[ApiController]
[Route("articles")]
public class ReplyController : ControllerBase
{
    private readonly IReplyService _replyService;

    public ReplyController(IReplyService replyService) {
        _replyService = replyService;
    }

    [HttpPost("{articleId}/comments/{commentId}/replys")]
    public async Task<ActionResult> Create(string articleId, string commentId, Reply request) {
        var result = await _replyService.Create(articleId, commentId, request);

        return CreatedAtAction("Get", "Comment", new { articleId }, result);
    }

    [HttpPut("{articleId}/comments/{commentId}/replys/{replyId}")]
    public async Task<ActionResult> Create(string articleId, string commentId, string replyId, Reply request) {
        var result = await _replyService.Update(articleId, commentId, replyId, request);

        return Ok(result);
    }

    [HttpDelete("{articleId}/comments/{commentId}/replys/{replyId}")]
    public async Task<ActionResult> Delete(string articleId, string commentId, string replyId) {
        await _replyService.Delete(articleId, commentId, replyId);
        
        return NoContent();
    }
}