using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.BizTalk.BaseFunctoids;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace BizTalk.MapperExtensions.Functoid.Wizard
{
	public class WzPageResourceSetup2 : Microsoft.BizTalk.Wizard.WizardInteriorPage, WizardControlInterface
	{
		#region Global Variables
		public event AddWizardResultEvent _AddWizardResultEvent;
		private bool _IsLoaded = false;
		#endregion

		#region Form Components
        private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label5;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.PictureBox FunctoidIcon;
		private System.Windows.Forms.ComboBox cmbCategory;
		private System.Windows.Forms.GroupBox grpLanguage;
		private System.Windows.Forms.RadioButton optCS;
		private System.Windows.Forms.RadioButton optVB;
		private System.Windows.Forms.ErrorProvider errorProvider;
		#endregion

		public WzPageResourceSetup2()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		public bool NextButtonEnabled
		{
			get {	return true;	}
		}

		public bool NeedSummary
		{
			get {	return false;	}
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
				_AddWizardResultEvent(this,e);
			}
		}

		private void WzPageResourceSetup2_Leave(object sender, System.EventArgs e)
		{
			try
			{
				// Save wizard results
				AddWizardResult(WizardValues.FunctoidBitmap, FunctoidIcon.Image);
                AddWizardResult(WizardValues.FunctoidExceptionText, "");
				AddWizardResult(WizardValues.CodeLanguage, (optCS.Checked ? "CSharp" : "VB"));
                AddWizardResult(WizardValues.FunctoidCategory, cmbCategory.SelectedItem.ToString());
			}
			catch(Exception err)
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
			foreach(Control ctl in this.Controls)
			{
				errorProvider.SetError(ctl, "");
			}
		}

		private bool GetAllStates()
		{
			return true;
		}

		
		private void WzPageResourceSetup2_Load(object sender, System.EventArgs e)
		{
			string[] strFunctoidCategories= Enum.GetNames(typeof(FunctoidCategory));

			try
			{
				if (_IsLoaded)
					return;
				foreach(string strFunctoidCategory in strFunctoidCategories)
				{
					cmbCategory.Items.Add(strFunctoidCategory);
				}

                cmbCategory.SelectedIndex = cmbCategory.FindStringExact("String");
                AddWizardResult(WizardValues.FunctoidCategory, cmbCategory.SelectedItem.ToString());

				_IsLoaded = true;
			}
			catch(Exception err)
			{
				MessageBox.Show(err.Message);
				Trace.WriteLine(err.Message + Environment.NewLine + err.StackTrace);
			}
		}		
		private void FunctoidIcon_DoubleClick(object sender, System.EventArgs e)
		{
			if(openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) 
			{
				FunctoidIcon.Image = Image.FromFile(openFileDialog.FileName);
                AddWizardResult(WizardValues.FunctoidBitmap, FunctoidIcon.Image);
			}
		}

		private void FunctoidIcon_Click(object sender, System.EventArgs e)
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(WzPageResourceSetup2));
			this.FunctoidIcon.Image = ((System.Drawing.Image)(resources.GetObject("FunctoidIcon.Image")));
            AddWizardResult(WizardValues.FunctoidBitmap, FunctoidIcon.Image);
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WzPageResourceSetup2));
            this.label2 = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.FunctoidIcon = new System.Windows.Forms.PictureBox();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.grpLanguage = new System.Windows.Forms.GroupBox();
            this.optVB = new System.Windows.Forms.RadioButton();
            this.optCS = new System.Windows.Forms.RadioButton();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FunctoidIcon)).BeginInit();
            this.grpLanguage.SuspendLayout();
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
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // FunctoidIcon
            // 
            resources.ApplyResources(this.FunctoidIcon, "FunctoidIcon");
            this.FunctoidIcon.Name = "FunctoidIcon";
            this.FunctoidIcon.TabStop = false;
            this.FunctoidIcon.Click += new System.EventHandler(this.FunctoidIcon_Click);
            this.FunctoidIcon.DoubleClick += new System.EventHandler(this.FunctoidIcon_DoubleClick);
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbCategory, "cmbCategory");
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Sorted = true;
            // 
            // grpLanguage
            // 
            this.grpLanguage.Controls.Add(this.optVB);
            this.grpLanguage.Controls.Add(this.optCS);
            resources.ApplyResources(this.grpLanguage, "grpLanguage");
            this.grpLanguage.Name = "grpLanguage";
            this.grpLanguage.TabStop = false;
            // 
            // optVB
            // 
            resources.ApplyResources(this.optVB, "optVB");
            this.optVB.Name = "optVB";
            // 
            // optCS
            // 
            this.optCS.Checked = true;
            resources.ApplyResources(this.optCS, "optCS");
            this.optCS.Name = "optCS";
            this.optCS.TabStop = true;
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "bmp";
            resources.ApplyResources(this.openFileDialog, "openFileDialog");
            this.openFileDialog.RestoreDirectory = true;
            // 
            // WzPageResourceSetup2
            // 
            this.Controls.Add(this.grpLanguage);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.FunctoidIcon);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Name = "WzPageResourceSetup2";
            resources.ApplyResources(this, "$this");
            this.SubTitle = "Specify Functoid Properties";
            this.Title = "Functoid Properties";
            this.Load += new System.EventHandler(this.WzPageResourceSetup2_Load);
            this.Leave += new System.EventHandler(this.WzPageResourceSetup2_Leave);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.panelHeader, 0);
            this.Controls.SetChildIndex(this.FunctoidIcon, 0);
            this.Controls.SetChildIndex(this.cmbCategory, 0);
            this.Controls.SetChildIndex(this.grpLanguage, 0);
            this.panelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FunctoidIcon)).EndInit();
            this.grpLanguage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

	}
}

