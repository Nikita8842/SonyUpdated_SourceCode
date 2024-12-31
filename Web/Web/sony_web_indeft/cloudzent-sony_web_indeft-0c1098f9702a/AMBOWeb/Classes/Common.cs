
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Configuration;

namespace AMBOWeb.Classes
{
    public static class Common
    {
        /// <summary>
        /// Bela, This is for converting datatable into list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dtTmp"></param>
        /// <returns>list of object</returns>
        public static List<T> ConvertTo<T>(DataTable dtTmp) where T : new()
        {
            var dataList = new List<T>();
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
            var objFieldNames = (from PropertyInfo aProp in typeof(T).GetProperties(flags)
                                 select new
                                 {
                                     Name = aProp.Name,
                                     Type = Nullable.GetUnderlyingType(aProp.PropertyType) ?? aProp.PropertyType
                                 }).ToList();
            var dataTblFieldNames = (from DataColumn aHeader in dtTmp.Columns
                                     select new { Name = aHeader.ColumnName, Type = aHeader.DataType }).ToList();
            var commonFields = objFieldNames.Intersect(dataTblFieldNames).ToList();
            foreach (DataRow dataRow in dtTmp.AsEnumerable().ToList())
            {
                var aTSource = new T();
                foreach (var aField in commonFields)
                {
                    try
                    {
                        PropertyInfo propertyInfos = aTSource.GetType().GetProperty(aField.Name);
                        propertyInfos.SetValue(aTSource, dataRow[aField.Name], null);
                    }
                    catch (Exception ex)
                    {
                        string exec = ex.Message;
                    }
                }
                dataList.Add(aTSource);
            }
            return dataList;
        }


        public static DataTable ToDataTable(IEnumerable<dynamic> v)
        {
            var firstRecord = v.FirstOrDefault();
            if (firstRecord == null)
                return null;

            PropertyInfo[] infos = firstRecord.GetType().GetProperties();
            DataTable table = new DataTable();

            foreach (var info in infos)
            {

                Type propType = info.PropertyType;
                if (propType.IsGenericType
                    && propType.GetGenericTypeDefinition() == typeof(Nullable<>)) //Nullable types should be handled too
                {
                    table.Columns.Add(info.Name, Nullable.GetUnderlyingType(propType));
                }
                else
                {
                    table.Columns.Add(info.Name, info.PropertyType);
                }
            }

            DataRow row;
            foreach (var record in v)
            {
                row = table.NewRow();
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    row[i] = infos[i].GetValue(record) != null ? infos[i].GetValue(record) : DBNull.Value;
                }

                table.Rows.Add(row);
            }
            table.AcceptChanges();
            return table;
        }

        public enum Status
        {
            Success,
            Failure,
            Error,
            Exception,
            NotFound
        }

        public static class SessionKeys
        {
            public static string BasicSession = "BasicSession";
            public static string PageSpecific = "PageSpecific";
        }

        
        public enum LogType
        {
            Error,
            Warn,
            Debug,
            Info
        }

        #region Text Logging Methods
        private static void WriteTextLog(string Message, LogType LogType)
        {
            if(Convert.ToString(WebConfigurationManager.AppSettings["LogEnable"]).ToUpper() == "TRUE")
            {
                string logPath = Convert.ToString(WebConfigurationManager.AppSettings["LogPath"]);
                string LogTypeForOutput = "";
                if (String.IsNullOrWhiteSpace(logPath))
                    return;
                switch(LogType)
                {
                    case LogType.Debug:
                        LogTypeForOutput = "DEBUG";
                        break;
                    case LogType.Warn:
                        LogTypeForOutput = "WARNING";
                        break;
                    case LogType.Error:
                        LogTypeForOutput = "ERROR";
                        break;
                    case LogType.Info:
                        LogTypeForOutput = "INFO";
                        break;
                }
                FileStream objFilestream = new FileStream(logPath + "\\WebLog_" + DateTime.Now.ToString("dd.MM.yyyy") +
                    ".log", FileMode.Append, FileAccess.Write);
                StreamWriter objStreamWriter = new StreamWriter((Stream)objFilestream);
                objStreamWriter.WriteLine(DateTime.Now.ToString() + " --> [" + LogTypeForOutput + "] : " + Message);
                objStreamWriter.Close();
                objFilestream.Close();
                LogTypeForOutput = null;
                logPath = null;
            }
        }

