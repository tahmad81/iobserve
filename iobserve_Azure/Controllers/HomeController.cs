﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Web.Mvc;
using iobserve.Data;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
namespace iobserve_Azure.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Dashboard()
        {

            var startDate = DateTime.Today.AddDays(-1);
            var endData = DateTime.Today;
            SetSession(startDate, endData);
            return View(new Models.DashboardModel() { Duration = 3, StartDate = DateTime.Today.AddDays(-1), Enddate = DateTime.Today });
        }
        public ActionResult _LoadObservations([DataSourceRequest] DataSourceRequest request)
        {
            var data = Load_Observations();
            return Json( data.ToDataSourceResult(request),JsonRequestBehavior.AllowGet);

        }
        public ActionResult _LoadObservationPartialView()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("Observations");
            }
            return null;

        }
        #region Private Methods
        private void SetSession(DateTime startDate, DateTime endData)
        {
            using (iobserverContext context = new iobserverContext())
            {
                var data = context.V_reports.Where(a => a.Language_code == "en" && a.__createdAt >= startDate && a.__createdAt <= endData).ToList();

                System.Web.HttpContext.Current.Session.Add("data", data);
            }
        }
        private List<Models.Observations> Load_Observations()
        {
            var data = System.Web.HttpContext.Current.Session["data"] as List<V_report>;
            if (data != null)
            {
                var modeldata = (from obs in data
                                 select new Models.Observations
                                 {

                                     Date = obs.__createdAt.Date,
                                     Risk_Eliminated = "Yes",
                                     Risk_Level = obs.Risklevel,
                                     Risk_Type = obs.Scenario_Name,
                                     Supervisor_Notified = obs.Supnotified
                                 }).ToList();

                return modeldata;
            }
            return null;
        }
        #endregion
        [HttpPost()]
        public ActionResult Dashboard(Models.DashboardModel model)
        {
            DateTime baseDate = DateTime.Today;

            var today = baseDate;
            var yesterday = baseDate.AddDays(-1);
            var thisWeekStart = baseDate.AddDays(-(int)baseDate.DayOfWeek);
            var thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1);
            var lastWeekStart = thisWeekStart.AddDays(-7);
            var lastWeekEnd = thisWeekStart.AddSeconds(-1);
            var thisMonthStart = baseDate.AddDays(1 - baseDate.Day);
            var thisMonthEnd = thisMonthStart.AddMonths(1).AddSeconds(-1);
            var lastMonthStart = thisMonthStart.AddMonths(-1);
            var lastMonthEnd = thisMonthStart.AddSeconds(-1);

            switch (model.Duration)
            {
                case 1:
                    SetSession(model.StartDate, model.Enddate);
                    break;
                case 2:
                    SetSession(thisWeekStart, DateTime.Today);
                    break;
                case 3:
                    SetSession(thisMonthStart, DateTime.Today);
                    break;
                case 4:
                    SetSession(lastWeekStart, lastWeekEnd);
                    break;
                case 5:
                    SetSession(lastMonthStart, lastMonthEnd);
                    break;


            }

            return View(model);
        }
        #region Charts
        public ActionResult _RiskLevelData()
        {
            List<Models.RiskLevel> modelList = new List<Models.RiskLevel>();
            using (iobserverContext context = new iobserverContext())
            {
                var startDate = DateTime.Today.AddDays(-30);
                var endData = DateTime.Today;
                // var data = context.V_reports.Where(a => a.Language_code == "en").ToList();
                var data = System.Web.HttpContext.Current.Session["data"] as List<V_report>;
                var disRisksLevel = data.Select(a => a.Risklevel).Distinct().ToList().OrderBy(o => o);

                foreach (var rlevel in disRisksLevel)
                {
                    var hcount = data.Where(aa => aa.Risklevel == rlevel && aa.Scenario_Name == "Hazard").Count();
                    var bcount = data.Where(aa => aa.Risklevel == rlevel && aa.Scenario_Name == "Risky Behavior").Count();
                    var gjobcount = data.Where(aa => aa.Risklevel == rlevel && aa.Scenario_Name == "Good Job").Count();

                    modelList.Add(new Models.RiskLevel() { Risk_Level = rlevel, Hazard_Count = hcount, Behv_Count = bcount, GoodJob_Count = gjobcount });
                }
            }

            return Json(modelList);
        }

        public ActionResult _StatusData()
        {
            var data = System.Web.HttpContext.Current.Session["data"] as List<V_report>;
            int cnt = data.Count;
            var subdata = data.Where(a => a.Status_text == "Submitted").Count();
            var review = data.Where(a => a.Status_text == "Reviewed").Count();
            var resoled = data.Where(a => a.Status_text == "Resolved").Count();
            var pend = data.Where(a => a.Status_text == "Pending").Count();
            List<Models.StatusModel> model = new List<Models.StatusModel>();
            try
            {
                model.Add(new Models.StatusModel { Status = "Submitted", Percent_Value = (subdata * 100) / cnt });
                model.Add(new Models.StatusModel { Status = "Reviewed", Percent_Value = (review * 100) / cnt });
                model.Add(new Models.StatusModel { Status = "Resolved", Percent_Value = (resoled * 100) / cnt });
                model.Add(new Models.StatusModel { Status = "Pending", Percent_Value = (pend * 100) / cnt });
            }
            catch { }
            return Json(model);
        }

        public ActionResult _StatusDataPercentage()
        {
            var data = System.Web.HttpContext.Current.Session["data"] as List<V_report>;
            int cnt = data.Count;
            var hazard = data.Where(a => a.Scenario_Name == "Hazard").Count();
            var rbeh = data.Where(a => a.Scenario_Name == "Risky Behavior").Count();
            var gJob = data.Where(a => a.Scenario_Name == "Good Job").Count();
            List<Models.StatusModel> model = new List<Models.StatusModel>();
            try
            {
                model.Add(new Models.StatusModel { Status = "Hazard", Percent_Value = (hazard * 100) / cnt });
                model.Add(new Models.StatusModel { Status = "Risky Behavior", Percent_Value = (rbeh * 100) / cnt });
                model.Add(new Models.StatusModel { Status = "Good Job", Percent_Value = (gJob * 100) / cnt });
            }
            catch { }
            return Json(model);
        }
        #endregion

    }
}