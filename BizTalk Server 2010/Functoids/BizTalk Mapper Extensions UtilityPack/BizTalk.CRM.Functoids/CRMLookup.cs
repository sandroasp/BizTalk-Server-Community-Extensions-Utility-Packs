using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.BizTalk.BaseFunctoids;
using System.Reflection;
using System.Data.SqlClient;
using System.Data;
using BizTalk.Common.Services;

namespace BizTalk.MapperExtensions.CRMFunctoids
{

    [Serializable]
    public class CRMLookup : BaseFunctoid
    {

        public CRMLookup(): base()
        {
            //ID for this functoid
            this.ID = 8000;

            // resource assembly must be ProjectName.ResourceName if building with VS.Net
            SetupResourceAssembly("BizTalk.MapExtention.LookupFunctoid.FunctoidResource", Assembly.GetExecutingAssembly());

            //Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
            SetName("IDS_CRMLOOKUP_NAME");
            SetTooltip("IDS_CRMLOOKUP_TOOLTIP");
            SetDescription("IDS_CRMLOOKUP_DESCRIPTION");
            SetBitmap("IDS_CRMLOOKUP_BITMAP");

            //category for this functoid. This functoid goes under the String Functoid Tab in the
            this.Category = FunctoidCategory.None;

            // Set the limits for the number of input parameters. This example: 1 parameter
            this.SetMinParams(4);
            this.SetMaxParams(4);

            // Add one line of code as set out below for each input param. For multiple input params, each line would be identical.
            AddInputConnectionType(ConnectionType.All); //first input
            AddInputConnectionType(ConnectionType.All); //second input
            AddInputConnectionType(ConnectionType.All); //third input
            AddInputConnectionType(ConnectionType.All); //fourth input
 
            // The functoid output can go to any node type.
            this.OutputConnectionType = ConnectionType.All;

            // Set the function name that is to be called when invoking this functoid.
            // To test the map in Visual Studio, this functoid does not need to be in the GAC.
            // If using this functoid in a deployed BizTalk app. then it must be in the GAC
            SetExternalFunctionName(GetType().Assembly.FullName, GetType().FullName, "CRMLookupValue");
        }

        /// <summary>
        /// The real operation to be executed by the functoid
        /// </summary>
        public string CRMLookupValue(string guidField, string entityName, string keyField, string valueIn)
        {

            SqlConnection conn = null;
            SqlDataReader rdr = null;
            string returnValue = string.Empty;
            
            try
            {
                System.Diagnostics.Debug.Write("CRM Functoid IN");

                // create and open a connection object
                conn = new SqlConnection(SsoServices.Read("BizTalkMapperExtensions", "CrmDatabaseConnectionString"));
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
            catch (Exception exc)
            {
                System.Diagnostics.Debug.Write("CRM Functoid error - " + exc.Message.ToString());
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
