
namespace EmployeeManagementSystem
{
    #region Using
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
    #endregion


    /// <summary>
    /// UnityConfig
    /// </summary>
    public static class UnityConfig
    {
        #region Method
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<IRepository<Designation>, Repository<Designation>>();
            container.RegisterType<IRepository<Customer>, Repository<Customer>>();
            container.RegisterType<IRepository<Templates>, Repository<Templates>>();
            container.RegisterType<IRepository<TemplatesType>, Repository<TemplatesType>>();
            container.RegisterType<IRepository<States>, Repository<States>>();
            container.RegisterType<IRepository<City>, Repository<City>>();
            container.RegisterType<IRepository<Departments>, Repository<Departments>>();
            container.RegisterType<IRepository<Technologies>, Repository<Technologies>>();
            container.RegisterType<IRepository<Countries>, Repository<Countries>>();
            container.RegisterType<IRepository<Skills>, Repository<Skills>>();
            container.RegisterType<IRepository<Notifications>, Repository<Notifications>>();
            container.RegisterType<IRepository<Openings>, Repository<Openings>>();
            container.RegisterType<IRepository<Candidates>, Repository<Candidates>>();
            container.RegisterType<IRepository<CandidateSkills>, Repository<CandidateSkills>>();
            container.RegisterType<IRepository<CandidateTechnologies>, Repository<CandidateTechnologies>>();
            container.RegisterType<IRepository<SettingView>, Repository<SettingView>>();
            container.RegisterType<IRepository<PermissionModules>, Repository<PermissionModules>>();
            container.RegisterType<IRepository<RolePermission>, Repository<RolePermission>>();
            container.RegisterType<IRepository<Employee>, Repository<Employee>>();
            container.RegisterType<IRepository<Leave>, Repository<Leave>>();
            container.RegisterType<IRepository<Interviewers>, Repository<Interviewers>>();
            container.RegisterType<IRepository<Interviews>, Repository<Interviews>>();
            container.RegisterType<IRepository<Report>, Repository<Report>>();
            container.RegisterType<IRepository<ReportFilter>, Repository<ReportFilter>>();
            container.RegisterType<IRepository<UserViewModel>, Repository<UserViewModel>>();
            container.RegisterType<ICustomerService, CustomerService>();
            container.RegisterType<IDataRepositoryContext, RepositoryContext>();
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<UserController>(new InjectionConstructor());
            container.RegisterType<EmployeeController>(new InjectionConstructor());
            //container.RegisterType<TemplateTypeController>(new InjectionConstructor);
            //container.RegisterType<LeaveController>(new InjectionConstructor);
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));

            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
        #endregion
    }
}