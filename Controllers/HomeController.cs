using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProyectoGuitarra.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ProyectoGuitarra.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        //Obtener sesion
        var sessionObj = HttpContext.Session.GetString("UserData");


        if (sessionObj is not null)
        {
            //La sesion existe
            var obj = JsonConvert.DeserializeObject<SessionModel>(sessionObj);
            
            
            //Tipo de vista
            if (obj.tipo == "Gratis")
            {
                //El usuario tiene un plan gratis
                return View("/wwwroot/webPageGT/Registrado/indexRegistrado.cshtml");
            }
            else
            {
                //El usuario tiene un plan Premiun
                return View("MenuPremiun");
            }



        }
        else
        {
            //El usuario es invitado
            return View("/wwwroot/webPageGT/Invitado/indexInvitado.cshtml");

        }


    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
