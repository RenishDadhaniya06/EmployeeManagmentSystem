﻿
namespace EmployeeManagementSystem.Controllers
{
    using EmployeeManagementSystem.Helper;
    #region Using
    using EmployeeManagementSystem.Models;
    using EmployeeMangmentSystem.Repository.Models;
    using EmployeeMangmentSystem.Repository.Models.ViewModel;
    using Helpers;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    #endregion


    /// <summary>
    /// ResourceController
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class ResourceController : Controller
    {
        // GET: Resource
        public async Task<ActionResult> Index()
        {
            ViewBag.Skills = await APIHelpers.GetAsync<List<Skills>>("api/Skill/GetSkills");
            return View();
        }

        //public async Task<ActionResult> DisplayResources(Guid id)
        //{
        //    try
        //    {
        //        ResourceViewModel model = new ResourceViewModel();
        //        ViewBag.Skills = await APIHelpers.GetAsync<List<Skills>>("api/Skill/GetSkills");
        //        var data = await APIHelpers.GetAsync<List<EmployeeUserViewModel>>("api/Resource/GetAvailableResources/" + id);
        //        model.EmployeeUserViewModels = data;
        //        model.Resource = id;
        //        return View("Index", model);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        public async Task<ActionResult> DisplayResources(ResourceViewModel model1)
        {
            try
            {
                ResourceViewModel model = new ResourceViewModel();
                ViewBag.Skills = await APIHelpers.GetAsync<List<Skills>>("api/Skill/GetSkills");
                var data = await APIHelpers.GetAsync<List<EmployeeUserViewModel>>("api/Resource/GetAvailableResources?id=" + model1.Resource + "&workingid=" + Convert.ToBoolean(model1.IsCurrentlyWorking));
                model.EmployeeUserViewModels = data;
                model.Resource = model1.Resource;
                model.IsCurrentlyWorking = model1.IsCurrentlyWorking;
                return View("Index", model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<FileResult> Print(Guid id)
        {
            try
            {
                var data = await APIHelpers.GetAsync<List<EmployeeUserViewModel>>("api/Resource/GetAvailableResources/" + id);
                var builder = new PdfBuilder<List<EmployeeUserViewModel>>(data, Server.MapPath("/Views/Resource/Print.cshtml"));
                return builder.GetPdf();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}