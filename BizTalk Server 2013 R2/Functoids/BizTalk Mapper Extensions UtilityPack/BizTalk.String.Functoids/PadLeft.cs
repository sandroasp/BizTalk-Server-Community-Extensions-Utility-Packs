using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.BizTalk.BaseFunctoids;
using System.Reflection;

namespace BizTalk.String.Functoids
{
    [Serializable]
    public class PadLeft : BaseFunctoid
    {
        public PadLeft()
            : base()
        {
            //ID for this functoid
            this.ID = 10005;

            // resource assembly must be ProjectName.ResourceName if building with VS.Net
            SetupResourceAssembly("BizTalk.String.Functoids.StringResources", Assembly.GetExecutingAssembly());

            //Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
            SetName("IDS_PADLEFTFUNCTOID_NAME");
            SetTooltip("IDS_PADLEFTFUNCTOID_TOOLTIP");
            SetDescription("IDS_PADLEFTFUNCTOID_DESCRIPTION");
            SetBitmap("IDS_PADLEFTFUNCTOID_BITMAP");

            //category for this functoid. This functoid goes under the String Functoid Tab in the
            this.Category = FunctoidCategory.String;

            // Set the limits for the number of input parameters. This example: 1 parameter
            this.SetMinParams(3);
            this.SetMaxParams(3);

            // Add one line of code as set out below for each input param. For multiple input params, each line would be identical.
            AddInputConnectionType(ConnectionType.AllExceptRecord); //first input
            AddInputConnectionType(ConnectionType.AllExceptRecord); //Second input
            AddInputConnectionType(ConnectionType.AllExceptRecord); //Third input

            // The functoid output can go to any node type.
            this.OutputConnectionType = ConnectionType.AllExceptRecord;

            // Set the function name that is to be called when invoking this functoid.
            // To test the map in Visual Studio, this functoid does not need to be in the GAC.
            // If using this functoid in a deployed BizTalk app. then it must be in the GAC
            //SetExternalFunctionName(GetType().Assembly.FullName, GetType().FullName, "PadLeftText");

            SetScriptBuffer(ScriptType.CSharp, this.GetCSharpBuffer());
            HasSideEffects = false;
        }

        /// <summary>
        /// Returns a new string that right-aligns the characters in this instance by padding them on the left with a specified Unicode character, for a specified total length.
        /// </summary>
        /// <param name="strOriginal">String that will be pad on the left with a specified Unicode character</param>
        /// <param name="totalWidth">The number of characters in the resulting string, equal to the number of original characters plus any additional padding characters. </param>
        /// <param name="paddingChar">A Unicode padding character. </param>
        /// <returns>A new string that is equivalent to this instance, but right-aligned and padded on the left with as many paddingChar characters as needed to create a length of totalWidth.</returns>
        public string PadLeftText(string strOriginal, int totalWidth, string paddingChar)
        {
            char padChar = '*';
            if (paddingChar.Length > 0)
            {
                padChar = paddingChar[0];
                return strOriginal.PadLeft(totalWidth, padChar);
            }
            return strOriginal;
        }

        private string GetCSharpBuffer()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("public string PadLeftText(string strOriginal, int totalWidth, string paddingChar)\n");
            builder.Append("{\n");
            builder.Append("\tchar padChar = '*';\n");
            builder.Append("\tif (paddingChar.Length > 0)\n");
            builder.Append("\t{\n");
            builder.Append("\t\tpadChar = paddingChar[0];\n");
            builder.Append("\t\treturn strOriginal.PadLeft(totalWidth, padChar);\n");
            builder.Append("\t}\n");
            builder.Append("\treturn strOriginal;\n");
            builder.Append("}\n");
            return builder.ToString();
        }
    }
}
