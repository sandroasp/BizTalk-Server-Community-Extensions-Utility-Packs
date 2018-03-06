using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizTalk.MapperExtensions.Functoid.Wizard
{
    /// <summary>
    /// List of constants to find values in the namevaluecollection
    /// </summary>
    internal class WizardValues
    {
        public const string AssemblyKeyFile = "AssemblyKeyFile";
        public const string AssemblyKeyName = "AssemblyKeyName";
        public const string AssemblyName = "AssemblyName";

        #region Geral Properties
        /// <summary>
        /// defines the classname, as entered by the user
        /// </summary>
        public const string ClassName = "ClassName";
        /// <summary>
        /// defines the namespace in which the component should reside, as entered by the user
        /// </summary>
        public const string Namespace = "Namespace";
        #endregion

        #region Functoid Properties
        /// <summary>
        /// defines the Functoid ID, as entered by the user
        /// </summary>
        public const string FunctoidID = "FunctoidID";
        /// <summary>
        /// defines the Functoid name, as entered by the user
        /// </summary>
        public const string FunctoidName = "FunctoidName";
        /// <summary>
        /// defines the description (single-line) of the Functoid, as entered by the user
        /// </summary>
        public const string FunctoidDescription = "FunctoidDescription";
        /// <summary>
        /// defines the ToolTip of the Functoid, as entered by the user
        /// </summary>
        public const string FunctoidToolTip = "FunctoidToolTip";
        /// <summary>
        /// defines the Bitmap of the Functoid, as entered by the user
        /// </summary>
        public const string FunctoidBitmap = "FunctoidBitmap";
        /// <summary>
        /// defines the Functoid Category, as entered by the user
        /// </summary>
        public const string FunctoidCategory = "FunctoidCategory";
        /// <summary>
        /// defines the ReturnType of the Functoid, as entered by the user
        /// </summary>
        public const string FunctoidReturnType = "FunctoidReturnType";
        /// <summary>
        /// defines the OutputConnType of the Functoid, as entered by the user
        /// </summary>
        public const string FunctoidOutputConnType = "FunctoidOutputConnType";
        /// <summary>
        /// defines the InputConnType of the Functoid, as entered by the user
        /// </summary>
        public const string FunctoidInputConnType = "FunctoidInputConnType";
        /// <summary>
        /// defines the ExceptionText of the Functoid, as entered by the user
        /// </summary>
        public const string FunctoidExceptionText = "FunctoidExceptionText";
        /// <summary>
        /// defines the MinParams of the Functoid, as entered by the user
        /// </summary>
        public const string FunctoidMinParams = "FunctoidMinParams";
        /// <summary>
        /// defines the MaxParams of the Functoid, as entered by the user
        /// </summary>
        public const string FunctoidMaxParams = "FunctoidMaxParams";
        #endregion

        #region Language Properties
        /// <summary>
        /// defines the programming languages in which the functoid project should
        /// be implemented, as choosen by the user
        /// </summary>
        public const string CodeLanguage = "CodeLanguage";
        #endregion
    }
}