        public static void LogError(string Message)
        {
            WriteTextLog(Message, LogType.Error);
        }

        public static void LogWarning(string Message)
        {
            WriteTextLog(Message, LogType.Warn);
        }

        public static void LogInfo(string Message)
        {
            WriteTextLog(Message, LogType.Info);
        }

        public static void LogDebug(string Message)
        {
            WriteTextLog(Message, LogType.Debug);
        }
        #endregion Text Logging Methods

        public enum Module
        {
            Region=1,
            State,
            City,
            Location,
            Branch,
            Product,
            SubProduct,
            Material,
            Competitor,
            CompetitorProduct,
            CompetitorModel,
            ProductCategory,
            SFA,
            SFAlevel,
            SFASubLevel,
            DealerSFA,
            ProductSFA,
            Employee,
            Dealer,
            DealerClassification,
            Channel,
            ProductTargetCategory,
            IncentiveCategory,
            PerPiece,
            Base,
            Festival,
            Asset,
            MonthlyAttendance,
            DailyTiming,
            EmployeeStructure,
            SFAMasterForHR,
            SFASalaryMaster,
            ShiftMaster,
            SFABranchChange
        }

        public static string[] GetReportHeaders(int ModuleId)
        {
            string[] headers=null;

            switch (ModuleId)
            {
                case (int)Module.Region:
                    headers = new string[] { "REGIONNAME", "STATUS" };                    
                    break;

                case (int)Module.Branch:
                    headers = new string[] { "BRANCHCODE", "BRANCHNAME", "STATUS" };
                    break;

                case (int)Module.City:
                    headers = new string[] { "CITYNAME", "CITYTYPENAME", "STATENAME", "REGIONNAME", "STATUS" };
                    break;

                case (int)Module.Location:
                    headers = new string[] { "LOCATIONNAME", "CITYNAME", "STATENAME" ,"REGIONNAME", "STATUS" };
                    break;

                case (int)Module.State:
                    headers = new string[] { "STATENAME", "REGIONNAME", "STATUS" };
                    break;

                case (int)Module.Product:
                    headers = new string[] { "CATEGORYNAME", "DESCRIPTION", "DIVISION", "STATUS" };
                    break;

                case (int)Module.SubProduct:
                    headers = new string[] { "SUBCATEGORYNAME", "DESCRIPTION", "CATEGORYNAME", "DIVISION", "STATUS" };
                    break;

                case (int)Module.Material:
                    headers = new string[] { "MATERIALCODE", "MATERIALNAME", "DESCRIPTION", "CATEGORYNAME", "SUBCATEGORYNAME", "MOP", "STATUS" };
                    break;

                case (int)Module.Competitor:
                    headers = new string[] { "CompetitorName", "CompetitorCode", "STATUS" };
                    break;

                case (int)Module.CompetitorProduct:
                    headers = new string[] { "ProductName", "CompetitorName", "SonyProductCategory", "TopModel", "CategoryName", "STATUS" };
                    break;

                case (int)Module.CompetitorModel:
                    headers = new string[] { "CompetitorCode", "CompetitorProductName", "SonyProductCategoryName", "SonySubCategoryName", "SonyMaterialName","CompetitorModelName", };
                    break;

                case (int)Module.ProductCategory:
                    headers = new string[] { "GroupName", "DisplayOrder", "ProductCategory" };
                    break;

                case (int)Module.SFA:
                    headers = new string[] { "FIRSTNAME", "LASTNAME", "EMPLOYEECODE" /*, "LOGINID"*/, "BRANCHNAME", "REGIONNAME",
                        "STATENAME", /*"CITYNAME",*/ "DIVISION", "ROLE", "ADDRESS", "MOBILENUMBER", "ALTERNATEMOBILE", "EMAIL",
                        "PAN", "DOB", "DOJ", "DOL", "IMEI1", "IMEI2", "SFATYPE", "SFALEVEL", "AGENCYNAME","STATUS", "DEALERNAME",
                        "DEALERCODE", "MASTERCODE", "DEALERCITY", "DEALERLOCATION", "CHANNEL","INCENTIVECATEGORY", "SHIFT" };
                    break;

                case (int)Module.SFAlevel:
                    headers = new string[] { "SFALevelName", "CreationDate" };
                    break;

                case (int)Module.ShiftMaster:
                    headers = new string[] { "ShiftName", "CreationDate" };
                    break;

                case (int)Module.SFABranchChange:
                    headers = new string[] { "FirstName","LastName","LoginId","BranchName","CityName","StateName","AgencyName","Status","SFAType"};
                    break;

                case (int)Module.SFASubLevel:
                    headers = new string[] { "SFASubLevelName", "SFALevelName", "Description", "CreationDate" };
                    break;

                case (int)Module.DealerSFA:
                    headers = new string[] { "SFANAME", "BRANCHNAME", "CITYNAME", "STATENAME", "LOCATIONNAME", "DEALERNAME" };
                    break;

                case (int)Module.ProductSFA:
                    headers = new string[] { "BRANCHNAME", "DEALERNAME", "SFANAME", "SFACODE", "PRODCATNAME", "CHANNEL", "CITY", "LOCATION" };
                    break;

                case (int)Module.Employee:
                    headers = new string[] { "FIRSTNAME", "LASTNAME", "LOGINID", "EMPLOYEECODE", "ROLE", "BRANCHNAME", "REGIONNAME",
                        "STATENAME", "CITYNAME", "ADDRESS", "MOBILENUMBER", "ALTERNATEMOBILE", "PAN", "DOB", "DOJ", "DOL",
                        "SFATYPE", "SFALEVEL", "AGENCYNAME","DIVISION", "STATUS" };
                    break;

                case (int)Module.Dealer:
                    headers = new string[] { "DEALERCODE", "DEALERNAME", "SAPCODE", "MASTERCODE1", "MASTERCODE2", "PAYERCODE", "PAYERNAME",
                        "OUTLETTYPE", "STORECODE", "CUSTOMERID", "BRANCHNAME", "LOCATIONNAME", "CITYNAME","STATENAME",
                        "LATITUDE", "LONGITUDE", "CHANNELNAME", "ADDRESS", "MOBILE", "PSIOutlet1", "PSIOutlet2", "TIN", "PAN",
                        "CONTACTPERSON", "EMAIL", "OPENINGDATE","CLOSINGDATE", "STATUS"};
                    break;

                case (int)Module.DealerClassification:
                    headers = new string[] { "DEALERNAME", "BRANCHNAME", "PRODCATNAME", "CLASSNAME"};
                    break;

                case (int)Module.Channel:
                    headers = new string[] { "CHANNELCODE", "CHANNELNAME" };
                    break;

                case (int)Module.ProductTargetCategory:
                    headers = new string[] { "TARGETCATEGORY", "PRODUCTCATEGORY", "STATUS" };
                    break;

                case (int)Module.IncentiveCategory:
                    headers = new string[] { "CATEGORYNAME", "STATUS" };
                    break;

                case (int)Module.Base:
                    headers = new string[] { "TARGETCATNAME", "STATUS" };
                    break;

                case (int)Module.PerPiece:
                    headers = new string[] { "SCHEMENAME", "APPLICABILITY", "DATEFROM", "DATETO" };
                    break;

                case (int)Module.Festival:
                    headers = new string[] { "SCHEMENAME", "CALCULATEBASEINCENTIVE", "DATEFROM", "DATETO" };
                    break;

                case (int)Module.Asset:
                    headers = new string[] { "PRODUCTCODE", "PRODUCTNAME", "CATEGORY", "TYPE", "STATUS" };
                    break;

                case (int)Module.EmployeeStructure:
                    headers = new string[] { "RDIBRANCHNAME", "RDINAME", "SFANAME" };
                    break;

                case (int)Module.SFAMasterForHR:
                    headers = new string[] { "LOGINID","SFANAME","SOURCENAME","AGENCY/REF","REF SFA CODE", "DOL","DOJ","DOB",
                                               "GENDER","FATHERNAME","LEVELOFEDUCATION","EDUCATION","EXPERIENCE", "BANKNAME",
                                                "BANKACCOUNTNO", "REGION","BRANCH","STATE","CITY", "CityType","Location",
                                                "DealerName","DealerCode","Channel","SFADivision","Level","SubLevel","SFAType",
                                                "StaffingAgency", "ADDRESS","EMAIL","MOBILE","ESIACCOUNTNO","PFACCOUNTNO",
                                                "MEDINSURANCENO", "MICOVERAGEFROM","MICOVERAGETO","PERSONALINSURANCENO",
                                                "PICOVERAGEFROM","PICOVERAGETO"};
                    break;

                case (int)Module.SFASalaryMaster:
                    headers = new string[] { "SFANAME","LOGINID","BRANCH","STATE","CITY","DIVISION","SFALEVEL","BASIC","HRA",
                                            "MED","CONV","OTHER","AIRTIME","INSURANCE"};
                    break;
            }
            return headers;
        }

