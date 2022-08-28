using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using CalculatorService;
using System.Web.Http.Cors;

namespace WebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CalculatorController : Controller
    {
        private ExtensibilityProvider provider;

        public CalculatorController()
        {
            //  The path to Extensions dll that created in Extended Operation project
            //  Please see Project option/Build/Output path

            //string path = @"D:\PROJECTS\NET\Calc\WebAPI\bin\Extensions";
            string path = ConfigurationManager.AppSettings["extensionPath"];
            provider = new ExtensibilityProvider(path);
        }
        public JsonResult GetCalculatorOperations()
        {
            try
            {
                //  If cannot configure app.config use the HttpContext to provide current path of Extensions dll
                //var provider = new ExtensibilityProvider(ControllerContext.HttpContext.Server.MapPath("~//bin//Extensions"));
                List<string> result = provider.Calculator?.GetOperations();                
                return Json(new { Status = "OK", result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = ex.Message;
                return Json(new { Status = "Fail", result }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetOperationResult(char operation, int left, int right)
        {
            try
            {
                //  If cannot configure app.config use the HttpContext to provide current path of Extensions dll
                //var provider = new ExtensibilityProvider(ControllerContext.HttpContext.Server.MapPath("~//bin//Extensions"));
                string result = provider.Calculator?.Calculate(operation, left, right);
                return Json(new { Status = "OK", result }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string result = ex.Message;
                return Json(new { Status = "Fail", result }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}