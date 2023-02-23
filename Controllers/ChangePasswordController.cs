using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using ProyectoGuitarra.Models;
using Newtonsoft.Json;
using System.Text;

namespace ProyectoGuitarra.Controllers;

public class ChangePasswordController : Controller
{
    // 
    // GET: /HelloWorld/
    public IActionResult Index()
    {
        return View("/wwwroot/webPageGT/Invitado/registerAndLogin/forgotPass.cshtml");
    }
    // 
    // GET: /HelloWorld/Welcome/ 
    [HttpPost]
    public async Task<ActionResult> Index(TokenModel email)
    {
    
        //Se hace un objeto para las HttpRequest
        using var client = new HttpClient();

        //POST
        var stringPayload = JsonConvert.SerializeObject(email);
        var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");
        var httpResponse = await client.PostAsync($"http://webapplication1-dev.eba-kqzerfvq.us-east-1.elasticbeanstalk.com/Tokens?email={email.correo}", httpContent);
        var responseContent = httpResponse.Content.ReadAsStringAsync().Result;
        Console.WriteLine(responseContent); 
        TempData["correo"] = email.correo;

        return RedirectToAction("Index", "recoverpassword");
    }
}