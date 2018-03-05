<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.BizTalk.BaseFunctoids;
using System.Reflection;

namespace BizTalk.String.Functoids
{
    [Serializable]
    public class AdvanceCompare : BaseFunctoid
    {
        public AdvanceCompare()
            : base()
        {
            //ID for this functoid
            this.ID = 10002;

            // resource assembly must be ProjectName.ResourceName if building with VS.Net
            SetupResourceAssembly("BizTalk.String.Functoids.StringResources", Assembly.GetExecutingAssembly());

            //Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
            SetName("IDS_ADVCOMPAREFUNCTOID_NAME");
            SetTooltip("IDS_ADVCOMPAREFUNCTOID_TOOLTIP");
            SetDescription("IDS_ADVCOMPAREFUNCTOID_DESCRIPTION");
            SetBitmap("IDS_ADVCOMPAREFUNCTOID_BITMAP");

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
            //SetExternalFunctionName(GetType().Assembly.FullName, GetType().FullName, "Compare");

            SetScriptBuffer(ScriptType.CSharp, this.GetCSharpBuffer());
            HasSideEffects = false;
        }

        private string GetCSharpBuffer()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("public bool Compare(string strA, string strB, string ignoreCase)\n");
            builder.Append("{\n");
            builder.Append("\tbool isToIgnoreCase = false;\n");
            builder.Append("\tbool outBooleanResult = bool.TryParse(ignoreCase, out isToIgnoreCase);\n");
            builder.Append("\tif (string.Compare(strA, strB, isToIgnoreCase) == 0)\n");
            builder.Append("\t\treturn true;\n");
            builder.Append("\treturn false;\n");
            builder.Append("}\n");
            return builder.ToString();
        }

        /// <summary>
        /// Compares two specified String objects, ignoring or honoring their case, and returns an boolean that indicates if they are equal or not.
        /// </summary>
        /// <param name="strA">The first string to compare.</param>
        /// <param name="strB">The second string to compare.</param>
        /// <param name="ignoreCase">True to ignore case during the comparison; otherwise, False.</param>
        /// <returns>Returns an boolean that indicates if they are equal or not.</returns>
        public bool Compare(string strA, string strB, bool ignoreCase)
        {
            if (string.Compare(strA, strB, ignoreCase) == 0)
                return true;
            return false;
        }

    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.BizTalk.BaseFunctoids;
using System.Reflection;

namespace BizTalk.String.Functoids
{
    [Serializable]
    public class AdvanceCompare : BaseFunctoid
    {
        public AdvanceCompare()
            : base()
        {
            //ID for this functoid
            this.ID = 10002;

            // resource assembly must be ProjectName.ResourceName if building with VS.Net
            SetupResourceAssembly("BizTalk.String.Functoids.StringResources", Assembly.GetExecutingAssembly());

            //Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
            SetName("IDS_ADVCOMPAREFUNCTOID_NAME");
            SetTooltip("IDS_ADVCOMPAREFUNCTOID_TOOLTIP");
            SetDescription("IDS_ADVCOMPAREFUNCTOID_DESCRIPTION");
            SetBitmap("IDS_ADVCOMPAREFUNCTOID_BITMAP");

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
            //SetExternalFunctionName(GetType().Assembly.FullName, GetType().FullName, "Compare");

            SetScriptBuffer(ScriptType.CSharp, this.GetCSharpBuffer());
            HasSideEffects = false;
        }

        private string GetCSharpBuffer()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("public bool Compare(string strA, string strB, string ignoreCase)\n");
            builder.Append("{\n");
            builder.Append("\tbool isToIgnoreCase = false;\n");
            builder.Append("\tbool outBooleanResult = bool.TryParse(ignoreCase, out isToIgnoreCase);\n");
            builder.Append("\tif (string.Compare(strA, strB, isToIgnoreCase) == 0)\n");
            builder.Append("\t\treturn true;\n");
            builder.Append("\treturn false;\n");
            builder.Append("}\n");
            return builder.ToString();
        }

        /// <summary>
        /// Compares two specified String objects, ignoring or honoring their case, and returns an boolean that indicates if they are equal or not.
        /// </summary>
        /// <param name="strA">The first string to compare.</param>
        /// <param name="strB">The second string to compare.</param>
        /// <param name="ignoreCase">True to ignore case during the comparison; otherwise, False.</param>
        /// <returns>Returns an boolean that indicates if they are equal or not.</returns>
        public bool Compare(string strA, string strB, bool ignoreCase)
        {
            if (string.Compare(strA, strB, ignoreCase) == 0)
                return true;
            return false;
        }

    }
}
>>>>>>> origin/master
