using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.BizTalk.BaseFunctoids;
using System.Reflection;

namespace BizTalk.String.Functoids
{
    [Serializable]
    public class Replace : BaseFunctoid
    {
        public Replace()
            : base()
        {
            //ID for this functoid
            this.ID = 10003;

            // resource assembly must be ProjectName.ResourceName if building with VS.Net
            SetupResourceAssembly("BizTalk.String.Functoids.StringResources", Assembly.GetExecutingAssembly());

            //Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
            SetName("IDS_REPLACEFUNCTOID_NAME");
            SetTooltip("IDS_REPLACEFUNCTOID_TOOLTIP");
            SetDescription("IDS_REPLACEFUNCTOID_DESCRIPTION");
            SetBitmap("IDS_REPLACEFUNCTOID_BITMAP");

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
            //SetExternalFunctionName(GetType().Assembly.FullName, GetType().FullName, "ReplaceText");

            SetScriptBuffer(ScriptType.CSharp, this.GetCSharpBuffer());
            HasSideEffects = false;
        }

        private string GetCSharpBuffer()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("public string ReplaceText(string element, string oldValue, string newValue)\n");
            builder.Append("{\n");
            builder.Append("\treturn element.Replace(oldValue, newValue);\n");
            builder.Append("}\n");
            return builder.ToString();
        }

        /// <summary>
        /// Returns a new string in which all occurrences of a specified string in the current instance are replaced with another specified string.
        /// </summary>
        /// <param name="element">String where we will replace the values</param>
        /// <param name="oldValue">The string to be replaced. </param>
        /// <param name="newValue">The string to replace all occurrences of oldValue.</param>
        /// <returns>A string that is equivalent to the current string except that all instances of oldValue are replaced with newValue. If oldValue is not found in the current instance, the method returns the current instance unchanged.</returns>
        public string ReplaceText(string element, string oldValue, string newValue)
        {
            return element.Replace(oldValue, newValue);
        }

    }
}
