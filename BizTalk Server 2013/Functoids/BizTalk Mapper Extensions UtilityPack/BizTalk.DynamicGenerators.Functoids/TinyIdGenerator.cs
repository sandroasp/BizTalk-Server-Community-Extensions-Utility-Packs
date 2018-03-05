using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.BizTalk.BaseFunctoids;
using System.Reflection;

namespace BizTalk.DynamicGenerators.Functoids
{
    [Serializable]
    public class TinyIdGenerator : BaseFunctoid
    {
        private const string DEF_ENCODING_TABLE = "0234567abcdefghijklmnpqrstuvwxyz";
        private const int DEF_ENCODING_TABLE_LENGTH = 32;

        public TinyIdGenerator()
            : base()
        {
            //ID for this functoid
            this.ID = 10601;

            // resource assembly must be ProjectName.ResourceName if building with VS.Net
            SetupResourceAssembly("BizTalk.DynamicGenerators.Functoids.DynamicGenerator", Assembly.GetExecutingAssembly());

            //Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
            SetName("IDS_TINYIDGENERATORFUNCTOID_NAME");
            SetTooltip("IDS_TINYIDGENERATORFUNCTOID_TOOLTIP");
            SetDescription("IDS_TINYIDGENERATORFUNCTOID_DESCRIPTION");
            SetBitmap("IDS_TINYIDGENERATORFUNCTOID_BITMAP");

            //category for this functoid. This functoid goes under the String Functoid Tab in the
            this.Category = FunctoidCategory.None;

            // Set the limits for the number of input parameters. This example: 1 parameter
            this.SetMinParams(0);
            this.SetMaxParams(0);

            // The functoid output can go to any node type.
            this.OutputConnectionType = ConnectionType.All;

            // Set the function name that is to be called when invoking this functoid.
            // To test the map in Visual Studio, this functoid does not need to be in the GAC.
            // If using this functoid in a deployed BizTalk app. then it must be in the GAC
            SetExternalFunctionName(GetType().Assembly.FullName, GetType().FullName, "GenerateTinyId");
        }

        /// <summary>
        /// The real operation to be executed by the functoid
        /// </summary>
        public string GenerateTinyId()
        {
            Guid guid = Guid.NewGuid();
            string modifiedBase64 = Convert.ToBase64String(guid.ToByteArray())
                .Replace('+', '-').Replace('/', '_') // avoid invalid URL characters
                .Substring(0, 22);
            return modifiedBase64;
        }
    }
}
