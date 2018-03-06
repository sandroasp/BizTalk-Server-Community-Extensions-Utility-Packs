using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using EnvDTE;
using EnvDTE80;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using Microsoft.Win32;
using System.Windows.Forms;
using System.IO;
using VSLangProj;

namespace BizTalk.MapperExtensions.Functoid.Wizard
{
    public delegate void AddWizardResultEvent(object sender, PropertyPairEvent e);
    public delegate void AddFunctoidParameterEvent(object sender, PropertyPairEvent e);

    enum ContextOptions : int
    {
        WizardType,
        ProjectName,
        LocalDirectory,
        InstallationDirectory,
        FExclusive,
        SolutionName,
        Silent
    }

    /// <summary>
    /// defines the supported languages we generate sourcecode for
    /// </summary>
    internal enum CodeLanguages
    {
        CSharp = 0,
        VisualBasicNET = 1
    }

    /// <summary>
    /// Class (com-object) called by Visual Studio .NET to start a new Functoid project. 
	/// </summary>
    [ProgId("VSWizard.FunctoidProjectWizard")]
    public class FunctoidProjectWizard : IDTWizard
    {
        #region Custom Wizard Properties
        private string _assemblyKeyFile = "AssemblyKeyFile";
        private string _assemblyName = "AssemblyName";
        private string _assemblyKeyName = "AssemblyKeyName";
        private string _className = "ClassName";
        private string _nameSpace = "NameSpace";

        private string _functoidID = "Id";
        private Image _functoidBitmap;
        private string _functoidName = "Name";
        private string _functoidToolTip = "ToolTip";
        private string _functoidCategory = "Category";
        private string _functoidDescription = "Description";
        private string _functoidReturnType = "ReturnType";
        private string _functoidOutputConnType = "OutputConnType";
        private string _functoidInputConnType = "InputConnType";
        private string _functoidExceptionText = "ExceptionText";
        private string _functoidMinParams = "MinParams";
        private string _functoidMaxParams = "MaxParams";

        private CodeLanguages _codeLanguage = CodeLanguages.CSharp;
        #endregion

        #region Basic Properties
        private string _projectDirectory = null;
        private string _projectName = null;
        private string _solutionName = null;
        bool _fExclusive = false;
        #endregion

        #region Global Variables
        private ResourceTemplate _resourceTemplate = null;
        private Solution _functoidSolution = null;
        private string _bizTalkInstallPath = null;
        #endregion

        #region Properties Collections
        private Hashtable _wizardResults = null;
        private NameValueCollection _functoidParameters = null;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public FunctoidProjectWizard()
		{
			const string BizTalkKey = @"SOFTWARE\Microsoft\BizTalk Server\3.0";
			RegistryKey bizTalkReg = Registry.LocalMachine.OpenSubKey(BizTalkKey);
			_bizTalkInstallPath = bizTalkReg.GetValue("InstallPath").ToString();
			bizTalkReg.Close();
		}


		/// <summary>
		/// Main function for wizard project. Calls the wizard-
		/// form and then delegates control to the CreateSolution function
		/// </summary>
		/// <param name="Application"></param>
		/// <param name="hwndOwner"></param>
		/// <param name="contextParams"></param>
		/// <param name="CustomParams"></param>
		/// <param name="retval"></param>
		public void Execute(object Application, int hwndOwner, ref object[] contextParams, ref object[] CustomParams, ref EnvDTE.wizardResult retval)
		{
            //System.Diagnostics.Debugger.Launch();
			_DTE IDEObject = (_DTE)Application;

			try
			{
                FunctoidProjectWizardForm wizardForm = new FunctoidProjectWizardForm();
                wizardForm.Enabled = true;
				if (wizardForm.ShowDialog() == DialogResult.OK)
				{
					// Retrieve the wizard data
					_wizardResults = wizardForm.WizardResults;
					_functoidParameters = wizardForm.FunctoidParameters;

					// Create the solution
					CreateSolution(IDEObject,contextParams);
					retval = wizardResult.wizardResultSuccess;
				}
				else
				{
					retval = wizardResult.wizardResultCancel;					
				}
			}
			catch(Exception err)
			{
				MessageBox.Show(err.Message);
				retval = wizardResult.wizardResultFailure;
			}
		}
	

