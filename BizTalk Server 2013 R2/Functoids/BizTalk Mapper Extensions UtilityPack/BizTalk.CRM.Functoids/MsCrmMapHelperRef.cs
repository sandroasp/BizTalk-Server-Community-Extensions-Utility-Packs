using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.BizTalk.BaseFunctoids;
using System.Reflection;

namespace BizTalk.MapperExtensions.CRMFunctoids
{
    [Serializable]
    public class MsCrmMapHelperRef : BaseFunctoid
    {
        public MsCrmMapHelperRef()
            : base()
        {

            //ID for this functoid
            this.ID = 329997;

            // resource assembly must be ProjectName.ResourceName if building with VS.Net
            SetupResourceAssembly("BizTalk.MapExtention.LookupFunctoid.MsCrmMapHelperResource", Assembly.GetExecutingAssembly());

            //Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
            SetName("FUNCTOID_NAME_REF");
            SetTooltip("FUNCTOID_TOOLTIP_REF");
            SetDescription("FUNCTOID_DESCRIPTION_REF");
            SetBitmap("FUNCTOID_BITMAP_REF");

            //category for this functoid. This functoid goes under the String Functoid Tab in the
            this.Category = FunctoidCategory.None;

            // Set the limits for the number of input parameters. This example: 1 parameter
            this.SetMinParams(3);
            this.SetMaxParams(3);

            // Add one line of code as set out below for each input param. For multiple input params, each line would be identical.
            AddInputConnectionType(ConnectionType.All); //first input
            AddInputConnectionType(ConnectionType.All); //second input
            AddInputConnectionType(ConnectionType.All); //third input

            // The functoid output can go to any node type.
            this.OutputConnectionType = ConnectionType.All;

            SetScriptBuffer(ScriptType.XsltCallTemplate, DefineTemplate());

        }

        private string DefineTemplate()
        {

            StringBuilder returnValue = new StringBuilder();

            returnValue.AppendLine(@"<xsl:template name=""MsCrmMapHelperRef"">");
            returnValue.AppendLine(@"<xsl:param name=""param1"" />");
            returnValue.AppendLine(@"<xsl:param name=""param2"" />");
            returnValue.AppendLine(@"<xsl:param name=""param3"" />");
            returnValue.AppendLine(@"<key xmlns=""http://schemas.datacontract.org/2004/07/System.Collections.Generic""><xsl:value-of select=""$param2"" /></key>");
            returnValue.AppendLine(@"<value xmlns=""http://schemas.datacontract.org/2004/07/System.Collections.Generic"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:a=""http://schemas.microsoft.com/xrm/2011/Contracts"">");
            returnValue.AppendLine(@"<xsl:attribute name=""xsi:type"">");
            returnValue.AppendLine(@"<xsl:value-of select=""'a:EntityReference'"" />");
            returnValue.AppendLine(@"</xsl:attribute>");
            returnValue.AppendLine(@"<Id xmlns=""http://schemas.microsoft.com/xrm/2011/Contracts"" xmlns:ser=""http://schemas.microsoft.com/2003/10/Serialization/"">");
            returnValue.AppendLine(@"<xsl:attribute name=""xsi:type""><xsl:value-of select=""'ser:guid'"" /></xsl:attribute><xsl:value-of select=""$param1"" /></Id>");
            returnValue.AppendLine(@"<LogicalName xmlns=""http://schemas.microsoft.com/xrm/2011/Contracts""><xsl:value-of select=""$param3"" /></LogicalName>");
            returnValue.AppendLine(@"<Name xmlns=""http://schemas.microsoft.com/xrm/2011/Contracts"" />");
            returnValue.AppendLine(@"</value>");
            returnValue.AppendLine(@"</xsl:template>");

            return returnValue.ToString();

        }

    }
}
