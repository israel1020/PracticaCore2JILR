using Microsoft.AspNetCore.Mvc;
using PracticaCore2JILR.Models;
using PracticaCore2JILR.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticaCore2JILR.ViewComponents
{
    public class MenuGenerosViewComponent:ViewComponent
    {
        private RepositoryLibros repository;
        public MenuGenerosViewComponent(RepositoryLibros repository)
        {
            this.repository = repository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Genero> series = this.repository.GetGeneros();
            return View(series);
        }
    }
}
