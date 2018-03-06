using Microsoft.Win32;
using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

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
        private const string vszFile = "FunctoidProjectWizard.vsz";

        public CustomActions()
        {
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

        private bool AddVsDirLine()
        {
            string text1;
            StreamReader reader1;
            string text2;
            StreamWriter writer1;
            try
            {
                Guid guid = new Guid("{7a51b143-7eea-450d-baef-827253c52e43}");
                text1 = vszFile 
                    + "|{" 
                    + guid.ToString() 
                    + "}|BizTalk Server Functoid Project|300|Creates a BizTalk Server Functoid Project|{" 
                    + guid.ToString() 
                    + "}|226| |#133";
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
                Guid guid = new Guid("{7a51b143-7eea-450d-baef-827253c52e43}");
                text1 = vszFile + "|{" + guid.ToString() + "}|BizTalk Server Functoid Project|300|Creates a BizTalk Server Functoid Project|{" + guid.ToString() + "}|226| |#133";
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
                text1 = this._BizTalkInstallPath + @"Developer Tools\BizTalkProjects\FunctoidProjectWizard.vsz";
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
}