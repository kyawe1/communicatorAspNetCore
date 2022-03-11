using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using communicator.Entity;
using communicator.Models;
using communicator.Context;

namespace communicator.Controllers;


public class IdentityController:Controller{
    private readonly ApplicationContext _context;
    private SignInManager<ApplicationUser> _signInManager;
    private UserManager<ApplicationUser> _userManager;

    public IdentityController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,ApplicationContext context)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _context = context;
    }


    [HttpGet]  
    public IActionResult Login(){
        return View();
    } 
    [HttpGet]
    public IActionResult Register(string? next){
         return View();
    }
    [HttpPost] 
    [ValidateAntiForgeryToken] 
    public async Task<IActionResult> Login(LoginViewModel model){
        if(ModelState.IsValid){
            var result =await  _signInManager.PasswordSignInAsync(model.Email,model.Password,model.RememberMe,false);
            if(result.Succeeded){
                return RedirectToAction("Index", "Home");
            }
            else{
                ModelState.AddModelError("", "Invalid login attempt");
            }
        }
            return View();
    } 
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterViewModel model,string? next){
        if(ModelState.IsValid){
            var user=new ApplicationUser(){
                UserName=model.Email,
                Email=model.Email
            };
            var result=_userManager.CreateAsync(user,model.Password).Result;
            if (result.Succeeded){
                Profile profile = new Profile{
                    UserId=user.Id
                };
                _context.profiles.Add(profile);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }else{
                ModelState.AddModelError("","Something Wrong");
            }
        }
        return View(model);
    }
}


