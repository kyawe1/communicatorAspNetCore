using Microsoft.AspNetCore.Mvc;


namespace communicator.Areas.Admin.Controllers;


[Area("Admin")]
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