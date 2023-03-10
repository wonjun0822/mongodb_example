using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using mongodb_example.Model;
using mongodb_example.Interface;

namespace mongodb_example.Controllers;

[ApiController]
[Route("article")]
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

    [HttpGet("id")]
    public async Task<ActionResult> GetById(string id) {
        var result = await _articleService.GetById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody]Article request) {
        await _articleService.Create(request);
        
        return CreatedAtAction(nameof(Get), new { id = request.id }, request);
    }

    [HttpPut("id")]
    public async Task<ActionResult> Update(string id, [FromBody]Article request) {
        var result = await _articleService.Update(id, request);
        
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("id")]
    public async Task<ActionResult> Delete(string id) {
        await _articleService.Delete(id);
        
        return NoContent();
    }
}