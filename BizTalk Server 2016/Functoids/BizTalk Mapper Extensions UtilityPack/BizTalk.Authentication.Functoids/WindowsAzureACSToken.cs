<<<<<<< HEAD
﻿using Microsoft.BizTalk.BaseFunctoids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace BizTalk.Authentication.Functoids
{
    [Serializable]
    public class WindowsAzureACSToken : BaseFunctoid
    {

        public WindowsAzureACSToken()
            : base()
        {
            //ID for this functoid
            this.ID = 10701;

            // resource assembly must be ProjectName.ResourceName if building with VS.Net
            SetupResourceAssembly("BizTalk.Authentication.Functoids.AuthenticationResources", Assembly.GetExecutingAssembly());

            //Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
            SetName("IDS_AZUREACSTOKENFUNCTOID_NAME");
            SetTooltip("IDS_AZUREACSTOKENFUNCTOID_TOOLTIP");
            SetDescription("IDS_AZUREACSTOKENFUNCTOID_DESCRIPTION");
            SetBitmap("IDS_AZUREACSTOKENFUNCTOID_BITMAP");

            //category for this functoid. This functoid goes under the String Functoid Tab in the
            this.Category = FunctoidCategory.None;

            // Set the limits for the number of input parameters. This example: 1 parameter
            this.SetMinParams(4);
            this.SetMaxParams(4);

            // Add one line of code as set out below for each input param. For multiple input params, each line would be identical.
            AddInputConnectionType(ConnectionType.AllExceptRecord); //first input
            AddInputConnectionType(ConnectionType.AllExceptRecord); //Second input
            AddInputConnectionType(ConnectionType.AllExceptRecord); //Third input
            AddInputConnectionType(ConnectionType.AllExceptRecord); //Fourth input
            // The functoid output can go to any node type.
            this.OutputConnectionType = ConnectionType.AllExceptRecord;

            // Set the function name that is to be called when invoking this functoid.
            // To test the map in Visual Studio, this functoid does not need to be in the GAC.
            // If using this functoid in a deployed BizTalk app. then it must be in the GAC
            SetExternalFunctionName(GetType().Assembly.FullName, GetType().FullName, "GetWindowsAzureACSToken");

        }


        /// <summary>
        /// This functoid allows you to obtain a Windows Azure ACS WRAP access token. 
        /// Note this token needs to be added to the outbound request message Http Header. 
        /// In order to do so use the Authorization key and assign it's value as 'WRAP access_token={RECEIVED TOKEN}'
        /// </summary>
        /// <param name="issuerName">Issuername belonging to the BizTalk Service namespace</param>
        /// <param name="issuerKey">Issuerkey belonging to the BizTalk Service namespace</param>
        /// <param name="endPoint">Full uri pointing to a BizTalk Service Bridge or windows azure servicbus queue/topic</param>
        /// <param name="bizTalkServiceACSNamespace">BizTalk Service Namespace</param>
        /// <returns></returns>
        public string GetWindowsAzureACSToken(string issuerName, string issuerKey, string endPoint, string windowsAzureACSNamespace)
        {
            string acsToken = string.Empty;
            
            //Get acsEndPoint
            Uri acsEndPoint = ConstructAcsEndpointUri(windowsAzureACSNamespace);

            using (var client = new WebClient())
            {
                // Create the body of the Post Request, formatted as a HTTP Form
                string postData = "wrap_name=" + Uri.EscapeDataString(issuerName) +
                                    "&wrap_password=" + Uri.EscapeDataString(issuerKey) +
                                    "&wrap_scope=" + Uri.EscapeDataString(endPoint.Replace("https", "http"));
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                string[] tokenVariables = client.UploadString(acsEndPoint, "POST", postData).Split("&".ToCharArray());
                string[] tokenVariable = tokenVariables[0].Split("=".ToCharArray());
                string[] authorizationTokenVariables = Uri.UnescapeDataString(tokenVariable[1]).Split("&".ToCharArray());
                acsToken = Uri.UnescapeDataString(tokenVariable[1]);
            }

            //Return token
            return acsToken;
        }

 
        /// <summary>
        /// Using basic logic it tries to extract the Acs Endpoint. 
        /// </summary>
        /// <param name="endpoint">Windows Azure Servicebus endpoint address or Windows Azure BizTalk Service endpoint address</param>
        /// <returns>ACS endpoint uri</returns>
        public Uri ConstructAcsEndpointUri(string acsNamespace)
        {
            //return acs uri
            return new Uri(String.Format("https://{0}.accesscontrol.windows.net/WRAPv0.9/", acsNamespace));
        }
    }
}
=======
﻿using Microsoft.BizTalk.BaseFunctoids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace BizTalk.Authentication.Functoids
{
    [Serializable]
    public class WindowsAzureACSToken : BaseFunctoid
    {

        public WindowsAzureACSToken()
            : base()
        {
            //ID for this functoid
            this.ID = 10701;

            // resource assembly must be ProjectName.ResourceName if building with VS.Net
            SetupResourceAssembly("BizTalk.Authentication.Functoids.AuthenticationResources", Assembly.GetExecutingAssembly());

            //Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
            SetName("IDS_AZUREACSTOKENFUNCTOID_NAME");
            SetTooltip("IDS_AZUREACSTOKENFUNCTOID_TOOLTIP");
            SetDescription("IDS_AZUREACSTOKENFUNCTOID_DESCRIPTION");
            SetBitmap("IDS_AZUREACSTOKENFUNCTOID_BITMAP");

            //category for this functoid. This functoid goes under the String Functoid Tab in the
            this.Category = FunctoidCategory.None;

            // Set the limits for the number of input parameters. This example: 1 parameter
            this.SetMinParams(4);
            this.SetMaxParams(4);

            // Add one line of code as set out below for each input param. For multiple input params, each line would be identical.
            AddInputConnectionType(ConnectionType.AllExceptRecord); //first input
            AddInputConnectionType(ConnectionType.AllExceptRecord); //Second input
            AddInputConnectionType(ConnectionType.AllExceptRecord); //Third input
            AddInputConnectionType(ConnectionType.AllExceptRecord); //Fourth input
            // The functoid output can go to any node type.
            this.OutputConnectionType = ConnectionType.AllExceptRecord;

            // Set the function name that is to be called when invoking this functoid.
            // To test the map in Visual Studio, this functoid does not need to be in the GAC.
            // If using this functoid in a deployed BizTalk app. then it must be in the GAC
            SetExternalFunctionName(GetType().Assembly.FullName, GetType().FullName, "GetWindowsAzureACSToken");

        }


        /// <summary>
        /// This functoid allows you to obtain a Windows Azure ACS WRAP access token. 
        /// Note this token needs to be added to the outbound request message Http Header. 
        /// In order to do so use the Authorization key and assign it's value as 'WRAP access_token={RECEIVED TOKEN}'
        /// </summary>
        /// <param name="issuerName">Issuername belonging to the BizTalk Service namespace</param>
        /// <param name="issuerKey">Issuerkey belonging to the BizTalk Service namespace</param>
        /// <param name="endPoint">Full uri pointing to a BizTalk Service Bridge or windows azure servicbus queue/topic</param>
        /// <param name="bizTalkServiceACSNamespace">BizTalk Service Namespace</param>
        /// <returns></returns>
        public string GetWindowsAzureACSToken(string issuerName, string issuerKey, string endPoint, string windowsAzureACSNamespace)
        {
            string acsToken = string.Empty;
            
            //Get acsEndPoint
            Uri acsEndPoint = ConstructAcsEndpointUri(windowsAzureACSNamespace);

            using (var client = new WebClient())
            {
                // Create the body of the Post Request, formatted as a HTTP Form
                string postData = "wrap_name=" + Uri.EscapeDataString(issuerName) +
                                    "&wrap_password=" + Uri.EscapeDataString(issuerKey) +
                                    "&wrap_scope=" + Uri.EscapeDataString(endPoint.Replace("https", "http"));
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                string[] tokenVariables = client.UploadString(acsEndPoint, "POST", postData).Split("&".ToCharArray());
                string[] tokenVariable = tokenVariables[0].Split("=".ToCharArray());
                string[] authorizationTokenVariables = Uri.UnescapeDataString(tokenVariable[1]).Split("&".ToCharArray());
                acsToken = Uri.UnescapeDataString(tokenVariable[1]);
            }

            //Return token
            return acsToken;
        }

 
        /// <summary>
        /// Using basic logic it tries to extract the Acs Endpoint. 
        /// </summary>
        /// <param name="endpoint">Windows Azure Servicebus endpoint address or Windows Azure BizTalk Service endpoint address</param>
        /// <returns>ACS endpoint uri</returns>
        public Uri ConstructAcsEndpointUri(string acsNamespace)
        {
            //return acs uri
            return new Uri(String.Format("https://{0}.accesscontrol.windows.net/WRAPv0.9/", acsNamespace));
        }
    }
}
>>>>>>> origin/master
