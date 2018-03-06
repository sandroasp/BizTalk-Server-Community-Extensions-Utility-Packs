using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Drawing;
using VSLangProj;
using System.Resources;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace BizTalk.MapperExtensions.Functoid.Wizard
{
    /// <summary>
    /// Summary description for RenderHelper.
    /// </summary>
    internal class ResourceTemplate
    {
        protected NameValueCollection _functoidParameters = null;
        protected string _thisProjectNamespace;
        protected CodeLanguages _implementationLanguage;
        protected string _assemblyKeyFile;
        protected string _assemblyKeyName;
        protected string _assemblyName;
        protected string _biztalkDir;
        protected string _className;
        protected string _nameSpace;
        protected string _id;
        protected Image _icon;
        protected string _name;
        protected string _toolTip;
        protected string _category;
        protected string _description;
        protected string _returnType;
        protected string _outputConnType;
        protected string _inputConnType;
        protected string _exceptionText;
        protected string _minParams;
        protected string _maxParams;

        public ResourceTemplate()
        {
            _thisProjectNamespace = "BizTalk.MapperExtensions.Functoid.Wizard";
        }

        public void RenderAndAddToProject(string resourceName,
            string newFileName,
            string relativePath,
            string destDir,
            VSProject FunctoidVSProject,
            bool Embedded)
        {
            string NewFileName = Render(resourceName, newFileName, relativePath, destDir);
            EnvDTE.ProjectItem FunctoidProjectItem =
                FunctoidVSProject.Project.ProjectItems.AddFromFile(NewFileName);
            if (Embedded)
            {
                FunctoidProjectItem.Properties.Item("BuildAction").Value =
                    prjBuildAction.prjBuildActionEmbeddedResource;
            }
        }

        public void RenderResourceFileAndAddToProject(
            string newFileName,
            string destDir,
            VSProject FunctoidVSProject,
            bool Embedded)
        {
            string NewFileName = RenderResourceFile(newFileName, destDir);
            EnvDTE.ProjectItem FunctoidProjectItem =
                FunctoidVSProject.Project.ProjectItems.AddFromFile(NewFileName);
            if (Embedded)
            {
                FunctoidProjectItem.Properties.Item("BuildAction").Value =
                    prjBuildAction.prjBuildActionEmbeddedResource;
            }
        }

        public string RenderResourceFile(
            string newFileName,
            string destDir)
        {
            // add our resource bundle
            string resourceBundle = Path.Combine(destDir, newFileName);
            ResXResourceWriter resx = new ResXResourceWriter(resourceBundle);
            resx.AddResource("FunctoidId", _id);
            resx.AddResource("FunctoidName", _name);
            resx.AddResource("FunctoidDescription", _description);
            resx.AddResource("FunctoidToolTip", _toolTip);
            resx.AddResource("FunctoidException", _exceptionText);
            resx.AddResource("FunctoidBitmap", _icon);
            resx.Close();

            return resourceBundle;
        }

        /// <summary>
        /// Renders the template files into a compilable sourcefile.
        /// Replaces code-markers with values taken from the wizard pages. 
        /// </summary>
        /// <param name="resourceName">Name of the code-template</param>
        /// <param name="newFileName">Name of the file to be generated</param>
        /// <param name="relativePath">Relative path of the resource inside this project</param>
        /// <param name="destDir">The dir where the file will be placed</param>
        /// <returns></returns>
        public string Render(string resourceName,
            string newFileName,
            string relativePath,
            string destDir)
        {
            string strNewFileName = Path.Combine(destDir, newFileName);
            string strFileString = GetResource(_thisProjectNamespace + "." + relativePath + "." + resourceName);

            //  Replace the template fields in the file with the appropriate values
            strFileString = strFileString.Replace("[!output ASSEMBLYKEYFILE]", _assemblyKeyFile);
            strFileString = strFileString.Replace("[!output ASSEMBLYKEYNAME]", _assemblyKeyName);
            strFileString = strFileString.Replace("[!output ASSEMBLYNAME]", _assemblyName);
            strFileString = strFileString.Replace("[!output BIZTALKPATH]", _biztalkDir);
            strFileString = strFileString.Replace("[!output CATEGORY]", _category);
            strFileString = strFileString.Replace("[!output CLASSNAME]", _className);
            strFileString = strFileString.Replace("[!output INPUTCONNTYPE]", _inputConnType);
            strFileString = strFileString.Replace("[!output MAXPARAMS]", _maxParams);
            strFileString = strFileString.Replace("[!output MINPARAMS]", _minParams);
            strFileString = strFileString.Replace("[!output NAMESPACE]", _nameSpace);
            strFileString = strFileString.Replace("[!output OUTPUTCONNTYPE]", _outputConnType);
            strFileString = strFileString.Replace("[!output RETURNTYPE]", _returnType);
            strFileString = strFileString.Replace("[!output GUID]", Guid.NewGuid().ToString());
            if (strFileString.IndexOf("[!output PARAMETERS]") > 0)
            {
                int KeyCount = _functoidParameters.Keys.Count;
                string Parameters = "";
                if (KeyCount > 0)
                {
                    switch (_implementationLanguage)
                    {
                        case CodeLanguages.CSharp:
                            Parameters = _functoidParameters[0] + " " + _functoidParameters.Keys[0];
                            for (int i = 1; i < KeyCount; i++)
                            {
                                Parameters += ", " + _functoidParameters[i] + " " + _functoidParameters.Keys[i];
                            }
                            break;

                        case CodeLanguages.VisualBasicNET:
                            Parameters = "ByVal " + _functoidParameters.Keys[0] + " As " + _functoidParameters[0];
                            for (int i = 1; i < KeyCount; i++)
                            {
                                Parameters += ", ByVal " + _functoidParameters.Keys[i] + " As " + _functoidParameters[i];
                            }
                            break;
                    }
                }
                strFileString = strFileString.Replace("[!output PARAMETERS]", Parameters);
            }

            //  Write the string back out to the new class file in our solution 
            FileStream NewSource = new FileStream(strNewFileName,
                FileMode.CreateNew, FileAccess.Write);

            StreamWriter sw = new StreamWriter(NewSource, Encoding.Unicode);
            sw.Write(strFileString);
            sw.Close();
            NewSource.Close();
            return (strNewFileName);
        }

        /// <summary>
        /// Helper to get resource from manifest.
        /// </summary>
        /// <param name="resource">Full resource name</param>
        /// <returns>Resource value</returns>
        protected string GetResource(string resource)
        {
            //System.Diagnostics.Debugger.Launch();
            string value = null;
            if (null != resource)
            {
                Assembly assem = this.GetType().Assembly;
                //string[] teste=assem.GetManifestResourceNames();
                Stream stream = assem.GetManifestResourceStream(resource);
                StreamReader reader = null;

                using (reader = new StreamReader(stream))
                {
                    value = reader.ReadToEnd();
                }
            }
            return value;
        }

        public NameValueCollection FunctoidParameters
        {
            get { return _functoidParameters; }
            set { _functoidParameters = value; }
        }

        public string AssemblyKeyFile
        {
            get { return this._assemblyKeyFile; }
            set { this._assemblyKeyFile = value; }
        }

        public string AssemblyName
        {
            get { return this._assemblyName; }
            set { this._assemblyName = value; }
        }

        public string AssemblyKeyName
        {
            get { return this._assemblyKeyName; }
            set { this._assemblyKeyName = value; }
        }

        public string BiztalkDir
        {
            get { return this._biztalkDir; }
            set { this._biztalkDir = value; }
        }

        public string ClassName
        {
            get { return this._className; }
            set { this._className = value; }
        }

        public string NameSpace
        {
            get { return this._nameSpace; }
            set { this._nameSpace = value; }
        }

        public string Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        public Image Icon
        {
            get { return this._icon; }
            set { this._icon = value; }
        }

        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        public string ToolTip
        {
            get { return this._toolTip; }
            set { this._toolTip = value; }
        }

        public string Category
        {
            get { return this._category; }
            set { this._category = value; }
        }

        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        public string ReturnType
        {
            get { return this._returnType; }
            set { this._returnType = value; }
        }

        public string OutputConnType
        {
            get { return this._outputConnType; }
            set { this._outputConnType = value; }
        }

        public string InputConnType
        {
            get { return this._outputConnType; }
            set { this._inputConnType = value; }
        }

        public string ExceptionText
        {
            get { return this._exceptionText; }
            set { this._exceptionText = value; }
        }

        public string MinParams
        {
            get { return this._minParams; }
            set { this._minParams = value; }
        }

        public string MaxParams
        {
            get { return this._maxParams; }
            set { this._maxParams = value; }
        }

        public CodeLanguages ImplementationLanguage
        {
            get { return this._implementationLanguage; }
            set { this._implementationLanguage = value; }
        }
    }
}
