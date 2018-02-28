using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.BizTalk.BaseFunctoids;
using System.Reflection;

namespace BizTalk.Conversion.Functoids
{
    public sealed class MyMathDivideFunctoid : BaseFunctoid
    {
        // Methods
        public MyMathDivideFunctoid()
        {
            base.ID = 7861;
            base.SetupResourceAssembly("BizTalk.Conversion.Functoids.ConversionResource", typeof(MyMathDivideFunctoid).Assembly);
            base.SetName("IDS_MATHDIVIDE_NAME");
            base.SetTooltip("IDS_MATHDIVIDE_TOOLTIP");
            base.SetDescription("IDS_MATHDIVIDE_DESCRIPTION");
            base.SetBitmap("IDB_MATH_DIVIDE");
            base.SetExternalFunctionName(GetType().Assembly.FullName, GetType().FullName, "MathDivide");
            base.RequiredGlobalHelperFunctions = InlineGlobalHelperFunction.IsNumeric;
            base.SetScriptBuffer(ScriptType.CSharp, this.GetCSharpBuffer());
            base.RequiredGlobalHelperFunctions = InlineGlobalHelperFunction.IsNumeric;
            base.Category = FunctoidCategory.Math;
            base.OutputConnectionType = ConnectionType.AllExceptRecord;
            base.HasSideEffects = false;
            base.AddInputConnectionType(ConnectionType.AllExceptRecord);
            base.AddInputConnectionType(ConnectionType.AllExceptRecord);
        }

        private string GetCSharpBuffer()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("public string MathDivide(string val1, string val2)\n");
            builder.Append("{\n");
            builder.Append("\tstring retval = \"\";\n");
            builder.Append("\tdouble d1 = 0;\n");
            builder.Append("\tdouble d2 = 0;\n");
            builder.Append("\tif (IsNumeric(val1, ref d1) && IsNumeric(val2, ref d2))\n");
            builder.Append("\t{\n");
            builder.Append("\t\tif (d2 != 0)\n");
            builder.Append("\t\t{\n");
            builder.Append("\t\t\tdouble ret = d1 / d2;\n");
            builder.Append("\t\t\tretval = ret.ToString(System.Globalization.CultureInfo.InvariantCulture);\n");
            builder.Append("\t\t}\n");
            builder.Append("\t}\n");
            builder.Append("\treturn retval;\n");
            builder.Append("}\n");
            return builder.ToString();
        }
    }

    /// <summary>
    /// Floor Functoid - finds the floor of input
    /// </summary>
    public class FloorFunctoid : BaseFunctoid
    {
        public FloorFunctoid()
            : base()
        {
            this.ID = 11001;
            SetupResourceAssembly("MultipleFunctoids.Resource", Assembly.GetExecutingAssembly());

            SetName("NAME_FLOOR");
            SetDescription("DESCRIPTION_FLOOR");
            SetTooltip("DESCRIPTION_FLOOR");
            SetBitmap("IMAGE_FLOOR");

            SetExternalFunctionName(GetType().Assembly.FullName, " MultipleFunctoids.FloorFunctoid", "MathFloor");
            this.RequiredGlobalHelperFunctions = InlineGlobalHelperFunction.IsNumeric;

            AddScriptTypeSupport(ScriptType.CSharp);
            SetMinParams(1);
            SetMaxParams(1);

            this.Category = FunctoidCategory.Math;
            this.OutputConnectionType = ConnectionType.AllExceptRecord;
            AddInputConnectionType(ConnectionType.AllExceptRecord);
            this.HasSideEffects = false;
        }

        /// <summary>
        /// To create the C# function
        /// </summary>
        /// <param name="scriptType">Script type</param>
        /// <param name="numParams">Number of parameters</param>
        /// <param name="functionNumber">Functoid number</param>
        /// <returns>C# script</returns>
        protected override string GetInlineScriptBuffer(ScriptType scriptType, int numParams, int functionNumber)
        {
            if (ScriptType.CSharp == scriptType)
            {
                StringBuilder builder = new StringBuilder();

                builder.Append("public string MathFloor(string input)\n");
                builder.Append("{\n");
                builder.Append("  if(string.IsNullOrEmpty(input))\n");
                builder.Append("    return string.Empty;\n");
                builder.Append("double d = 0.0;\n");
                builder.Append("if (IsNumeric(input, ref d))\n");
                builder.Append("    return Math.Floor(d).ToString(System.Globalization.CultureInfo.InvariantCulture);\n");
                builder.Append("else\n");
                builder.Append("    return string.Empty;\n");
                builder.Append("}\n");

                return builder.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
    }

}
