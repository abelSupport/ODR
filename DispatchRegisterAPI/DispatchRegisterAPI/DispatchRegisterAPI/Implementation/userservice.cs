using DispatchRegisterAPI.ServiceDataContract;
using DispatchRegisterAPI.Interfaces;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DispatchRegisterAPI.Implementation
{
    public class userservice : BaseSQLDAL, Iuserservice
    {
        DataSet ds = new DataSet();

        #region Login, Change Password
        public UserDetailResponseDataContract Authenticate(UserDetailDataContract login)
        {
            UserDetailResponseDataContract userDataLoginContract = new UserDetailResponseDataContract();
            ServiceResponse serviceResponse = new ServiceResponse();
            serviceResponse.ServiceResponse = Response.Failed;
            try
            {

                List<UserDetailDataContract> userlst = new List<UserDetailDataContract>();
                UserDetailDataContract User = new UserDetailDataContract();
                User = null;
                ds.Clear();
                SqlParameter[] par ={
                                        new SqlParameter("@Username",SqlDbType.NVarChar),
                                        new SqlParameter("@LoginPwd",SqlDbType.NVarChar),
                                    };
                par[0].Value = login.MobileNo;
                par[1].Value = login.Password;
                ds = obj_sqldal.ExecuteDatasetSP("splogin", par);

                if (ds != null && ds.Tables.Count > 0) //Tip Details Success
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            User = (new UserDetailDataContract()
                            {
                                Name = Convert.ToString(dr["Name"]),
                                DesignationID = Convert.ToInt32(dr["Designation"]),
                                EmailID = Convert.ToString(dr["EmailID"]),
                                MobileNo = Convert.ToString(dr["MobileNo"]),
                                UserID = Convert.ToInt32(dr["UserID"]),
                                RoleID = Convert.ToInt32(dr["RoleID"]),
                                OfficeName = Convert.ToString(dr["DesignationLongName"]),
                                DesignationName = Convert.ToString(dr["DesignationName"])
                            });
                            userlst.Add(User);
                        }
                        serviceResponse.ServiceResponse = Response.Successful;
                    }
                }
                userDataLoginContract.Data = userlst;
                userDataLoginContract.ServiceResponse = serviceResponse.ServiceResponse;
                return userDataLoginContract;
            }
            catch (Exception w)
            {
                throw w;
            }
        }
        //public UserDetailResponseDataContract SignUp(UserDetailDataContract login)
        //{
        //    try
        //    {
        //        LoginResponseDataContract userDataLoginContract = new LoginResponseDataContract();
        //        ServiceResponse serviceResponse = new ServiceResponse();
        //        serviceResponse.ServiceResponse = Response.Failed;
        //        List<LoginDataContract> userlst = new List<LoginDataContract>();
        //        LoginDataContract User = new LoginDataContract();
        //        User = null;
        //        ds.Clear();
        //        SqlParameter[] par ={
        //                                new SqlParameter("@Username",SqlDbType.NVarChar),
        //                                new SqlParameter("@LoginPwd",SqlDbType.NVarChar),
        //                            };
        //        par[0].Value = login.MobileNo;
        //        par[1].Value = login.Password;
        //        ds = obj_sqldal.ExecuteDatasetSP("spSignUp", par);

        //        if (ds != null && ds.Tables.Count > 0) //Tip Details Success
        //        {
        //            DataTable dt = ds.Tables[0];
        //            if (dt.Rows.Count > 0)
        //            {
        //                string strUserID = Convert.ToString(dt.Rows[0]["UserID"]);
        //                userDataLoginContract.Msg = Convert.ToString(dt.Rows[0]["Msg"]);
        //                if (!string.IsNullOrEmpty(strUserID))
        //                {
        //                    serviceResponse.ServiceResponse = Response.Successful;
        //                }
        //            }
        //        }
        //        userDataLoginContract.Data = userlst;
        //        userDataLoginContract.ServiceResponse = serviceResponse.ServiceResponse;
        //        return userDataLoginContract;
        //    }
        //    catch (Exception w)
        //    {
        //        throw w;
        //    }
        //}
        //public UserDetailResponseDataContract ValidateUser(string username)
        //{
        //    try
        //    {
        //        LoginResponseDataContract userDataLoginContract = new LoginResponseDataContract();
        //        ServiceResponse serviceResponse = new ServiceResponse();
        //        serviceResponse.ServiceResponse = Response.Failed;
        //        List<LoginDataContract> userlst = new List<LoginDataContract>();
        //        LoginDataContract User = new LoginDataContract();
        //        User = null;
        //        ds.Clear();
        //        SqlParameter[] par ={
        //                                new SqlParameter("@Username",SqlDbType.NVarChar),

        //                            };
        //        par[0].Value = username;

        //        ds = obj_sqldal.ExecuteDatasetSP("spValidateUsername", par);

        //        if (ds != null && ds.Tables.Count > 0) //Tip Details Success
        //        {
        //            DataTable dt = ds.Tables[0];
        //            if (dt.Rows.Count > 0)
        //            {

        //                string strUserID = Convert.ToString(dt.Rows[0]["Status"]);
        //                userDataLoginContract.Msg = Convert.ToString(dt.Rows[0]["Msg"]);
        //                if (!string.IsNullOrEmpty(strUserID))
        //                {
        //                    serviceResponse.ServiceResponse = Response.Successful;
        //                }

        //            }
        //        }
        //        userDataLoginContract.Data = userlst;
        //        userDataLoginContract.ServiceResponse = serviceResponse.ServiceResponse;
        //        return userDataLoginContract;
        //    }
        //    catch (Exception w)
        //    {
        //        throw w;
        //    }
        //}
        public UserDetailResponseDataContract SetPassword(UserDetailDataContract login)
        {
            UserDetailResponseDataContract userDataLoginContract = new UserDetailResponseDataContract();
            ServiceResponse serviceResponse = new ServiceResponse();
            serviceResponse.ServiceResponse = Response.Failed;
            try
            {

                List<UserDetailDataContract> userlst = new List<UserDetailDataContract>();
                UserDetailDataContract User = new UserDetailDataContract();
                User = null;
                ds.Clear();
                SqlParameter[] par ={
                                        new SqlParameter("@UserID",SqlDbType.Int),
                                         new SqlParameter("@OldPwd",SqlDbType.NVarChar),
                                        new SqlParameter("@LoginPwd",SqlDbType.NVarChar),
                                    };
                par[0].Value = login.UserID;
                par[1].Value = login.Password;
                par[2].Value = login.ConfirmPassword;
                ds = obj_sqldal.ExecuteDatasetSP("spGeneratePassword", par);

                if (ds != null && ds.Tables.Count > 0) //Tip Details Success
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        int status = Convert.ToInt32(dt.Rows[0]["status"]);
                        if (status == 1)
                        {
                            serviceResponse.ServiceResponse = Response.Successful;
                        }
                        else
                        {
                            serviceResponse.ServiceResponse = Response.Failed;
                        }
                        userDataLoginContract.Msg = Convert.ToString(dt.Rows[0]["msg"]);

                    }
                    else
                    {
                        userDataLoginContract.Msg = "Failed";
                    }
                }
                else
                {
                    userDataLoginContract.Msg = "Failed";
                }
                userDataLoginContract.Data = null;
                userDataLoginContract.ServiceResponse = serviceResponse.ServiceResponse;
                return userDataLoginContract;
            }
            catch (Exception w)
            {
                userDataLoginContract.Msg = w.Message;
                return userDataLoginContract;
            }
        }

        #endregion

        #region User
        public UserDetailResponseDataContract AddUser(UserDetailDataContract objUser)
        {
            UserDetailResponseDataContract userDataLoginContract = new UserDetailResponseDataContract();
            ServiceResponse serviceResponse = new ServiceResponse();
            serviceResponse.ServiceResponse = Response.Failed;
            try
            {

                List<UserDetailDataContract> userlst = new List<UserDetailDataContract>();
                UserDetailDataContract User = new UserDetailDataContract();
                User = null;
                ds.Clear();
                SqlParameter[] par ={
                                         new SqlParameter("@UserID",SqlDbType.Int),
                                          new SqlParameter("@Name",SqlDbType.NVarChar),
                                            new SqlParameter("@EmailID",SqlDbType.NVarChar),
                                              new SqlParameter("@MobileNo",SqlDbType.NVarChar),
                                            new SqlParameter("@Designation",SqlDbType.Int),
                                        new SqlParameter("@RoleID",SqlDbType.Int),
                                        new SqlParameter("@Username",SqlDbType.NVarChar),
                                              new SqlParameter("@Password",SqlDbType.NVarChar),
                                              new SqlParameter("@CreatedBy",SqlDbType.NVarChar),
                                        new SqlParameter("@ProcType",SqlDbType.NVarChar),
                                    };
                par[0].Value = objUser.UserID;
                par[1].Value = objUser.Name;
                par[2].Value = objUser.EmailID;
                par[3].Value = objUser.MobileNo;
                par[4].Value = objUser.DesignationID;
                par[5].Value = objUser.RoleID;
                par[6].Value = objUser.Username;
                par[7].Value = objUser.Password;
                par[8].Value = objUser.CreatedBy;
                par[9].Value = objUser.UserID==0?"Insert":"Update";
                ds = obj_sqldal.ExecuteDatasetSP("spUser_AddUser", par);

                if (ds != null && ds.Tables.Count > 0) //Tip Details Success
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        userDataLoginContract.Msg = Convert.ToString(dt.Rows[0]["msg"]);
                        serviceResponse.ServiceResponse = Response.Successful;
                    }
                }
                userDataLoginContract.Data = userlst;
                userDataLoginContract.ServiceResponse = serviceResponse.ServiceResponse;
                return userDataLoginContract;
            }
            catch (Exception w)
            {
                userDataLoginContract.Msg = w.Message;
                serviceResponse.ServiceResponse = Response.Failed;

            }
            return userDataLoginContract;
        }
        public UserDetailResponseDataContract GetUserList(string userid)
        {
            UserDetailResponseDataContract userDataLoginContract = new UserDetailResponseDataContract();
            ServiceResponse serviceResponse = new ServiceResponse();
            serviceResponse.ServiceResponse = Response.Failed;
            try
            {

                List<UserDetailDataContract> userlst = new List<UserDetailDataContract>();
                UserDetailDataContract User = new UserDetailDataContract();
                User = null;
                ds.Clear();
                SqlParameter[] par ={
                                        new SqlParameter("@UserID",SqlDbType.Int),
                                        new SqlParameter("@ProcType",SqlDbType.NVarChar),
                                    };
                par[0].Value =!string.IsNullOrEmpty(userid)?Convert.ToInt32(userid):0;
                par[1].Value = "All";
                ds = obj_sqldal.ExecuteDatasetSP("spGetUsers", par);

                if (ds != null && ds.Tables.Count > 0) //Tip Details Success
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            User = (new UserDetailDataContract()
                            {
                                Name = Convert.ToString(dr["Name"]),
                                DesignationID = Convert.ToInt32(dr["Designation"]),
                                EmailID = Convert.ToString(dr["EmailID"]),
                                MobileNo = Convert.ToString(dr["MobileNo"]),
                                UserID = Convert.ToInt32(dr["UserID"]),
                                DesignationName = Convert.ToString(dr["DesignationName"]),
                                RoleName = Convert.ToString(dr["RoleName"]),
                                Username = Convert.ToString(dr["Username"]),
                                Password = Convert.ToString(dr["Password"]),
                                RoleID = Convert.ToInt32(dr["RoleID"]),
                            });
                            userlst.Add(User);
                        }
                        serviceResponse.ServiceResponse = Response.Successful;
                    }
                }
                userDataLoginContract.Data = userlst;
                userDataLoginContract.ServiceResponse = serviceResponse.ServiceResponse;
                return userDataLoginContract;
            }
            catch (Exception w)
            {
                throw w;
            }
        }

        public UserDetailResponseDataContract GetUserListByDesignation(string designationid)
        {
            UserDetailResponseDataContract userDataLoginContract = new UserDetailResponseDataContract();
            ServiceResponse serviceResponse = new ServiceResponse();
            serviceResponse.ServiceResponse = Response.Failed;
            try
            {

                List<UserDetailDataContract> userlst = new List<UserDetailDataContract>();
                UserDetailDataContract User = new UserDetailDataContract();
                User = null;
                ds.Clear();
                SqlParameter[] par ={
                                        new SqlParameter("@DesignationID",SqlDbType.Int),
                                        new SqlParameter("@ProcType",SqlDbType.NVarChar),
                                    };
                par[0].Value = Convert.ToInt32(designationid);
                par[1].Value = "ByDesignationID";
                ds = obj_sqldal.ExecuteDatasetSP("spGetUsers");

                if (ds != null && ds.Tables.Count > 0) //Tip Details Success
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            User = (new UserDetailDataContract()
                            {
                                Name = Convert.ToString(dr["Name"]),
                                DesignationID = Convert.ToInt32(dr["Designation"]),
                                EmailID = Convert.ToString(dr["EmailID"]),
                                MobileNo = Convert.ToString(dr["MobileNo"]),
                                UserID = Convert.ToInt32(dr["UserID"])
                            });
                            userlst.Add(User);
                        }
                        serviceResponse.ServiceResponse = Response.Successful;
                    }
                }
                userDataLoginContract.Data = userlst;
                userDataLoginContract.ServiceResponse = serviceResponse.ServiceResponse;
                return userDataLoginContract;
            }
            catch (Exception w)
            {
                throw w;
            }
        }

        #endregion

        #region Category
        public ModuleResponseDataContract GetModules()
        {
            ModuleResponseDataContract categoryResponseDataContract = new ModuleResponseDataContract();
            ServiceResponse serviceResponse = new ServiceResponse();
            serviceResponse.ServiceResponse = Response.Failed;
            categoryResponseDataContract.Msg = null;
            try
            {
                List<ModuleDataContract> listCategories = new List<ModuleDataContract>();
                SqlParameter[] par ={                                        
                                        new SqlParameter("@ModuleID",SqlDbType.Int),                                         
                                        new SqlParameter("@ProcType",SqlDbType.NVarChar),
                                    };
                par[0].Value = Convert.ToInt32(0);
                par[1].Value = Convert.ToString("All");
                ds.Clear();
                ds = obj_sqldal.ExecuteDatasetSP("spGetModule", par);

                if (ds != null && ds.Tables.Count > 0) //Tip Details Success
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            listCategories.Add(new ModuleDataContract()
                            {
                                ModuleID = Convert.ToInt32(dr["ModuleID"]),
                                ModuleName = Convert.ToString(dr["ModuleName"])

                            });
                        }
                        categoryResponseDataContract.Data = listCategories;
                        serviceResponse.ServiceResponse = Response.Successful;
                        categoryResponseDataContract.ServiceResponse = serviceResponse.ServiceResponse;
                    }
                    else
                    {
                        categoryResponseDataContract.Msg = "No Data Found";
                    }
                }
                else
                {
                    categoryResponseDataContract.Msg = "No Data Found";
                }
                return categoryResponseDataContract;
            }
            catch (Exception w)
            {

                categoryResponseDataContract.Msg = w.Message;
                return categoryResponseDataContract;
            }
        }

        #endregion

        #region SubCategory
        public AssetTypeResponseDataContract SubCategories()
        {
            AssetTypeResponseDataContract assetTypeResponseDataContract = new AssetTypeResponseDataContract();
            ServiceResponse serviceResponse = new ServiceResponse();
            serviceResponse.ServiceResponse = Response.Failed;
            assetTypeResponseDataContract.Msg = null;
            List<AssetTypeDataContract> listAssets = new List<AssetTypeDataContract>();
            try
            {

                SqlParameter[] par ={
                                         new SqlParameter("@SubCategoryID",SqlDbType.Int),  
                                        new SqlParameter("@CategoryID",SqlDbType.Int),
                                        new SqlParameter("@ProcType",SqlDbType.NVarChar),
                                    };
                par[0].Value = Convert.ToInt32(0);
                par[1].Value = Convert.ToInt32(0);
                par[2].Value = Convert.ToString("All");
                ds.Clear();
                ds = obj_sqldal.ExecuteDatasetSP("spGetSubCategory", par);

                if (ds != null && ds.Tables.Count > 0) //Tip Details Success
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            listAssets.Add(new AssetTypeDataContract()
                            {
                                AssetTypeID = Convert.ToInt32(dr["AssetTypeID"]),
                                AssetName = Convert.ToString(dr["AssetName"])

                            });
                        }
                        assetTypeResponseDataContract.Data = listAssets;
                        serviceResponse.ServiceResponse = Response.Successful;
                        assetTypeResponseDataContract.ServiceResponse = serviceResponse.ServiceResponse;
                    }
                    else
                    {
                        assetTypeResponseDataContract.Msg = "No Data Found";
                    }
                }
                else
                {
                    assetTypeResponseDataContract.Msg = "No Data Found";
                }
                return assetTypeResponseDataContract;
            }
            catch (Exception w)
            {

                assetTypeResponseDataContract.Msg = w.Message;
                return assetTypeResponseDataContract;
            }
        }
        public AssetTypeResponseDataContract GetSubCategoryBySubCategoryID(string assettypeid)
        {
            AssetTypeResponseDataContract assetTypeResponseDataContract = new AssetTypeResponseDataContract();
            ServiceResponse serviceResponse = new ServiceResponse();
            serviceResponse.ServiceResponse = Response.Failed;
            assetTypeResponseDataContract.Msg = null;
            List<AssetTypeDataContract> listAssets = new List<AssetTypeDataContract>();

            try
            {

                SqlParameter[] par ={
                                        new SqlParameter("@SubCategoryID",SqlDbType.Int),  
                                        new SqlParameter("@CategoryID",SqlDbType.Int),
                                        new SqlParameter("@ProcType",SqlDbType.NVarChar),
                                    };
                par[0].Value = (!string.IsNullOrEmpty(assettypeid)) ? Convert.ToInt32(assettypeid) : 0;
                par[1].Value = 0;
                par[2].Value = Convert.ToString("BySubCategoryID");
                ds.Clear();
                ds = obj_sqldal.ExecuteDatasetSP("spGetSubCategory", par);

                if (ds != null && ds.Tables.Count > 0) //Tip Details Success
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            listAssets.Add(new AssetTypeDataContract()
                            {
                                AssetTypeID = Convert.ToInt32(dr["AssetTypeID"]),
                                AssetName = Convert.ToString(dr["AssetName"])

                            });
                        }
                        assetTypeResponseDataContract.Data = listAssets;
                        serviceResponse.ServiceResponse = Response.Successful;
                        assetTypeResponseDataContract.ServiceResponse = serviceResponse.ServiceResponse;
                    }
                    else
                    {
                        assetTypeResponseDataContract.Msg = "No Data Found";
                    }
                }
                else
                {
                    assetTypeResponseDataContract.Msg = "No Data Found";
                }
                return assetTypeResponseDataContract;
            }
            catch (Exception w)
            {

                assetTypeResponseDataContract.Msg = w.Message;
                return assetTypeResponseDataContract;
            }
        }

        public AssetTypeResponseDataContract GetSubCategoryByCategoryID(string categoryid)
        {
            AssetTypeResponseDataContract assetTypeResponseDataContract = new AssetTypeResponseDataContract();
            ServiceResponse serviceResponse = new ServiceResponse();
            serviceResponse.ServiceResponse = Response.Failed;
            assetTypeResponseDataContract.Msg = null;
            List<AssetTypeDataContract> listAssets = new List<AssetTypeDataContract>();

            try
            {

                SqlParameter[] par ={
                                        new SqlParameter("@SubCategoryID",SqlDbType.Int),  
                                        new SqlParameter("@CategoryID",SqlDbType.Int),
                                        new SqlParameter("@ProcType",SqlDbType.NVarChar),
                                    };
                par[0].Value = 0;
                par[1].Value = (!string.IsNullOrEmpty(categoryid)) ? Convert.ToInt32(categoryid) : 0;
                par[2].Value = Convert.ToString("ByCategoryID");
                ds.Clear();
                ds = obj_sqldal.ExecuteDatasetSP("spGetSubCategory", par);

                if (ds != null && ds.Tables.Count > 0) //Tip Details Success
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            listAssets.Add(new AssetTypeDataContract()
                            {
                                AssetTypeID = Convert.ToInt32(dr["AssetTypeID"]),
                                AssetName = Convert.ToString(dr["AssetName"])

                            });
                        }
                        assetTypeResponseDataContract.Data = listAssets;
                        serviceResponse.ServiceResponse = Response.Successful;
                        assetTypeResponseDataContract.ServiceResponse = serviceResponse.ServiceResponse;
                    }
                    else
                    {
                        assetTypeResponseDataContract.Msg = "No Data Found";
                    }
                }
                else
                {
                    assetTypeResponseDataContract.Msg = "No Data Found";
                }
                return assetTypeResponseDataContract;
            }
            catch (Exception w)
            {

                assetTypeResponseDataContract.Msg = w.Message;
                return assetTypeResponseDataContract;
            }
        }

        #endregion


        public DesignationResponseDataContract GetDesignationList()
        {
            DesignationResponseDataContract designationResponseDataContract = new DesignationResponseDataContract();
            ServiceResponse serviceResponse = new ServiceResponse();
            serviceResponse.ServiceResponse = Response.Failed;
            designationResponseDataContract.Msg = null;
            try
            {
                List<DesignationDataContract> listCategories = new List<DesignationDataContract>();
                //SqlParameter[] par ={                                        
                //                        new SqlParameter("@CategoryID",SqlDbType.Int),                                         
                //                        new SqlParameter("@ProcType",SqlDbType.NVarChar),
                //                    };
                //par[0].Value = Convert.ToInt32(0);
                //par[1].Value = Convert.ToString("All");
                ds.Clear();
                ds = obj_sqldal.ExecuteDatasetSP("spGetDesignation");

                if (ds != null && ds.Tables.Count > 0) //Tip Details Success
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            listCategories.Add(new DesignationDataContract()
                            {
                                DesignationID = Convert.ToInt32(dr["DesignationId"]),
                                DesignationName = Convert.ToString(dr["DesignationName"])

                            });
                        }
                        designationResponseDataContract.Data = listCategories;
                        serviceResponse.ServiceResponse = Response.Successful;
                        designationResponseDataContract.ServiceResponse = serviceResponse.ServiceResponse;
                    }
                    else
                    {
                        designationResponseDataContract.Msg = "No Data Found";
                    }
                }
                else
                {
                    designationResponseDataContract.Msg = "No Data Found";
                }
                return designationResponseDataContract;
            }
            catch (Exception w)
            {

                designationResponseDataContract.Msg = w.Message;
                return designationResponseDataContract;
            }
        }

        public Tuple<List<DesignationDataContract>, string> GetDesignationListByIDs(string ids)
        {

            List<DesignationDataContract> listCategories = new List<DesignationDataContract>();
            string designationnames = string.Empty;
            try
            {

                SqlParameter[] par ={                                        
                                        new SqlParameter("@ids",SqlDbType.NVarChar),                                         
                                        new SqlParameter("@ProcType",SqlDbType.NVarChar),
                                    };
                par[0].Value = ids;
                par[1].Value = Convert.ToString("All");

                DataSet dsDesig = obj_sqldal.ExecuteDatasetSP("spDesignation_GetDesignationByID", par);

                if (dsDesig != null && dsDesig.Tables.Count > 0) //Tip Details Success
                {
                    DataTable dtDesig = dsDesig.Tables[0];
                    if (dtDesig.Rows.Count > 0)
                    {
                        foreach (DataRow desigdr in dtDesig.Rows)
                        {
                            designationnames += !string.IsNullOrEmpty(designationnames) ? ", " + Convert.ToString(desigdr["DesignationName"]) : Convert.ToString(desigdr["DesignationName"]);
                            listCategories.Add(new DesignationDataContract()
                            {
                                DesignationID = Convert.ToInt32(desigdr["DesignationId"]),
                                DesignationName = Convert.ToString(desigdr["DesignationName"])

                            });
                        }

                    }

                }

            }
            catch (Exception w)
            {

                string Msg = w.Message;

            }
            return Tuple.Create(listCategories, designationnames);
        }
        #region File Master

        public DispatchRegisterResponseDataContract AddFile(DispatchRegisterDataContract dispatchRegisterDataContract)
        {
            DispatchRegisterResponseDataContract dispatchRegisterResponseDataContract = new DispatchRegisterResponseDataContract();
            ServiceResponse serviceResponse = new ServiceResponse();
            serviceResponse.ServiceResponse = Response.Failed;
            dispatchRegisterResponseDataContract.Msg = null;
            try
            {
                List<DispatchRegisterDataContract> listCategories = new List<DispatchRegisterDataContract>();
                SqlParameter[] par ={                                        
                                        new SqlParameter("@DRID",SqlDbType.Int),  
                                         new SqlParameter("@FileNumber",SqlDbType.NVarChar),
                                          new SqlParameter("@Description",SqlDbType.NVarChar),
                                          new SqlParameter("@FileCategory",SqlDbType.NVarChar),
                                           new SqlParameter("@FileIncomingDate",SqlDbType.Date),
                                           new SqlParameter("@FileRecievedFrom",SqlDbType.Int),                                         
                                          new SqlParameter("@FileDispatchedNumber",SqlDbType.NVarChar),
                                          new SqlParameter("@FileDispatchedDate",SqlDbType.Date),
                                           new SqlParameter("@FileSentTo",SqlDbType.NVarChar),
                                          new SqlParameter("@FileSentDate",SqlDbType.Date),
                                           new SqlParameter("@Remark",SqlDbType.NVarChar),
                                          new SqlParameter("@Status",SqlDbType.NVarChar),
                                           new SqlParameter("@UserID",SqlDbType.Int),
                                            new SqlParameter("@IsGenerated",SqlDbType.Int),
                                             new SqlParameter("@FileUniqueNumber",SqlDbType.NVarChar),
                                               new SqlParameter("@IsFromOutOfSystem",SqlDbType.Int),
                                                 new SqlParameter("@OtherFileSentTo",SqlDbType.NVarChar),
                                                  new SqlParameter("@OtherFileRecievedFrom",SqlDbType.NVarChar),
                                                   new SqlParameter("@InternalSentTo",SqlDbType.NVarChar),
                                          new SqlParameter("@ProcType",SqlDbType.NVarChar),
                                    };
                par[0].Value = 0;
                par[1].Value = dispatchRegisterDataContract.FileNumber;
                par[2].Value = dispatchRegisterDataContract.Description;
                par[3].Value = dispatchRegisterDataContract.FileCategory;
                par[4].Value = dispatchRegisterDataContract.FileIncomingDate;
                par[5].Value = dispatchRegisterDataContract.FileRecievedFrom;
                par[6].Value = dispatchRegisterDataContract.FileDispatchedNumber;
                par[7].Value = !string.IsNullOrEmpty(dispatchRegisterDataContract.FileDispatchedDate) ? dispatchRegisterDataContract.FileDispatchedDate : null;
                par[8].Value = dispatchRegisterDataContract.FileSentTo;
                par[9].Value = !string.IsNullOrEmpty(dispatchRegisterDataContract.FileSentDate) ? dispatchRegisterDataContract.FileSentDate : null;
                par[10].Value = dispatchRegisterDataContract.Remark;
                par[11].Value = dispatchRegisterDataContract.Status;
                par[12].Value = dispatchRegisterDataContract.UserID;
                par[13].Value = dispatchRegisterDataContract.IsGenerated;
                par[14].Value = dispatchRegisterDataContract.FileCode;
                par[15].Value = dispatchRegisterDataContract.IsFromOutOfSystem;
                par[16].Value = dispatchRegisterDataContract.OtherFileSentTo;
                par[17].Value = dispatchRegisterDataContract.OtherFileRecievedFrom;
                par[18].Value = dispatchRegisterDataContract.InternalSentTo;
                par[19].Value = "Insert";
                ds.Clear();
                ds = obj_sqldal.ExecuteDatasetSP("spInsertDispatchRegisterDetails", par);

                if (ds != null && ds.Tables.Count > 0) //Tip Details Success
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        //foreach (DataRow dr in dt.Rows)
                        //{
                        //    listCategories.Add(new ModuleDataContract()
                        //    {
                        dispatchRegisterResponseDataContract.Msg = Convert.ToString(dt.Rows[0]["Msg"]);
                        //        ModuleName = Convert.ToString(dr["ModuleName"])

                        //    });
                        //}
                        listCategories.Add(new DispatchRegisterDataContract { FileCode = Convert.ToString(dt.Rows[0]["FileCode"]), FileNumber = Convert.ToString(dt.Rows[0]["DocInwardOutwardCounter"]) });
                        dispatchRegisterResponseDataContract.Data = listCategories;
                        serviceResponse.ServiceResponse = Response.Successful;
                        dispatchRegisterResponseDataContract.ServiceResponse = serviceResponse.ServiceResponse;
                    }
                    else
                    {
                        dispatchRegisterResponseDataContract.Msg = "File Not Added";
                    }
                }
                else
                {
                    dispatchRegisterResponseDataContract.Msg = "File Not Added";
                }
                return dispatchRegisterResponseDataContract;
            }
            catch (Exception w)
            {

                dispatchRegisterResponseDataContract.Msg = w.Message;
                return dispatchRegisterResponseDataContract;
            }
        }
        public DispatchRegisterResponseDataContract UpdateFile(DispatchRegisterDataContract dispatchRegisterDataContract)
        {
            DispatchRegisterResponseDataContract dispatchRegisterResponseDataContract = new DispatchRegisterResponseDataContract();
            ServiceResponse serviceResponse = new ServiceResponse();
            serviceResponse.ServiceResponse = Response.Failed;
            dispatchRegisterResponseDataContract.Msg = null;
            try
            {
                List<DispatchRegisterDataContract> listCategories = new List<DispatchRegisterDataContract>();
                SqlParameter[] par ={                                        
                                        new SqlParameter("@DRID",SqlDbType.Int),  
                                         new SqlParameter("@FileNumber",SqlDbType.NVarChar),
                                          new SqlParameter("@Description",SqlDbType.NVarChar),
                                          new SqlParameter("@FileCategory",SqlDbType.NVarChar),
                                           new SqlParameter("@FileIncomingDate",SqlDbType.Date),
                                           new SqlParameter("@FileRecievedFrom",SqlDbType.Int),                                         
                                          new SqlParameter("@FileDispatchedNumber",SqlDbType.NVarChar),
                                          new SqlParameter("@FileDispatchedDate",SqlDbType.Date),
                                           new SqlParameter("@FileSentTo",SqlDbType.NVarChar),
                                          new SqlParameter("@FileSentDate",SqlDbType.Date),
                                           new SqlParameter("@Remark",SqlDbType.NVarChar),
                                          new SqlParameter("@Status",SqlDbType.NVarChar),
                                           new SqlParameter("@UserID",SqlDbType.Int),
                                            new SqlParameter("@IsGenerated",SqlDbType.Int),
                                                new SqlParameter("@OtherFileSentTo",SqlDbType.NVarChar),
                                                 new SqlParameter("@OtherFileRecievedFrom",SqlDbType.NVarChar),
                                                  new SqlParameter("@InternalSentTo",SqlDbType.NVarChar),
                                          new SqlParameter("@ProcType",SqlDbType.NVarChar),
                                    };
                par[0].Value = dispatchRegisterDataContract.DRID;
                par[1].Value = dispatchRegisterDataContract.FileNumber;
                par[2].Value = dispatchRegisterDataContract.Description;
                par[3].Value = dispatchRegisterDataContract.FileCategory;
                par[4].Value = dispatchRegisterDataContract.FileIncomingDate;
                par[5].Value = dispatchRegisterDataContract.FileRecievedFrom;
                par[6].Value = dispatchRegisterDataContract.FileDispatchedNumber;
                par[7].Value = dispatchRegisterDataContract.FileDispatchedDate;
                par[8].Value = dispatchRegisterDataContract.FileSentTo;
                par[9].Value = !string.IsNullOrEmpty(dispatchRegisterDataContract.FileSentDate) ? dispatchRegisterDataContract.FileSentDate : null;
                par[10].Value = dispatchRegisterDataContract.Remark;
                par[11].Value = dispatchRegisterDataContract.Status;
                par[12].Value = dispatchRegisterDataContract.UserID;
                par[13].Value = dispatchRegisterDataContract.IsGenerated;
                par[14].Value = dispatchRegisterDataContract.OtherFileSentTo;
                par[15].Value = dispatchRegisterDataContract.OtherFileRecievedFrom;
                par[16].Value = dispatchRegisterDataContract.InternalSentTo;
                par[17].Value = "Update";

                ds.Clear();
                ds = obj_sqldal.ExecuteDatasetSP("spUpdateDispatchRegisterDetails", par);

                if (ds != null && ds.Tables.Count > 0) //Tip Details Success
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        //foreach (DataRow dr in dt.Rows)
                        //{
                        //    listCategories.Add(new ModuleDataContract()
                        //    {
                        dispatchRegisterResponseDataContract.Msg = Convert.ToString(dt.Rows[0]["Msg"]);
                        //        ModuleName = Convert.ToString(dr["ModuleName"])

                        //    });
                        //}
                        dispatchRegisterResponseDataContract.Data = listCategories;
                        serviceResponse.ServiceResponse = Response.Successful;
                        dispatchRegisterResponseDataContract.ServiceResponse = serviceResponse.ServiceResponse;
                    }
                    else
                    {
                        dispatchRegisterResponseDataContract.Msg = "File Not Added";
                    }
                }
                else
                {
                    dispatchRegisterResponseDataContract.Msg = "File Not Added";
                }
                return dispatchRegisterResponseDataContract;
            }
            catch (Exception w)
            {

                dispatchRegisterResponseDataContract.Msg = w.Message;
                return dispatchRegisterResponseDataContract;
            }
        }
        public DispatchRegisterResponseDataContract GetFiles(clsSearchFileParameters searchParams)
        {
            DispatchRegisterResponseDataContract dispatchRegisterResponseDataContract = new DispatchRegisterResponseDataContract();
            ServiceResponse serviceResponse = new ServiceResponse();
            serviceResponse.ServiceResponse = Response.Failed;
            dispatchRegisterResponseDataContract.Msg = null;
            try
            {
                List<DispatchRegisterDataContract> listDispatchRegisterDataContract = new List<DispatchRegisterDataContract>();
                SqlParameter[] par ={     new SqlParameter("@DRID",SqlDbType.Int), 
                                          new SqlParameter("@UserID",SqlDbType.Int),   
                                            new SqlParameter("@Designation",SqlDbType.Int),   
                                              new SqlParameter("@FileCategory",SqlDbType.Int),   
                                                new SqlParameter("@Status",SqlDbType.NVarChar),   
                                                  new SqlParameter("@FileUniqueNo",SqlDbType.NVarChar),   
                                                    new SqlParameter("@FromDate",SqlDbType.Date),   
                                                    new SqlParameter("@ToDate",SqlDbType.Date), 
                                                     new SqlParameter("@Role",SqlDbType.Int), 
                                                     new SqlParameter("@ProcType",SqlDbType.NVarChar),  
                                        
                                       
                                    };
                par[0].Value = searchParams.DRID;
                par[1].Value = searchParams.UserID;
                par[2].Value = searchParams.Designation;
                par[3].Value = searchParams.FileCategory;
                par[4].Value = searchParams.Status;
                par[5].Value = !string.IsNullOrEmpty(searchParams.FileUniqueNo) ? searchParams.FileUniqueNo : null;
                par[6].Value = !string.IsNullOrEmpty(searchParams.FromDate) ? searchParams.FromDate : null;
                par[7].Value = !string.IsNullOrEmpty(searchParams.ToDate) ? searchParams.ToDate : null;
                par[8].Value = searchParams.Role;
                par[9].Value = "";

                ds.Clear();
                ds = obj_sqldal.ExecuteDatasetSP("spDispatchRegister_GetDispatchRegisterData", par);

                if (ds != null && ds.Tables.Count > 0) //Tip Details Success
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            var tupleData = GetDesignationListByIDs(Convert.ToString(dr["FileSentTo"]));
                            listDispatchRegisterDataContract.Add(new DispatchRegisterDataContract()
                            {
                                DRID = Convert.ToInt32(dr["DRID"]),
                                FileNumber = Convert.ToString(dr["FileNumber"]),
                                FileCategory = Convert.ToInt32(dr["FileCategory"]),
                                FileIncomingDate = Convert.ToString(dr["FileIncomingDate"]),
                                FileRecievedFrom = Convert.ToInt32(dr["FileRecievedFrom"]),
                                FileDispatchedNumber = Convert.ToString(dr["FileDispatchedNumber"]),
                                Description = Convert.ToString(dr["Description"]),
                                FileDispatchedDate = Convert.ToString(dr["FileDispatchedDate"]),
                                FileSentTo = string.IsNullOrEmpty(Convert.ToString(dr["OtherFileSentTo"])) ? tupleData.Item2 : tupleData.Item2 + ", " + Convert.ToString(dr["OtherFileSentTo"]),
                                FileSentDate = Convert.ToString(dr["FileSentDate"]),
                                Remark = Convert.ToString(dr["Remark"]),
                                Status = Convert.ToString(dr["Status"]),
                                UserID = Convert.ToInt32(dr["UserID"]),
                                FileSentToList = tupleData.Item1,
                                FileCode = Convert.ToString(dr["FileCode"]),
                                IsGenerated = Convert.ToInt32(dr["IsGenerated"]),
                                UserName = Convert.ToString(dr["UserName"]),
                                FileRecievedFromName = string.IsNullOrEmpty(Convert.ToString(dr["OtherFileRecievedFrom"])) ? Convert.ToString(dr["FileRecievedFromName"]) : Convert.ToString(dr["OtherFileRecievedFrom"]),
                                FileCategoryName = Convert.ToString(dr["FileCategoryName"]),
                                OtherFileSentTo = Convert.ToString(dr["OtherFileSentTo"]),
                                IsFromOutOfSystem = Convert.ToInt32(dr["IsFromOutOfSystem"]),
                                OtherFileRecievedFrom = Convert.ToString(dr["OtherFileRecievedFrom"]),
                                InternalSentTo = Convert.ToString(dr["InternalSentTo"]),
                            });
                        }
                        dispatchRegisterResponseDataContract.Data = listDispatchRegisterDataContract;
                        serviceResponse.ServiceResponse = Response.Successful;
                        dispatchRegisterResponseDataContract.ServiceResponse = serviceResponse.ServiceResponse;
                    }
                    else
                    {
                        dispatchRegisterResponseDataContract.Msg = "File Not Added";
                    }
                }
                else
                {
                    dispatchRegisterResponseDataContract.Msg = "File Not Added";
                }
                return dispatchRegisterResponseDataContract;
            }
            catch (Exception w)
            {

                dispatchRegisterResponseDataContract.Msg = w.Message;
                return dispatchRegisterResponseDataContract;
            }
        }

        public DispatchRegisterResponseDataContract GetReportFiles(clsSearchFileParameters searchParams)
        {
            DispatchRegisterResponseDataContract dispatchRegisterResponseDataContract = new DispatchRegisterResponseDataContract();
            ServiceResponse serviceResponse = new ServiceResponse();
            serviceResponse.ServiceResponse = Response.Failed;
            dispatchRegisterResponseDataContract.Msg = null;
            try
            {
                List<DispatchRegisterDataContract> listDispatchRegisterDataContract = new List<DispatchRegisterDataContract>();
                SqlParameter[] par ={     new SqlParameter("@DRID",SqlDbType.Int), 
                                          new SqlParameter("@UserID",SqlDbType.Int),   
                                            new SqlParameter("@Designation",SqlDbType.Int),   
                                              new SqlParameter("@FileCategory",SqlDbType.Int),   
                                                new SqlParameter("@Status",SqlDbType.NVarChar),   
                                                  new SqlParameter("@FileUniqueNo",SqlDbType.NVarChar),   
                                                    new SqlParameter("@FromDate",SqlDbType.Date),   
                                                    new SqlParameter("@ToDate",SqlDbType.Date), 
                                                    new SqlParameter("@Role",SqlDbType.Int), 
                                                     new SqlParameter("@ProcType",SqlDbType.NVarChar),  
                                        
                                       
                                    };
                par[0].Value = searchParams.DRID;
                par[1].Value = searchParams.UserID;
                par[2].Value = searchParams.Designation;
                par[3].Value = searchParams.FileCategory;
                par[4].Value = searchParams.Status;
                par[5].Value = !string.IsNullOrEmpty(searchParams.FileUniqueNo) ? searchParams.FileUniqueNo : null;
                par[6].Value = !string.IsNullOrEmpty(searchParams.FromDate) ? searchParams.FromDate : null;
                par[7].Value = !string.IsNullOrEmpty(searchParams.ToDate) ? searchParams.ToDate : null;
                par[8].Value = searchParams.Role;
                par[9].Value = "";

                ds.Clear();
                ds = obj_sqldal.ExecuteDatasetSP("spDispatchRegister_GetReportDispatchRegisterData", par);

                if (ds != null && ds.Tables.Count > 0) //Tip Details Success
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            var tupleData = GetDesignationListByIDs(Convert.ToString(dr["FileSentTo"]));
                            listDispatchRegisterDataContract.Add(new DispatchRegisterDataContract()
                            {
                                DRID = Convert.ToInt32(dr["DRID"]),
                                FileNumber = Convert.ToString(dr["FileNumber"]),
                                FileCategory = Convert.ToInt32(dr["FileCategory"]),
                                FileIncomingDate = Convert.ToString(dr["FileIncomingDate"]),
                                FileRecievedFrom = Convert.ToInt32(dr["FileRecievedFrom"]),
                                FileDispatchedNumber = Convert.ToString(dr["FileDispatchedNumber"]),
                                Description = Convert.ToString(dr["Description"]),
                                FileDispatchedDate = Convert.ToString(dr["FileDispatchedDate"]),
                                FileSentTo = string.IsNullOrEmpty(Convert.ToString(dr["OtherFileSentTo"])) ? tupleData.Item2 : tupleData.Item2 + ", " + Convert.ToString(dr["OtherFileSentTo"]),
                                FileSentDate = Convert.ToString(dr["FileSentDate"]),
                                Remark = Convert.ToString(dr["Remark"]),
                                Status = Convert.ToString(dr["Status"]),
                                UserID = Convert.ToInt32(dr["UserID"]),
                                FileSentToList = tupleData.Item1,
                                FileCode = Convert.ToString(dr["FileCode"]),
                                IsGenerated = Convert.ToInt32(dr["IsGenerated"]),
                                UserName = Convert.ToString(dr["UserName"]),
                                FileRecievedFromName = string.IsNullOrEmpty(Convert.ToString(dr["OtherFileRecievedFrom"])) ? Convert.ToString(dr["FileRecievedFromName"]) : Convert.ToString(dr["OtherFileRecievedFrom"]),
                                FileCategoryName = Convert.ToString(dr["FileCategoryName"]),
                                OtherFileSentTo = Convert.ToString(dr["OtherFileSentTo"]),
                                OtherFileRecievedFrom = Convert.ToString(dr["OtherFileRecievedFrom"]),
                                InternalSentTo = Convert.ToString(dr["InternalSentTo"]),
                            });
                        }
                        dispatchRegisterResponseDataContract.Data = listDispatchRegisterDataContract;
                        serviceResponse.ServiceResponse = Response.Successful;
                        dispatchRegisterResponseDataContract.ServiceResponse = serviceResponse.ServiceResponse;
                    }
                    else
                    {
                        dispatchRegisterResponseDataContract.Msg = "File Not Added";
                    }
                }
                else
                {
                    dispatchRegisterResponseDataContract.Msg = "File Not Added";
                }
                return dispatchRegisterResponseDataContract;
            }
            catch (Exception w)
            {

                dispatchRegisterResponseDataContract.Msg = w.Message;
                return dispatchRegisterResponseDataContract;
            }
        }

        public DispatchRegisterResponseDataContract GetReportGlobalFiles(DispatchRegisterDataContract searchParams)
        {
            DispatchRegisterResponseDataContract dispatchRegisterResponseDataContract = new DispatchRegisterResponseDataContract();
            ServiceResponse serviceResponse = new ServiceResponse();
            serviceResponse.ServiceResponse = Response.Failed;
            dispatchRegisterResponseDataContract.Msg = null;
            try
            {
                List<DispatchRegisterDataContract> listDispatchRegisterDataContract = new List<DispatchRegisterDataContract>();
                SqlParameter[] par ={      
                                                new SqlParameter("@FileCode",SqlDbType.NVarChar),   
                                                  new SqlParameter("@FileRecievedFrom",SqlDbType.Int),   
                                                    new SqlParameter("@OtherFileRecievedFrom",SqlDbType.NVarChar),   
                                                    new SqlParameter("@FileDispatchedNumber",SqlDbType.NVarChar), 
                                                       new SqlParameter("@FileCategory",SqlDbType.Int),   
                                                    
                                        
                                       
                                    };
                par[0].Value = !string.IsNullOrEmpty(searchParams.FileCode) ? searchParams.FileCode : null;
                par[1].Value = searchParams.FileRecievedFrom == -1 ? null : Convert.ToString(searchParams.FileRecievedFrom);
                par[2].Value = !string.IsNullOrEmpty(searchParams.OtherFileRecievedFrom) ? searchParams.OtherFileRecievedFrom : null;
                par[3].Value = !string.IsNullOrEmpty(searchParams.FileDispatchedNumber) ? searchParams.FileDispatchedNumber : null;
                par[4].Value = searchParams.FileCategory == 0 ? null : Convert.ToString(searchParams.FileCategory);

                ds.Clear();
                ds = obj_sqldal.ExecuteDatasetSP("spDispatchRegister_GetGlobalReportDispatchRegisterData", par);

                if (ds != null && ds.Tables.Count > 0) //Tip Details Success
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            var tupleData = GetDesignationListByIDs(Convert.ToString(dr["FileSentTo"]));
                            listDispatchRegisterDataContract.Add(new DispatchRegisterDataContract()
                            {
                                DRID = Convert.ToInt32(dr["DRID"]),
                                FileNumber = Convert.ToString(dr["FileNumber"]),
                                FileCategory = Convert.ToInt32(dr["FileCategory"]),
                                FileIncomingDate = Convert.ToString(dr["FileIncomingDate"]),
                                FileRecievedFrom = Convert.ToInt32(dr["FileRecievedFrom"]),
                                FileDispatchedNumber = Convert.ToString(dr["FileDispatchedNumber"]),
                                Description = Convert.ToString(dr["Description"]),
                                FileDispatchedDate = Convert.ToString(dr["FileDispatchedDate"]),
                                FileSentTo = string.IsNullOrEmpty(Convert.ToString(dr["OtherFileSentTo"])) ? tupleData.Item2 : tupleData.Item2 + ", " + Convert.ToString(dr["OtherFileSentTo"]),
                                FileSentDate = Convert.ToString(dr["FileSentDate"]),
                                Remark = Convert.ToString(dr["Remark"]),
                                Status = Convert.ToString(dr["Status"]),
                                UserID = Convert.ToInt32(dr["UserID"]),
                                FileSentToList = tupleData.Item1,
                                FileCode = Convert.ToString(dr["FileCode"]),
                                IsGenerated = Convert.ToInt32(dr["IsGenerated"]),
                                UserName = Convert.ToString(dr["UserName"]),
                                FileRecievedFromName = string.IsNullOrEmpty(Convert.ToString(dr["OtherFileRecievedFrom"])) ? Convert.ToString(dr["FileRecievedFromName"]) : Convert.ToString(dr["OtherFileRecievedFrom"]),
                                FileCategoryName = Convert.ToString(dr["FileCategoryName"]),
                                OtherFileSentTo = Convert.ToString(dr["OtherFileSentTo"]),
                                OtherFileRecievedFrom = Convert.ToString(dr["OtherFileRecievedFrom"]),
                                InternalSentTo = Convert.ToString(dr["InternalSentTo"]),
                            });
                        }
                        dispatchRegisterResponseDataContract.Data = listDispatchRegisterDataContract;
                        serviceResponse.ServiceResponse = Response.Successful;
                        dispatchRegisterResponseDataContract.ServiceResponse = serviceResponse.ServiceResponse;
                    }
                    else
                    {
                        dispatchRegisterResponseDataContract.Msg = "File Not Added";
                    }
                }
                else
                {
                    dispatchRegisterResponseDataContract.Msg = "File Not Added";
                }
                return dispatchRegisterResponseDataContract;
            }
            catch (Exception w)
            {

                dispatchRegisterResponseDataContract.Msg = w.Message;
                return dispatchRegisterResponseDataContract;
            }
        }
        public DispatchRegisterResponseDataContract GetDocRefNo(string duidn, string docreceivedfromid)
        {
            DispatchRegisterResponseDataContract dispatchRegisterResponseDataContract = new DispatchRegisterResponseDataContract();
            ServiceResponse serviceResponse = new ServiceResponse();
            serviceResponse.ServiceResponse = Response.Failed;
            dispatchRegisterResponseDataContract.Msg = null;
            try
            {
                List<DispatchRegisterDataContract> listDispatchRegisterDataContract = new List<DispatchRegisterDataContract>();
                SqlParameter[] par ={    
                                          
                                                  new SqlParameter("@FileUniqueNo",SqlDbType.NVarChar), 
                                                   new SqlParameter("@FileRecievedFrom",SqlDbType.Int), 
                                                  
                                        
                                    };

                par[0].Value = duidn;
                par[1].Value = Convert.ToInt32(docreceivedfromid);

                ds.Clear();
                ds = obj_sqldal.ExecuteDatasetSP("spDispatchRegister_GetDocumentRefNo", par);

                if (ds != null && ds.Tables.Count > 0) //Tip Details Success
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {

                            listDispatchRegisterDataContract.Add(new DispatchRegisterDataContract()
                            {
                                DRID = Convert.ToInt32(dr["DRID"]),
                                FileNumber = Convert.ToString(dr["FileNumber"]),
                                FileCategory = Convert.ToInt32(dr["FileCategory"]),
                                FileIncomingDate = Convert.ToString(dr["FileIncomingDate"]),
                                FileRecievedFrom = Convert.ToInt32(dr["FileRecievedFrom"]),
                                FileDispatchedNumber = Convert.ToString(dr["FileDispatchedNumber"]),
                                FileDispatchedDate = Convert.ToString(dr["FileDispatchedDate"]),
                                Description = Convert.ToString(dr["Description"]),
                                FileSentDate = Convert.ToString(dr["FileSentDate"]),
                                Status = Convert.ToString(dr["Status"]),

                            });
                        }
                        dispatchRegisterResponseDataContract.Data = listDispatchRegisterDataContract;
                        serviceResponse.ServiceResponse = Response.Successful;
                        dispatchRegisterResponseDataContract.ServiceResponse = serviceResponse.ServiceResponse;
                    }
                    else
                    {
                        dispatchRegisterResponseDataContract.Msg = "File Not Added";
                    }
                }
                else
                {
                    dispatchRegisterResponseDataContract.Msg = "File Not Added";
                }
                return dispatchRegisterResponseDataContract;
            }
            catch (Exception w)
            {

                dispatchRegisterResponseDataContract.Msg = w.Message;
                return dispatchRegisterResponseDataContract;
            }
        }

        #endregion



        #region DocumentCategory
        public DocCategoryResponseDataContract GetDocumentCategories()
        {
            DocCategoryResponseDataContract categoryResponseDataContract = new DocCategoryResponseDataContract();
            ServiceResponse serviceResponse = new ServiceResponse();
            serviceResponse.ServiceResponse = Response.Failed;
            categoryResponseDataContract.Msg = null;
            try
            {
                List<DocCategoryDataContract> listCategories = new List<DocCategoryDataContract>();
                //SqlParameter[] par ={                                        
                //                        new SqlParameter("@ModuleID",SqlDbType.Int),                                         
                //                        new SqlParameter("@ProcType",SqlDbType.NVarChar),
                //                    };
                //par[0].Value = Convert.ToInt32(0);
                //par[1].Value = Convert.ToString("All");
                ds.Clear();
                ds = obj_sqldal.ExecuteDatasetSP("spDocCat_GetDocCat");

                if (ds != null && ds.Tables.Count > 0) //Tip Details Success
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            listCategories.Add(new DocCategoryDataContract()
                            {
                                DocCatID = Convert.ToInt32(dr["DocCatID"]),
                                DocumentCategoryName = Convert.ToString(dr["DocumentCategoryName"])
                            });
                        }
                        categoryResponseDataContract.Data = listCategories;
                        serviceResponse.ServiceResponse = Response.Successful;
                        categoryResponseDataContract.ServiceResponse = serviceResponse.ServiceResponse;
                    }
                    else
                    {
                        categoryResponseDataContract.Msg = "No Data Found";
                    }
                }
                else
                {
                    categoryResponseDataContract.Msg = "No Data Found";
                }
                return categoryResponseDataContract;
            }
            catch (Exception w)
            {

                categoryResponseDataContract.Msg = w.Message;
                return categoryResponseDataContract;
            }
        }

        #endregion


        public DashboardDataContractResponnse getdashboard(string userid, string year)
        {
            DashboardDataContractResponnse dashboardDataContractResponnse = new DashboardDataContractResponnse();
            ServiceResponse serviceResponse = new ServiceResponse();
            serviceResponse.ServiceResponse = Response.Failed;
            dashboardDataContractResponnse.Msg = null;
            List<DocStatusDataContract> docStatusWiseCountList = new List<DocStatusDataContract>();
            List<CategoryWiseCount> categoryWiseCountList = new List<CategoryWiseCount>();
            List<DispatchRegisterDataContract> listDispatchRegisterDataContract = new List<DispatchRegisterDataContract>();
            DashboardCountDataContract dashboardCountDataContract = new DashboardCountDataContract();
            DashboardDataContract dashboardDataContract = new DashboardDataContract();
            try
            {
                SqlParameter[] par ={                                        
                                        new SqlParameter("@UserID",SqlDbType.Int), 
                                         new SqlParameter("@Year",SqlDbType.NVarChar),    
                                        
                                    };
                par[0].Value = Convert.ToInt32(userid);
                par[1].Value = year;

                ds.Clear();
                ds = obj_sqldal.ExecuteDatasetSP("spDashboard_GetDashboardData", par);

                if (ds != null && ds.Tables.Count > 0) //Tip Details Success
                {

                    DataTable dtdash = ds.Tables[0];
                    if (dtdash.Rows.Count > 0)
                    {
                        foreach (DataRow drDash in dtdash.Rows)
                        {

                            dashboardCountDataContract.OpenFiles = Convert.ToString(drDash["OpenFiles"]);
                            dashboardCountDataContract.LastMonthOpenFiles = Convert.ToInt32(drDash["LastMonthOpenFiles"]);
                            dashboardCountDataContract.ClosedFiles = Convert.ToString(drDash["ClosedFiles"]);
                            dashboardCountDataContract.LastMonthClosedFiles = Convert.ToInt32(drDash["LastMonthClosedFiles"]);
                            dashboardCountDataContract.TotalFiles = Convert.ToInt32(drDash["TotalFiles"]);
                            dashboardCountDataContract.TotalOpenFiles = Convert.ToInt32(drDash["TotalOpenFiles"]);
                            dashboardCountDataContract.TotalClosedFiles = Convert.ToInt32(drDash["TotalClosedFiles"]);

                        }
                        dashboardDataContract.DashboardCountData = dashboardCountDataContract;
                    }


                    DataTable dtList = ds.Tables[1];
                    if (dtList.Rows.Count > 0)
                    {
                        foreach (DataRow drList in dtList.Rows)
                        {
                            var tupleData = GetDesignationListByIDs(Convert.ToString(drList["FileSentTo"]));
                            listDispatchRegisterDataContract.Add(new DispatchRegisterDataContract()
                            {
                                DRID = Convert.ToInt32(drList["DRID"]),
                                FileNumber = Convert.ToString(drList["FileNumber"]),
                                FileCategory = Convert.ToInt32(drList["FileCategory"]),
                                FileIncomingDate = Convert.ToString(drList["FileIncomingDate"]),
                                FileRecievedFrom = Convert.ToInt32(drList["FileRecievedFrom"]),
                                FileDispatchedNumber = Convert.ToString(drList["FileDispatchedNumber"]),
                                Description = Convert.ToString(drList["Description"]),
                                FileDispatchedDate = Convert.ToString(drList["FileDispatchedDate"]),
                                FileSentTo = string.IsNullOrEmpty(Convert.ToString(drList["OtherFileSentTo"])) ? tupleData.Item2 : tupleData.Item2 + ", " + Convert.ToString(drList["OtherFileSentTo"]),
                                FileSentDate = Convert.ToString(drList["FileSentDate"]),
                                Remark = Convert.ToString(drList["Remark"]),
                                Status = Convert.ToString(drList["Status"]),
                                UserID = Convert.ToInt32(drList["UserID"]),
                                FileSentToList = tupleData.Item1,
                                FileCode = Convert.ToString(drList["FileCode"]),
                                IsGenerated = Convert.ToInt32(drList["IsGenerated"]),
                                UserName = Convert.ToString(drList["UserName"]),
                                FileRecievedFromName = string.IsNullOrEmpty(Convert.ToString(drList["OtherFileRecievedFrom"])) ? Convert.ToString(drList["FileRecievedFromName"]) : Convert.ToString(drList["OtherFileRecievedFrom"]),
                                FileCategoryName = Convert.ToString(drList["FileCategoryName"]),
                            });
                        }
                        dashboardDataContract.OpenFilesList = listDispatchRegisterDataContract;

                    }


                    DataTable dtDCatw = ds.Tables[2];
                    if (dtDCatw.Rows.Count > 0)
                    {
                        foreach (DataRow drDCatw in dtDCatw.Rows)
                        {
                            categoryWiseCountList.Add(new CategoryWiseCount()
                            {
                                DocCount = Convert.ToInt32(drDCatw["DocCount"]),
                                DocCategory = Convert.ToString(drDCatw["DocumentCategoryName"]),
                            });


                        }

                        dashboardDataContract.CategoryWiseCountList = categoryWiseCountList;
                    }
                    DataTable dtDStsw = ds.Tables[3];
                    if (dtDStsw.Rows.Count > 0)
                    {
                        foreach (DataRow drDStsw in dtDStsw.Rows)
                        {
                            docStatusWiseCountList.Add(new DocStatusDataContract()
                            {
                                DocCount = Convert.ToInt32(drDStsw["DocCount"]),
                                DocStatus = Convert.ToString(drDStsw["DocStatus"]),
                            });
                        }

                        dashboardDataContract.StausWiseCountList = docStatusWiseCountList;
                    }
                    else
                    {
                        dashboardDataContractResponnse.Msg = "File Not Added";
                    }
                    dashboardDataContractResponnse.Data = dashboardDataContract;
                    serviceResponse.ServiceResponse = Response.Successful;
                    dashboardDataContractResponnse.ServiceResponse = serviceResponse.ServiceResponse;
                }
                else
                {
                    dashboardDataContractResponnse.Msg = "File Not Added";
                }
                return dashboardDataContractResponnse;
            }
            catch (Exception w)
            {

                dashboardDataContractResponnse.Msg = w.Message;
                return dashboardDataContractResponnse;
            }

        }



        public string getServerDate()
        {
            string strDate = DateTime.Now.Date.ToString("yyyy-MM-dd");
            return strDate;
        }

        public string getFileUniqueNumber()
        {
            string strDate = DateTime.Now.Date.ToString("yyyy-MM-dd");
            return strDate;
        }

        public DispatchRegisterDataContract getFileUniqueNumber(clsSearchFileParameters searchParams)
        {
            string strFileUniqueNumber = string.Empty;
            DispatchRegisterDataContract dispatchRegisterDataContract = new DispatchRegisterDataContract();
            try
            {

                SqlParameter[] par ={     new SqlParameter("@DRID",SqlDbType.Int), 
                                          new SqlParameter("@UserID",SqlDbType.Int),   
                                            new SqlParameter("@Designation",SqlDbType.Int),   
                                              new SqlParameter("@FileCategory",SqlDbType.Int),   
                                                new SqlParameter("@Status",SqlDbType.NVarChar),   
                                                  new SqlParameter("@FileUniqueNo",SqlDbType.NVarChar),   
                                                    new SqlParameter("@FromDate",SqlDbType.Date),   
                                                    new SqlParameter("@ToDate",SqlDbType.Date), 
                                                     new SqlParameter("@ProcType",SqlDbType.NVarChar),  
                                        
                                       
                                    };
                par[0].Value = searchParams.DRID;
                par[1].Value = searchParams.UserID;
                par[2].Value = searchParams.Designation;
                par[3].Value = searchParams.FileCategory;
                par[4].Value = searchParams.Status;
                par[5].Value = searchParams.FileUniqueNo;
                par[6].Value = searchParams.FromDate;
                par[7].Value = searchParams.ToDate;
                par[8].Value = "";

                ds.Clear();
                ds = obj_sqldal.ExecuteDatasetSP("spDispatchRegister_GetFileUniqueNumbe", par);

                if (ds != null && ds.Tables.Count > 0) //Tip Details Success
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        dispatchRegisterDataContract.FileCode = Convert.ToString(dt.Rows[0]["FileUniqueNumber"]);
                        dispatchRegisterDataContract.FileNumber = Convert.ToString(dt.Rows[0]["DocInwardOutwardCounter"]);

                    }
                    else
                    {
                        strFileUniqueNumber = "File Not Added";
                    }
                }
                else
                {
                    strFileUniqueNumber = "File Not Added";
                }
                return dispatchRegisterDataContract;
            }
            catch (Exception w)
            {

                strFileUniqueNumber = w.Message;
                return dispatchRegisterDataContract;
            }
        }
        
        public YearDataContractResponse getyears()
        {
            YearDataContractResponse yearDataContractResponse = new YearDataContractResponse();
            ServiceResponse serviceResponse = new ServiceResponse();
            serviceResponse.ServiceResponse = Response.Failed;
            yearDataContractResponse.Msg = null;
            List<YearDataContract> listYearDataContract = new List<YearDataContract>();
            YearDataContract yearDataContract = new YearDataContract();

            try
            {
                ds.Clear();
                ds = obj_sqldal.ExecuteDatasetSP("spYear_GetYears");

                if (ds != null && ds.Tables.Count > 0) //Tip Details Success
                {

                    DataTable dtdash = ds.Tables[0];
                    if (dtdash.Rows.Count > 0)
                    {
                        foreach (DataRow drDash in dtdash.Rows)
                        {
                            listYearDataContract.Add(new YearDataContract()
                            {
                                yearValue = Convert.ToString(drDash["yearValue"]),
                                yearDisplay = Convert.ToString(drDash["yearDisplay"])

                            });

                        }
                    }
                    else
                    {
                        yearDataContractResponse.Msg = "No data  Found";
                    }
                    yearDataContractResponse.Data = listYearDataContract;
                    serviceResponse.ServiceResponse = Response.Successful;
                    yearDataContractResponse.ServiceResponse = serviceResponse.ServiceResponse;
                }
                else
                {
                    yearDataContractResponse.Msg = "No data  Found";
                }
                return yearDataContractResponse;
            }
            catch (Exception w)
            {

                yearDataContractResponse.Msg = w.Message;
                return yearDataContractResponse;
            }

        }

        public DocStatusContractResponse getdocstaus()
        {
            DocStatusContractResponse docStatusContractResponse = new DocStatusContractResponse();
            ServiceResponse serviceResponse = new ServiceResponse();
            serviceResponse.ServiceResponse = Response.Failed;
            docStatusContractResponse.Msg = null;
            List<DocStatusDataContract> listDocStatusDataContract = new List<DocStatusDataContract>();
            YearDataContract yearDataContract = new YearDataContract();

            try
            {
                ds.Clear();
                ds = obj_sqldal.ExecuteDatasetSP("spDocStatus_GetDocStatus");

                if (ds != null && ds.Tables.Count > 0) //Tip Details Success
                {

                    DataTable dtdash = ds.Tables[0];
                    if (dtdash.Rows.Count > 0)
                    {
                        foreach (DataRow drDash in dtdash.Rows)
                        {
                            listDocStatusDataContract.Add(new DocStatusDataContract()
                            {
                                DocStatusID = Convert.ToInt32(drDash["DocStatusID"]),
                                DocStatus = Convert.ToString(drDash["DocStatus"])

                            });

                        }
                    }
                    else
                    {
                        docStatusContractResponse.Msg = "No data  Found";
                    }
                    docStatusContractResponse.Data = listDocStatusDataContract;
                    serviceResponse.ServiceResponse = Response.Successful;
                    docStatusContractResponse.ServiceResponse = serviceResponse.ServiceResponse;
                }
                else
                {
                    docStatusContractResponse.Msg = "No data  Found";
                }
                return docStatusContractResponse;
            }
            catch (Exception w)
            {

                docStatusContractResponse.Msg = w.Message;
                return docStatusContractResponse;
            }

        }

        #region Role
        public UserRoleResponseDataContract GetRoles()
        {
            UserRoleResponseDataContract userRoleResponseDataContract = new UserRoleResponseDataContract();
            ServiceResponse serviceResponse = new ServiceResponse();
            serviceResponse.ServiceResponse = Response.Failed;
            userRoleResponseDataContract.Msg = null;
            try
            {
                List<UserRoleDataContract> listUserRoleDataContract = new List<UserRoleDataContract>();
              
                ds.Clear();
                ds = obj_sqldal.ExecuteDatasetSP("spGetRole");

                if (ds != null && ds.Tables.Count > 0) //Tip Details Success
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            listUserRoleDataContract.Add(new UserRoleDataContract()
                            {
                                RoleID = Convert.ToInt32(dr["RoleID"]),
                                RoleName = Convert.ToString(dr["RoleName"])

                            });
                        }
                        userRoleResponseDataContract.Data = listUserRoleDataContract;
                        serviceResponse.ServiceResponse = Response.Successful;
                        userRoleResponseDataContract.ServiceResponse = serviceResponse.ServiceResponse;
                    }
                    else
                    {
                        userRoleResponseDataContract.Msg = "No Data Found";
                    }
                }
                else
                {
                    userRoleResponseDataContract.Msg = "No Data Found";
                }
                return userRoleResponseDataContract;
            }
            catch (Exception w)
            {

                userRoleResponseDataContract.Msg = w.Message;
                return userRoleResponseDataContract;
            }
        }

        #endregion

        #region Department 
        public DepartmentDataContractResponse GetDepartments()
        {
            DepartmentDataContractResponse departmentDataContractResponse = new DepartmentDataContractResponse();
            ServiceResponse serviceResponse = new ServiceResponse();
            serviceResponse.ServiceResponse = Response.Failed;
            departmentDataContractResponse.Msg = null;
            try
            {
                List<DepartmentDataContract> listDepartmentDataContract = new List<DepartmentDataContract>();

                ds.Clear();
                ds = obj_sqldal.ExecuteDatasetSP("spDept_getDepartment");

                if (ds != null && ds.Tables.Count > 0) //Tip Details Success
                {
                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            listDepartmentDataContract.Add(new DepartmentDataContract()
                            {
                                DeptID = Convert.ToInt32(dr["DeptID"]),
                                DepartmentName = Convert.ToString(dr["DepartmentName"]),
                                 DeptCode = Convert.ToString(dr["DeptCode"])

                            });
                        }
                        departmentDataContractResponse.Data = listDepartmentDataContract;
                        serviceResponse.ServiceResponse = Response.Successful;
                        departmentDataContractResponse.ServiceResponse = serviceResponse.ServiceResponse;
                    }
                    else
                    {
                        departmentDataContractResponse.Msg = "No Data Found";
                    }
                }
                else
                {
                    departmentDataContractResponse.Msg = "No Data Found";
                }
                return departmentDataContractResponse;
            }
            catch (Exception w)
            {

                departmentDataContractResponse.Msg = w.Message;
                return departmentDataContractResponse;
            }
        }

        #endregion
    }
}