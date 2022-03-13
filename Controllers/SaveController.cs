using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using communicator.Entity;
using communicator.Context;
using communicator.Models;

namespace communicator.Controllers;

public class SaveController:Controller{
    private readonly ApplicationContext context;
    private readonly UserManager<ApplicationUser> _userManager;
    public SaveController(ApplicationContext context,UserManager<ApplicationUser> userManager){
        this.context=context;
        _userManager=userManager;
    }
    [HttpGet]
    [Authorize]
    public IActionResult Index(){
        var UserId=_userManager.GetUserId(User);
        var saved_lists=context.savedblogs.AsNoTracking().Where(p=> p.UserId == UserId).Include(p=>p.blog).ThenInclude(p=>p.User).OrderByDescending(p=> p.blog.CreaetedAt).Select(p=> new SaveViewModel{
            BlogCreatedAt=p.blog.CreaetedAt,
            BlogId=p.BlogId,
            UserId=p.UserId,
            Title=p.blog.Title,
        }).ToList();
        
        foreach(var item in saved_lists){
            var profile=context.profiles.AsNoTracking().Where(p=> p.UserId==item.UserId).Select(p=> new {id=p.Id,Name=p.DisplayName}).FirstOrDefault();
            item.ProfileId=profile.id;
            item.Name=profile.Name;
        }

        return View(saved_lists);
    }
}