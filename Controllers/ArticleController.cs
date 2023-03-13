using Microsoft.AspNetCore.Mvc;

using mongodb_example.Model;
using mongodb_example.Interface;
using mongodb_example.DTO;

namespace mongodb_example.Controllers;

[ApiController]
[Route("articles")]
public class ArticleController : ControllerBase
{
    private readonly IArticleService _articleService;

    public ArticleController(IArticleService articleService) {
        _articleService = articleService;
    }

    [HttpGet]
    public async Task<ActionResult> Get() {
        var result = await _articleService.GetAll();

        return Ok(result);
    }

    [HttpGet("{articleId}")]
    public async Task<ActionResult> GetById(string articleId) {
        var result = await _articleService.GetById(articleId);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody]ArticleWriteDTO request) {
        var result = await _articleService.Create(request);
        
        return CreatedAtAction(nameof(Get), new { id = result.id }, result);
    }

    [HttpPut("{articleId}")]
    public async Task<ActionResult> Update(string articleId, [FromBody]ArticleWriteDTO request) {
        var result = await _articleService.Update(articleId, request);
        
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{articleId}")]
    public async Task<ActionResult> Delete(string articleId) {
        await _articleService.Delete(articleId);
        
        return NoContent();
    }
}