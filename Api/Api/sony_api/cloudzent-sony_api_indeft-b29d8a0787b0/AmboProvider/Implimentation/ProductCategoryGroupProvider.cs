using AmboLibrary.MasterMaintenance;
using AmboProvider.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using DBHelper;
using AmboLibrary.GlobalAccessible;
using System.Data.SqlClient;

namespace AmboProvider.Implimentation
{
   public class ProductCategoryGroupProvider: IProductCategoryGroupProvider
    {
        private readonly DbHelper _dataHelper;
        private readonly IErrorLogProvider _IErrorLogProvider;

        public ProductCategoryGroupProvider(IErrorLogProvider IErrorLogProvider)
        {
            _dataHelper = DbHelper.GetInstance("defaultConnection");
            _IErrorLogProvider = IErrorLogProvider;
        }

        public IEnumerable<ProductGroupCategoryMaster> GetProductCategoryGroup(ProCatGroupFilter InputParam)
        {
            IEnumerable<ProductGroupCategoryMaster> ListProductCategroyGroup = null;
            DataTable dtgroup = new DataTable();
            DataRow row;
            try
            {
                dtgroup.Columns.Add("FilterId");
                if (InputParam.GroupIds != null)
                {
                    foreach (Int64 groupid in InputParam.GroupIds)
                    {
                        row = dtgroup.NewRow();
                        row["FilterId"] = groupid;
                        dtgroup.Rows.Add(row);
                    }
                }

                SqlParameter[] objDBParam = new SqlParameter[1];
                objDBParam[0] = new SqlParameter("@GroupIds", dtgroup);

                var outputFromSP = _dataHelper.ExecuteProcedure("[dbo].[GetProductCategoryGroupGrid]", objDBParam);
                if (outputFromSP.Rows.Count > 0)
                {
                    ListProductCategroyGroup = (from categoryGroup in outputFromSP.AsEnumerable()
                                     select new ProductGroupCategoryMaster
                                     {
                                         GroupId = categoryGroup.Field<Int64>("GroupId"),
                                         GroupName = categoryGroup.Field<string>("GroupName"),
                                         DisplayOrder =categoryGroup.Field<int>("DisplayOrder")
                                         //CategoryName = categoryGroup.Field<string>("CategoryName"),
                                     });
                }
                else
                    ListProductCategroyGroup = new List<ProductGroupCategoryMaster>();
            }
            catch (Exception ex)
            { }
            return ListProductCategroyGroup;

        }

        public ProductGroupCategoryMaster GetProductCategoryGroupById(Common InputParam)
        {
            ProductGroupCategoryMaster ListProductCategroyGroup = null;
            DbParameterCollection objDBParam = new DbParameterCollection();
            List<int> productId = new List<int>();
            try
            {
                objDBParam.Add(new DbParameter("@GroupId", InputParam.Id, DbType.Int64));
                var outputFromSP = _dataHelper.ExecuteDataSet("[dbo].[GetProductCategoryGroupById]", objDBParam, CommandType.StoredProcedure);
                if (outputFromSP.Tables[0].Rows.Count > 0)
                {
                    if (outputFromSP.Tables[1].Rows.Count > 0)
                    {
                       for (int i=0;i< outputFromSP.Tables[1].Rows.Count;i++)
                        {
                            productId.Add(Convert.ToInt32(outputFromSP.Tables[1].Rows[i]["ProductId"]));
                        }
                    }

                    ListProductCategroyGroup = (from categoryGroup in outputFromSP.Tables[0].AsEnumerable()
                                                select new ProductGroupCategoryMaster
                                                {
                                                    GroupId = categoryGroup.Field<Int64>("GroupId"),
                                                    GroupName = categoryGroup.Field<string>("GroupName"),
                                                    DisplayOrder = categoryGroup.Field<int>("DisplayOrder"),
                                                    ProductIds = productId.ToArray<int>()
                                                }).FirstOrDefault(); ;
                }
                else
                    ListProductCategroyGroup = new ProductGroupCategoryMaster();
            }
            catch (Exception ex)
            { }
            return ListProductCategroyGroup;

        }




