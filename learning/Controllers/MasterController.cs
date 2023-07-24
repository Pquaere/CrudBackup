using HRPayroll.Models;
using learning.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace learning.Controllers
{
    public class MasterController : Controller
    {
        // GET: Master
        Repos Repo = new Repos();
        Common com = new Common();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Student()
        {
            ViewBag.state = Repo.BindState();
            return View();
        }
        [HttpPost]
        public ActionResult Student(Student model)
        {
            ViewBag.state = Repo.BindState();
            Student _iresult = new Student();
            if (model.SId > 0)
            {
              
            }
            else
            {

                _iresult = Repo.InsertStudent(model);

            }
            if (Convert.ToInt32(_iresult.flag) == 1)
            {
                TempData["msg"] = _iresult.msg;
                TempData["code"] = "1";
            }
            else
            {
                TempData["msg"] = _iresult.msg;
                TempData["code"] = "0";
            }
            return View();
        }

        public JsonResult BindCity(string stateId)
        {
            var res = Repo.BindCity(Convert.ToInt32(stateId));
            return Json(res, JsonRequestBehavior.AllowGet);

        }

        public JsonResult UploadFileForRegistration(HttpPostedFileBase File)
        {
            string Dirpath = "~/Content/writereaddata/StudentRegistration/";
            string path = "";
            string filename = File.FileName;
            bool res = false;
            string msg = "";
            if (!Directory.Exists(Server.MapPath(Dirpath)))
            {
                Directory.CreateDirectory(Server.MapPath(Dirpath));
            }
            string ext = Path.GetExtension(File.FileName);
            var status = com.ValidateImagePDF_FileExtWithSize(File, 2048);
            if (status == "Valid")
            {

                filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "_" + filename;
                string completepath = Path.Combine(Server.MapPath(Dirpath), filename);
                if (System.IO.File.Exists(completepath))
                {
                    System.IO.File.Delete(completepath);
                }

                File.SaveAs(completepath);
                path = Dirpath + filename;
                res = true;
            }
            else
            {
                msg = status;
            }
            return Json(new { result = res, fpath = path, mesg = msg });
        }

        public ActionResult Studentlist()
        {
            Student obj = new Student();
             obj.list = Repo.Studentlst();
            return View(obj);
        }

        public ActionResult Album()
        {
            return View();
        }
    }
}