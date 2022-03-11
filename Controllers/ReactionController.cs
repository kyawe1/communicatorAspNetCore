using communicator.Entity;
using communicator.Context;
using communicator.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace communicator.Controllers;

public class ReactionController : Controller
{
    private readonly ApplicationContext _context;
    public ReactionController(ApplicationContext context)
    {
        _context = context;
    }
    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public IActionResult Like(string blogId)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;
        var reaction = new Reaction()
        {
            BlogId = blogId,
            UserId = userId,
            IsLike = true
        };

        _context.reactions.Add(reaction);
        _context.SaveChanges();

        return RedirectToAction("Index", "Blog");
    }
    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public IActionResult UnLike(string blogId)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;

        var reaction = _context.reactions.FirstOrDefault(r => r.BlogId == blogId && r.UserId == userId);
        if (reaction == null)
        {
            return BadRequest();
        }
        _context.Remove(reaction);
        _context.SaveChanges();

        return RedirectToAction("Index", "Blog");
    }
}