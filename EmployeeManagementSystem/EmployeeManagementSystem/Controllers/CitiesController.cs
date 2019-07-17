﻿using EmployeeMangmentSystem.Repository.Models;
using EmployeeMangmentSystem.Resources;
using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    public class CitiesController : Controller
    {
        // GET: Cities
        public async Task<ActionResult> Index()
        {
            var data = await APIHelpers.GetAsync<List<City>>("api/City/GetCities");
            return View(data.ToList());
        }

        // GET: Cities/Details/5
        public ActionResult Details(Guid id)
        {
            return View();
        }

        // GET: Cities/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.States = await APIHelpers.GetAsync<List<States>>("api/State/GetStates");
            return View();
        }

        // POST: Cities/Create
        [HttpPost]
        public async Task<ActionResult> Create(City collection)
        {
            try
            {
                // TODO: Add insert logic here
                ModelState.Remove("Id");
                if (ModelState.IsValid)
                {
                    if (collection.Id == Guid.Empty)
                    {
                        await APIHelpers.PostAsync<City>("api/City/Post", collection);
                        TempData["sucess"] = CityResources.create;
                    }
                    else
                    {
                        await APIHelpers.PutAsync<City>("api/City/Put", collection);
                        TempData["sucess"] = CityResources.update;
                    }
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: Cities/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            ViewBag.States = await APIHelpers.GetAsync<List<States>>("api/State/GetStates");
            return View("Create", await APIHelpers.GetAsync<City>("api/City/Get/" + id));
        }

        // POST: State/Delete/5
        [HttpGet]
        public async Task<ActionResult> DeleteConfirm(Guid id)
        {
            try
            {
                // TODO: Add delete logic here
                await APIHelpers.DeleteAsync<bool>("api/City/Delete/" + id);
                TempData["sucess"] = CityResources.delete;
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData["error"] = CommonResources.error;
                return RedirectToAction("AccessDenied", "Error");
            }
        }
    }
}