        public enum Role
        {
            Admin=1,
            SFA,
            RDI,
            NRDI,
            NationalSalesAdmin,
            BM,
            BuisnessPlanning,
            MD,
            SalesPIC,
            Marketing,
            Viewer,
            HRAdmin,
            BranchHR_RHR
        }

        public enum Modules
        {
            SFA=2,
            Dealer,
            Employee,
            SFAScheme,
            Incentive,
            Location,
            Product,            
            Asset,            
            Communication,
            Reports,
            HR,
            Security
        }

        public enum SubModules
        {
            SFAMaster=1, SFALevel, SFASubLevel, DealerSFAMapping, ProductCategorySFAMapping, AssignIncentiveCategory,
            DealerMaster, DealerMasterCodeUpdation, DealerClassificationMapping, ChannelManager,
            EmployeeManager, EmployeeStructureMapping, SalesPICOutletMapping, UserBranchChannelProCatMapping,
            ProductTargetCategoryMapping, TargetDateSettingMaster, ProductDefinitionUnderTargetCategory, IncentiveTargetCategoryMapping, 
            PerPieceIncentiveDefinition, BaseIncentiveDefinition, FestivalIncentiveDefinition,
            DeviationUploadByRDI, DeviationApproval, FinalDeviationUpload, AssignFestivalTarget, AssignTargettoSFA,QSRQuantityUpload,
            Region, State, City, Location, Branch,
            ProductCat, ProductSubCat, Material, Competitor, CompetitorProduct, CompetitorModel, ProductCatGroup,
            AssetManager, AssetAssignmenttoRDI, AssetIssuetoSFA, AssetCollectionfromSFA,
            BroadcastMessage, Feedback, Trainings,                       
            SFAMasterforHR =60, SFASalaryMaster,             
            RoleRightMapping,ShiftMaster,SFABranchChange= 65                                                                          /*Moduleis added vijay 13_August_24 SFABranchChange*/
        }

        public enum Right
        {
            View=1,
            Create,
            Edit,
            Delete
        }

    }
}