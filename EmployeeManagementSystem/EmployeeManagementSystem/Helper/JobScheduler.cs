using EmployeeMangmentSystem.Repository.Models.ViewModel;
using EmployeeMangmentSystem.Repository.Repository.Classes;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementSystem.Helper
{
    public class JobScheduler  
    {  
        public static void Start()  
        {  
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();  
            scheduler.Start();  
  
            IJobDetail job = JobBuilder.Create<Jobclass>().Build();

            ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("trigger1", "group1")
            .StartNow()
           .WithCronSchedule("0 00 05 1/1 * ? *")
            .Build();  
  
            scheduler.ScheduleJob(job, trigger);  
        }  
    }  
}