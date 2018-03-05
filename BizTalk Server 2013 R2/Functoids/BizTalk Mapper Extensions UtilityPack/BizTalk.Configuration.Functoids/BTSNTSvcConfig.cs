using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.BizTalk.BaseFunctoids;
using System.Reflection;
using System.Collections.Specialized;

namespace BizTalk.Configuration.Functoids
{
    [Serializable]
    public class BTSNTSvcConfig : BaseFunctoid
    {
        public BTSNTSvcConfig()
            : base()
		{
			//ID for this functoid
			this.ID = 10205;

			// resource assembly must be ProjectName.ResourceName if building with VS.Net
            SetupResourceAssembly("BizTalk.Configuration.Functoids.ConfigurationResources", Assembly.GetExecutingAssembly());

			//Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
            SetName("IDS_BTSNTSvcFUNCTOID_NAME");
            SetTooltip("IDS_BTSNTSvcFUNCTOID_TOOLTIP");
            SetDescription("IDS_BTSNTSvcFUNCTOID_DESCRIPTION");
            SetBitmap("IDS_BTSNTSvcFUNCTOID_BITMAP");

            //category for this functoid. This functoid goes under the String Functoid Tab in the
			//VS.Net toolbox for functoids.
            this.Category = FunctoidCategory.None;

            // Set the limits for the number of input parameters.
			this.SetMinParams(2);
			this.SetMaxParams(2);

            // Add one line of code as set out below for each input param. For multiple input params, each line would be identical.
			AddInputConnectionType(ConnectionType.All); //first input

            // The functoid output can go to any node type.
			this.OutputConnectionType = ConnectionType.All;

            // Set the function name that is to be called when invoking this functoid.
            // To test the map in Visual Studio, this functoid does not need to be in the GAC.
            // If using this functoid in a deployed BizTalk app. then it must be in the GAC
            SetExternalFunctionName(GetType().Assembly.FullName, GetType().FullName, "ReadFromBTSNTSvc");
		}

        /// <summary>
        /// Read method helps get configuration data
        /// </summary>        
        /// <param name="appName">The name of the affiliate application to represent the configuration container to access</param>
        /// <param name="propName">The property name to read</param>
        /// <returns>
        ///  The value of the property stored in the given affiliate application of this component.
        /// </returns>
        public static string ReadFromBTSNTSvc(string keyname, string section = null)
        {
            try
            {
                if(string.IsNullOrEmpty(section))
                    return System.Configuration.ConfigurationManager.AppSettings.Get(keyname);

                string keyvalue = "";
                NameValueCollection InitParams;
                InitParams = (NameValueCollection)System.Configuration.ConfigurationManager.GetSection(section);
                keyvalue = InitParams[keyname];
                return keyvalue;
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine(e.Message);
                throw;
            }
        }
    }
}
