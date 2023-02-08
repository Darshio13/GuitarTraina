using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProyectoGuitarra.Models;
using System.Text.Encodings.Web;

namespace ProyectoGuitarra.Controllers;

public class LoginController : Controller
{

    public IActionResult Index()
    {
        return View("/wwwroot/webPageGT/Invitado/registerAndLogin/login.cshtml");
    }

    [HttpPost]
    public async Task<ActionResult> Index(LoginForm loginInputs)
    {
        //Parametros de la sesión
        string? correo = loginInputs.MailInput;
        string? password = loginInputs.PasswordInput;

        //Request a la Api
        try
        {
            //Se hace request A Login con los datos de formulario
            using var client = new HttpClient();
            var GetLogin = await client.GetStringAsync($"http://webapplication1-dev.eba-kqzerfvq.us-east-1.elasticbeanstalk.com/LogIn?email={correo}&password={password}");

            //Las credenciales coinciden
            if (GetLogin is not null)
            {
                //Se usa el correo para sacar los datos del usuario en GET User
                var GetUser = await client.GetStringAsync($"http://webapplication1-dev.eba-kqzerfvq.us-east-1.elasticbeanstalk.com/Users?email={correo}");
                Console.WriteLine(GetUser);

                if (GetUser is not null)
                {

                //Se transforma el Json en un objeto de la clase SessionModel
                SessionModel? CurrentSession = JsonConvert.DeserializeObject<SessionModel>(GetUser);
                //Guardar sesión
                var str = JsonConvert.SerializeObject(CurrentSession);
                HttpContext.Session.SetString("UserData", str);
                }

                    
            }

        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            return RedirectToAction("Index", "login");
        }

        //Redirigir al Inicio
        return RedirectToAction("Index", "Home");

    }


}