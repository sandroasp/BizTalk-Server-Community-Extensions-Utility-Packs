<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.BizTalk.BaseFunctoids;
using System.Reflection;

namespace BizTalk.Configuration.Functoids
{
    [Serializable]
    public class EnvironmentVariableConf : BaseFunctoid
    {
        public EnvironmentVariableConf()
            : base()
		{
			//ID for this functoid
			this.ID = 10202;

			// resource assembly must be ProjectName.ResourceName if building with VS.Net
            SetupResourceAssembly("BizTalk.Configuration.Functoids.ConfigurationResources", Assembly.GetExecutingAssembly());

			//Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
			SetName("IDS_ENVVARFUNCTOID_NAME");
            SetTooltip("IDS_ENVVARCONFFUNCTOID_TOOLTIP");
            SetDescription("IDS_ENVVARCONFFUNCTOID_DESCRIPTION");
            SetBitmap("IDS_ENVVARCONFFUNCTOID_BITMAP");

            //category for this functoid. This functoid goes under the String Functoid Tab in the
			//VS.Net toolbox for functoids.
			this.Category = FunctoidCategory.None;

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
            SetExternalFunctionName(GetType().Assembly.FullName, GetType().FullName, "GetEnvironmentVariableValue");
		}

        /// <summary>
        /// Return configuration value
        /// </summary>
        /// <param name="key">Key in configuration</param>
        /// <param name="nameConfigFile">Name of configuration file</param>
        /// <returns></returns>
        public static string GetEnvironmentVariableValue(string key)
        {
            return Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Machine);
        }
    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.BizTalk.BaseFunctoids;
using System.Reflection;

namespace BizTalk.Configuration.Functoids
{
    [Serializable]
    public class EnvironmentVariableConf : BaseFunctoid
    {
        public EnvironmentVariableConf()
            : base()
		{
			//ID for this functoid
			this.ID = 10202;

			// resource assembly must be ProjectName.ResourceName if building with VS.Net
            SetupResourceAssembly("BizTalk.Configuration.Functoids.ConfigurationResources", Assembly.GetExecutingAssembly());

			//Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
			SetName("IDS_ENVVARFUNCTOID_NAME");
            SetTooltip("IDS_ENVVARCONFFUNCTOID_TOOLTIP");
            SetDescription("IDS_ENVVARCONFFUNCTOID_DESCRIPTION");
            SetBitmap("IDS_ENVVARCONFFUNCTOID_BITMAP");

            //category for this functoid. This functoid goes under the String Functoid Tab in the
			//VS.Net toolbox for functoids.
			this.Category = FunctoidCategory.None;

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
            SetExternalFunctionName(GetType().Assembly.FullName, GetType().FullName, "GetEnvironmentVariableValue");
		}

        /// <summary>
        /// Return configuration value
        /// </summary>
        /// <param name="key">Key in configuration</param>
        /// <param name="nameConfigFile">Name of configuration file</param>
        /// <returns></returns>
        public static string GetEnvironmentVariableValue(string key)
        {
            return Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Machine);
        }
    }
}
>>>>>>> origin/master
