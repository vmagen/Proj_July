﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace webAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            //RegisterScheduler();
        }


        protected void RegisterScheduler()
        {
            TimeSpan ts = new TimeSpan(0, 0, 30);

            Task.Factory.StartNew(() =>
            {
                ScheduleTask.Trigger(() =>
                {
                    //webAPI.Models.UserPrefrencesModel.updateKnnTables("vmagen@gmail.com", "5", 3);

                }, ts);
            });
        }
    }
}
