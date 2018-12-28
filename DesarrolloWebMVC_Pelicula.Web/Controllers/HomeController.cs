using DesarrolloWebMVC_Pelicula.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DesarrolloWebMVC_Pelicula.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            RegistroPelicula rp = new RegistroPelicula();
            return View(rp.RecuperarTodos());
        }
    }
}