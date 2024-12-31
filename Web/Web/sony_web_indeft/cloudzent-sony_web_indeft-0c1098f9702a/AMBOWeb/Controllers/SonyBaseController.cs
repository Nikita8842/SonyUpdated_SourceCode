using AMBOModels.UserValidation;
using AMBOWeb.Filters;
using AMBOWeb.Models;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static AMBOWeb.Classes.Common;

namespace AMBOWeb.Controllers
{
    public class SonyBaseController : Controller
    {
        public List<SonyNotification> objNotifications { get; set; }
        public BaseSession objUserSession { get; set; }
        public SonyBaseController()
        {
            objNotifications = null;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            #region Session Check
            if (Session["BaseSession"] != null)
            {
                objUserSession = Session["BaseSession"] as BaseSession;
            }
            else
            {
                AddErrorNotification("Your session has timed-out. Please re-login to continue.");
                TempData["Notification"] = objNotifications;
                filterContext.Result = new RedirectResult(Url.Action("Index", "Login"));
                return;
            }
            #endregion Session Check
            base.OnActionExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            #region Notification Check
            if (objNotifications != null)
                TempData["Notification"] = objNotifications;
            #endregion Notification Check
            base.OnActionExecuted(filterContext);
        }

        public virtual DataTable ConvertExcelToDataTable(string filePath)
        {
            try
            {
                using (XLWorkbook wb = new XLWorkbook(filePath))
                {
                    IXLWorksheet workSheet = wb.Worksheet(1);
                    DataTable dt = new DataTable();
                    bool firstRow = true;
                    foreach (IXLRow row in workSheet.Rows())
                    {
                        if (firstRow)
                        {
                            foreach (IXLCell cell in row.Cells())
                                dt.Columns.Add(cell.Value.ToString());
                            firstRow = false;
                        }
                        else
                        {
                            if (row.IsEmpty())
                                row.Delete();
                            else
                            {
                                dt.Rows.Add();
                                int i = 0;
                                foreach (IXLCell cell in row.Cells(workSheet.FirstCellUsed().Address.ColumnNumber, workSheet.LastCellUsed().Address.ColumnNumber))
                                {
                                    dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                                    i++;
                                }
                            }
                        }
                    }
                    return dt;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public void AddSuccessNotification(string Message)
        {
            if (objNotifications == null)
                objNotifications = new List<SonyNotification>();
            objNotifications.Add(new SonyNotification() { Type = "success", Message = Message });
        }

        public void AddWarningNotification(string Message)
        {
            if (objNotifications == null)
                objNotifications = new List<SonyNotification>();
            objNotifications.Add(new SonyNotification() { Type = "warn", Message = Message });
        }

        public void AddErrorNotification(string Message)
        {
            if (objNotifications == null)
                objNotifications = new List<SonyNotification>();
            objNotifications.Add(new SonyNotification() { Type = "error", Message = Message });
        }

        public void AddInfoNotification(string Message)
        {
            if (objNotifications == null)
                objNotifications = new List<SonyNotification>();
            objNotifications.Add(new SonyNotification() { Type = "info", Message = Message });
        }

        public static string ResolveServerUrl(string serverUrl, bool forceHttps)
        {
            if (serverUrl.IndexOf("://") > -1)
                return serverUrl;

            string newUrl = serverUrl;
            Uri originalUri = System.Web.HttpContext.Current.Request.Url;
            newUrl = (forceHttps ? "https" : originalUri.Scheme) +
                "://" + originalUri.Authority + newUrl;
            return newUrl;
        }
    }

    public class SonyNotification
    {
        public string Type { get; set; }
        public string Message { get; set; }
    }
}