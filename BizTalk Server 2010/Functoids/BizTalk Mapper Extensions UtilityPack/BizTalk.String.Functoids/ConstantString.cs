using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.BizTalk.BaseFunctoids;
using System.Reflection;

namespace BizTalk.String.Functoids
{
    [Serializable]
    public class ConstantString : BaseFunctoid
    {
        public ConstantString()
            : base()
        {
            //ID for this functoid
            this.ID = 10000;

            // resource assembly must be ProjectName.ResourceName if building with VS.Net
            SetupResourceAssembly("BizTalk.String.Functoids.StringResources", Assembly.GetExecutingAssembly());

            //Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
            SetName("IDS_CONSTANTFUNCTOID_NAME");
            SetTooltip("IDS_CONSTANTFUNCTOID_TOOLTIP");
            SetDescription("IDS_CONSTANTFUNCTOID_DESCRIPTION");
            SetBitmap("IDS_CONSTANTFUNCTOID_BITMAP");

            //category for this functoid. This functoid goes under the String Functoid Tab in the
            //VS.Net toolbox for functoids.
            this.Category = FunctoidCategory.String;

            // Set the limits for the number of input parameters.
            this.SetMinParams(1);
            this.SetMaxParams(1);

            // Add one line of code as set out below for each input param. For multiple input params, each line would be identical.
            AddInputConnectionType(ConnectionType.AllExceptRecord); //first input

            // The functoid output can go to any node type.
            this.OutputConnectionType = ConnectionType.AllExceptRecord;

            // Set the function name that is to be called when invoking this functoid.
            // To test the map in Visual Studio, this functoid does not need to be in the GAC.
            // If using this functoid in a deployed BizTalk app. then it must be in the GAC
            //SetExternalFunctionName(GetType().Assembly.FullName, GetType().FullName, "SetConstantValue");

            SetScriptBuffer(ScriptType.CSharp, this.GetCSharpBuffer());
            HasSideEffects = false;
        }

        private string GetCSharpBuffer()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("public string SetConstantValue(string value)\n");
            builder.Append("{\n");
            builder.Append("\treturn value;\n");
            builder.Append("}\n");
            return builder.ToString();
        }

        //this is the function that gets called when the Map is executed which has this functoid.
        /// <summary>
        /// Use the String Constant functoid to return a value. This functoid only accept one value and allows you to set constant values in elements.
        /// </summary>
        /// <param name="value">String to return</param>
        /// <returns>Returns the a string value</returns>
        public string SetConstantValue(string value)
        {
            return value;
        }
    }
}
