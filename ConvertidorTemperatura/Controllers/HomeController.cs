using System.Web.Mvc;

namespace ConvertidorTemperatura.Controllers
{
    public class HomeController : Controller
    {
        ServiceReferenceTemp.TempConvertSoap ws = new ServiceReferenceTemp.TempConvertSoapClient();
        public ActionResult Index(string result)
        {
            ViewBag.resultado = result;
            return View();
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
        [HttpPost]
        public ActionResult Convertir(FormCollection objeto)
        {
            if(objeto["temperatura"] == "" || objeto["convertirTemp"] == null)
            {
                return RedirectToAction("Index", "Home", new { @result = "Llena los campos requeridos" });
            }

            if (int.Parse(objeto["temperatura"]) < 0)
            {
                return RedirectToAction("Index", "Home", new { @result = "Dato no valido" });
            }

            string temperatura = objeto["temperatura"];
            string final;

            if (objeto["convertirTemp"] == "1")
            {
                string cf = ws.CelsiusToFahrenheit(temperatura);
                final = temperatura + " grados celsius son " + cf + " farenheit";
                return RedirectToAction("Index", "Home", new { @result = final });
            }

            if (int.Parse(objeto["temperatura"]) < 32)
            {
                return RedirectToAction("Index", "Home", new { @result = "Dato no valido" });
            }

            string fc = ws.FahrenheitToCelsius(temperatura);
            final = temperatura + " grados farenheit son " + fc + " celsius";

            return RedirectToAction("Index", "Home", new { @result = final });
        }
    }
}