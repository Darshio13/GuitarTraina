using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using ProyectoGuitarra.Models;
using Newtonsoft.Json;
using System.Text;

namespace ProyectoGuitarra.Controllers;

public class RecoverPasswordController : Controller
{
    
   public IActionResult Index()
    {
        
        return View("/wwwroot/webPageGT/Invitado/registerAndLogin/changePass.cshtml");
    }


    [HttpPost]
    public async Task<ActionResult> Index(ChangePasswordModel credentials)
    {
        credentials.email= (string?)TempData["correo"];

        Console.WriteLine(credentials.email);
        Console.WriteLine(credentials.token);
        //Encriptacion del paswword
        credentials.password = BCrypt.Net.BCrypt.HashPassword(credentials.password);


        //Se hace un objeto para las HttpRequest
        using var client = new HttpClient();
        //PUT
        var stringPayload = JsonConvert.SerializeObject(credentials);
        var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
        var httpResponse = await client.PutAsync($"http://webapplication1-dev.eba-kqzerfvq.us-east-1.elasticbeanstalk.com/ChangePassword?email={credentials.email}&token={credentials.token}&password={credentials.password}",httpContent);
        var responseContent = httpResponse.Content.ReadAsStringAsync().Result;

        Console.WriteLine(responseContent); 

        return RedirectToAction("Index", "login");
    }

    
}