        public int CreateProductCategoryGroup(ProductGroupCategoryMaster InputParam, out string Message)
        { 
           
            Message = string.Empty;
            Int64 groupId = 0;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@GroupName", InputParam.GroupName, DbType.String));
                objDBParam.Add(new DbParameter("@DisplayOrder", InputParam.DisplayOrder, DbType.Int32));
                objDBParam.Add(new DbParameter("@UserId", InputParam.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[CreateProductCategoryGroup]", objDBParam, CommandType.StoredProcedure);
               if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                {
                    groupId = Convert.ToInt64(outputFromSP.Rows[0]["GroupId"]);
                    int[] ProductData = InputParam.ProductIds;
                    for(int i=0;i< ProductData.Length; i++)
                    {
                      
                        DbParameterCollection productData= new DbParameterCollection();
                        productData.Add(new DbParameter("@GroupId", groupId, DbType.Int64));
                        productData.Add(new DbParameter("@ProductId", ProductData[i], DbType.Int64));
                        productData.Add(new DbParameter("@UserId", InputParam.UserId, DbType.Int64));
                        var mappingOutputFromSP = _dataHelper.ExecuteDataTable("[dbo].[AddUpdateProductCategoryGroupMapping]", productData, CommandType.StoredProcedure);
                        if(!Convert.ToBoolean(mappingOutputFromSP.Rows[0]["Status"]))
                        {
                            Message = Convert.ToString(mappingOutputFromSP.Rows[0]["Description"]);
                            break;
                        }
                    }
                    Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                    return 1;

                }
                else
                {
                    Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                    //if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                        return 0;
                }

            }
            catch (Exception ex)
            {
                //Write Application Error log here
                Message = ex.Message;
            }
            return 0;
        }


        public int UpdateProductCategoryGroup(ProductGroupCategoryMaster InputParam, out string Message)
        {

            Message = string.Empty;
            Int64 groupId = 0;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@GroupId", InputParam.GroupId, DbType.String));
                objDBParam.Add(new DbParameter("@GroupName", InputParam.GroupName, DbType.String));
                objDBParam.Add(new DbParameter("@DisplayOrder", InputParam.DisplayOrder, DbType.Int32));
                objDBParam.Add(new DbParameter("@UserId", InputParam.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[UpdateProductCategoryGroup]", objDBParam, CommandType.StoredProcedure);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                {
                    groupId = InputParam.GroupId;
                    int[] ProductData = InputParam.ProductIds;
                    for (int i = 0; i < ProductData.Length; i++)
                    {

                        DbParameterCollection productData = new DbParameterCollection();
                        productData.Add(new DbParameter("@GroupId", groupId, DbType.Int64));
                        productData.Add(new DbParameter("@ProductId", ProductData[i], DbType.Int64));
                        productData.Add(new DbParameter("@UserId", InputParam.UserId, DbType.Int64));
                        var mappingOutputFromSP = _dataHelper.ExecuteDataTable("[dbo].[AddUpdateProductCategoryGroupMapping]", productData, CommandType.StoredProcedure);
                        if (!Convert.ToBoolean(mappingOutputFromSP.Rows[0]["Status"]))
                        {
                            Message = Convert.ToString(mappingOutputFromSP.Rows[0]["Description"]);
                            break;
                        }
                    }
                    Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                    return 1;

                }
                else
                {
                    Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                    //if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 0;
                }

            }
            catch (Exception ex)
            {
                //Write Application Error log here
                Message = ex.Message;
            }
            return 0;
        }


        public int DeleteProductCategoryGroup(Common InputParam, out string Message)
        {
            Message = string.Empty;
            DbParameterCollection objDBParam = new DbParameterCollection();
            try
            {
                objDBParam.Add(new DbParameter("@GroupId", InputParam.Id, DbType.Int64));
                objDBParam.Add(new DbParameter("@UserId", InputParam.UserId, DbType.Int32));
                var outputFromSP = _dataHelper.ExecuteDataTable("[dbo].[DeleteProductCategoryGroup]", objDBParam, CommandType.StoredProcedure);
                Message = Convert.ToString(outputFromSP.Rows[0]["Description"]);
                if (Convert.ToBoolean(outputFromSP.Rows[0]["Status"]))
                    return 1;
            }
            catch (Exception ex)
            {
                //Log exception here
                Message = ex.Message;
            }
            return 0;
        }

    }
}
