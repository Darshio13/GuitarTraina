using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProyectoGuitarra.Models;
using System.Text;
using System.Text.Encodings.Web;

namespace ProyectoGuitarra.Controllers;

public class ReporteController : Controller
{
    // 
    // GET: /HelloWorld/
    public IActionResult Index()
    {
        var sessionObj = HttpContext.Session.GetString("UserData");
        //Chequeo de la sesion
        if (sessionObj is not null)
        {
            //Tipo de vista    
            var obj = JsonConvert.DeserializeObject<SessionModel>(sessionObj);

            if (obj.tipo == "Gratis")
            {
                //El usuario tiene un plan gratis
                return View("/wwwroot/webPageGT/Registrado/herramientas/reporteErrores/reporteErrores.cshtml");
            }
            else
            {
                //El usuario tiene un plan Premiun
                return View("/wwwroot/webPageGT/PRO/herramientas/reporteErrores/reporteErrores.cshtml");
            }
        }
        else
        {
            //No hay sesion activa, se regresa al usuario
            return RedirectToAction("Index", "home");
        }

        //return View("/wwwroot/webPageGT/Registrado/herramientas/reporteErrores/reporteErrores.cshtml");
    }

    [HttpPost]
    public async Task<ActionResult> Index(ReporteErrorModel reportedata)
    {
        Console.WriteLine("Help");
        //Objetos de la sesion
        var sessionObj = HttpContext.Session.GetString("UserData");

        var obj = JsonConvert.DeserializeObject<SessionModel>(sessionObj);
        reportedata.user=obj.userName;

        Console.WriteLine(obj.userName);
        //Se hace un objeto para las HttpRequest
        using var client = new HttpClient();

        //Se encripta la contraseña con Bcrypt
        //POST
        var stringPayload = JsonConvert.SerializeObject(reportedata);
        var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

        var httpResponse = await client.PostAsync($"http://webapplication1-dev.eba-kqzerfvq.us-east-1.elasticbeanstalk.com/ErrorReport?title={reportedata.title}&number={reportedata.number}&steps={reportedata.steps}&description={reportedata.description}&user={reportedata.user}", httpContent);
        var responseContent = httpResponse.Content.ReadAsStringAsync().Result;

        Console.WriteLine(responseContent);



        
        

        return RedirectToAction("Index", "home");
    }

}