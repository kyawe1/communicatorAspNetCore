using communicator.Context;
using communicator.Models;
using communicator.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;



namespace communicator.Controllers;


public class FriendController : Controller
{
    private readonly ApplicationContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    public FriendController(ApplicationContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    [HttpGet]
    [Authorize]
    public IActionResult Index()
    {
        string id = _userManager.GetUserId(User);
        IEnumerable<Friend> friends = _context.friends.Where(f => f.First_UserId == id).Include(p => p.Second_User).ToList();
        IEnumerable<Friend> friends2 = _context.friends.Where(f => f.Second_UserId == id).Include(p => p.First_User).ToList();
        IEnumerable<Friend> friends3 = friends.Concat(friends2);
        return View(friends3);
    }
    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public IActionResult Make(string id)
    {
        string Id = _userManager.GetUserId(User);
        Friend? temp = _context.friends.Where(f => f.First_UserId == id && f.Second_UserId == Id || f.First_UserId == Id && f.Second_UserId == id).FirstOrDefault();
        if (temp == null)
        {
            Friend friend = new Friend();
            friend.First_UserId = Id;
            friend.Second_UserId = id;
            _context.friends.Add(friend);
            _context.SaveChanges();
            return View();
        }
        var profile1 = _context.profiles.Where(p => p.User.Id.Equals(Id)).FirstOrDefault();
        if (profile1 == null)
        {
            return NotFound();
        }
        return View(profile1.Id);
    }
    // [HttpPost]
    // [ValidateAntiForgeryToken]
    [HttpGet]
    public IActionResult Confirm(string id)
    {
        string Id = _userManager.GetUserId(User);
        Friend? temp = _context.friends.Where(f => f.Second_UserId == Id && f.First_UserId==id).FirstOrDefault();
        if (temp != null)
        {
            temp.friend = true;
            temp.pending = false;
            _context.SaveChanges();
            var profile1 = _context.profiles.Where(p => p.User.Id.Equals(Id)).FirstOrDefault();
            if (profile1 == null)
            {
                return NotFound();
            }
            return RedirectToAction("Index","Profile",new {id=profile1.Id});
            
        }
        return RedirectToAction("Index");
    }
}
