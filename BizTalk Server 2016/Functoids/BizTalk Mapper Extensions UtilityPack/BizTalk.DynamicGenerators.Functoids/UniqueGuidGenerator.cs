using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.BizTalk.BaseFunctoids;
using System.Reflection;

namespace BizTalk.DynamicGenerators.Functoids
{
    [Serializable]
    public class UniqueGuidGenerator : BaseFunctoid
    {
        public UniqueGuidGenerator() : base()
		{
			//ID for this functoid
			this.ID = 10602;

			// resource assembly must be ProjectName.ResourceName if building with VS.Net
            SetupResourceAssembly("BizTalk.DynamicGenerators.Functoids.DynamicGenerator", Assembly.GetExecutingAssembly());

			//Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
            SetName("IDS_GUIDGENERATORFUNCTOID_NAME");
            SetTooltip("IDS_GUIDGENERATORFUNCTOID_TOOLTIP");
            SetDescription("IDS_GUIDGENERATORFUNCTOID_DESCRIPTION");
            SetBitmap("IDS_GUIDGENERATORFUNCTOID_BITMAP");

            //category for this functoid. This functoid goes under the String Functoid Tab in the
			//VS.Net toolbox for functoids.
			this.Category = FunctoidCategory.String;

            // Set the limits for the number of input parameters.
			this.SetMinParams(0);
			this.SetMaxParams(0);

            // The functoid output can go to any node type.
			this.OutputConnectionType = ConnectionType.All;

            // Set the function name that is to be called when invoking this functoid.
            // To test the map in Visual Studio, this functoid does not need to be in the GAC.
            // If using this functoid in a deployed BizTalk app. then it must be in the GAC
            SetExternalFunctionName(GetType().Assembly.FullName, GetType().FullName, "GenerateUniqueGuid");
		}

		//this is the function that gets called when the Map is executed which has this functoid.
        public string GenerateUniqueGuid()
		{
            return Guid.NewGuid().ToString();
		}
    }
}
