using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestCookies2.Models;

namespace TestCookies2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string temp)
        {
            string Unit = "";
            string Temp = "";
            //int Temp = 0;
            

            if (temp != null)
            {
                Temp = temp;
            }
            else if (Request.Cookies["Unit"] == null) //takes http request from the web. the Cookie obj Unit doesn't exist with the value 75F 
            {
                //get temp in F
                Temp = "75";
            }
            else //the cookie exists
            {
                Unit = Request.Cookies["Unit"].Value;
                if (Unit == "C")
                {
                    Temp = ((Convert.ToInt32(temp) + 32) * 5 / 9).ToString();
                }
                else if (Unit == "F")
                {
                    Temp = ((Convert.ToInt32(temp) - 32) * 1.8).ToString();
                }
                else
                {
                    Temp = "75";
                }
            }

            ViewBag.Temp = Temp + " " + Unit;
            return View();

        }

        public IActionResult ChangeUnit(string Unit, string Temp) // identifying unit and passing along with the link itself
        {
            // 1. Get the data
            // verify first request
            HttpCookie newCookie; //building cookie and establish base obj HttpCookie
            if (Request.Cookies["Unit"] == null) //takes http request from the web. the Cookie obj Unit doesn't exist with the value 75F 
            {
                //passing in name and value for the cookie
                newCookie = new HttpCookie("Unit", "F"); //newCookie is built with default "F"
            }

            else //the cookie exists
            {
                newCookie = Request.Cookies["Unit"];
            }
            //save the data backs
            newCookie.Value = Unit;
            newCookie.Expires = DateTime.Now.AddMinutes(1); // established cookie "set data" lasts for a min
            Response.Cookies.Add(newCookie); //sending cookie back to client
            return RedirectToAction("Index", new { temp = Temp }); //not returning view, redirects to Index; attaching to the http response 

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


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