		/// <summary>
		/// Creates the solution and calls the functions to create the projects
		/// </summary>
		/// <param name="IDEObject"></param>
		/// <param name="contextParams"></param>
		public void CreateSolution(_DTE IDEObject,object[] contextParams)
		{
			#region Get Wizard Results
			// Get basic wizard results
			_projectDirectory = contextParams[(int)ContextOptions.LocalDirectory].ToString();
			_projectName = contextParams[(int)ContextOptions.ProjectName].ToString();
			_solutionName = contextParams[(int)ContextOptions.SolutionName].ToString();
			_fExclusive = bool.Parse(contextParams[(int)ContextOptions.FExclusive].ToString());	

			// Get custom wizard results
			_assemblyKeyFile = (string)_wizardResults[WizardValues.AssemblyKeyFile];
			_assemblyKeyName = (string)_wizardResults[WizardValues.AssemblyKeyName];
			_assemblyName = (string)_wizardResults[WizardValues.AssemblyName];
			_className = (string)_wizardResults[WizardValues.ClassName];
			_nameSpace = (string)_wizardResults[WizardValues.Namespace];
            _functoidID = (string)_wizardResults[WizardValues.FunctoidID];
            _functoidBitmap = (Image)_wizardResults[WizardValues.FunctoidBitmap];
            _functoidName = (string)_wizardResults[WizardValues.FunctoidName];
            _functoidToolTip = (string)_wizardResults[WizardValues.FunctoidToolTip];
            _functoidCategory = (string)_wizardResults[WizardValues.FunctoidCategory];
            _functoidDescription = (string)_wizardResults[WizardValues.FunctoidDescription];
            _functoidReturnType = (string)_wizardResults[WizardValues.FunctoidReturnType];
            _functoidOutputConnType = (string)_wizardResults[WizardValues.FunctoidOutputConnType];
            _functoidInputConnType = (string)_wizardResults[WizardValues.FunctoidInputConnType];
            _functoidExceptionText = (string)_wizardResults[WizardValues.FunctoidExceptionText];
            _functoidMinParams = (string)_wizardResults[WizardValues.FunctoidMinParams];
            _functoidMaxParams = (string)_wizardResults[WizardValues.FunctoidMaxParams];

			// Get Code Language
			switch((string)_wizardResults[WizardValues.CodeLanguage])
			{
				case "CSharp":
					_codeLanguage = CodeLanguages.CSharp;
					break;
				case "VB":
                    _codeLanguage = CodeLanguages.VisualBasicNET;
					break;
			}
			#endregion

			#region Create the resource template class & load it with wizard values
			// Create the resource template class 
			_resourceTemplate = new ResourceTemplate();

			// Load template class with the wizard values
			_resourceTemplate.AssemblyKeyFile = _assemblyKeyFile;
			_resourceTemplate.AssemblyKeyName = _assemblyKeyName;
			_resourceTemplate.AssemblyName = _assemblyName;
			_resourceTemplate.BiztalkDir = _bizTalkInstallPath;
			_resourceTemplate.ClassName = _className;
			_resourceTemplate.NameSpace = _nameSpace;
            _resourceTemplate.Id = _functoidID;
            _resourceTemplate.Icon = _functoidBitmap;
            _resourceTemplate.Name = _functoidName;
            _resourceTemplate.ToolTip = _functoidToolTip;
            _resourceTemplate.Category = _functoidCategory;
            _resourceTemplate.Description = _functoidDescription;
            _resourceTemplate.ReturnType = _functoidReturnType;
            _resourceTemplate.OutputConnType = _functoidOutputConnType;
            _resourceTemplate.InputConnType = _functoidInputConnType;
            _resourceTemplate.ExceptionText = _functoidExceptionText;
            _resourceTemplate.MinParams = _functoidMinParams;
            _resourceTemplate.MaxParams = _functoidMaxParams;
			_resourceTemplate.FunctoidParameters = _functoidParameters;
			_resourceTemplate.ImplementationLanguage = _codeLanguage;
			#endregion

			#region Create Solution & Projects
			if (!_fExclusive) //New solution or existing?
			{
				// Get a reference to the solution from the IDE Object
				_functoidSolution = IDEObject.Solution; 
			}
			else
			{
				// Use the solution class to create a new solution 
				_functoidSolution = IDEObject.Solution; 
				_functoidSolution.Create(_projectDirectory,_projectName);
			}

			//Create the project
			CreateFunctoidProject();
			
			// Save the Solution file
			_functoidSolution.SaveAs(_projectDirectory + "\\" + _projectName + ".sln");
			#endregion
		}


		/// <summary>
		/// Creates the designtime project and adds the appropriate files to the
		/// project.
		/// </summary>
		public void CreateFunctoidProject()
		{
			string relativePath = null;
			string projectTemplateFileName = null;
			string projectFileName = null;
			string extension = null;
			
			relativePath = "Templates";
			if (_codeLanguage == CodeLanguages.CSharp)
			{
				projectTemplateFileName = "Functoid.$csproj";
				projectFileName = _projectName + ".csproj";
				extension = "$cs";
			}
			else
			{
				projectTemplateFileName = "Functoid.$vbproj";
				projectFileName = _projectName + ".vbproj";
				extension = "$vb";
			}

			string FunctoidDirPath = _projectDirectory;

			// Create the directory only if it does not already exist.
			if (!Directory.Exists(FunctoidDirPath))
				Directory.CreateDirectory(FunctoidDirPath);

			// Create the projectfile for the solution
			_resourceTemplate.Render(projectTemplateFileName, projectFileName, relativePath, FunctoidDirPath);

			EnvDTE.Project FunctoidProject =
 
				_functoidSolution.AddFromFile(FunctoidDirPath + "\\" + projectFileName, false);

			// Get a reference to the Visual Studio Project and 
			// use it to add a reference to the framework assemblies
			VSProject FunctoidVSProject = (VSProject)FunctoidProject.Object;
			
			FunctoidVSProject.References.Add("System.dll");
			FunctoidVSProject.References.Add("System.Xml.dll");
			FunctoidVSProject.References.Add("System.Data.dll");
			FunctoidVSProject.References.Add(_bizTalkInstallPath + 
				@"Developer Tools\Microsoft.BizTalk.BaseFunctoids.dll");  
  
			// Render and add other project files
			_resourceTemplate.RenderAndAddToProject("AssemblyInfo." + extension,
				"AssemblyInfo." + extension.Replace("$",""), relativePath, FunctoidDirPath, FunctoidVSProject, false);
			_resourceTemplate.RenderAndAddToProject("Functoid." + extension,
				_className + "." + extension.Replace("$",""), relativePath, FunctoidDirPath, FunctoidVSProject, false);

			// Render and add resource project file
			_resourceTemplate.RenderResourceFileAndAddToProject(_className + ".resx", FunctoidDirPath, FunctoidVSProject, true);

			// Save the project file
			FunctoidVSProject.Project.Save(_projectDirectory + @"\" + projectFileName); 
		}
    }
}
