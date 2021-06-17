using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

using System.Data.SqlClient;
using DispatchRegisterAPI.Implementation;
using DispatchRegisterAPI.ServiceDataContract;

namespace DispatchRegisterAPI.Models
{
    public partial class ReportViwer : System.Web.UI.Page
    {
        SQLDAL bsql = new SQLDAL();
        GeneratePDF objpdf2 = new GeneratePDF();
        string Constr = System.Configuration.ConfigurationSettings.AppSettings["connectionString"].ToString();
        SqlConnection con = new SqlConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            string uid = Convert.ToString(Request.QueryString["uid"]);
            string duidn = Convert.ToString(Request.QueryString["duidn"]);
            string fromdate = Convert.ToString(Request.QueryString["fromdate"]);
            string todate = Convert.ToString(Request.QueryString["todate"]);
            string doccat = Convert.ToString(Request.QueryString["doccat"]);
            //if (!string.IsNullOrEmpty(fromdate) && string.IsNullOrEmpty(todate))
            //{
            //    todate = fromdate;
            //}
            //if (!string.IsNullOrEmpty(todate) && !string.IsNullOrEmpty(fromdate))
            //{
            //    fromdate = todate;
            //}

            try
            {
                SqlParameter[] par ={    
                                         new SqlParameter("@DRID",SqlDbType.Int), 
                                          new SqlParameter("@UserID",SqlDbType.Int),   
                                            new SqlParameter("@Designation",SqlDbType.Int),   
                                              new SqlParameter("@FileCategory",SqlDbType.Int),   
                                                new SqlParameter("@Status",SqlDbType.NVarChar),   
                                                  new SqlParameter("@FileUniqueNo",SqlDbType.NVarChar),   
                                                    new SqlParameter("@FromDate",SqlDbType.Date),   
                                                    new SqlParameter("@ToDate",SqlDbType.Date), 
                                                     new SqlParameter("@ProcType",SqlDbType.NVarChar),  
                                        
                                       
                                    };
                par[0].Value = null;
                par[1].Value = !string.IsNullOrEmpty(uid) ? uid : null;
                par[2].Value = null;
                par[3].Value = doccat != "0" ? doccat : null;
                par[4].Value = null;
                par[5].Value = !string.IsNullOrEmpty(duidn) ? duidn : null;
                par[6].Value = !string.IsNullOrEmpty(fromdate) ? fromdate : null;
                par[7].Value = !string.IsNullOrEmpty(todate) ? todate : null;
                par[8].Value = "";
                DataSet ds = bsql.ExecuteDatasetSP("spDispatchRegister_GetReportDispatchRegisterData", par);

                if (ds != null && ds.Tables.Count > 0) //Tip Details Success
                {
                    DataTable dtList = ds.Tables[0];
                    if (dtList.Rows.Count > 0)
                    {                      
                        foreach (DataRow drList in dtList.Rows)
                        {
                            var tupleData =GetDesignationListByIDs(Convert.ToString(drList["FileSentTo"]));
                            drList["FileSentTo"] = string.IsNullOrEmpty(Convert.ToString(drList["OtherFileSentTo"])) ? tupleData.Item2 : tupleData.Item2 + ", " + Convert.ToString(drList["OtherFileSentTo"]);
                        }
                    }

                }
                ds.DataSetName = "dsDoc";
                ds.Tables[0].TableName = "dtDoc";
                ds.Tables[1].TableName = "dtHeading";
                Session["dsreport2"] = null;
                Session["XSLPath2"] = null;
                Session["XSLPath2"] = Server.MapPath("~/models/Document.xslt");
                Session["dsReport2"] = ds;
                bool reportflag = false;
                if (Session["dsReport2"] != null)
                {
                    if (!string.IsNullOrEmpty(Session["dsReport2"].ToString()))
                    {
                        DataSet dsReports = (DataSet)Session["dsReport2"];
                        XmlDocument xml = new XmlDocument();
                        string xmlstring = objpdf2.DStoXML(dsReports);
                        xml.LoadXml(xmlstring);
                        string xslFile = Session["XSLPath2"].ToString();
                        byte[] getBytes;
                        getBytes = objpdf2.StreamPDF(xml, xslFile);

                        if (getBytes.Length > 0)
                        {
                            Response.Clear();
                            string FileName = "Document";// Request.QueryString["FileName"].ToString();
                            Response.AddHeader("content-type", "application/msword");
                            Response.ContentType = "application/pdf";
                            //Response.AddHeader("Accept-Header", getBytes.Length.ToString());
                            Response.AddHeader("content-disposition", "inline;filename=" + FileName+".pdf");
                            //Response.OutputStream.Write(getBytes, 0, Convert.ToInt32(getBytes.Length));
                            Response.BinaryWrite(getBytes);
                            Response.Flush();
                            try
                            {
                                Response.End();
                            }
                            catch
                            {
                            }
                            reportflag = true;
                            Session["dsReport2"] = null;
                        }
                    }
                }

                if (!reportflag)
                {
                    Response.Write("Invalid report inputs.");
                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
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

                DataSet dsDesig = bsql.ExecuteDatasetSP("spDesignation_GetDesignationByID", par);

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
    }

}