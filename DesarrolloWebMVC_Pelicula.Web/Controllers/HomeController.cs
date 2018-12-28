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

        public ActionResult Grabar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Grabar(FormCollection coleccion)
        {
            RegistroPelicula rp = new RegistroPelicula();
            Pelicula peli = new Pelicula
            {
                //Codigo = int.Parse(coleccion["Codigo"]),

                Titulo = coleccion["Titulo"],
                Director = coleccion["Director"],
                AutorPrincipal = coleccion["AutorPrincipal"],
                No_Actores = int.Parse(coleccion["No_Actores"]),
                Duracion = float.Parse(coleccion["Duracion"]),
                Estreno = int.Parse(coleccion["Estreno"])
            };
            rp.GrabarPelicula(peli);
            return RedirectToAction("Index");
        }

        public ActionResult Borrar(int cod)
        {
            RegistroPelicula peli = new RegistroPelicula();
            peli.Borrar(cod);
            return RedirectToAction("Index");
        }

        public ActionResult Modificacion(int cod)
        {
            RegistroPelicula rp = new RegistroPelicula();
            Pelicula peli = rp.Recuperar(cod);
            return View(peli);
        }
        [HttpPost]
        public ActionResult Modificacion(FormCollection coleccion)
        {
            RegistroPelicula peli = new RegistroPelicula();
            Pelicula rpt = new Pelicula
            {
                Codigo = int.Parse(coleccion["Codigo"]),
                Titulo = coleccion["Titulo"],
                AutorPrincipal = coleccion["AutorPrincipal"],
                Director = coleccion["Director"],
                No_Actores = int.Parse(coleccion["No_Actores"]),
                Duracion = float.Parse(coleccion["Duracion"]),
                Estreno = int.Parse(coleccion["Estreno"])
            };
            peli.Modificar(rpt);
            return RedirectToAction("Index");
        }
    }
}