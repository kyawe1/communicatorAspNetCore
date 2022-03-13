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
        //all Friends
        // IEnumerable<Friend> friends = _context.friends.Where(f => f.First_UserId == id || f.Second_UserId==id && f.friend==true).Include(p => p.Second_User).Include(p=> p.First_User).ToList();
        // List
        //khan ya tae kaung
        IEnumerable<FriendViewModel> friends2 = _context.friends.Where(f => f.Second_UserId == id && f.friend == false).Include(p => p.First_User).Select(p => new FriendViewModel
        {
            Id = p.Id,
            Sender_UserId = p.First_UserId,
        }).ToList();
        foreach (var i in friends2)
        {
            var profile_small = _context.profiles.Where(p => p.UserId == i.Sender_UserId).Select(p => new
            {
                Profile_Name = p.DisplayName,
                Profile_Id = p.Id,
                // Profile_Pic=p.Profile_Pic
            }).FirstOrDefault();
            if (profile_small != null)
            {
                i.Profile_Name = (string)profile_small.Profile_Name;
                i.Profile_Id = profile_small.Profile_Id;
            }
        }
        // foreach(var q in friends){
        //     if(q.First_UserId==id){
        //         var profile_small = _context.profiles.Where(p => p.UserId == i.Sender_UserId).Select(p => new
        //         {
        //             Profile_Name = p.DisplayName,
        //             Profile_Id = p.Id,
        //             // Profile_Pic=p.Profile_Pic
        //         }).FirstOrDefault();
                
        //         if (profile_small != null)
        //         {
        //             var tempFriendView=new FriendViewModel(){
        //                 Id=q.Id,
        //                 Sender_UserId=q.Second_UserId,
        //                 Profile_Name=profile_small.Profile_Name,
        //                 Profile_Id=profile_small.Profile_Id,
        //                 // Profile_Pic=profile_small.Profile_Pic
        //             };
                    
        //         }
        //     }
        // }
        // IEnumerable<Friend> friends3 = friends.Concat(friends2);
        return View(friends2);
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
        Friend? temp = _context.friends.Where(f => f.Second_UserId == Id && f.First_UserId == id).FirstOrDefault();
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
            return RedirectToAction("Index", "Profile", new { id = profile1.Id });

        }
        return RedirectToAction("Index");
    }
    [HttpGet]
    public IActionResult Cencel(string id)
    {
        Friend? temp = _context.friends.Find(id);
        if (temp != null)
        {
            _context.Entry(temp).State=EntityState.Deleted;
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }
    [HttpGet]
    [Authorize]
    public IActionResult ShowFriends(){
        return View();
    }
}
