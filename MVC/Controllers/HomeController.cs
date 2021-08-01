using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private static string WebAPIURL = "http://localhost:58108/";


        public async Task<ActionResult> IndexAsync()
        {
            var tokenBased = new HttpClient();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(WebAPIURL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var responseMessage = await client.GetAsync("Account/ValidLogin?userName=admin&userPassword = admin");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultMessage = responseMessage.Content.ReadAsStringAsync().Result;
                    tokenBased = JsonCovert.DeserializeObject<string>(resultMessage);
                    Session["TokenNumber"] = tokenBased;
                    Session["UserName"] = "admin";

                }
            }
            return Content(tokenBased);

        }
        public async Task<ActionResult> GetEmployee()
        {
            string ReturnMessage = String.Empty;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.BaseAddress = new Uri(WebAPIURL);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["TokenNumber"].ToString() + "Admin5");
                var responseMessage = await client.GetAsync("Account/GetEmployee");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultMessage = responseMessage.Content.ReadAsStringAsync().Result;
                    ReturnMessage = JsonConvert.DeserializeObject<string>(resultMessage);
                }
            }
            return Content(ReturnMessage);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        private ActionResult Content(HttpClient tokenBased)
        {
            throw new NotImplementedException();
        }
    }
}