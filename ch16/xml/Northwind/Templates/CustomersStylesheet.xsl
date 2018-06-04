<?xml version="1.0"?>
<xsl:stylesheet
 xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
 version="1.0">
<xsl:template match="/">

<HTML>
<HEAD>
  <TITLE>Customers</TITLE>
</HEAD>
<BODY>
  <xsl:for-each select="Northwind/Customers">
    <p>
    <b>Customer:</b>
    <br><xsl:value-of select="CustomerID"/></br>
    <br><xsl:value-of select="CompanyName"/></br>
    <br><xsl:value-of select="PostalCode"/></br>
    <br><xsl:value-of select="Country"/></br>
    <br><xsl:value-of select="Phone"/></br>
    </p>
  </xsl:for-each>
</BODY>
</HTML>

</xsl:template>
</xsl:stylesheet>