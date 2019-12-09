
namespace EmployeeManagementSystem.Controllers
{
    using EmployeeManagementSystem.Helper;
    #region Using
    using EmployeeManagementSystem.Models;
    using EmployeeMangmentSystem.Repository.Models;
    using EmployeeMangmentSystem.Resources;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity.Validation;
    using System.Data.OleDb;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    #endregion


    /// <summary>
    /// SkillController
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    [SessionTimeout]
    public class SkillController : Controller
    {
        // GET: Department
        public async Task<ActionResult> Index()
        {
            try
            {
                var data = await APIHelpers.GetAsync<List<Skills>>("api/Skill/GetSkills");
                if (data == null)
                {
                    data = new List<Skills>();
                }
                return View(data.ToList());
            }
            catch (Exception ex)
            {
                return RedirectToAction("AccessDenied", "Error");
            }

        }

        public FileResult DownloadExcel()
        {
            string path = "/Doc/Users.xlsx";
            return File(path, "application/vnd.ms-excel", "Users.xlsx");
        }

        [HttpPost]
        public async Task<ActionResult> UploadExcel(HttpPostedFileBase FileUpload)
        {

            List<string> data = new List<string>();
            if (Request.Files["FileUpload1"].ContentLength > 0)
            {
                string extension = System.IO.Path.GetExtension(Request.Files["FileUpload1"].FileName).ToLower();
                string connString = "";
                List<Skills> skills = new List<Skills>();
               string[] validFileTypes = { ".xls", ".xlsx", ".csv" };

                string path1 = string.Format("{0}/{1}", Server.MapPath("~/Content/Uploads"), Request.Files["FileUpload1"].FileName);
                if (!Directory.Exists(path1))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Content/Uploads"));
                }
                if (validFileTypes.Contains(extension))
                {
                    if (System.IO.File.Exists(path1))
                    { System.IO.File.Delete(path1); }
                    Request.Files["FileUpload1"].SaveAs(path1);
                    if (extension == ".csv")
                    {
                        skills = Utility.ConvertCSVtoDataTable(path1);
                    }
                    //Connection String to Excel Workbook  
                    else if (extension.Trim() == ".xls")
                    {
                        connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path1 + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        skills = Utility.ConvertXSLXtoDataTable(path1, connString);
                    }
                    else if (extension.Trim() == ".xlsx")
                    {
                        connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        skills = Utility.ConvertXSLXtoDataTable(path1, connString);
                    }
                    foreach (var a in skills)
                    {
                        await APIHelpers.PostAsync<Skills>("api/Skill/Post", new Skills() { Name = a.Name});
                    }
                    //deleting excel file from folder  
                    if ((System.IO.File.Exists(path1)))
                    {
                        System.IO.File.Delete(path1);
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "Please Upload Files in .xls, .xlsx or .csv format";
                    return RedirectToAction("CreateBulk");
                }
            }
            else
            {
                TempData["error"] = CommonResources.error;
                return RedirectToAction("CreateBulk");
            }
        
        }

        public async Task<FileResult> Print()
        {
            try
            {
                var data = await APIHelpers.GetAsync<List<Skills>>("api/Skill/GetSkills");
                var builder = new PdfBuilder<List<Skills>>(data, Server.MapPath("/Views/Skill/Print.cshtml"));
                return builder.GetPdf();
            }
            catch (Exception ex)
            {
                return File("AccessDenied", "Error");
            }
        }

        // GET: Designation/Details/5
        public ActionResult Details(Guid id)
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        // GET: Designation/Create
        public ActionResult Create()
        {
            try
            {
                return View(new Skills());
            }
            catch (Exception ex)
            {
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        public ActionResult CreateBulk()
        {
            try
            {
                return View(new Skills());
            }
            catch (Exception ex)
            {
                return RedirectToAction("AccessDenied", "Error");
            }
        }
        
        // POST: Designation/Create
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(Skills collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // TODO: Add insert logic here
                    if (collection.Id == Guid.Empty)
                    {
                        await APIHelpers.PostAsync<Skills>("api/Skill/Post", collection);
                        TempData["sucess"] = SkillsResources.create;
                    }
                    else
                    {
                        await APIHelpers.PutAsync<Skills>("api/Skill/Put", collection);
                        TempData["sucess"] = SkillsResources.update;
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(collection);
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = CommonResources.error;
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        // GET: Designation/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            try
            {
                return View("Create", await APIHelpers.GetAsync<Skills>("api/Skill/Get/" + id));
            }
            catch (Exception ex)
            {
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        // POST: Designation/Edit/5


        // POST: Designation/Delete/5
        [HttpGet]
        public async Task<ActionResult> DeleteConfirm(string id)
        {
            try
            {
                // TODO: Add delete logic here
                await APIHelpers.DeleteAsync<Skills>("api/Skill/Delete/" + id);
                TempData["sucess"] = SkillsResources.delete;
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["error"] = CommonResources.error;
                return RedirectToAction("AccessDenied", "Error");
            }
        }

        public async Task<JsonResult> PostSkill(string name)
        {
            try
            {
                if(name != "")
                {
                    Skills skill = new Skills
                    {
                        Id = Guid.Empty,
                        Name = name
                    };
                    await APIHelpers.PostAsync<Skills>("api/Skill/Post", skill);
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

    public static class Utility 
    {
        public static List<Skills> ConvertCSVtoDataTable(string strFilePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                string[] headers = sr.ReadLine().Split(',');
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }

                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    if (rows.Length > 1)
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < headers.Length; i++)
                        {
                            dr[i] = rows[i].Trim();
                        }
                        dt.Rows.Add(dr);
                    }
                }

            }


            return ConvertDataTable(dt);
        }
        private static List<Skills> ConvertDataTable(DataTable dt)
        {
            List<Skills> data = new List<Skills>();
            foreach (DataRow row in dt.Rows)
            {
                Skills item = GetItem(row);
                data.Add(item);
            }
            return data;
        }
        private static Skills GetItem(DataRow dr)
        {
            Type temp = typeof(Skills);
            Skills obj = Activator.CreateInstance<Skills>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        public static List<Skills> ConvertXSLXtoDataTable(string strFilePath, string connString)
        {
            OleDbConnection oledbConn = new OleDbConnection(connString);
            DataTable dt = new DataTable();
            try
            {

                oledbConn.Open();
                using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn))
                {
                    OleDbDataAdapter oleda = new OleDbDataAdapter();
                    oleda.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    oleda.Fill(ds);

                    dt = ds.Tables[0];
                }
            }
            catch
            {
            }
            finally
            {

                oledbConn.Close();
            }

            return ConvertDataTable(dt);

        }
    }
}