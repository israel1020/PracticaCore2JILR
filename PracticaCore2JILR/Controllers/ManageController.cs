using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PracticaCore2JILR.Models;
using PracticaCore2JILR.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PracticaCore2JILR.Controllers
{
    public class ManageController : Controller
    {
        private RepositoryLibros repository;
        public ManageController(RepositoryLibros repository)
        {
            this.repository = repository;
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(string email, string password)
        {
            Usuario usuario = this.repository.FindUsuario(email, password);
            if(usuario != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    ClaimTypes.Name, ClaimTypes.NameIdentifier);
                Claim claimName = new Claim(ClaimTypes.Name, usuario.Nombre + " " + usuario.Apellido);
                Claim claimId = new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString());
                Claim claimEmail = new Claim("Email", usuario.Email);
                Claim claimFoto = new Claim("Foto", usuario.Foto);
                Claim claimPass = new Claim("Pass", usuario.Password);
                identity.AddClaim(claimName);
                identity.AddClaim(claimId);
                identity.AddClaim(claimPass);
                identity.AddClaim(claimFoto);
                identity.AddClaim(claimEmail);
                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    userPrincipal);
                string controller = TempData["controller"].ToString();
                string action = TempData["action"].ToString();
                return RedirectToAction(action, controller);
            }
            else
            {
                ViewData["Mensaje"] = "Usuario/Password incorrectos";
            }
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult ErrorAcceso()
        {
            return View();
        }
    }
}
