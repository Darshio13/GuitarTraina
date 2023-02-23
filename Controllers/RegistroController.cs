using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProyectoGuitarra.Models;
using System.Text;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;

namespace ProyectoGuitarra.Controllers;

public class RegistroController : Controller
{

    public IActionResult Index()
    {
        return View("/wwwroot/webPageGT/Invitado/registerAndLogin/register.cshtml");
    }


    [HttpPost]
    public async Task<ActionResult> Index(RegistroModel RegistroInput)
    {
        //Se hace un objeto para las HttpRequest
        using var client = new HttpClient();
        //Se encripta la contraseña con Bcrypt
        RegistroInput.password = BCrypt.Net.BCrypt.HashPassword(RegistroInput.password);
        //POST
        var stringPayload = JsonConvert.SerializeObject(RegistroInput);
        var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
        var httpResponse = await client.PostAsync($"http://webapplication1-dev.eba-kqzerfvq.us-east-1.elasticbeanstalk.com/Users?email={RegistroInput.email}&username={RegistroInput.username}&password={RegistroInput.password}", httpContent);
        var responseContent = httpResponse.Content.ReadAsStringAsync().Result;
        Console.WriteLine(responseContent);
    

        return RedirectToAction("Index", "login");
    }

}