﻿using System.Web.Mvc;

namespace VoKhoiNamHai_Web_SaleDb.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get => "Admin";
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new {controller = "Dashboard" ,action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}