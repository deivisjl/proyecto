using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyecto.App_Start;
using proyecto.ViewModels;
using System.Net.Mail;
using Rotativa.MVC;

namespace proyecto.Controllers
{
    public class DefaultController : Controller
    {
        private Usuario usuario = new Usuario();

        public ActionResult Index()
        {
            return View(usuario.Obtener(FrontOfficeStartup.UsuarioVisualizando(), true));
        }

        public JsonResult EnviarCorreo(ContactoViewModel model)
        {
            var rm = new ResponseModel();
            if (ModelState.IsValid)
            {
                try
                {
                var _usuario = usuario.Obtener(FrontOfficeStartup.UsuarioVisualizando());

                var mail = new MailMessage();
                mail.From = new MailAddress(model.Correo, model.Nombre);
                mail.To.Add(_usuario.Email);
                mail.Subject = "Correo desde contacto";
                mail.IsBodyHtml = true;
                mail.Body = model.Mensaje;

                var SmtpServer = new SmtpClient("smtp.gmail.com");
                SmtpServer.Port = 587;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("deivisjl@gmail.com","(Dvsjslpzprz91)");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                }
                catch (Exception e)
                {
                    rm.SetResponse(false, e.Message);
                    return Json(rm);
                    throw;
                }
                rm.SetResponse(true);
                rm.function = "CerrarContacto()";
            }
            return Json(rm);
        }

        public ActionResult ExportaAPDF()
        {
            return new Rotativa.MVC.ActionAsPdf("PDF");
            
        }
        public ActionResult PDF()
        {
            return View(
                    usuario.Obtener(FrontOfficeStartup.UsuarioVisualizando(),true)
                );
        }
        
	}
}