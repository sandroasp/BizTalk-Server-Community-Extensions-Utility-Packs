using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.BizTalk.BaseFunctoids;
using System.Reflection;

namespace BizTalk.Conversion.Functoids
{
    [Serializable]
    public class HumanReadableDateToEpoch : BaseFunctoid
    {
        public HumanReadableDateToEpoch() : base()
		{
			//ID for this functoid
			this.ID = 7800;

			// resource assembly must be ProjectName.ResourceName if building with VS.Net
            SetupResourceAssembly("BizTalk.Conversion.Functoids.ConversionResource", Assembly.GetExecutingAssembly());

			//Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
            SetName("IDS_DATETOEPOCHFUNCTOID_NAME");
            SetTooltip("IDS_DATETOEPOCHFUNCTOID_TOOLTIP");
            SetDescription("IDS_DATETOEPOCHFUNCTOID_DESCRIPTION");
            SetBitmap("IDS_DATETOEPOCHFUNCTOID_BITMAP");

            //category for this functoid. This functoid goes under the String Functoid Tab in the
			//VS.Net toolbox for functoids.
			this.Category = FunctoidCategory.Conversion;

            // Set the limits for the number of input parameters.
			this.SetMinParams(2);
			this.SetMaxParams(2);

            // Add one line of code as set out below for each input param. For multiple input params, each line would be identical.
			AddInputConnectionType(ConnectionType.All); //first input
            AddInputConnectionType(ConnectionType.All); //second input

            // The functoid output can go to any node type.
			this.OutputConnectionType = ConnectionType.All;

            // Set the function name that is to be called when invoking this functoid.
            // To test the map in Visual Studio, this functoid does not need to be in the GAC.
            // If using this functoid in a deployed BizTalk app. then it must be in the GAC
            SetExternalFunctionName(GetType().Assembly.FullName, GetType().FullName, "ConvertHumanReadableDateToEpoch");
		}

        /// <summary>
        /// This functoid allows you to convert a traditional date (Human Readable Date) into a unix date (Epoch Date)
        /// </summary>
        /// <param name="unixtime">Data Tradicional (Human Readable Date)</param>
        /// <returns>Data Unix (Epoch Date)</returns>
        public long ConvertHumanReadableDateToEpoch(string datetime, string format)
        {
            DateTime myDate = DateTime.ParseExact(datetime, format, null);

            return (myDate.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }
    }
}
