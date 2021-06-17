using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Xsl;
using System.IO;
using Fonet;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fonet.Render.Pdf;
namespace DispatchRegisterAPI.Models
{
    public class GeneratePDF
    {
        public byte[] StreamPDF(XmlDocument objSourceData, string XSLTFile)
        {
            try
            {

                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load(XSLTFile);
                MemoryStream ms = new MemoryStream();
                xslt.Transform(objSourceData, null, ms);
                MemoryStream output = new MemoryStream();
                XmlDocument doc = new XmlDocument();
                ms.Position = 0;
                doc.Load(ms);
                PdfRendererOptions options = new PdfRendererOptions();
                options.FontType = FontType.Embed;
                options.Kerning = true;
                // Create the pdf
                FonetDriver driver = FonetDriver.Make();
                  driver.Options = options;
                driver.Render(doc, output);
                ms.Dispose();
                return output.ToArray();
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public string DStoXML(DataSet dsXML)
        {
            string rtnXML = "";
            try
            {
                StringWriter stringWriter = new StringWriter();
                XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
                dsXML.WriteXml(xmlTextWriter, XmlWriteMode.IgnoreSchema);
                rtnXML = stringWriter.ToString();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return rtnXML;

        }
    }
}