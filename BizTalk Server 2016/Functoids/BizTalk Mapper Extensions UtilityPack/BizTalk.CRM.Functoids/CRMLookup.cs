using System;
using Microsoft.BizTalk.BaseFunctoids;
using System.Reflection;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using BizTalk.Common.Services;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;

namespace BizTalk.MapperExtensions.CRMFunctoids
{

    [Serializable]
    public class CRMLookup : BaseFunctoid
    {

        public CRMLookup(): base()
        {
            //ID for this functoid
            ID = 10400;

            // resource assembly must be ProjectName.ResourceName if building with VS.Net
            SetupResourceAssembly("BizTalk.MapExtention.LookupFunctoid.CRMLookupResources", Assembly.GetExecutingAssembly());

            //Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
            SetName("IDS_CRMLOOKUP_NAME");
            SetTooltip("IDS_CRMLOOKUP_TOOLTIP");
            SetDescription("IDS_CRMLOOKUP_DESCRIPTION");
            SetBitmap("IDS_CRMLOOKUP_BITMAP");

            //category for this functoid. This functoid goes under the String Functoid Tab in the
            Category = FunctoidCategory.None;

            // Set the limits for the number of input parameters. This example: 1 parameter
            SetMinParams(5);
            SetMaxParams(5);

            // Add one line of code as set out below for each input param. For multiple input params, each line would be identical.
            AddInputConnectionType(ConnectionType.All); //first input
            AddInputConnectionType(ConnectionType.All); //second input
            AddInputConnectionType(ConnectionType.All); //third input
            AddInputConnectionType(ConnectionType.All); //fourth input
            AddInputConnectionType(ConnectionType.All); //five input
 
            // The functoid output can go to any node type.
            OutputConnectionType = ConnectionType.All;

            // Set the function name that is to be called when invoking this functoid.
            // To test the map in Visual Studio, this functoid does not need to be in the GAC.
            // If using this functoid in a deployed BizTalk app. then it must be in the GAC
            SetExternalFunctionName(GetType().Assembly.FullName, GetType().FullName, "CRMLookupValue");
        }

        /// <summary>
        /// The real operation to be executed by the functoid
        /// </summary>
        public string CRMLookupValue(string ssoApplicationName, string guidField, string entityName, string keyField, string valueIn)
        {

            SqlConnection conn = null;
            SqlDataReader rdr = null;
            string returnValue = string.Empty;
            
            try
            {
                System.Diagnostics.Debug.Write("CRM Functoid IN");

                // SQL / Local Web Services Or CRM Online
                string connectionType = SsoServices.Read(ssoApplicationName, "CrmConnectionType");

                if (connectionType.ToLower() == "sql")
                {

                    // create and open a connection object
                    conn = new SqlConnection(SsoServices.Read(ssoApplicationName, "CrmDatabaseConnectionString"));
                    conn.Open();

                    // 1. create a command object identifying
                    // the stored procedure
                    SqlCommand cmd = new SqlCommand("BizTalkCRMLookup", conn);

                    // 2. set the command object so it knows
                    // to execute a stored procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    // 4. add parameter to command, which
                    // will be passed to the stored procedure
                    cmd.Parameters.Add(new SqlParameter("@guidField", guidField));
                    cmd.Parameters.Add(new SqlParameter("@entityName", entityName));
                    cmd.Parameters.Add(new SqlParameter("@keyField", keyField));
                    cmd.Parameters.Add(new SqlParameter("@valueIn", valueIn));

                    // execute the command
                    rdr = cmd.ExecuteReader();

                    //read data
                    if (rdr.Read())
                    {
                        returnValue = rdr[0].ToString();
                    }

                }
                else
                {
                    string userName = SsoServices.Read(ssoApplicationName, "CrmUserName");
                    string userPassword = SsoServices.Read(ssoApplicationName, "CrmUserPassword");
                    string uri = SsoServices.Read(ssoApplicationName, "CrmUri");

                    AuthenticationCredentials credentials = new AuthenticationCredentials();

                    if (uri.ToLower().Contains("dynamics.com"))
                    {
                        credentials.ClientCredentials.UserName.UserName = userName;
                        credentials.ClientCredentials.UserName.Password = userPassword;
                    }
                    else
                    {
                        string userDomain = SsoServices.Read(ssoApplicationName, "CrmUserDomain");

                        credentials.ClientCredentials.Windows.ClientCredential = new NetworkCredential(userName, userPassword, userDomain);
                    }

                    Uri organizationUri = new Uri(uri);
                    Uri homeRealmUri = new Uri(uri);

                    OrganizationServiceProxy organizationService = new OrganizationServiceProxy(organizationUri, homeRealmUri, credentials.ClientCredentials, null);

                    QueryExpression query = new QueryExpression(entityName);
                    query.Criteria.AddCondition(keyField, ConditionOperator.Equal, valueIn);
                    query.ColumnSet.AddColumn(guidField);
                    Entity resultEntity = organizationService.RetrieveMultiple(query).Entities.First();

                    returnValue = resultEntity.Attributes[guidField].ToString();
                }

            }
            catch (Exception exc)
            {
                System.Diagnostics.Debug.Write("CRM Functoid error - " + exc.Message);
            }
            finally
            {

                if (conn != null)
                {
                    conn.Close();
                }
                
                if (rdr != null)
                {
                    rdr.Close();
                }

                System.Diagnostics.Debug.Write("CRM Functoid OUT");
            }

            return returnValue;
            
        }

     
    }
}
