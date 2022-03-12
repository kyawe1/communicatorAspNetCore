using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace communicator.Areas.Admin.Controllers;


[Area("Admin")]
// [Authorize(Roles="Admin")]
public class HomeController : Controller {
    public HomeController(){

    }
    /// <summary>
    ///     this endpoint should return 
    /// overview of your app
    /// </summary>
    /// <returns></returns>
    public IActionResult Index(){
        return View();
    }
}