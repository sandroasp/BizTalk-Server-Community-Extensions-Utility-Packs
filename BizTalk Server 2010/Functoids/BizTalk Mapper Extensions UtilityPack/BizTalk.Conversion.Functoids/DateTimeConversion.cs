using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.BizTalk.BaseFunctoids;
using System.Reflection;

namespace BizTalk.Conversion.Functoids
{
    [Serializable]
    public class DateTimeConversion : BaseFunctoid
    {
        public DateTimeConversion()
            : base()
		{
			//ID for this functoid
			this.ID = 7802;

			// resource assembly must be ProjectName.ResourceName if building with VS.Net
            SetupResourceAssembly("BizTalk.Conversion.Functoids.ConversionResource", Assembly.GetExecutingAssembly());

			//Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
            SetName("IDS_CONVERTDATETIMEFUNCTOID_NAME");
            SetTooltip("IDS_CONVERTDATETIMEFUNCTOID_TOOLTIP");
            SetDescription("IDS_CONVERTDATETIMEFUNCTOID_DESCRIPTION");
            SetBitmap("IDS_CONVERTDATETIMEFUNCTOID_BITMAP");

            //category for this functoid. This functoid goes under the String Functoid Tab in the
			//VS.Net toolbox for functoids.
			this.Category = FunctoidCategory.Conversion;

            // Set the limits for the number of input parameters.
			this.SetMinParams(3);
			this.SetMaxParams(3);

            // Add one line of code as set out below for each input param. For multiple input params, each line would be identical.
            AddInputConnectionType(ConnectionType.All); //first input
            AddInputConnectionType(ConnectionType.All); //second input
            AddInputConnectionType(ConnectionType.All); //third input

            // The functoid output can go to any node type.
			this.OutputConnectionType = ConnectionType.All;

            // Set the function name that is to be called when invoking this functoid.
            // To test the map in Visual Studio, this functoid does not need to be in the GAC.
            // If using this functoid in a deployed BizTalk app. then it must be in the GAC
            SetExternalFunctionName(GetType().Assembly.FullName, GetType().FullName, "FormatDate");
		}

        /// <summary>
        /// Converting datetime to date in Biztalk Maps
        /// </summary>
        /// <param name="inputDate">input date in string format</param>
        /// <param name="inputFormat">input Format in string format (yyyy-MM-dd HH:mm:ss)</param>
        /// <param name="outputFormat">output Format in string format (yyyy-MM-dd HH:mm:ss)</param>
        /// <returns></returns>
        public string FormatDate(string inputDate, string inputFormat, string outputFormat)
        {
            System.DateTime date;
            if (System.DateTime.TryParseExact(inputDate, inputFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AssumeLocal, out date))
            {
                return date.ToString(outputFormat);
            }
            return null;
        }

    }
}
