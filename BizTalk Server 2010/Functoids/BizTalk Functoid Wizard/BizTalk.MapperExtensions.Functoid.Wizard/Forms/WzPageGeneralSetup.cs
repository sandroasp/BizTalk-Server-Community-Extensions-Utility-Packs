using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Security.Cryptography;

namespace BizTalk.MapperExtensions.Functoid.Wizard
{
    public class WzPageGeneralSetup : Microsoft.BizTalk.Wizard.WizardInteriorPage, WizardControlInterface
    {
        #region Global Variables
        private const string AssemblyKeyFileRegEx = @"^(((?<DRIVE>[a-zA-Z]:)|(\\\\(?<SERVER>[0-9]*[a-z\-][a-z0-9\-]*)\\(?<VOLUME>[^\.\x01-\x1F\\""\*\?<>:|\\/][^\x01-\x1F\\""\*\?|><:\\/]*)))?(?<FOLDERS>(?<FOLDER1>(\.|(\.\.)|([^\.\x01-\x1F\\""\*\?|><:\\/][^\x01-\x1F\\""\*\?<>:|\\/]*)))?(?<FOLDERm>[\\/](\.|(\.\.)|([^\.\x01-\x1F\\""\*\?|><:\\/][^\x01-\x1F\\""\*\?<>:|\\/]*)))*)?[\\/]?(?!^(PRN|AUX|CLOCK\$|NUL|CON|COM\d|LPT\d|\..*)(\..+)?$)[^\x00-\x1f\\?*:\"";|/]+)*$";
        private const string AssemblyNameRegEx = @"^(?!^(PRN|AUX|CLOCK\$|NUL|CON|COM\d|LPT\d|\..*)(\..+)?$)[^\x00-\x1f\\?*:\"";|/]+$";
        private const string NamespaceRegEx = @"^[A-Za-z_][_A-Za-z0-9]*(\.[A-Za-z_][A-Za-z0-9_]*)*$";
        private const string ClassnameRegEx = @"^[A-Za-z_][A-Za-z0-9_]*$";
        public event AddWizardResultEvent _AddWizardResultEvent;
        private string _projectDir;
        #endregion

        #region Form Components
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button cmdSelectKey;
        private System.Windows.Forms.TextBox txtAssemblyKeyFile;
        private System.Windows.Forms.TextBox txtClassname;
        private System.Windows.Forms.TextBox txtNamespace;
        private Button cmdNewKey;
        private System.Windows.Forms.ErrorProvider errorProvider;
        #endregion

        public WzPageGeneralSetup()
        {
            // This call is required by the Windows Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitializeComponent call
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        public bool NextButtonEnabled
        {
            get { return GetAllStates(); }
        }

        public bool NeedSummary
        {
            get { return false; }
        }

        protected void AddWizardResult(string strName, object Value)
        {
            PropertyPairEvent PropertyPair = new PropertyPairEvent(strName, Value);
            OnAddWizardResult(PropertyPair);
        }

        /// <summary>
        /// The protected OnRaiseProperty method raises the event by invoking 
        /// the delegates. The sender is always this, the current instance 
        /// of the class.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnAddWizardResult(PropertyPairEvent e)
        {
            if (e != null)
            {
                // Invokes the delegates. 
                _AddWizardResultEvent(this, e);
            }
        }

        private void WzPageGeneralSetup_Leave(object sender, System.EventArgs e)
        {
            try
            {
                // Save wizard results
                AddWizardResult(WizardValues.ClassName, txtClassname.Text);
                AddWizardResult(WizardValues.Namespace, txtNamespace.Text);
                AddWizardResult(WizardValues.AssemblyName, txtNamespace.Text);
                AddWizardResult(WizardValues.AssemblyKeyFile, txtAssemblyKeyFile.Text);
                AddWizardResult(WizardValues.AssemblyKeyName, "Default");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                Trace.WriteLine(err.Message + Environment.NewLine + err.StackTrace);
            }
        }


        /// <summary>
        /// Resets all of the errorproviders when anything succeeds
        /// </summary>
        private void ResetAllErrProviders()
        {
            foreach (Control ctl in this.Controls)
            {
                errorProvider.SetError(ctl, "");
            }
        }

        private void Element_Changed(object sender, System.EventArgs e)
        {
            EnableNext(GetAllStates());
        }

        private bool GetAllStates()
        {
            return (Regex.IsMatch(txtAssemblyKeyFile.Text, AssemblyKeyFileRegEx) &&
                Regex.IsMatch(txtNamespace.Text, AssemblyNameRegEx) &&
                Regex.IsMatch(txtClassname.Text, ClassnameRegEx) &&
                Regex.IsMatch(txtNamespace.Text, NamespaceRegEx));
        }

        private void cmdSelectKey_Click(object sender, System.EventArgs e)
        {
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                txtAssemblyKeyFile.Text = openFileDialog.FileName;
            }
        }

        private void cmdNewKey_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                GenerateKeyFile(saveFileDialog.FileName);
                txtAssemblyKeyFile.Text = saveFileDialog.FileName;
            }
        }

        private void GenerateKeyFile(string filename)
        {
            CspParameters parms = new CspParameters();
            parms.KeyNumber = 2;
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider(parms);
            byte[] array = provider.ExportCspBlob(!provider.PublicOnly);

            using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
                fs.Write(array, 0, array.Length);
        }

        private void txtClassname_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Validate Classname
            if (txtClassname.Text.Trim() == "")
                errorProvider.SetError(txtClassname, "Classname cannot be empty");
            else if (!Regex.IsMatch(txtClassname.Text, ClassnameRegEx))
                errorProvider.SetError(txtClassname, "Illegal classname");
            else
                errorProvider.SetError(txtClassname, "");

            // Enable Next if everything's OK
            EnableNext(GetAllStates());
        }

        private void txtNamespace_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Validate Namespace
            if (txtNamespace.Text.Trim() == "")
                errorProvider.SetError(txtNamespace, "Namespace cannot be empty");
            else if (!Regex.IsMatch(txtNamespace.Text, NamespaceRegEx))
                errorProvider.SetError(txtNamespace, "Illegal namespace");
            else
                errorProvider.SetError(txtNamespace, "");

            // Enable Next if everything's OK
            EnableNext(GetAllStates());

        }

        private void txtAssemblyName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Validate AssemblyName
            if (txtNamespace.Text.Trim() == "")
                errorProvider.SetError(txtNamespace, "Assembly Name cannot be empty");
            else if (!Regex.IsMatch(txtNamespace.Text, AssemblyNameRegEx))
                errorProvider.SetError(txtNamespace, "Illegal filename");
            else
                errorProvider.SetError(txtNamespace, "");

            // Enable Next if everything's OK
            EnableNext(GetAllStates());
        }

        private void txtAssemblyKeyFile_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Validate Assembly Key File
            if (!Regex.IsMatch(txtAssemblyKeyFile.Text, AssemblyKeyFileRegEx))
                errorProvider.SetError(cmdSelectKey, "Illegal path/filename");
            else
                errorProvider.SetError(cmdSelectKey, "");

            // Enable Next if everything's OK
            EnableNext(GetAllStates());
        }

        #region Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WzPageGeneralSetup));
            this.txtClassname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAssemblyKeyFile = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdSelectKey = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.cmdNewKey = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            resources.ApplyResources(this.panelHeader, "panelHeader");
            // 
            // labelTitle
            // 
            resources.ApplyResources(this.labelTitle, "labelTitle");
            // 
            // labelSubTitle
            // 
            resources.ApplyResources(this.labelSubTitle, "labelSubTitle");
            // 
            // txtClassname
            // 
            resources.ApplyResources(this.txtClassname, "txtClassname");
            this.txtClassname.Name = "txtClassname";
            this.txtClassname.TextChanged += new System.EventHandler(this.Element_Changed);
            this.txtClassname.Validating += new System.ComponentModel.CancelEventHandler(this.txtClassname_Validating);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            resources.ApplyResources(this.errorProvider, "errorProvider");
            // 
            // txtNamespace
            // 
            resources.ApplyResources(this.txtNamespace, "txtNamespace");
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.TextChanged += new System.EventHandler(this.Element_Changed);
            this.txtNamespace.Validating += new System.ComponentModel.CancelEventHandler(this.txtNamespace_Validating);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtAssemblyKeyFile
            // 
            resources.ApplyResources(this.txtAssemblyKeyFile, "txtAssemblyKeyFile");
            this.txtAssemblyKeyFile.Name = "txtAssemblyKeyFile";
            this.txtAssemblyKeyFile.TextChanged += new System.EventHandler(this.Element_Changed);
            this.txtAssemblyKeyFile.Validating += new System.ComponentModel.CancelEventHandler(this.txtAssemblyKeyFile_Validating);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // cmdSelectKey
            // 
            resources.ApplyResources(this.cmdSelectKey, "cmdSelectKey");
            this.cmdSelectKey.Name = "cmdSelectKey";
            this.cmdSelectKey.Click += new System.EventHandler(this.cmdSelectKey_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "snk";
            resources.ApplyResources(this.openFileDialog, "openFileDialog");
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "snk";
            resources.ApplyResources(this.saveFileDialog, "saveFileDialog");
            // 
            // cmdNewKey
            // 
            resources.ApplyResources(this.cmdNewKey, "cmdNewKey");
            this.cmdNewKey.Name = "cmdNewKey";
            this.cmdNewKey.Click += new System.EventHandler(this.cmdNewKey_Click);
            // 
            // WzPageGeneralSetup
            // 
            this.Controls.Add(this.txtAssemblyKeyFile);
            this.Controls.Add(this.cmdNewKey);
            this.Controls.Add(this.cmdSelectKey);
            this.Controls.Add(this.txtNamespace);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtClassname);
            this.Controls.Add(this.label2);
            this.Name = "WzPageGeneralSetup";
            resources.ApplyResources(this, "$this");
            this.SubTitle = "Specify General Project Properties";
            this.Title = "General Project Properties";
            this.Leave += new System.EventHandler(this.WzPageGeneralSetup_Leave);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtClassname, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.txtNamespace, 0);
            this.Controls.SetChildIndex(this.cmdSelectKey, 0);
            this.Controls.SetChildIndex(this.cmdNewKey, 0);
            this.Controls.SetChildIndex(this.txtAssemblyKeyFile, 0);
            this.Controls.SetChildIndex(this.panelHeader, 0);
            this.panelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

    }
}

