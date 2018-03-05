using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.BizTalk.BaseFunctoids;
using System.Reflection;

namespace BizTalk.Conversion.Functoids
{
    [Serializable]
    public class EpochToHumanReadableDate : BaseFunctoid
    {
        public EpochToHumanReadableDate() : base()
		{
			//ID for this functoid
			this.ID = 7801;

			// resource assembly must be ProjectName.ResourceName if building with VS.Net
            SetupResourceAssembly("BizTalk.Conversion.Functoids.ConversionResource", Assembly.GetExecutingAssembly());

			//Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
            SetName("IDS_EPOCHTODATEFUNCTOID_NAME");
            SetTooltip("IDS_EPOCHTODATEFUNCTOID_TOOLTIP");
            SetDescription("IDS_EPOCHTODATEFUNCTOID_DESCRIPTION");
            SetBitmap("IDS_EPOCHTODATEFUNCTOID_BITMAP");

            //category for this functoid. This functoid goes under the String Functoid Tab in the
			//VS.Net toolbox for functoids.
			this.Category = FunctoidCategory.Conversion;

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
            SetExternalFunctionName(GetType().Assembly.FullName, GetType().FullName, "ConvertEpochToHumanReadableDate");
		}

        /// <summary>
        /// This functoid allows you to convert a unix date (Epoch Date) into a traditional date (Human Readable Date)
        /// </summary>
        /// <param name="unixtime">Unix Date (Epoch Date)</param>
        /// <returns>Tradicional Date (Human Readable Date)</returns>
        public DateTime ConvertEpochToHumanReadableDate(long unixtime)
        {
            return new DateTime(1970, 1, 1).AddSeconds(Convert.ToDouble(unixtime));
        }
    }
}
