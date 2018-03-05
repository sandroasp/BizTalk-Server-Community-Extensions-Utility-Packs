<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.BizTalk.BaseFunctoids;
using System.Reflection;
using Microsoft.EnterpriseSingleSignOn.Interop;
using System.Collections;
using System.Collections.Specialized;

namespace BizTalk.Configuration.Functoids
{
    [Serializable]
    public class SSOConfig : BaseFunctoid
    {
        private static string idenifierGUID = "ConfigProperties";

        public SSOConfig()
            : base()
		{
			//ID for this functoid
			this.ID = 10203;

			// resource assembly must be ProjectName.ResourceName if building with VS.Net
            SetupResourceAssembly("BizTalk.Configuration.Functoids.ConfigurationResources", Assembly.GetExecutingAssembly());

			//Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
			SetName("IDS_SSOFUNCTOID_NAME");
            SetTooltip("IDS_SSOFUNCTOID_TOOLTIP");
            SetDescription("IDS_SSOFUNCTOID_DESCRIPTION");
            SetBitmap("IDS_SSOFUNCTOID_BITMAP");

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
            SetExternalFunctionName(GetType().Assembly.FullName, GetType().FullName, "SSORead");
		}

        /// <summary>
        /// Read method helps get configuration data
        /// </summary>        
        /// <param name="appName">The name of the affiliate application to represent the configuration container to access</param>
        /// <param name="propName">The property name to read</param>
        /// <returns>
        ///  The value of the property stored in the given affiliate application of this component.
        /// </returns>
        public static string SSORead(string appName, string propName)
        {
            try
            {
                //Instantiate SSOConfigStore Object
                SSOConfigStore ssoStore = new SSOConfigStore();

                //Instantiate ConfigurationPropertyBag
                ConfigurationPropertyBag appMgmtBag = new ConfigurationPropertyBag();

                //Get value based on appName
                ((ISSOConfigStore)ssoStore).GetConfigInfo(appName, idenifierGUID, SSOFlag.SSO_FLAG_RUNTIME, (IPropertyBag)appMgmtBag);

                //Create propertyValue object
                object propertyValue = null;

                //Read property
                appMgmtBag.Read(propName, out propertyValue, 0);

                //return property value
                return (string)propertyValue;
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine(e.Message);
                throw;
            }
        }
    }

    [Serializable]
    public class ConfigurationPropertyBag : IPropertyBag
    {
        private HybridDictionary properties;
        internal ConfigurationPropertyBag()
        {
            properties = new HybridDictionary();
        }
        public void Read(string propName, out object ptrVar, int errLog)
        {
            ptrVar = properties[propName];
        }
        public void Write(string propName, ref object ptrVar)
        {
            properties.Add(propName, ptrVar);
        }
        public bool Contains(string key)
        {
            return properties.Contains(key);
        }
        public void Remove(string key)
        {
            properties.Remove(key);
        }
    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.BizTalk.BaseFunctoids;
using System.Reflection;
using Microsoft.EnterpriseSingleSignOn.Interop;
using System.Collections;
using System.Collections.Specialized;

namespace BizTalk.Configuration.Functoids
{
    [Serializable]
    public class SSOConfig : BaseFunctoid
    {
        private static string idenifierGUID = "ConfigProperties";

        public SSOConfig()
            : base()
		{
			//ID for this functoid
			this.ID = 10203;

			// resource assembly must be ProjectName.ResourceName if building with VS.Net
            SetupResourceAssembly("BizTalk.Configuration.Functoids.ConfigurationResources", Assembly.GetExecutingAssembly());

			//Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
			SetName("IDS_SSOFUNCTOID_NAME");
            SetTooltip("IDS_SSOFUNCTOID_TOOLTIP");
            SetDescription("IDS_SSOFUNCTOID_DESCRIPTION");
            SetBitmap("IDS_SSOFUNCTOID_BITMAP");

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
            SetExternalFunctionName(GetType().Assembly.FullName, GetType().FullName, "SSORead");
		}

        /// <summary>
        /// Read method helps get configuration data
        /// </summary>        
        /// <param name="appName">The name of the affiliate application to represent the configuration container to access</param>
        /// <param name="propName">The property name to read</param>
        /// <returns>
        ///  The value of the property stored in the given affiliate application of this component.
        /// </returns>
        public static string SSORead(string appName, string propName)
        {
            try
            {
                //Instantiate SSOConfigStore Object
                SSOConfigStore ssoStore = new SSOConfigStore();

                //Instantiate ConfigurationPropertyBag
                ConfigurationPropertyBag appMgmtBag = new ConfigurationPropertyBag();

                //Get value based on appName
                ((ISSOConfigStore)ssoStore).GetConfigInfo(appName, idenifierGUID, SSOFlag.SSO_FLAG_RUNTIME, (IPropertyBag)appMgmtBag);

                //Create propertyValue object
                object propertyValue = null;

                //Read property
                appMgmtBag.Read(propName, out propertyValue, 0);

                //return property value
                return (string)propertyValue;
            }
            catch (Exception e)
            {
                System.Diagnostics.Trace.WriteLine(e.Message);
                throw;
            }
        }
    }

    [Serializable]
    public class ConfigurationPropertyBag : IPropertyBag
    {
        private HybridDictionary properties;
        internal ConfigurationPropertyBag()
        {
            properties = new HybridDictionary();
        }
        public void Read(string propName, out object ptrVar, int errLog)
        {
            ptrVar = properties[propName];
        }
        public void Write(string propName, ref object ptrVar)
        {
            properties.Add(propName, ptrVar);
        }
        public bool Contains(string key)
        {
            return properties.Contains(key);
        }
        public void Remove(string key)
        {
            properties.Remove(key);
        }
    }
}
>>>>>>> origin/master
