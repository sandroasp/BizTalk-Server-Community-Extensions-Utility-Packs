<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.BizTalk.BaseFunctoids;
using System.Reflection;
using System.Text.RegularExpressions;

namespace BizTalk.String.Functoids
{
    [Serializable]
    public class Normalize : BaseFunctoid
    {
        public Normalize()
            : base()
        {
            //ID for this functoid
            this.ID = 10004;

            // resource assembly must be ProjectName.ResourceName if building with VS.Net
            SetupResourceAssembly("BizTalk.String.Functoids.StringResources", Assembly.GetExecutingAssembly());

            //Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
            SetName("IDS_NORMALIZEFUNCTOID_NAME");
            SetTooltip("IDS_NORMALIZEFUNCTOID_TOOLTIP");
            SetDescription("IDS_NORMALIZEFUNCTOID_DESCRIPTION");
            SetBitmap("IDS_NORMALIZEFUNCTOID_BITMAP");

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
            //SetExternalFunctionName(GetType().Assembly.FullName, GetType().FullName, "NormalizeText");

            SetScriptBuffer(ScriptType.CSharp, this.GetCSharpBuffer());
            HasSideEffects = false;
        }

        private string GetCSharpBuffer()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("public string NormalizeText(string input)\n");
            builder.Append("{\n");
            builder.Append(string.Format("\treturn System.Text.RegularExpressions.Regex.Replace(input, @{0});\n", @"""( |\t|\r?\n)\1+"", ""$1"""));
            builder.Append("}\n");
            return builder.ToString();
        }

        /// <summary>
        /// Function normalize the text. It will remove two or more consecutive spaces and replace them with a single space, 
        /// remove two or more consecutive newlines and replace them with a single newline and "condense" multiple tabs into one.
        /// </summary>
        /// <param name="input">String to Normalize</param>
        /// <returns>Return the string normalize without consecutive spaces, lines or tabs</returns>
        public string NormalizeText(string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(input, @"( |\t|\r?\n)\1+", "$1");
        }

    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.BizTalk.BaseFunctoids;
using System.Reflection;
using System.Text.RegularExpressions;

namespace BizTalk.String.Functoids
{
    [Serializable]
    public class Normalize : BaseFunctoid
    {
        public Normalize()
            : base()
        {
            //ID for this functoid
            this.ID = 10004;

            // resource assembly must be ProjectName.ResourceName if building with VS.Net
            SetupResourceAssembly("BizTalk.String.Functoids.StringResources", Assembly.GetExecutingAssembly());

            //Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
            SetName("IDS_NORMALIZEFUNCTOID_NAME");
            SetTooltip("IDS_NORMALIZEFUNCTOID_TOOLTIP");
            SetDescription("IDS_NORMALIZEFUNCTOID_DESCRIPTION");
            SetBitmap("IDS_NORMALIZEFUNCTOID_BITMAP");

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
            //SetExternalFunctionName(GetType().Assembly.FullName, GetType().FullName, "NormalizeText");

            SetScriptBuffer(ScriptType.CSharp, this.GetCSharpBuffer());
            HasSideEffects = false;
        }

        private string GetCSharpBuffer()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("public string NormalizeText(string input)\n");
            builder.Append("{\n");
            builder.Append(string.Format("\treturn System.Text.RegularExpressions.Regex.Replace(input, @{0});\n", @"""( |\t|\r?\n)\1+"", ""$1"""));
            builder.Append("}\n");
            return builder.ToString();
        }

        /// <summary>
        /// Function normalize the text. It will remove two or more consecutive spaces and replace them with a single space, 
        /// remove two or more consecutive newlines and replace them with a single newline and "condense" multiple tabs into one.
        /// </summary>
        /// <param name="input">String to Normalize</param>
        /// <returns>Return the string normalize without consecutive spaces, lines or tabs</returns>
        public string NormalizeText(string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(input, @"( |\t|\r?\n)\1+", "$1");
        }

    }
}
>>>>>>> origin/master
