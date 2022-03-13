using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using communicator.Context;
using communicator.Models;
using communicator.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



namespace communicator.Controllers;

public class ProfileController : Controller
{
    private readonly ApplicationContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    public ProfileController(ApplicationContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    /// <summary>
    ///     0 is not friend
    ///     1 is friend
    ///     2 is pending
    ///     3 is requested
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Index(string? id)
    {
        if (id != null)
        {
            if (User.Identity.IsAuthenticated)
            {
                string user = _userManager.GetUserId(User);
                Friend? friend = _context.friends.Where(p => p.First_UserId == user || p.Second_UserId == user).FirstOrDefault();
                if (friend == null)
                {
                    ViewData["friend"] = 0;
                }
                else
                {
                    if (friend.pending == false && friend.friend == true)
                    {
                        ViewData["friend"] = 1;
                    }
                    else
                    {
                        if (friend.First_UserId == user && friend.pending == true)
                        {
                            ViewData["friend"] = 2;
                        }
                        else
                        {
                            ViewData["friend"] = 3;
                        }
                    }
                }
            }
            ProfileViewModel? profile = _context.profiles.Include(p => p.User).Where(p => p.Id.Equals(id)).Select(i => new ProfileViewModel
            {
                UserId = i.UserId,
                DisplayName = i.DisplayName,
                address = i.address,
                Date_Of_Birth = i.Date_Of_Birth,
                Email = i.User.Email,
                PhoneNumber = i.User.PhoneNumber
            }).FirstOrDefault();
            if (profile == null)
            {
                return RedirectToAction("Index", "Blog", new { id = id });
            }
            return View(profile);
        }
        var profile1 = _context.profiles.Where(p => p.User.Id.Equals(_userManager.GetUserId(User))).Include(p => p.User).Select(i => new ProfileViewModel
        {

            DisplayName = i.DisplayName,
            address = i.address,
            Date_Of_Birth = i.Date_Of_Birth,
            Email = i.User.Email,
            PhoneNumber = i.User.PhoneNumber

        }).FirstOrDefault();
        if (profile1 == null)
        {
            return RedirectToAction("Create", "Profile");
        }
        return View(profile1);
    }
    [HttpGet]
    [Authorize]
    public IActionResult Create()
    {
        string Id = _userManager.GetUserId(User);
        Profile? profile = _context.profiles.Where(p => p.UserId.Equals(Id)).FirstOrDefault();
        if (profile != null)
        {
            return RedirectToAction("Index", "Profile", new { id = profile.Id });
        }
        return View();
    }
    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind(new string[] { "Date_Of_Birth", "DisplayName", "address" })] ProfileCreateViewModel model)
    {
        if (ModelState.IsValid)
        {
            Profile profile = new Profile
            {
                Date_Of_Birth = model.Date_Of_Birth,
                DisplayName = model.DisplayName,
                address = model.address,
                UserId = _userManager.GetUserId(User)
            };
            _context.profiles.Add(profile);
            _context.SaveChanges();
            return RedirectToAction("Index", "Profile", new { id = profile.Id });
        }
        return View(model);
    }
    [HttpGet]
    [Authorize]
    public IActionResult Update()
    {
        string Id = _userManager.GetUserId(User);
        ProfileCreateViewModel? profile = _context.profiles.Where(p => p.UserId.Equals(Id)).Select(i => new ProfileCreateViewModel
        {
            Date_Of_Birth = i.Date_Of_Birth,
            DisplayName = i.DisplayName,
            address = i.address
        }).FirstOrDefault();
        if (profile == null)
        {
            return RedirectToAction("Create", "Profile");
        }
        return View(profile);
    }
    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public IActionResult Update([Bind(new string[] { "Date_Of_Birth", "DisplayName", "address" })] ProfileCreateViewModel model)
    {
        if (ModelState.IsValid)
        {
            string Id = _userManager.GetUserId(User);
            Profile profile = _context.profiles.Where(p => p.UserId.Equals(Id)).First();
            profile.DisplayName = model.DisplayName;
            profile.Date_Of_Birth = model.Date_Of_Birth;
            profile.address = model.address;
            _context.Entry(profile).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index", "Profile", new { id = profile.Id });
        }
        return View(model);
    }

}
