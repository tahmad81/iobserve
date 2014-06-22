using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Web.Mvc;
using iobserve.Data;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Diagnostics;
using System.Text.RegularExpressions;
namespace iobserve_Azure.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Dashboard()
        {

            DateTime baseDate = DateTime.Today;
            var today = baseDate;
            var yesterday = baseDate.AddDays(-1);
            var thisWeekStart = baseDate.AddDays(-(int)baseDate.DayOfWeek);
            SetSession(thisWeekStart, DateTime.Today);
            return View(new Models.DashboardModel() { Duration = 2, StartDate = DateTime.Today.AddDays(-1), Enddate = DateTime.Today });
        }
        public ActionResult _LoadObservations([DataSourceRequest] DataSourceRequest request, string category, string dataType, string riskLevel, string gridAction)
        {
            var currentList = new List<Models.Observations>();
            if (gridAction.Equals("False"))
                currentList = Load_Observations();
            var sessionData = System.Web.HttpContext.Current.Session["gridData"] as List<Models.Observations>;

            if (gridAction.Equals("False"))
            {
                if (dataType.Equals("1"))
                {
                    category = category == "Risky" ? "Risky Behavior" : category;
                    currentList = currentList.Where(a => a.Risk_Type == category && a.Risk_Level == riskLevel).ToList();
                }
                else if (dataType.Equals("2"))
                {
                    currentList = currentList.Where(a => a.StatusText == category).ToList();
                }
                else
                {
                    category = category == "Risky" ? "Risky Behavior" : category;
                    currentList = currentList.Where(a => a.Risk_Type == category).ToList();
                }
                System.Web.HttpContext.Current.Session["gridData"] = currentList;
                return Json(currentList.OrderByDescending(a => a.Date).ToList().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = System.Web.HttpContext.Current.Session["gridData"] as List<Models.Observations>;
                return Json(data.OrderByDescending(a => a.Date).ToList().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            }
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
            try
            {
                using (iobserverContext context = new iobserverContext())
                {
                    var data = context.V_reports.Where(a => a.Language_code == "en" && a.__createdAt >= startDate && a.__createdAt <= endData).ToList();

                    System.Web.HttpContext.Current.Session.Add("data", data);
                    System.Web.HttpContext.Current.Session.Add("gridData", Load_Observations());
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);

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
                                     Supervisor_Notified = obs.Supnotified,
                                     StatusText = obs.Status_text,
                                     Long = obs.Lng,
                                     Lat = obs.Lat,
                                     Id = obs.Id,
                                     LCId = obs.Location_scenario_id

                                 }).ToList();

                return modeldata;
            }
            return null;
        }
        #endregion
        //string dd = "ANSWER, PgS1_q1a5, PgS1_q1a4, PgS1_q1a1, PgS1_q1a2, PgS1_q1a3";
        public ActionResult ObservationDetails(string DetailId, string id)
        {
            var lstQ = new List<Models.Question>();
            using (iobserve.Data.iobserverContext context = new iobserverContext())
            {
                var data = System.Web.HttpContext.Current.Session["data"] as List<V_report>;
                var response = data.Where(a => a.Id == id).Select(a => a.Response_array).FirstOrDefault();
                var questions = context.V_questions.Where(a => a.Location_scenario_id == DetailId && a.Language_code == "en").OrderBy(a => a.Seqno).ToList();

                //\q1+\a\d+
                Debug.WriteLine(response);
                Debug.WriteLine("Total questions found : " + questions.Count.ToString());
                questions.ForEach(q =>
                {
                    var question = new iobserve_Azure.Models.Question();

                    question.QuestionText = q.Question_text;
                    var qData = q.Answer_options_text.Split(new char[] { ';' });
                    question.Type = q.Answer_format;
                    question.SequenceNo = q.Seqno.GetValueOrDefault();
                    int ansOptSequenceno=1;
                    List<Models.AnswerOptions> optList = new List<Models.AnswerOptions>();

                    qData.ToList().ForEach(ans =>
                    {
                        optList.Add(new Models.AnswerOptions
                        {
                            OptionText = ans,
                            SequenceNo = ansOptSequenceno++


                        });

                    });
                    var wno = q.Seqno.ToString();
                    string pattern = @"q" + wno + @"a(?<AnsNo>\d+)";
                    var matches = Regex.Matches(response, pattern);
                    if (matches.Count == 0) Debug.WriteLine("Question no : " + wno + " No match");
                    foreach (Match m in matches)
                    {
                        var ans =int.Parse(m.Groups["AnsNo"].Value);
                     Models.AnswerOptions cAnsOption=  optList.Where(a => a.SequenceNo == ans).FirstOrDefault();
                     cAnsOption.IsSelected = true;
                        Debug.WriteLine("Question No :" + wno.ToString() + " " + "Answer No :" + ans.ToString());


                    }
                    question.AnswerOptionList = optList;
                    lstQ.Add(question);
                });
            }
            return View(lstQ);
        }

        [HttpPost()]
        public ActionResult Dashboard(Models.DashboardModel model)
        {
            DateTime baseDate = DateTime.Today;
            DateTime startDate, endDate;
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
                    model.StartDate = thisWeekStart;
                    model.Enddate = DateTime.Today;
                    break;
                case 3:
                    SetSession(thisMonthStart, DateTime.Today);
                    model.StartDate = thisMonthStart;
                    model.Enddate = DateTime.Today;
                    break;
                case 4:
                    SetSession(lastWeekStart, lastWeekEnd);
                    model.StartDate = lastWeekStart;
                    model.Enddate = lastWeekEnd;
                    break;
                case 5:
                    SetSession(lastMonthStart, lastMonthEnd);
                    model.StartDate = lastMonthStart;
                    model.Enddate = lastMonthEnd;
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