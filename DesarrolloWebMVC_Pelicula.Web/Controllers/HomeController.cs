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
            RegistroProducto rp = new RegistroProducto();
            return View(rp.RecuperarTodos());

            //RegistroPelicula rp = new RegistroPelicula();
            //return View(rp.RecuperarTodos());
        }

        public ActionResult Grabar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Grabar(FormCollection coleccion)
        {
            RegistroProducto rp = new RegistroProducto();
            Producto prod = new Producto()
            {
                Descripcion = coleccion["Descripcion"],
                Precio = float.Parse(coleccion["Precio"])
            };
            rp.GrabarProducto(prod);

            //RegistroPelicula rp = new RegistroPelicula();
            //Pelicula peli = new Pelicula
            //{
            //    //Codigo = int.Parse(coleccion["Codigo"]),
            //    Titulo = coleccion["Titulo"],
            //    AutorPrincipal = coleccion["AutorPrincipal"],
            //    Director = coleccion["Director"],
            //    No_Actores = int.Parse(coleccion["No_Actores"]),
            //    Duracion = float.Parse(coleccion["Duracion"]),
            //    Estreno = int.Parse(coleccion["Estreno"])
            //};
            ////UpdateModel(peli, coleccion);
            //rp.GrabarPelicula(peli);

            return RedirectToAction("Index");
        }

        public ActionResult Borrar(int cod)
        {
            RegistroProducto reg = new RegistroProducto();
            reg.Borrar(cod);
            //RegistroPelicula peli = new RegistroPelicula();
            //peli.Borrar(cod);
            return RedirectToAction("Index");
        }

        public ActionResult Modificacion(int cod)
        {
            RegistroProducto rp = new RegistroProducto();
            Producto pro = rp.Recuperar(cod);
            return View(pro);

            //RegistroPelicula rp = new RegistroPelicula();
            //Pelicula peli = rp.Recuperar(cod);
            //return View(peli);
        }
        [HttpPost]
        public ActionResult Modificacion(FormCollection coleccion)
        {
            RegistroProducto rp = new RegistroProducto();
            var rpt = new Producto();
            UpdateModel(rpt, coleccion);

            rp.Modificar(rpt);

            //RegistroPelicula peli = new RegistroPelicula();
            ////Pelicula rpt = new Pelicula
            ////{
            ////    Codigo = int.Parse(coleccion["Codigo"]),
            ////    Titulo = coleccion["Titulo"],
            ////    AutorPrincipal = coleccion["AutorPrincipal"],
            ////    Director = coleccion["Director"],
            ////    No_Actores = int.Parse(coleccion["No_Actores"]),
            ////    Duracion = float.Parse(coleccion["Duracion"]),
            ////    Estreno = int.Parse(coleccion["Estreno"])
            ////};
            //var rpt = new Pelicula();
            //UpdateModel<Pelicula>(rpt, coleccion);

            //peli.Modificar(rpt);

            return RedirectToAction("Index");
        }
    }
}