using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticaCore2JILR.Extensions;
using PracticaCore2JILR.Filters;
using PracticaCore2JILR.Models;
using PracticaCore2JILR.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaCore2JILR.Controllers
{
    public class LibrosController : Controller
    {
        private RepositoryLibros repository;
        public LibrosController(RepositoryLibros repository)
        {
            this.repository = repository;
        }
        public IActionResult Libros()
        {
            List<Libro> libros = this.repository.GetLibros();
            return View(libros);
        }
        public IActionResult Details(int idlibro)
        {
            Libro libro = this.repository.FindLibro(idlibro);
            
            return View(libro);
        }
        public IActionResult AñadirCarrito(int? idlibro)
        {
            if (idlibro != null)
            {
                List<int> listIdLibro;
                if (HttpContext.Session.GetString("IDLIBROS") == null)
                {
                    listIdLibro = new List<int>();
                }
                else
                {
                    listIdLibro =
                        HttpContext.Session.GetObject<List<int>>("IDLIBROS");
                }
                listIdLibro.Add(idlibro.Value);
                HttpContext.Session.SetObj("IDLIBROS", listIdLibro);
            }
            return RedirectToAction("Libros");

        }
        public IActionResult DetailsGeneros(int idgenero)
        {
            List<Libro> libros = this.repository.GetLibrosGenero(idgenero);
            return View(libros);
        }
        [AuthorizeUsuarios]
        public IActionResult PerfilUsuario()
        {
            return View();
        }
        public IActionResult Carrito(int? ideliminar)
        {
            
            List<int> listIdLibros = HttpContext.Session.GetObject<List<int>>("IDLIBROS");
            if(listIdLibros == null)
            {
                ViewData["ERROR"] = "NO HAY";
                return View();
            }
            else
            {
                if(ideliminar != null)
                {
                    listIdLibros.Remove(ideliminar.Value);
                    if(listIdLibros.Count == 0)
                    {
                        HttpContext.Session.SetObj("IDLIBROS", null);
                        HttpContext.Session.Remove("IDLIBROS");
                    }
                    else
                    {
                        HttpContext.Session.SetObj("IDLIBROS", listIdLibros);
                    }
                }
               
                List<Libro> libros = this.repository.GetLibrosSesion(listIdLibros);
                return View(libros);
            }
        }
        [AuthorizeUsuarios]
        public IActionResult Comprar()
        {
            return View();
        }
    }
}
