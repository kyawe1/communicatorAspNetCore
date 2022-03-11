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
        var saved_lists=context.savedblogs.AsNoTracking().Where(p=> p.UserId == UserId).Include(p=>p.blog).ThenInclude(p=>p.User).OrderByDescending(p=> p.blog.CreaetedAt).ToList();
        return View(saved_lists);
    }
}