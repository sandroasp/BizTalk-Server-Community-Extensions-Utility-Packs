using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.BizTalk.BaseFunctoids;
using System.Reflection;

namespace BizTalk.CustomAdvanced.Functoids
{
    [Serializable]
    public class DefaultValueMapping : BaseFunctoid
    {

        public DefaultValueMapping()
            : base()
		{
			//ID for this functoid
            this.ID = 10100;

			// resource assembly must be ProjectName.ResourceName if building with VS.Net
            SetupResourceAssembly("BizTalk.CustomAdvanced.Functoids.CustomAdvancedResources", Assembly.GetExecutingAssembly());

			//Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
            SetName("IDS_DEFAULTVALUEMAPFUNCTOID_NAME");
            SetTooltip("IDS_DEFAULTVALUEMAPFUNCTOID_TOOLTIP");
            SetDescription("IDS_DEFAULTVALUEMAPFUNCTOID_DESCRIPTION");
            SetBitmap("IDS_DEFAULTVALUEMAPFUNCTOID_BITMAP");


            //category for this functoid. This functoid goes under the String Functoid Tab in the
            this.Category = FunctoidCategory.None;

            // Set the limits for the number of input parameters. This example: 1 parameter
            this.SetMinParams(2);
            this.SetMaxParams(2);

            // Add one line of code as set out below for each input param. For multiple input params, each line would be identical.
            AddInputConnectionType(ConnectionType.AllExceptRecord); //first input
            AddInputConnectionType(ConnectionType.AllExceptRecord); //Second input

            // The functoid output can go to any node type.
            this.OutputConnectionType = ConnectionType.AllExceptRecord;

            // Set the function name that is to be called when invoking this functoid.
            // To test the map in Visual Studio, this functoid does not need to be in the GAC.
            // If using this functoid in a deployed BizTalk app. then it must be in the GAC
            SetExternalFunctionName(GetType().Assembly.FullName, GetType().FullName, "ReturnValueFromOptionalField");

            SetScriptBuffer(ScriptType.CSharp, this.GetCSharpBuffer());
            HasSideEffects = false;
		}

        private string GetCSharpBuffer()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("public string ReturnValueFromOptionalField(string input, string defaultValue)\n");
            builder.Append("{\n");
            builder.Append("\tif (System.String.IsNullOrEmpty(input))\n");
            builder.Append("\t\treturn defaultValue;\n");
            builder.Append("\treturn input;\n");
            builder.Append("}\n");
            return builder.ToString();
        }

        public string ReturnValueFromOptionalField(string input, string defaultValue)
        {
            if (System.String.IsNullOrEmpty(input))
                return defaultValue;
            return input;
        }
    }
}
