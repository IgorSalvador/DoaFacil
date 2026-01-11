using DoaFacil.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DoaFacil.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected bool WasSucceeded(Result result)
        {
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error);

                return false;
            }

            return true;
        }
    }
}