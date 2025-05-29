using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Ocsp;

public class ValidarCodigoController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        var email = TempData["EmailUsuario"] as string;

        var model = new ValidarCodigoModel
        {
            Email = email
        };

        TempData.Keep("EmailUsuario");
        return View(model);
    }
}
