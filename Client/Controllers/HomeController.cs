using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Client.Models;
using IdentityModel.Client;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        public async Task<IActionResult> RequestToken()
        {
            var discover = await DiscoveryClient.GetAsync("http://localhost:5000");
            if (discover.IsError)
            {
                Console.WriteLine(discover.Error);
            }
            var tokenClient = new TokenClient(discover.TokenEndpoint, "client", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("API");
            if(tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
            }
            return View(tokenResponse);
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
