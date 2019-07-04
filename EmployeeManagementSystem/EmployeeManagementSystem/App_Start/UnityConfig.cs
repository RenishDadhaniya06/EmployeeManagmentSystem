using EmployeeManagementSystem.Controllers;
using EmployeeMangmentSystem.Repository.Models;
using EmployeeMangmentSystem.Repository.Repository.Classes;
using EmployeeMangmentSystem.Repository.Repository.Interfaces;
using EmployeeMangmentSystem.Repository.StoreProcedureService;
using EmployeeMangmentSystem.Services.Services;
using EmployeeMangmentSystem.Services.Services.Classes;
using System.Web.Http;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.WebApi;

namespace EmployeeManagementSystem
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<IRepository<Customer>, Repository<Customer>>();
            container.RegisterType<ICustomerService, CustomerService>();
            container.RegisterType<IDataRepositoryContext, RepositoryContext>();
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<UserController>(new InjectionConstructor());
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));

            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}