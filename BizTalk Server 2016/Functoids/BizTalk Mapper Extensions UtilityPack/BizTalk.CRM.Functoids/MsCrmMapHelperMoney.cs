using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.BizTalk.BaseFunctoids;
using System.Reflection;

namespace BizTalk.MapperExtensions.CRMFunctoids
{
    [Serializable]
    public class MsCrmMapHelperMoney : BaseFunctoid
    {
        public MsCrmMapHelperMoney()
            : base()
        {

            //ID for this functoid
            this.ID = 329995;

            // resource assembly must be ProjectName.ResourceName if building with VS.Net
            SetupResourceAssembly("BizTalk.MapExtention.LookupFunctoid.MsCrmMapHelperResource", Assembly.GetExecutingAssembly());

            //Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
            SetName("FUNCTOID_NAME_MONEY");
            SetTooltip("FUNCTOID_TOOLTIP_MONEY");
            SetDescription("FUNCTOID_DESCRIPTION_MONEY");
            SetBitmap("FUNCTOID_BITMAP_MONEY");

            //category for this functoid. This functoid goes under the String Functoid Tab in the
            this.Category = FunctoidCategory.None;

            // Set the limits for the number of input parameters. This example: 1 parameter
            this.SetMinParams(2);
            this.SetMaxParams(2);

            // Add one line of code as set out below for each input param. For multiple input params, each line would be identical.
            AddInputConnectionType(ConnectionType.All); //first input
            AddInputConnectionType(ConnectionType.All); //second input

            // The functoid output can go to any node type.
            this.OutputConnectionType = ConnectionType.All;

            SetScriptBuffer(ScriptType.XsltCallTemplate, DefineTemplate());

        }

        private string DefineTemplate()
        {

            StringBuilder returnValue = new StringBuilder();

            returnValue.AppendLine(@"<xsl:template name=""MsCrmMapHelperMoney"">");
            returnValue.AppendLine(@"<xsl:param name=""param1"" />");
            returnValue.AppendLine(@"<xsl:param name=""param2"" />");
            returnValue.AppendLine(@"<key xmlns=""http://schemas.datacontract.org/2004/07/System.Collections.Generic""><xsl:value-of select=""$param2"" /></key>");
            returnValue.AppendLine(@"<value xmlns=""http://schemas.datacontract.org/2004/07/System.Collections.Generic"" xmlns:xs=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:a=""http://schemas.microsoft.com/xrm/2011/Contracts"">");
            returnValue.AppendLine(@"<xsl:attribute name=""xsi:type"">");
            returnValue.AppendLine(@"<xsl:value-of select=""'a:Money'"" />");
            returnValue.AppendLine(@"</xsl:attribute>");
            returnValue.AppendLine(@"<Value xmlns=""http://schemas.microsoft.com/xrm/2011/Contracts"">");
            returnValue.AppendLine(@"<xsl:value-of select=""$param1"" />");
            returnValue.AppendLine(@"</Value>");
            returnValue.AppendLine(@"</value>");
            returnValue.AppendLine(@"</xsl:template>");

            return returnValue.ToString();

        }

    }
}
