<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
 <xsl:output method="xml" indent="yes" encoding="utf-8"/>


  <xsl:template match="/">
    <fo:root xmlns:fo="http://www.w3.org/1999/XSL/Format">
      <fo:layout-master-set>
        <fo:simple-page-master master-name="dsDoc"
                      page-height="29.7cm"
                      page-width="40cm"
                      margin-top="1cm"
                      margin-bottom="1cm"
                      margin-left="1cm"
                      margin-right="1cm"
                      font="Kokila"
                        reference-orientation="180">
          <fo:region-body margin-bottom="1cm"/>
          <fo:region-after extent="1cm"/>
        </fo:simple-page-master>
      </fo:layout-master-set>


      <fo:page-sequence master-reference="dsDoc">
        <fo:static-content flow-name="xsl-region-after">
          <fo:block>
            <fo:leader color="black" leader-pattern="rule" rule-style="solid" leader-length="100%"/>
          </fo:block>
        </fo:static-content>

         <fo:flow flow-name="xsl-region-body">
          
  
          <fo:block padding-top="30pt">

            <fo:table table-layout="auto" width="100%" text-align="left" >
             <fo:table-column />
              <fo:table-column  />
              <fo:table-column />
              <fo:table-column />
              <fo:table-column column-width="130px"/>
              <fo:table-column column-width="130px"/>
              <fo:table-column/>
              <fo:table-column column-width="150px"/>
              <fo:table-column column-width="130px"/>
              <fo:table-column />
              <fo:table-column />
              <fo:table-column />

              <fo:table-body>
                <fo:table-row font-family="Kokila">

                  <fo:table-cell padding="3pt"  >
                    <fo:block font-size="10pt" font-weight="bold"  text-align="center">
                      <fo:external-graphic content-width="10cm" height="5cm">
                        <xsl:attribute name="src">http://192.168.5.6:8081/drapitest/models/drlogo1.png</xsl:attribute>
                      </fo:external-graphic>
                    </fo:block>
                  </fo:table-cell>
                  <fo:table-cell padding="3pt"  number-columns-spanned="10">
                    <fo:block font-size="10pt" text-align="center">
                     
                         <fo:block text-indent="10mm" font-size="20px" font-weight="bold" line-height="30px"  text-align="center">
           Online Despatch Register
          </fo:block>
           <fo:block text-indent="15mm" font-size="20px"  line-height="30px" text-align="center">
            (Office of  <xsl:value-of select="dsDoc/dtHeading/DesignationLongName" />)
          </fo:block>
                      <fo:block text-indent="15mm" font-size="15px"  line-height="30px" text-align="center">
                        (date from  <xsl:value-of select="dsDoc/dtHeading/FromDate" />  to <xsl:value-of select="dsDoc/dtHeading/ToDate" />)
                      </fo:block>
                    </fo:block>
                  </fo:table-cell>

                  <fo:table-cell padding="3pt"  >
                    <fo:block font-size="10pt" font-weight="bold" text-align="center">
                      <fo:external-graphic content-width="10cm" height="5cm" text-align="start">
                        <xsl:attribute name="src">http://192.168.5.6:8081/drapitest/models/drlogo2.png</xsl:attribute>
                      </fo:external-graphic>
                    </fo:block>
                  </fo:table-cell>
                </fo:table-row>

                <fo:table-row background-color="#F0EFEF" font-family="Kokila">
                  <fo:table-cell padding="3pt" border="1px solid black">
                    <fo:block font-size="10pt" font-weight="bold" >

                    </fo:block>
                  </fo:table-cell>
                  <fo:table-cell padding="3pt" border="1px solid black" >
                    <fo:block font-size="10pt" font-weight="bold" >

                    </fo:block>
                  </fo:table-cell>
                  <fo:table-cell padding="3pt" border="1px solid black" >
                    <fo:block font-size="10pt" font-weight="bold" >

                    </fo:block>
                  </fo:table-cell >
                  <fo:table-cell  background-color="#edeb47" padding="3pt" border="1px solid black" number-columns-spanned="5">
                    <fo:block font-size="16pt" font-weight="bold" text-align="center">
                      Document Incoming Details
                    </fo:block>
                  </fo:table-cell>

                  <fo:table-cell  background-color="#cad6a3" padding="3pt" border="1px solid black" number-columns-spanned="4">
                    <fo:block font-size="16pt" font-weight="bold" text-align="center">
                      Document Outgoing Details
                    </fo:block>
                  </fo:table-cell>
                </fo:table-row>
                <fo:table-row background-color="#F0EFEF" font-size="14pt"  font-family="Kokila">
                  <fo:table-cell padding="3pt" border="1px solid black" >
                    <fo:block  font-weight="bold" >
                      DUIDN
                    </fo:block>
                  </fo:table-cell>
                  <fo:table-cell  padding="3pt" border="1px solid black">
                    <fo:block  font-weight="bold" >
                     Office Inward / Outward No.
                  </fo:block>
                  </fo:table-cell>
                  <fo:table-cell    padding="3pt" border="1px solid black">
                    <fo:block  font-weight="bold" >
                      Document Category
                    </fo:block>
                  </fo:table-cell>

                  <fo:table-cell  background-color="#edeb47" padding="3pt" border="1px solid black">
                    <fo:block  font-weight="bold" >
                      Document Received Date (dd-mm-yyyy)
                    </fo:block>
                  </fo:table-cell>
                  <fo:table-cell  background-color="#edeb47" padding="3pt" border="1px solid black">
                    <fo:block  font-weight="bold" >
                      Document Received From
                    </fo:block>
                  </fo:table-cell>
                  <fo:table-cell  background-color="#edeb47" padding="3pt" border="1px solid black">
                    <fo:block  font-weight="bold" >
                      Document Reference No.
                    </fo:block>
                  </fo:table-cell>


                  <fo:table-cell  background-color="#edeb47" padding="3pt" border="1px solid black">
                    <fo:block  font-weight="bold" >
                      Document Reference Date (dd-mm-yyyy)
                    </fo:block>
                  </fo:table-cell>
                  <fo:table-cell  background-color="#edeb47" padding="3pt" border="1px solid black">
                    <fo:block  font-weight="bold" >
                      Subject
                    </fo:block>
                  </fo:table-cell>
                  <fo:table-cell  background-color="#cad6a3" padding="3pt" border="1px solid black">
                    <fo:block  font-weight="bold" >
                      Document Sent To
                    </fo:block>
                  </fo:table-cell>

                  <fo:table-cell  background-color="#cad6a3" padding="3pt" border="1px solid black">
                    <fo:block  font-weight="bold" >
                      Document Sent To Date (dd-mm-yyyy)
                    </fo:block>
                  </fo:table-cell>
                  <fo:table-cell  background-color="#cad6a3" padding="3pt" border="1px solid black">
                    <fo:block  font-weight="bold" >
                      Remark
                    </fo:block>
                  </fo:table-cell>
                  <fo:table-cell  background-color="#cad6a3" padding="3pt" border="1px solid black">
                    <fo:block  font-weight="bold" >
                      Status
                    </fo:block>
                  </fo:table-cell>

                </fo:table-row>
                <xsl:for-each select="dsDoc/dtDoc">
                  <fo:table-row font-size="12pt" font-family="Kokila">
                    <fo:table-cell padding="3pt" border="1px solid black">
                      <fo:block >
                        <xsl:value-of select="FileCode" />
                      </fo:block>
                    </fo:table-cell>
                    <fo:table-cell padding="3pt" border="1px solid black">
                      <fo:block >
                        <xsl:value-of select="FileNumber" />
                      </fo:block>
                    </fo:table-cell>
                    <fo:table-cell padding="3pt" border="1px solid black">
                      <fo:block>
                        <xsl:value-of select="FileCategoryName" />
                      </fo:block>
                    </fo:table-cell>
                    <fo:table-cell padding="3pt" border="1px solid black">
                      <fo:block >
                        <xsl:value-of select="FileIncomingDate" />
                      </fo:block>
                    </fo:table-cell>
                    <fo:table-cell padding="3pt" border="1px solid black">
                      <fo:block >
                        <xsl:value-of select="FileRecievedFromName" />
                      </fo:block>
                    </fo:table-cell>
                    <fo:table-cell padding="3pt" border="1px solid black">
                      <fo:block >
                        <xsl:value-of select="FileDispatchedNumber" />
                      </fo:block>
                    </fo:table-cell>

                    <fo:table-cell padding="3pt" border="1px solid black">
                      <fo:block >
                        <xsl:value-of select="FileDispatchedDate" />
                      </fo:block>
                    </fo:table-cell>
                    <fo:table-cell padding="3pt" border="1px solid black">
                      <fo:block >
                        <xsl:value-of select="Description" />
                      </fo:block>
                    </fo:table-cell>
                    <fo:table-cell padding="3pt" border="1px solid black">
                      <fo:block >
                        <xsl:value-of select="FileSentTo" />
                      </fo:block>
                    </fo:table-cell>
                    <fo:table-cell padding="3pt" border="1px solid black">
                      <fo:block >
                        <xsl:value-of select="FileSentDate" />
                      </fo:block>
                    </fo:table-cell>

                    <fo:table-cell padding="3pt" border="1px solid black">
                      <fo:block >
                        <xsl:value-of select="Remark" />
                      </fo:block>
                    </fo:table-cell>
                    <fo:table-cell padding="3pt" border="1px solid black">
                      <fo:block >
                        <xsl:value-of select="Status" />
                      </fo:block>
                    </fo:table-cell>
                  </fo:table-row>
                </xsl:for-each>
              </fo:table-body>
            </fo:table>
          </fo:block>
        </fo:flow>
      </fo:page-sequence>
    </fo:root>
  </xsl:template>
</xsl:stylesheet>
