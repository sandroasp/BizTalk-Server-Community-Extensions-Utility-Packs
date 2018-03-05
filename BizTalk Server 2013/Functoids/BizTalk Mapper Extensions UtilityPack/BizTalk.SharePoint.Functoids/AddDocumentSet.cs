using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.BizTalk.BaseFunctoids;
using System.Reflection;
using Microsoft.SharePoint.Client;

namespace BizTalk.SharePoint.Functoids
{
    [Serializable]
    public class AddDocumentSet : BaseFunctoid
    {
        public AddDocumentSet()
            : base()
        {
            //ID for this functoid
            this.ID = 10801;

            // resource assembly must be ProjectName.ResourceName if building with VS.Net
            SetupResourceAssembly("BizTalk.SharePoint.Functoids.SharePointResources", Assembly.GetExecutingAssembly());

            //Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
            SetName("IDS_ADDDOCUMENTSET_NAME");
            SetTooltip("IDS_ADDDOCUMENTSET_TOOLTIP");
            SetDescription("IDS_ADDDOCUMENTSET_DESCRIPTION");
            SetBitmap("IDS_ADDDOCUMENTSET_BITMAP");

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
            SetExternalFunctionName(GetType().Assembly.FullName, GetType().FullName, "AddSharePointDocumentSet");

        }

        #region class wide variables
        private ClientContext clientContext { get; set; }
        private List list { get; set; }
        private Web web { get; set; }
        #endregion
        #region public methods

        /// <summary>
        /// This functions adds a document set to an existing sharepoint list
        /// </summary>
        /// <param name="siteUrl">SharePoint Site Url</param>
        /// <param name="listName">SharePoint List Name containing the custom content-type Document Set</param>
        /// <param name="docSetContentTypeName">SharePoint List Content Type</param>
        /// <param name="newDocSetName">Name of the to document set to create</param>

        /// <returns>true if document set has been created</returns>
        public bool AddSharePointDocumentSet(string siteUrl, string listName, string docSetContentTypeName, string newDocSetName)
        {
            try
            {

                using (this.clientContext = new ClientContext(siteUrl))
                {
                    this.web = clientContext.Web;
                    this.list = clientContext.Web.Lists.GetByTitle(listName);
                    this.clientContext.Load(this.clientContext.Site);

                    ContentTypeCollection listContentTypes = list.ContentTypes;
                    this.clientContext.Load(listContentTypes, types => types.Include
                                                      (type => type.Id, type => type.Name,
                                                      type => type.Parent));

                    var result = this.clientContext.LoadQuery(listContentTypes).Where
                        (c => c.Name == docSetContentTypeName);

                    this.clientContext.ExecuteQuery();

                    ContentType targetDocumentSetContentType = result.FirstOrDefault();

                    ListItemCreationInformation newItemInfo = new ListItemCreationInformation();
                    newItemInfo.UnderlyingObjectType = FileSystemObjectType.Folder;
                    newItemInfo.LeafName = newDocSetName;
                    ListItem newListItem = list.AddItem(newItemInfo);

                    newListItem["ContentTypeId"] = targetDocumentSetContentType.Id.ToString();
                    newListItem["Title"] = newDocSetName;
                    newListItem.Update();

                    clientContext.Load(list);
                    clientContext.ExecuteQuery();
                }
            }
            catch
            {
                return false;
            }
            return true;
            

        }




        #endregion



       
    }
}
