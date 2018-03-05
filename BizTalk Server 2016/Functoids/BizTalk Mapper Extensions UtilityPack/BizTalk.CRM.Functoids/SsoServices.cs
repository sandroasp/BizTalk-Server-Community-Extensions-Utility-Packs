<<<<<<< HEAD
﻿using System;
using System.Collections.Specialized;
using Microsoft.BizTalk.SSOClient.Interop;

namespace BizTalk.Common.Services
{

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

    public static class SsoServices
    {

        private const string identifierGuid = @"ConfigProperties";

        /// <summary>
        /// Read method helps get configuration data
        /// </summary>        
        /// <param name="propName">The property name to read</param>
        /// <returns>
        ///  The value of the property stored in the given affiliate application of this component.
        /// </returns>
        public static string Read(string applicationname, string propName)
        {
            try
            {

                SSOConfigStore ssoStore = new SSOConfigStore();
                ConfigurationPropertyBag appMgmtBag = new ConfigurationPropertyBag();
                ((ISSOConfigStore)ssoStore).GetConfigInfo(applicationname, identifierGuid, SSOFlag.SSO_FLAG_RUNTIME, appMgmtBag);
                object propertyValue;

                appMgmtBag.Read(propName, out propertyValue, 0);

                return (string)propertyValue;

            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.Write(exc.Message);
                throw;
            }

        }
    }

   
}
=======
﻿using System;
using System.Collections.Specialized;
using Microsoft.BizTalk.SSOClient.Interop;

namespace BizTalk.Common.Services
{

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

    public static class SsoServices
    {

        private const string identifierGuid = @"ConfigProperties";

        /// <summary>
        /// Read method helps get configuration data
        /// </summary>        
        /// <param name="propName">The property name to read</param>
        /// <returns>
        ///  The value of the property stored in the given affiliate application of this component.
        /// </returns>
        public static string Read(string applicationname, string propName)
        {
            try
            {

                SSOConfigStore ssoStore = new SSOConfigStore();
                ConfigurationPropertyBag appMgmtBag = new ConfigurationPropertyBag();
                ((ISSOConfigStore)ssoStore).GetConfigInfo(applicationname, identifierGuid, SSOFlag.SSO_FLAG_RUNTIME, appMgmtBag);
                object propertyValue;

                appMgmtBag.Read(propName, out propertyValue, 0);

                return (string)propertyValue;

            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.Write(exc.Message);
                throw;
            }

        }
    }

   
}
>>>>>>> origin/master
