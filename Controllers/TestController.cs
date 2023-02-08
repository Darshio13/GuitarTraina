using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProyectoGuitarra.Models;
using System.Text.Encodings.Web;

namespace ProyectoGuitarra.Controllers;

public class TestController : Controller
{
    // 
    // GET: /HelloWorld/
    public string Index()
    {
        //string xd = "XD";
        //HttpContext.Session.Set("Message", );
        //HttpContext.Session.Remove("Message");
        HttpContext.Session.SetString("Message", "OJOOOOO");
        return HttpContext.Session.GetString("Message");
    }
    // 
    // GET: /HelloWorld/Welcome/ 
    public string C1()
    {
        HttpContext.Session.SetString("Message", "Cambio 1");

        return "Cambio hecho";
    }

    public string C2()
    {
        HttpContext.Session.Remove("Message");

        return "Cambio hecho";
    }
}