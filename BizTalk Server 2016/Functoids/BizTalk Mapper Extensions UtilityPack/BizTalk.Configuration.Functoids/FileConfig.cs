using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.BizTalk.BaseFunctoids;
using System.Reflection;
using System.Configuration;

namespace BizTalk.Configuration.Functoids
{
    [Serializable]
    public class FileConfig : BaseFunctoid
    {
        public FileConfig()
            : base()
		{
			//ID for this functoid
			this.ID = 10201;

			// resource assembly must be ProjectName.ResourceName if building with VS.Net
            SetupResourceAssembly("BizTalk.Configuration.Functoids.ConfigurationResources", Assembly.GetExecutingAssembly());

			//Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
			SetName("IDS_FILEFUNCTOID_NAME");
            SetTooltip("IDS_FILEFUNCTOID_TOOLTIP");
            SetDescription("IDS_FILEFUNCTOID_DESCRIPTION");
            SetBitmap("IDS_FILEFUNCTOID_BITMAP");

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
            SetExternalFunctionName(GetType().Assembly.FullName, GetType().FullName, "ReadFromConfigFile");
		}

        /// <summary>
        /// Return configuration value
        /// </summary>
        /// <param name="key">Key in configuration</param>
        /// <param name="nameConfigFile">Name of configuration file</param>
        /// <returns></returns>
        public static string ReadFromConfigFile(string key, string pathConfigFile)
        {
            // Map to the application configuration file.
            // Instantiate ExeConfigurationFileMap
            ExeConfigurationFileMap configFile = new ExeConfigurationFileMap();

            // Bind filepath to configFile object
            configFile.ExeConfigFilename = pathConfigFile;

            // Instantiate Configuration object and assign configFile
            System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);

            // Return desired value from configuration file
            return config.AppSettings.Settings[key].Value.ToString();
        }
    }
}
