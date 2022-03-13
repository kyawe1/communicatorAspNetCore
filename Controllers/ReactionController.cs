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
    // [HttpPost]
    [Authorize]
    // [ValidateAntiForgeryToken]
    [HttpGet]
    public IActionResult Like(string id)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;
        if(_context.reactions.Where(p=>p.BlogId==id && p.UserId==userId).Count()!=0){
            return RedirectToAction("Index", "Blog");
        }
        var reaction = new Reaction()
        {
            BlogId = id,
            UserId = userId,
            IsLike = true
        };

        _context.reactions.Add(reaction);
        _context.SaveChanges();

        return RedirectToAction("Index", "Blog");
    }
    // [HttpPost]
    [Authorize]
    // [ValidateAntiForgeryToken]
    [HttpGet]
    public IActionResult UnLike(string id)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;

        var reaction = _context.reactions.FirstOrDefault(r => r.BlogId == id && r.UserId == userId);
        if (reaction == null)
        {
            return BadRequest();
        }
        _context.Remove(reaction);
        _context.SaveChanges();

        return RedirectToAction("Index", "Blog");
    }
}