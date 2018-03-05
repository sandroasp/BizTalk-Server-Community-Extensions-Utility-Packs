using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.BizTalk.BaseFunctoids;
using System.Reflection;
using System.Globalization;

namespace BizTalk.String.Functoids
{
    [Serializable]
    public class ToTitle : BaseFunctoid
    {
        public ToTitle()
            : base()
        {
            //ID for this functoid
            this.ID = 10001;

            // resource assembly must be ProjectName.ResourceName if building with VS.Net
            SetupResourceAssembly("BizTalk.String.Functoids.StringResources", Assembly.GetExecutingAssembly());

            //Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
            SetName("IDS_TOTITLECASEFUNCTOID_NAME");
            SetTooltip("IDS_TOTITLECASEFUNCTOID_TOOLTIP");
            SetDescription("IDS_TOTITLECASEFUNCTOID_DESCRIPTION");
            SetBitmap("IDS_TOTITLECASEFUNCTOID_BITMAP");

            //category for this functoid. This functoid goes under the String Functoid Tab in the
            this.Category = FunctoidCategory.String;

            // Set the limits for the number of input parameters. This example: 1 parameter
            this.SetMinParams(1);
            this.SetMaxParams(1);

            // Add one line of code as set out below for each input param. For multiple input params, each line would be identical.
            AddInputConnectionType(ConnectionType.AllExceptRecord); //first input

            // The functoid output can go to any node type.
            this.OutputConnectionType = ConnectionType.AllExceptRecord;

            // Set the function name that is to be called when invoking this functoid.
            // To test the map in Visual Studio, this functoid does not need to be in the GAC.
            // If using this functoid in a deployed BizTalk app. then it must be in the GAC
            //SetExternalFunctionName(GetType().Assembly.FullName, GetType().FullName, "ToTitleCase");

            SetScriptBuffer(ScriptType.CSharp, this.GetCSharpBuffer());
            HasSideEffects = false;
        }

        private string GetCSharpBuffer()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("public string ToTitleCase(string input)\n");
            builder.Append("{\n");
            builder.Append("\treturn System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input);\n");
            builder.Append("}\n");
            return builder.ToString();
        }

        /// <summary>
        /// Converts the specified string to title case
        /// (except for words that are entirely in uppercase, which are considered to be acronyms).
        /// </summary>
        /// <param name="input">The string to convert to title case.</param>
        /// <returns>Returns the specified string converted to title case.</returns>
        public string ToTitleCase(string input)
        {
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input);
        }

    }
}
