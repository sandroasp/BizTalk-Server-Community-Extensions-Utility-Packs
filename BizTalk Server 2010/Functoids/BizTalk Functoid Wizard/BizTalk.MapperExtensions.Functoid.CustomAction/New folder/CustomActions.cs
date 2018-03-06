using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using Microsoft.Win32;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;


namespace BizTalk.MapperExtensions.Functoid.CustomAction
{
    [RunInstaller(true)]
    public class CustomActions : Installer
    {
        private string _BizTalkInstallPath = null;
        private string _DotNetFrameworkPath = null;
        private Exception _Exception = null;
        private bool _GeneralError = false;
        private string _VisualStudioInstallPath = null;
        private string _VsDirPath = null;
        private const string vszFile = "BizTalkFunctoidWizard.vsz";

        /// <summary>
        /// 
        /// </summary>
        public CustomActions()
        {
            Debugger.Launch();
            RegistryKey regkey;
            try
            {
                // BizTalk installation folder
                regkey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\BizTalk Server\3.0");
                if (regkey == null) throw new Exception("Biztalk Server doesn't seem to be installed.");
                this._BizTalkInstallPath = regkey.GetValue("InstallPath").ToString();
                regkey.Close();

                // Visual studio installation folder
                this._VsDirPath = this._BizTalkInstallPath + @"Developer Tools\BizTalkProjects\BTSProjects.vsdir";
                regkey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\VisualStudio\9.0");
                if (regkey == null) throw new Exception("Visual Studio doesn't seem to be installed.");
                this._VisualStudioInstallPath = regkey.GetValue("InstallDir").ToString();
                regkey.Close();

                // .NET framework installation folder
                regkey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\.NETFramework");
                if (regkey == null) throw new Exception(".NET Framework doesn't seem to be installed.");
                this._DotNetFrameworkPath = regkey.GetValue("InstallRoot").ToString();
                regkey.Close();
            }
            catch (Exception e)
            {
                this._Exception = e;
                this._GeneralError = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool AddVsDirLine()
        {
            string text1;
            StreamReader reader1;
            string text2;
            StreamWriter writer1;
            try
            {
                Guid guid = new Guid("1BA86F75-E5FB-478B-BA71-8C6AB761ECFF");
                text1 = vszFile + "| |BizTalk Server Functoid Project|300|Creates a BizTalk Server Functoid Project|{" + guid.ToString() + "}|226| |#133";
                if ((File.GetAttributes(this._VsDirPath) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    File.SetAttributes(this._VsDirPath, FileAttributes.Normal);
                }
                reader1 = new StreamReader(this._VsDirPath);
                text2 = reader1.ReadToEnd();
                reader1.Close();
                if (text2.IndexOf(vszFile) == -1)
                {
                    writer1 = File.AppendText(this._VsDirPath);
                    writer1.WriteLine(text1);
                    writer1.Close();
                }
                File.SetAttributes(this._VsDirPath, FileAttributes.ReadOnly);
            }
            catch (Exception exception1)
            {
                return this.HandleError("AddVsDirLine", exception1);
            }
            return true;
        }

        private bool RemoveVsDirLine()
        {
            string text1;
            StreamReader reader1;
            string text2;
            StreamWriter writer1;
            try
            {
                Guid guid = new Guid("ef7e327e-cd33-11d4-8326-00c04fa0ce8d");
                text1 = vszFile + "| |BizTalk Server Functoid Project|300|Creates a BizTalk Server Functoid|{" + guid.ToString() + "}|226| |#133";
                if ((File.GetAttributes(this._VsDirPath) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    File.SetAttributes(this._VsDirPath, FileAttributes.Normal);
                }
                reader1 = new StreamReader(this._VsDirPath);
                text2 = reader1.ReadToEnd();
                reader1.Close();

                writer1 = File.CreateText(this._VsDirPath);
                writer1.Write(text2.Replace(text1, ""));
                writer1.Close();

                File.SetAttributes(this._VsDirPath, FileAttributes.ReadOnly);
            }
            catch (Exception exception1)
            {
                return this.HandleError("RemoveVsDirLine", exception1);
            }
            return true;
        }

        private bool AddVszFile()
        {
            string text1;
            FileInfo info1;
            try
            {
                text1 = this._BizTalkInstallPath + @"Developer Tools\BizTalkProjects\" + vszFile;
                info1 = new FileInfo(text1);
                if (info1.Exists)
                {
                    info1.Delete();
                }
                using (TextWriter writer1 = new StreamWriter(text1, false))
                {
                    writer1.WriteLine("VSWIZARD 7.0");
                    writer1.WriteLine("Wizard=VSWizard.FunctoidProjectWizard");
                    writer1.WriteLine("Param=\"WIZARD_NAME = FunctoidProjectWizard\"");
                    writer1.WriteLine("Param=\"WIZARD_UI = FALSE\"");
                    writer1.WriteLine("Param=\"PROJECT_TYPE = CSPROJ\"");
                }
            }
            catch (Exception exception1)
            {
                return this.HandleError("AddVszFile", exception1);
            }
            return true;
        }

        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);
            try
            {
                if (!this._GeneralError)
                {
                    return;
                }
                throw this._Exception;
            }
            catch (Exception exception1)
            {
                base.Context.LogMessage(exception1.Message);
                throw exception1;
            }
        }

        private bool HandleError(string functionName, Exception e)
        {
            base.Context.LogMessage(e.Message);
            DialogResult result2 = MessageBox.Show(e.ToString(), "Functoid Wizard Installer", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            switch (result2)
            {
                case DialogResult.Abort:
                    {
                        throw e;
                    }
                case DialogResult.Retry:
                    {
                        return false;
                    }
                case DialogResult.Ignore:
                    {
                        if (MessageBox.Show("If you choose to ignore you will have to perform some actions manually. " + Environment.NewLine + "Continue?", "FunctoidWizard 2009 Installer - " + functionName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        {
                            throw e;
                        }
                        break;
                    }
            }

            return true;
        }

        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);
            try
            {
                if (this._GeneralError)
                {
                    throw this._Exception;
                }
                while (!this.AddVszFile())
                {
                }
                while (!this.AddVsDirLine())
                {
                }
                /*while (!this.CompileBaseClasses())
                {
                }*/
                while (!this.RegisterFunctoidWizard(false))
                {
                }
            }
            catch (Exception exception1)
            {
                base.Context.LogMessage(exception1.Message);
                throw exception1;
            }
        }

        private bool RegisterFunctoidWizard(bool unregister)
        {
            string text1 = "";
            string text2;
            string text3 = "";
            ProcessStartInfo info1;
            Process process1;
            try
            {
                if (base.Context.Parameters.ContainsKey("ApplicationPath"))
                    text1 = this.Context.Parameters["ApplicationPath"];
                else if (base.Context.Parameters.ContainsKey("assemblypath"))
                    text1 = Path.GetDirectoryName(base.Context.Parameters["assemblypath"]);

                if (!text1.EndsWith(@"\"))
                {
                    text1 = text1 + @"\";
                }
                text2 = this._DotNetFrameworkPath + @"v2.0.50727\RegAsm.exe";
                if (unregister)
                {
                    text3 = "/u ";
                }
                text3 += "\"" + text1 + "BizTalk.MapperExtensions.Functoid.Wizard.dll\" /codebase /s";
                info1 = new ProcessStartInfo(text2, text3);
                info1.CreateNoWindow = true;
                info1.WindowStyle = ProcessWindowStyle.Hidden;
                process1 = Process.Start(info1);
                process1.WaitForExit();
            }
            catch (Exception exception1)
            {
                if (!unregister)
                {
                    return this.HandleError("RegisterFunctoidWizard", exception1);
                }
                return true;
            }
            return true;
        }

        private void RemoveVszFile()
        {
            string text1;
            FileInfo info1;
            try
            {
                text1 = this._BizTalkInstallPath + @"Developer Tools\BizTalkProjects\BizTalkFunctoidWizard.vsz";
                info1 = new FileInfo(text1);
                if (!info1.Exists)
                {
                    return;
                }
                info1.Delete();
            }
            catch (Exception exception1)
            {
                base.Context.LogMessage(exception1.Message);
            }
        }

        public override void Rollback(IDictionary savedState)
        {
            try
            {
                if (this._GeneralError)
                {
                    throw this._Exception;
                }
                this.RemoveVszFile();
            }
            catch (Exception exception1)
            {
                base.Context.LogMessage(exception1.Message);
            }
            base.Rollback(savedState);
        }

        public override void Uninstall(IDictionary savedState)
        {
            try
            {
                if (this._GeneralError)
                {
                    throw this._Exception;
                }
                this.RemoveVszFile();
                this.RemoveVsDirLine();
                this.RegisterFunctoidWizard(true);
            }
            catch (Exception exception1)
            {
                base.Context.LogMessage(exception1.Message);
            }
            base.Uninstall(savedState);
        }
    }



    //[RunInstaller(true)]
    //public partial class CustomActions : System.Configuration.Install.Installer
    //{
    //    /// <summary>
    //    /// contains the BizTalk Server [version] installation folder
    //    /// </summary>
    //    private string _BizTalkInstallPath = null;
    //    /// <summary>
    //    /// contains the retrieved BizTalk Server target Visual Studio version from registry.
    //    /// </summary>
    //    private string _TargetVSVersion = null;
    //    /// <summary>
    //    /// contains the Visual Studio Wizard definition file location
    //    /// </summary>
    //    private string _BizTalkVszFileLocation = null;
    //    /// <summary>
    //    /// contains the path to the running .NET framework version for use of RegAsm.exe
    //    /// </summary>
    //    private string _DotNetFrameworkPath = null;
    //    /// <summary>
    //    /// stores any exception that might occur for review
    //    /// </summary>
    //    private Exception _Exception = null;
    //    /// <summary>
    //    /// defines whether the occured exception is a 'general' exception
    //    /// </summary>
    //    private bool _GeneralError = false;
    //    /// <summary>
    //    /// contains the Visual Studio installation folder
    //    /// </summary>
    //    private string _VisualStudioInstallPath = null;
    //    /// <summary>
    //    ///  contains the path to the base folder where the Wizard definition file resides
    //    /// </summary>
    //    private string _VsDirPath = null;
    //    /// <summary>
    //    /// defines the Wizard definition file
    //    /// </summary>
    //    private const string vszFile = "BizTalkFunctoidProjectWizard.vsz";

    //    /// <summary>
    //    /// plain constructor, determines the locations of various of the used components
    //    /// (BizTalk Server, .NET framework, Visual Studio)
    //    /// </summary>
    //    public CustomActions()
    //    {
    //        Debugger.Launch();
    //        // regkey will contain the opened registry key for retrieving data
    //        RegistryKey regkey;

    //        try
    //        {
    //            // retrieve the BizTalk Server installation folder
    //            string bizTalkInstallRegistryKey = @"SOFTWARE\Microsoft\BizTalk Server\3.0";
    //            regkey = Registry.LocalMachine.OpenSubKey(bizTalkInstallRegistryKey);

    //            try
    //            {
    //                this._BizTalkInstallPath = regkey.GetValue("InstallPath").ToString();
    //                this._TargetVSVersion = regkey.GetValue("TargetVSVersion").ToString();
    //                this._BizTalkVszFileLocation = Path.Combine(this._BizTalkInstallPath, string.Format(@"Developer Tools\BizTalkProjects\{0}", vszFile));

    //                regkey.Close();
    //            }
    //            catch
    //            {
    //                base.Context.LogMessage(string.Format(@"Unable to locate BizTalk installation folder from registry. Tried InstallPath (2006/2009/2010) in HKLM\{0}", bizTalkInstallRegistryKey));
    //            }

    //            // Visual studio installation folder
    //            this._VsDirPath = Path.Combine(this._BizTalkInstallPath, @"Developer Tools\BizTalkProjects\BTSProjects.vsdir");
    //            string vsInstallFolderRegistryKey = string.Format(@"SOFTWARE\Microsoft\VisualStudio\{0}", this._TargetVSVersion);

    //            try
    //            {
    //                regkey = Registry.LocalMachine.OpenSubKey(vsInstallFolderRegistryKey);

    //                // set the actual Visual Studio installation folder for later use
    //                this._VisualStudioInstallPath = regkey.GetValue("InstallDir").ToString();

    //                regkey.Close();
    //            }
    //            catch
    //            {
    //                base.Context.LogMessage(string.Format("Unable to find Visual Studio installation path for version {0}", this._TargetVSVersion));
    //            }

    //            // .NET framework installation folder
    //            regkey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\.NETFramework");
    //            this._DotNetFrameworkPath = regkey.GetValue("InstallRoot").ToString();
    //            string frameworkVersion = string.Format("v{0}.{1}.{2}", Environment.Version.Major, Environment.Version.Minor, Environment.Version.Build);

    //            // the path to the .NET framework folder is in the form vx.y.z, where x.y.z is Major, Minor and Build
    //            // version of the framework. within the folder defined in HKLM\SOFTWARE\Microsoft\.NETFramework
    //            this._DotNetFrameworkPath = Path.Combine(this._DotNetFrameworkPath, frameworkVersion);

    //            regkey.Close();

    //        }
    //        catch (Exception e)
    //        {
    //            base.Context.LogMessage(e.Message);

    //            this._Exception = e;
    //            this._GeneralError = true;
    //        }
    //    }

    //    /// <summary>
    //    /// actually registers our wizard within the Visual Studio environment by adding a line to BTSProjects.vsdir
    //    /// </summary>
    //    /// <returns>whether the action actually succeeded</returns>
    //    private bool AddVsDirLine()
    //    {
    //        string vszLine = null;
    //        string definitionBuffer = null;

    //        try
    //        {
    //            Guid guid = new Guid("40a10721-c2d9-4cde-8dfb-8f1b42d246db");
    //            vszLine = vszFile + "|{" + guid.ToString() + "}|BizTalk Server Functoid Project|300|Creates a BizTalk Server Functoid project|{" + guid.ToString() + "}|226| |#133";
    //            // reset file attributes
    //            if ((File.GetAttributes(this._VsDirPath) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
    //            {
    //                File.SetAttributes(this._VsDirPath, FileAttributes.Normal);
    //            }
    //            using (StreamReader reader = new StreamReader(this._VsDirPath))
    //            {
    //                definitionBuffer = reader.ReadToEnd();
    //            }

    //            // only append the wizard line of not present
    //            if (definitionBuffer.IndexOf(vszFile) == -1)
    //            {
    //                using (StreamWriter writer = File.AppendText(this._VsDirPath))
    //                {
    //                    writer.WriteLine(vszLine);
    //                }
    //            }

    //            // set the RO flag to the file
    //            File.SetAttributes(this._VsDirPath, FileAttributes.ReadOnly);
    //        }
    //        catch (Exception e)
    //        {
    //            return this.HandleError("AddVsDirLine", e);
    //        }

    //        return true;
    //    }

    //    /// <summary>
    //    /// creates the .vsz file. existing file is removed if need be
    //    /// </summary>
    //    /// <returns>whether the operation succeeded</returns>
    //    private bool AddVszFile()
    //    {
    //        try
    //        {
    //            this.RemoveVszFile();

    //            using (TextWriter writer = new StreamWriter(this._BizTalkVszFileLocation, false))
    //            {
    //                writer.WriteLine("VSWIZARD 7.0");
    //                writer.WriteLine("Wizard=VSWizard.FunctoidProjectWizard");
    //                writer.WriteLine("Param=\"WIZARD_NAME = FunctoidProjectWizard\"");
    //                writer.WriteLine("Param=\"WIZARD_UI = FALSE\"");
    //                writer.WriteLine("Param=\"PROJECT_TYPE = CSPROJ\"");
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            return this.HandleError("AddVszFile", e);
    //        }
    //        return true;
    //    }

    //    public override void Commit(IDictionary savedState)
    //    {
    //        base.Commit(savedState);

    //        try
    //        {
    //            if (!this._GeneralError)
    //            {
    //                return;
    //            }
    //            throw this._Exception;
    //        }
    //        catch (Exception e)
    //        {
    //            base.Context.LogMessage(e.Message);

    //            throw e;
    //        }
    //    }

    //    private bool HandleError(string functionName, Exception e)
    //    {
    //        base.Context.LogMessage(e.Message);

    //        DialogResult result2 = MessageBox.Show(e.ToString(), "Functoid Project Wizard Installer", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);

    //        switch (result2)
    //        {
    //            case DialogResult.Abort:
    //                {
    //                    throw e;
    //                }
    //            case DialogResult.Retry:
    //                {
    //                    return false;
    //                }
    //            case DialogResult.Ignore:
    //                {
    //                    if (MessageBox.Show("If you choose to ignore you will have to perform some actions manually as described in the readme. " + Environment.NewLine + "Continue?", "Functoid Project Wizard Installer - " + functionName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
    //                    {
    //                        throw e;
    //                    }
    //                    break;
    //                }
    //        }

    //        return true;
    //    }

    //    public override void Install(IDictionary stateSaver)
    //    {
    //        Debugger.Break();
    //        base.Install(stateSaver);

    //        try
    //        {
    //            if (this._GeneralError)
    //            {
    //                throw this._Exception;
    //            }
    //            while (!this.AddVszFile())
    //            {
    //            }
    //            while (!this.AddVsDirLine())
    //            {
    //            }
    //            while (!this.RegisterFunctoidProjectWizard(false))
    //            {
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            base.Context.LogMessage(e.Message);

    //            throw e;
    //        }
    //    }

    //    /// <summary>
    //    /// performs the necessery steps to either register or remove the wizard
    //    /// </summary>
    //    /// <param name="unregister">whether the installer is registering or removing the wizards</param>
    //    /// <returns>whether the operation succeeded</returns>
    //    private bool RegisterFunctoidProjectWizard(bool unregister)
    //    {
    //        string regAsmLocation;
    //        string regAsmArguments;
    //        ProcessStartInfo piInfo;
    //        Process process;

    //        try
    //        {
    //            // we use RegAsm.exe by spawning it just like the command-line would
    //            regAsmLocation = Path.Combine(this._DotNetFrameworkPath, "RegAsm.exe");

    //            // append /u if we're removing
    //            if (unregister)
    //            {
    //                regAsmLocation = regAsmLocation + " /u";
    //            }

    //            // format the RegAsm arguments
    //            regAsmArguments = string.Format("\"{0}\"", base.Context.Parameters["assemblypath"]);
    //            regAsmArguments += " /codebase /s";

    //            // create and run the command-line in the background
    //            piInfo = new ProcessStartInfo(regAsmLocation, regAsmArguments);
    //            piInfo.CreateNoWindow = true;
    //            piInfo.WindowStyle = ProcessWindowStyle.Hidden;
    //            process = Process.Start(piInfo);
    //            process.WaitForExit();
    //        }
    //        catch (Exception e)
    //        {
    //            if (!unregister)
    //            {
    //                return this.HandleError("RegisterFunctoidProjectWizard", e);
    //            }

    //            return true;
    //        }

    //        return true;
    //    }

    //    /// <summary>
    //    /// removes the wizard definition file
    //    /// </summary>
    //    private void RemoveVszFile()
    //    {
    //        FileInfo fi;
    //        try
    //        {
    //            fi = new FileInfo(this._BizTalkVszFileLocation);
    //            if (!fi.Exists)
    //            {
    //                return;
    //            }
    //            fi.Delete();
    //        }
    //        catch (Exception e)
    //        {
    //            base.Context.LogMessage(e.Message);
    //        }
    //    }

    //    public override void Rollback(IDictionary savedState)
    //    {
    //        try
    //        {
    //            if (this._GeneralError)
    //            {
    //                throw this._Exception;
    //            }

    //            this.RemoveVszFile();
    //        }
    //        catch (Exception e)
    //        {
    //            base.Context.LogMessage(e.Message);
    //        }
    //        base.Rollback(savedState);
    //    }

    //    public override void Uninstall(IDictionary savedState)
    //    {
    //        try
    //        {
    //            if (this._GeneralError)
    //            {
    //                throw this._Exception;
    //            }

    //            this.RemoveVszFile();
    //            this.RegisterFunctoidProjectWizard(true);
    //        }
    //        catch (Exception e)
    //        {
    //            base.Context.LogMessage(e.Message);
    //        }
    //        base.Uninstall(savedState);
    //    }
    //}
}