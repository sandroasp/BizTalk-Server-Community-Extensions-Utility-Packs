using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.BizTalk.BaseFunctoids;
using System.Reflection;
using Microsoft.Win32;

namespace BizTalk.Configuration.Functoids
{
    [Serializable]
    public class RegistryConfig : BaseFunctoid
    {
        public RegistryConfig()
            : base()
		{
			//ID for this functoid
			this.ID = 7900;

			// resource assembly must be ProjectName.ResourceName if building with VS.Net
            SetupResourceAssembly("BizTalk.Configuration.Functoids.ConfigurationResources", Assembly.GetExecutingAssembly());

			//Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
			SetName("IDS_REGISTRYFUNCTOID_NAME");
            SetTooltip("IDS_REGISTRYFUNCTOID_TOOLTIP");
            SetDescription("IDS_REGISTRYFUNCTOID_DESCRIPTION");
            SetBitmap("IDS_REGISTRYFUNCTOID_BITMAP");

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
            SetExternalFunctionName(GetType().Assembly.FullName, GetType().FullName, "ReadFromRegistry");
		}

        /// <summary>
        /// Return value from registry
        /// </summary>
        /// <param name="subKey">subKey in the Hive</param>
        /// <param name="value">Value from subKey</param>
        /// <returns>Request value from the subKey</returns>
        public static string ReadFromRegistry(string subKey, string value)
        {
            // Create value variable
            string retValue = null;

            // Opening the registry key
            RegistryKey rk = Registry.LocalMachine;

            // Open a subKey as read-only
            RegistryKey sk1 = rk.OpenSubKey(subKey, RegistryKeyPermissionCheck.ReadSubTree, System.Security.AccessControl.RegistryRights.FullControl);

            // If the RegistrySubKey does exist
            if (sk1 != null)
            {
                try
                {
                    // If the RegistryKey exists get its value or null is returned.
                    retValue = (string)sk1.GetValue(value.ToUpper());
                }
                catch (Exception e)
                {
                    System.Diagnostics.Trace.WriteLine(e.Message);
                    throw;
                }
            }

            // Return value
            return retValue;
        }
    }
}
