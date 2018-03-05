<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.BizTalk.BaseFunctoids;
using System.Text.RegularExpressions;

namespace BizTalk.String.Functoids
{
    [Serializable]
    public class RemoveLeadingZeros:BaseFunctoid
{
        public RemoveLeadingZeros()
            : base()
        {
            //ID for this functoid
            this.ID = 10008;

            // resource assembly must be ProjectName.ResourceName if building with VS.Net
            SetupResourceAssembly("BizTalk.String.Functoids.StringResources", Assembly.GetExecutingAssembly());

            //Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
            SetName("IDS_REMOVELEADINGZEROSFUNCTOID_NAME");
            SetTooltip("IDS_REMOVELEADINGZEROSFUNCTOID_TOOLTIP");
            SetDescription("IDS_REMOVELEADINGZEROSFUNCTOID_DESCRIPTION");
            SetBitmap("IDS_REMOVELEADINGZEROSFUNCTOID_BITMAP");

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
            //SetExternalFunctionName(GetType().Assembly.FullName, GetType().FullName, "RemoveZeros");

            SetScriptBuffer(ScriptType.CSharp, this.GetCSharpBuffer());
            HasSideEffects = false;

        }

        private string GetCSharpBuffer()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("public Int32 RemoveZeros(string oldValue)\n");
            builder.Append("{\n");
            builder.Append("\t\treturn Int32.Parse(Regex.Replace(oldValue, \"^0+\",\"\"));\n");
            builder.Append("}\n");
            return builder.ToString();
        }

        /// <summary>
        /// Returns an integer in which all leading zeros are removed.
        /// </summary>
        /// <param name="oldValue">String where we will remove the leading zeros</param>
        /// <returns>An integer</returns>
        public Int32 RemoveZeros(string oldValue)
        {
            return Int32.Parse(Regex.Replace(oldValue, "^0+", ""));
        }

    }
}

=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.BizTalk.BaseFunctoids;
using System.Text.RegularExpressions;

namespace BizTalk.String.Functoids
{
    [Serializable]
    public class RemoveLeadingZeros:BaseFunctoid
{
        public RemoveLeadingZeros()
            : base()
        {
            //ID for this functoid
            this.ID = 10008;

            // resource assembly must be ProjectName.ResourceName if building with VS.Net
            SetupResourceAssembly("BizTalk.String.Functoids.StringResources", Assembly.GetExecutingAssembly());

            //Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
            SetName("IDS_REMOVELEADINGZEROSFUNCTOID_NAME");
            SetTooltip("IDS_REMOVELEADINGZEROSFUNCTOID_TOOLTIP");
            SetDescription("IDS_REMOVELEADINGZEROSFUNCTOID_DESCRIPTION");
            SetBitmap("IDS_REMOVELEADINGZEROSFUNCTOID_BITMAP");

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
            //SetExternalFunctionName(GetType().Assembly.FullName, GetType().FullName, "RemoveZeros");

            SetScriptBuffer(ScriptType.CSharp, this.GetCSharpBuffer());
            HasSideEffects = false;

        }

        private string GetCSharpBuffer()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("public Int32 RemoveZeros(string oldValue)\n");
            builder.Append("{\n");
            builder.Append("\t\treturn Int32.Parse(Regex.Replace(oldValue, \"^0+\",\"\"));\n");
            builder.Append("}\n");
            return builder.ToString();
        }

        /// <summary>
        /// Returns an integer in which all leading zeros are removed.
        /// </summary>
        /// <param name="oldValue">String where we will remove the leading zeros</param>
        /// <returns>An integer</returns>
        public Int32 RemoveZeros(string oldValue)
        {
            return Int32.Parse(Regex.Replace(oldValue, "^0+", ""));
        }

    }
}

>>>>>>> origin/master
