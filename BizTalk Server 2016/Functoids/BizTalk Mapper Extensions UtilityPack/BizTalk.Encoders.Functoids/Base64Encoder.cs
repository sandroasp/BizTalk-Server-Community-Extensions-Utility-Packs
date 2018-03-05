<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.BizTalk.BaseFunctoids;
using System.Reflection;

namespace BizTalk.Encoders.Functoids
{
    [Serializable]
    public class Base64Encoder : BaseFunctoid
    {
        public Base64Encoder() : base()
		{
			//ID for this functoid
			this.ID = 10501;

			// resource assembly must be ProjectName.ResourceName if building with VS.Net
            SetupResourceAssembly("BizTalk.Encoders.Functoids.EncodersResources", Assembly.GetExecutingAssembly());

			//Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
            SetName("IDS_BASE64ENCODERFUNCTOID_NAME");
            SetTooltip("IDS_BASE64ENCODERFUNCTOID_TOOLTIP");
            SetDescription("IDS_BASE64ENCODERFUNCTOID_DESCRIPTION");
            SetBitmap("IDS_BASE64ENCODERFUNCTOID_BITMAP");

            //category for this functoid. This functoid goes under the String Functoid Tab in the
			//VS.Net toolbox for functoids.
			this.Category = FunctoidCategory.String;

            // Set the limits for the number of input parameters.
			this.SetMinParams(1);
			this.SetMaxParams(1);

            // Add one line of code as set out below for each input param. For multiple input params, each line would be identical.
			AddInputConnectionType(ConnectionType.All); //first input

            // The functoid output can go to any node type.
			this.OutputConnectionType = ConnectionType.All;

            // Set the function name that is to be called when invoking this functoid.
            // To test the map in Visual Studio, this functoid does not need to be in the GAC.
            // If using this functoid in a deployed BizTalk app. then it must be in the GAC
            SetExternalFunctionName(GetType().Assembly.FullName, GetType().FullName, "Base64Encode");
		}

        public string Base64Encode(string data)
        {
            byte[] encData_byte = new byte[data.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }
    }
}
=======
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.BizTalk.BaseFunctoids;
using System.Reflection;

namespace BizTalk.Encoders.Functoids
{
    [Serializable]
    public class Base64Encoder : BaseFunctoid
    {
        public Base64Encoder() : base()
		{
			//ID for this functoid
			this.ID = 10501;

			// resource assembly must be ProjectName.ResourceName if building with VS.Net
            SetupResourceAssembly("BizTalk.Encoders.Functoids.EncodersResources", Assembly.GetExecutingAssembly());

			//Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
            SetName("IDS_BASE64ENCODERFUNCTOID_NAME");
            SetTooltip("IDS_BASE64ENCODERFUNCTOID_TOOLTIP");
            SetDescription("IDS_BASE64ENCODERFUNCTOID_DESCRIPTION");
            SetBitmap("IDS_BASE64ENCODERFUNCTOID_BITMAP");

            //category for this functoid. This functoid goes under the String Functoid Tab in the
			//VS.Net toolbox for functoids.
			this.Category = FunctoidCategory.String;

            // Set the limits for the number of input parameters.
			this.SetMinParams(1);
			this.SetMaxParams(1);

            // Add one line of code as set out below for each input param. For multiple input params, each line would be identical.
			AddInputConnectionType(ConnectionType.All); //first input

            // The functoid output can go to any node type.
			this.OutputConnectionType = ConnectionType.All;

            // Set the function name that is to be called when invoking this functoid.
            // To test the map in Visual Studio, this functoid does not need to be in the GAC.
            // If using this functoid in a deployed BizTalk app. then it must be in the GAC
            SetExternalFunctionName(GetType().Assembly.FullName, GetType().FullName, "Base64Encode");
		}

        public string Base64Encode(string data)
        {
            byte[] encData_byte = new byte[data.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }
    }
}
>>>>>>> origin/master
