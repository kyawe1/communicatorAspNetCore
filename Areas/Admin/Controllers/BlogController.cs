using communicator.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using communicator.Context;
using Microsoft.AspNetCore.Identity;
using communicator.Entity;
using Microsoft.EntityFrameworkCore;

namespace communicator.Areas.Admin.Controllers;

/// <summary>
/// this is blog for admin ;
/// </summary>
[Area("Admin")]
public class BlogController : Controller{
    private readonly ApplicationContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public BlogController(ApplicationContext context,UserManager<ApplicationUser> userManager){
        _db=context;
        _userManager=userManager;
    }
    /// <summary>
    /// this must the compelete view
    /// with table and date desending
    /// </summary>
    /// <returns>view with all blog and paginate</returns>
    public IActionResult Index(){
        IEnumerable<BlogViewModel> blogs=_db.Blogs.Include(p=> p.User).Select(blog=>new BlogViewModel(){
            Id=blog.Id,
            Title=blog.Title,
            Content=blog.Content,
            UserId=blog.UserId,
            User=blog.User
        }).ToList();
        return View(blogs);
    }
    /// <summary>
    ///    this is HttpGet ....
    /// </summary>
    /// <returns>view to create Blog .....</returns>
    [HttpGet]
    public IActionResult Create(){
        return View();
    }
    /// <summary>
    ///     this is http post to create the blog...
    /// </summary>
    /// <param name="model">this should be the blog object to create</param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind(new string[] {"Title","Content","User"})]CreateBlog model){
        if(ModelState.IsValid){
            try{
            Blog blog=new Blog(){
                Title=model.Title,
                Content = model.Content,
                UserId=model.User ?? _userManager.GetUserId(User)
            };
            _db.Blogs.Add(blog);
            _db.SaveChanges();
            return RedirectToAction("Index");
            }
            catch(Exception e){
                ModelState.AddModelError("","Something went wrong");
            }
        }
        return View(model);
    }
    /// <summary>
    ///     this is update blog .....
    /// </summary>
    /// <returns>create update view</returns>
    [HttpGet]
    public IActionResult Update(string Id){
        Blog blog=_db.Blogs.FirstOrDefault(p=>p.Id==Id);
        if(blog!=null){
            CreateBlog model=new CreateBlog(){
                Title=blog.Title,
                Content=blog.Content,
                User=blog.UserId
            };
            return View(model);
        }
        return NotFound();
    }
    /// <summary>
    ///     this is update blog.....
    /// </summary>
    /// <param name="model">this should blog .....</param>
    /// <param name="id">this should blog .....</param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(CreateBlog model,string id){
        if(ModelState.IsValid){
            try{
                Blog blog=_db.Blogs.Find(id);
                blog.Title=model.Title;
                blog.Content=model.Content;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception e){
                ModelState.AddModelError("","Something went wrong");
            }
        }
        return View();
    }
    [HttpGet]
    public IActionResult Delete(string id){
        Blog blog=_db.Blogs.Find(id);
        if(blog==null){
            return NotFound();
        }
        _db.Entry(blog).State=Microsoft.EntityFrameworkCore.EntityState.Deleted;
        _db.SaveChanges();
        return RedirectToAction("Index","Blog",new {area="Admin"});
    }
}