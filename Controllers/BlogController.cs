using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using communicator.Context;
using communicator.Entity;
using Microsoft.AspNetCore.Identity;
using communicator.Models;
using communicator.AuthorizePolicys;

namespace communicator.Controllers;

public class BlogController : Controller
{
    private readonly ApplicationContext context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAuthorizationService _handler;
    public BlogController(ApplicationContext context, UserManager<ApplicationUser> userManager, IAuthorizationService handler)
    {
        this.context = context;
        _userManager = userManager;
        _handler = handler;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Index()
    {
        IEnumerable<BlogViewModel> blogs = context.Blogs.AsNoTracking().Include(p => p.User).Select(i => new BlogViewModel()
        {
            Id = i.Id,
            Title = i.Title,
            Content = i.Content,
            AuthorId = i.User.Id,
            CreatedAt = i.CreaetedAt,
            AuthorEmail=i.User.UserName
        }).OrderBy(p => p.CreatedAt).ToList();

        foreach(var i in blogs)
        {
            Profile p=context.profiles.Where(p=>p.UserId==i.AuthorId).First();
            i.ProfileId=p.Id;
            i.Name=p.DisplayName;
        }
        return View(blogs);
    }
    /// <summary>
    /// It's detail for blog
    /// </summary>
    /// <param name="id"> this is blog id</param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Detail(string id)
    {
        BlogViewModel? blog = context.Blogs.AsNoTracking().Where(p=> p.Id==id).Include(p => p.User).Select(i => new BlogViewModel()
        {
            Id = i.Id,
            Title = i.Title,
            Content = i.Content,
            AuthorId = i.User.Id,
            CreatedAt = i.CreaetedAt,
            AuthorEmail=i.User.UserName
        }).FirstOrDefault();
        if(blog == null ){
            return NotFound();
        }
        Profile p=context.profiles.Where(p=>p.UserId==blog.AuthorId).First();
        blog.ProfileId=p.Id;
        blog.Name=p.DisplayName;
        return View(blog);
    }

    [HttpGet]
    [Authorize]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public IActionResult Create([Bind(new string[] { "Title", "Content" })] BlogCreateViewModel model)
    {
        if (ModelState.IsValid)
        {
            Blog blog = new Blog()
            {
                Title = model.Title,
                Content = model.Content,
                UserId = _userManager.GetUserId(User)
            };
            context.Blogs.Add(blog);
            context.SaveChanges();
            return RedirectToAction("Index", "Blog");
        }
        return View(model);
    }
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Update(string Id)
    {
        BlogCreateViewModel? model;
        Blog? blog = context.Blogs.Find(Id);
        if (blog == null)
        {
            return NotFound();
        }
        if ((await _handler.AuthorizeAsync(HttpContext.User, blog, "SameUser")).Succeeded)
        {
            model = new BlogCreateViewModel
            {
                Title = blog.Title,
                Content = blog.Content
            };
            return View(model);
        }
        return NotFound();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize()]
    public async Task<IActionResult> Update([Bind(new string[] { "Title", "Content" })] BlogCreateViewModel model, string Id)
    {
        if (ModelState.IsValid)
        {
            Blog? blog = context.Blogs.Find(Id);
            if (blog == null)
            {
                return NotFound();
            }
            if ((await _handler.AuthorizeAsync(HttpContext.User, blog, "SameUser")).Succeeded)
            {
                blog.Title = model.Title;
                blog.Content = model.Content;
                context.Entry(blog).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index", "Blog");
            }

        }
        return View(model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize()]
    public async Task<IActionResult> Delete(string Id)
    {
        if (ModelState.IsValid)
        {
            Blog? blog = context.Blogs.Find(Id);
            if (blog == null)
            {
                return NotFound();
            }
            if ((await _handler.AuthorizeAsync(HttpContext.User, blog, "SameUser")).Succeeded)
            {
                context.Entry(blog).State = EntityState.Deleted;
                context.SaveChanges();
                return RedirectToAction("Index", "Blog");
            }
        }
        return BadRequest();
    }

    // [HttpPost]
    // [ValidateAntiForgeryToken]
    [HttpGet]
    public IActionResult Save(string id){
        var blog=context.Blogs.Where(p=> p.Id==id).FirstOrDefault();
        if(blog == null ){
            return NotFound();
        }
        Save save=new Save(){
            BlogId=id,
            UserId=_userManager.GetUserId(User)
        };
        context.savedblogs.Add(save);
        context.SaveChanges();
        return Ok();
    }
}
