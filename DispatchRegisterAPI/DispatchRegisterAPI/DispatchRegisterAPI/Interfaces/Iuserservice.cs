using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DispatchRegisterAPI.Interfaces;
using DispatchRegisterAPI.ServiceDataContract;
using System.ServiceModel.Web;

namespace DispatchRegisterAPI.Interfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "Iuserservice" in both code and config file together.
    [ServiceContract]
    public interface Iuserservice
    {
        #region Login Module
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "user/authenticate", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        UserDetailResponseDataContract Authenticate(UserDetailDataContract login);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "user/setpassword", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        UserDetailResponseDataContract SetPassword(UserDetailDataContract login);

        //[OperationContract]
        //[WebInvoke(Method = "POST", UriTemplate = "user/signup", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        //UserDetailResponseDataContract SignUp(UserDetailDataContract login);

        //[OperationContract]
        //[WebGet(UriTemplate = "user/validateuser?username={username}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //UserDetailResponseDataContract ValidateUser(string username);
         #endregion

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "user/adduser", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        UserDetailResponseDataContract AddUser(UserDetailDataContract objUser);

        [OperationContract]
        [WebGet(UriTemplate = "user/getuser?userid={userid}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UserDetailResponseDataContract GetUserList(string userid);

        [OperationContract]
        [WebGet(UriTemplate = "user/getusers?designationid={designationid}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UserDetailResponseDataContract GetUserListByDesignation(string designationid);
        [OperationContract]
        [WebGet(UriTemplate = "user/userrole", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        UserRoleResponseDataContract GetRoles();
        [OperationContract]
        [WebGet(UriTemplate = "module/getmodules", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ModuleResponseDataContract GetModules();

        [OperationContract]
        [WebGet(UriTemplate = "designation/getdesignations", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DesignationResponseDataContract GetDesignationList();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "file/addfile", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        DispatchRegisterResponseDataContract AddFile(DispatchRegisterDataContract dispatchRegisterDataContract);
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "file/updatefile", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        DispatchRegisterResponseDataContract UpdateFile(DispatchRegisterDataContract dispatchRegisterDataContract);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "file/files", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        DispatchRegisterResponseDataContract GetFiles(clsSearchFileParameters searchParams);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "file/reportdata", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        DispatchRegisterResponseDataContract GetReportFiles(clsSearchFileParameters searchParams);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "file/globalreportdata", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        DispatchRegisterResponseDataContract GetReportGlobalFiles(DispatchRegisterDataContract searchParams);

        [OperationContract]
        [WebGet(UriTemplate = "doccategory/getdocumentcategories", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DocCategoryResponseDataContract GetDocumentCategories();

        [OperationContract]
        [WebGet(UriTemplate = "dashboard/getdashboarddata?userid={userid}&year={year}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DashboardDataContractResponnse getdashboard(string userid,string year);

        [OperationContract]
        [WebGet(UriTemplate = "serverdate/getserverdate", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string getServerDate();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "file/fileuniquenumber", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        DispatchRegisterDataContract getFileUniqueNumber(clsSearchFileParameters searchParams);


        [OperationContract]
        [WebGet(UriTemplate = "file/getdocrefno?duidn={duidn}&docreceivedfromid={docreceivedfromid}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DispatchRegisterResponseDataContract GetDocRefNo(string duidn, string docreceivedfromid);


        [OperationContract]
        [WebGet(UriTemplate = "years/getyears", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        YearDataContractResponse getyears();
        [OperationContract]
        [WebGet(UriTemplate = "file/docstatus", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DocStatusContractResponse getdocstaus();

        [OperationContract]
        [WebGet(UriTemplate = "department/getdepartment", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        DepartmentDataContractResponse GetDepartments();
    }
}
