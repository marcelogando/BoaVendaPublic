using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BoaVenda.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity.Name != null)
            {
                return View();
            }
            else
            {
                return BadRequest("Por favor, primeiro entre na sua conta para poder acessar a dashboard");
            }
        }
    }
}