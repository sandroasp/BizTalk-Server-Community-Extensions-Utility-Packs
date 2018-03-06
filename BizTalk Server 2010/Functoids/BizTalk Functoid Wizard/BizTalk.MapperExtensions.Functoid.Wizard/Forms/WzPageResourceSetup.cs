using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace BizTalk.MapperExtensions.Functoid.Wizard
{
	public class WzPageResourceSetup : Microsoft.BizTalk.Wizard.WizardInteriorPage, WizardControlInterface
	{
		#region Global Variables
		private const string IdRegEx= @"^0*([7-9]\d{3,}|[1-6]\d{4,}|600[1-9]|60[1-9]\d|6[1-9]\d{2})$";
		public event AddWizardResultEvent _AddWizardResultEvent;
		#endregion

		#region Form Components
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtId;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.TextBox txtTooltip;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.ErrorProvider errorProvider;
		#endregion

		public WzPageResourceSetup()
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
			get {	return GetAllStates();	}
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

		private void WzPageResourceSetup_Leave(object sender, System.EventArgs e)
		{
			try
			{
				// Save wizard results
				AddWizardResult(WizardValues.FunctoidID, txtId.Text);
				AddWizardResult(WizardValues.FunctoidName, txtName.Text);
				AddWizardResult(WizardValues.FunctoidToolTip, txtTooltip.Text);
				AddWizardResult(WizardValues.FunctoidDescription, txtDescription.Text);
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

		private void Element_Changed(object sender, System.EventArgs e)
		{
			EnableNext(GetAllStates());
		}

		private bool GetAllStates()
		{
			return (Regex.IsMatch(txtId.Text, IdRegEx) && 
				txtName.Text.Trim().Length > 0);
		}

		private void txtId_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			// Validate Id
			if (txtId.Text.Trim() == "")
				errorProvider.SetError(txtId, "Id cannot be empty");
			else if (!Regex.IsMatch(txtId.Text,IdRegEx))
				errorProvider.SetError(txtId, "Id must be a number > 6000");
			else
				errorProvider.SetError(txtId, "");		
			
			// Enable Next if everything's OK
			EnableNext(GetAllStates());		
		}

		private void txtName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			// Validate Name
			if (txtName.Text.Trim() == "")
				errorProvider.SetError(txtName, "Name cannot be empty");
			else
				errorProvider.SetError(txtName, "");		
			
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(WzPageResourceSetup));
			this.txtId = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.errorProvider = new System.Windows.Forms.ErrorProvider();
			this.txtName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtTooltip = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.panelHeader.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelHeader
			// 
			this.panelHeader.Name = "panelHeader";
			this.panelHeader.Size = ((System.Drawing.Size)(resources.GetObject("panelHeader.Size")));
			// 
			// labelTitle
			// 
			this.labelTitle.Name = "labelTitle";
			this.labelTitle.Size = ((System.Drawing.Size)(resources.GetObject("labelTitle.Size")));
			this.labelTitle.Text = resources.GetString("labelTitle.Text");
			// 
			// labelSubTitle
			// 
			this.labelSubTitle.Name = "labelSubTitle";
			this.labelSubTitle.Size = ((System.Drawing.Size)(resources.GetObject("labelSubTitle.Size")));
			this.labelSubTitle.Text = resources.GetString("labelSubTitle.Text");
			// 
			// txtId
			// 
			this.txtId.AccessibleDescription = resources.GetString("txtId.AccessibleDescription");
			this.txtId.AccessibleName = resources.GetString("txtId.AccessibleName");
			this.txtId.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtId.Anchor")));
			this.txtId.AutoSize = ((bool)(resources.GetObject("txtId.AutoSize")));
			this.txtId.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtId.BackgroundImage")));
			this.txtId.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtId.Dock")));
			this.txtId.Enabled = ((bool)(resources.GetObject("txtId.Enabled")));
			this.errorProvider.SetError(this.txtId, resources.GetString("txtId.Error"));
			this.txtId.Font = ((System.Drawing.Font)(resources.GetObject("txtId.Font")));
			this.errorProvider.SetIconAlignment(this.txtId, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtId.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.txtId, ((int)(resources.GetObject("txtId.IconPadding"))));
			this.txtId.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtId.ImeMode")));
			this.txtId.Location = ((System.Drawing.Point)(resources.GetObject("txtId.Location")));
			this.txtId.MaxLength = ((int)(resources.GetObject("txtId.MaxLength")));
			this.txtId.Multiline = ((bool)(resources.GetObject("txtId.Multiline")));
			this.txtId.Name = "txtId";
			this.txtId.PasswordChar = ((char)(resources.GetObject("txtId.PasswordChar")));
			this.txtId.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtId.RightToLeft")));
			this.txtId.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtId.ScrollBars")));
			this.txtId.Size = ((System.Drawing.Size)(resources.GetObject("txtId.Size")));
			this.txtId.TabIndex = ((int)(resources.GetObject("txtId.TabIndex")));
			this.txtId.Text = resources.GetString("txtId.Text");
			this.txtId.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtId.TextAlign")));
			this.txtId.Visible = ((bool)(resources.GetObject("txtId.Visible")));
			this.txtId.WordWrap = ((bool)(resources.GetObject("txtId.WordWrap")));
			this.txtId.Validating += new System.ComponentModel.CancelEventHandler(this.txtId_Validating);
			this.txtId.TextChanged += new System.EventHandler(this.Element_Changed);
			// 
			// label2
			// 
			this.label2.AccessibleDescription = resources.GetString("label2.AccessibleDescription");
			this.label2.AccessibleName = resources.GetString("label2.AccessibleName");
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label2.Anchor")));
			this.label2.AutoSize = ((bool)(resources.GetObject("label2.AutoSize")));
			this.label2.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label2.Dock")));
			this.label2.Enabled = ((bool)(resources.GetObject("label2.Enabled")));
			this.errorProvider.SetError(this.label2, resources.GetString("label2.Error"));
			this.label2.Font = ((System.Drawing.Font)(resources.GetObject("label2.Font")));
			this.errorProvider.SetIconAlignment(this.label2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label2.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.label2, ((int)(resources.GetObject("label2.IconPadding"))));
			this.label2.Image = ((System.Drawing.Image)(resources.GetObject("label2.Image")));
			this.label2.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label2.ImageAlign")));
			this.label2.ImageIndex = ((int)(resources.GetObject("label2.ImageIndex")));
			this.label2.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label2.ImeMode")));
			this.label2.Location = ((System.Drawing.Point)(resources.GetObject("label2.Location")));
			this.label2.Name = "label2";
			this.label2.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label2.RightToLeft")));
			this.label2.Size = ((System.Drawing.Size)(resources.GetObject("label2.Size")));
			this.label2.TabIndex = ((int)(resources.GetObject("label2.TabIndex")));
			this.label2.Text = resources.GetString("label2.Text");
			this.label2.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label2.TextAlign")));
			this.label2.Visible = ((bool)(resources.GetObject("label2.Visible")));
			// 
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			this.errorProvider.Icon = ((System.Drawing.Icon)(resources.GetObject("errorProvider.Icon")));
			// 
			// txtName
			// 
			this.txtName.AccessibleDescription = resources.GetString("txtName.AccessibleDescription");
			this.txtName.AccessibleName = resources.GetString("txtName.AccessibleName");
			this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtName.Anchor")));
			this.txtName.AutoSize = ((bool)(resources.GetObject("txtName.AutoSize")));
			this.txtName.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtName.BackgroundImage")));
			this.txtName.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtName.Dock")));
			this.txtName.Enabled = ((bool)(resources.GetObject("txtName.Enabled")));
			this.errorProvider.SetError(this.txtName, resources.GetString("txtName.Error"));
			this.txtName.Font = ((System.Drawing.Font)(resources.GetObject("txtName.Font")));
			this.errorProvider.SetIconAlignment(this.txtName, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtName.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.txtName, ((int)(resources.GetObject("txtName.IconPadding"))));
			this.txtName.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtName.ImeMode")));
			this.txtName.Location = ((System.Drawing.Point)(resources.GetObject("txtName.Location")));
			this.txtName.MaxLength = ((int)(resources.GetObject("txtName.MaxLength")));
			this.txtName.Multiline = ((bool)(resources.GetObject("txtName.Multiline")));
			this.txtName.Name = "txtName";
			this.txtName.PasswordChar = ((char)(resources.GetObject("txtName.PasswordChar")));
			this.txtName.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtName.RightToLeft")));
			this.txtName.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtName.ScrollBars")));
			this.txtName.Size = ((System.Drawing.Size)(resources.GetObject("txtName.Size")));
			this.txtName.TabIndex = ((int)(resources.GetObject("txtName.TabIndex")));
			this.txtName.Text = resources.GetString("txtName.Text");
			this.txtName.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtName.TextAlign")));
			this.txtName.Visible = ((bool)(resources.GetObject("txtName.Visible")));
			this.txtName.WordWrap = ((bool)(resources.GetObject("txtName.WordWrap")));
			this.txtName.Validating += new System.ComponentModel.CancelEventHandler(this.txtName_Validating);
			this.txtName.TextChanged += new System.EventHandler(this.Element_Changed);
			// 
			// label1
			// 
			this.label1.AccessibleDescription = resources.GetString("label1.AccessibleDescription");
			this.label1.AccessibleName = resources.GetString("label1.AccessibleName");
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label1.Anchor")));
			this.label1.AutoSize = ((bool)(resources.GetObject("label1.AutoSize")));
			this.label1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label1.Dock")));
			this.label1.Enabled = ((bool)(resources.GetObject("label1.Enabled")));
			this.errorProvider.SetError(this.label1, resources.GetString("label1.Error"));
			this.label1.Font = ((System.Drawing.Font)(resources.GetObject("label1.Font")));
			this.errorProvider.SetIconAlignment(this.label1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label1.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.label1, ((int)(resources.GetObject("label1.IconPadding"))));
			this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
			this.label1.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label1.ImageAlign")));
			this.label1.ImageIndex = ((int)(resources.GetObject("label1.ImageIndex")));
			this.label1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label1.ImeMode")));
			this.label1.Location = ((System.Drawing.Point)(resources.GetObject("label1.Location")));
			this.label1.Name = "label1";
			this.label1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label1.RightToLeft")));
			this.label1.Size = ((System.Drawing.Size)(resources.GetObject("label1.Size")));
			this.label1.TabIndex = ((int)(resources.GetObject("label1.TabIndex")));
			this.label1.Text = resources.GetString("label1.Text");
			this.label1.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label1.TextAlign")));
			this.label1.Visible = ((bool)(resources.GetObject("label1.Visible")));
			// 
			// label3
			// 
			this.label3.AccessibleDescription = resources.GetString("label3.AccessibleDescription");
			this.label3.AccessibleName = resources.GetString("label3.AccessibleName");
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label3.Anchor")));
			this.label3.AutoSize = ((bool)(resources.GetObject("label3.AutoSize")));
			this.label3.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label3.Dock")));
			this.label3.Enabled = ((bool)(resources.GetObject("label3.Enabled")));
			this.errorProvider.SetError(this.label3, resources.GetString("label3.Error"));
			this.label3.Font = ((System.Drawing.Font)(resources.GetObject("label3.Font")));
			this.errorProvider.SetIconAlignment(this.label3, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label3.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.label3, ((int)(resources.GetObject("label3.IconPadding"))));
			this.label3.Image = ((System.Drawing.Image)(resources.GetObject("label3.Image")));
			this.label3.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label3.ImageAlign")));
			this.label3.ImageIndex = ((int)(resources.GetObject("label3.ImageIndex")));
			this.label3.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label3.ImeMode")));
			this.label3.Location = ((System.Drawing.Point)(resources.GetObject("label3.Location")));
			this.label3.Name = "label3";
			this.label3.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label3.RightToLeft")));
			this.label3.Size = ((System.Drawing.Size)(resources.GetObject("label3.Size")));
			this.label3.TabIndex = ((int)(resources.GetObject("label3.TabIndex")));
			this.label3.Text = resources.GetString("label3.Text");
			this.label3.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label3.TextAlign")));
			this.label3.Visible = ((bool)(resources.GetObject("label3.Visible")));
			// 
			// txtTooltip
			// 
			this.txtTooltip.AccessibleDescription = resources.GetString("txtTooltip.AccessibleDescription");
			this.txtTooltip.AccessibleName = resources.GetString("txtTooltip.AccessibleName");
			this.txtTooltip.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtTooltip.Anchor")));
			this.txtTooltip.AutoSize = ((bool)(resources.GetObject("txtTooltip.AutoSize")));
			this.txtTooltip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtTooltip.BackgroundImage")));
			this.txtTooltip.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtTooltip.Dock")));
			this.txtTooltip.Enabled = ((bool)(resources.GetObject("txtTooltip.Enabled")));
			this.errorProvider.SetError(this.txtTooltip, resources.GetString("txtTooltip.Error"));
			this.txtTooltip.Font = ((System.Drawing.Font)(resources.GetObject("txtTooltip.Font")));
			this.errorProvider.SetIconAlignment(this.txtTooltip, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtTooltip.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.txtTooltip, ((int)(resources.GetObject("txtTooltip.IconPadding"))));
			this.txtTooltip.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtTooltip.ImeMode")));
			this.txtTooltip.Location = ((System.Drawing.Point)(resources.GetObject("txtTooltip.Location")));
			this.txtTooltip.MaxLength = ((int)(resources.GetObject("txtTooltip.MaxLength")));
			this.txtTooltip.Multiline = ((bool)(resources.GetObject("txtTooltip.Multiline")));
			this.txtTooltip.Name = "txtTooltip";
			this.txtTooltip.PasswordChar = ((char)(resources.GetObject("txtTooltip.PasswordChar")));
			this.txtTooltip.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtTooltip.RightToLeft")));
			this.txtTooltip.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtTooltip.ScrollBars")));
			this.txtTooltip.Size = ((System.Drawing.Size)(resources.GetObject("txtTooltip.Size")));
			this.txtTooltip.TabIndex = ((int)(resources.GetObject("txtTooltip.TabIndex")));
			this.txtTooltip.Text = resources.GetString("txtTooltip.Text");
			this.txtTooltip.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtTooltip.TextAlign")));
			this.txtTooltip.Visible = ((bool)(resources.GetObject("txtTooltip.Visible")));
			this.txtTooltip.WordWrap = ((bool)(resources.GetObject("txtTooltip.WordWrap")));
			// 
			// label5
			// 
			this.label5.AccessibleDescription = resources.GetString("label5.AccessibleDescription");
			this.label5.AccessibleName = resources.GetString("label5.AccessibleName");
			this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label5.Anchor")));
			this.label5.AutoSize = ((bool)(resources.GetObject("label5.AutoSize")));
			this.label5.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label5.Dock")));
			this.label5.Enabled = ((bool)(resources.GetObject("label5.Enabled")));
			this.errorProvider.SetError(this.label5, resources.GetString("label5.Error"));
			this.label5.Font = ((System.Drawing.Font)(resources.GetObject("label5.Font")));
			this.errorProvider.SetIconAlignment(this.label5, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label5.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.label5, ((int)(resources.GetObject("label5.IconPadding"))));
			this.label5.Image = ((System.Drawing.Image)(resources.GetObject("label5.Image")));
			this.label5.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label5.ImageAlign")));
			this.label5.ImageIndex = ((int)(resources.GetObject("label5.ImageIndex")));
			this.label5.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label5.ImeMode")));
			this.label5.Location = ((System.Drawing.Point)(resources.GetObject("label5.Location")));
			this.label5.Name = "label5";
			this.label5.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label5.RightToLeft")));
			this.label5.Size = ((System.Drawing.Size)(resources.GetObject("label5.Size")));
			this.label5.TabIndex = ((int)(resources.GetObject("label5.TabIndex")));
			this.label5.Text = resources.GetString("label5.Text");
			this.label5.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label5.TextAlign")));
			this.label5.Visible = ((bool)(resources.GetObject("label5.Visible")));
			// 
			// txtDescription
			// 
			this.txtDescription.AccessibleDescription = resources.GetString("txtDescription.AccessibleDescription");
			this.txtDescription.AccessibleName = resources.GetString("txtDescription.AccessibleName");
			this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtDescription.Anchor")));
			this.txtDescription.AutoSize = ((bool)(resources.GetObject("txtDescription.AutoSize")));
			this.txtDescription.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtDescription.BackgroundImage")));
			this.txtDescription.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtDescription.Dock")));
			this.txtDescription.Enabled = ((bool)(resources.GetObject("txtDescription.Enabled")));
			this.errorProvider.SetError(this.txtDescription, resources.GetString("txtDescription.Error"));
			this.txtDescription.Font = ((System.Drawing.Font)(resources.GetObject("txtDescription.Font")));
			this.errorProvider.SetIconAlignment(this.txtDescription, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtDescription.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.txtDescription, ((int)(resources.GetObject("txtDescription.IconPadding"))));
			this.txtDescription.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtDescription.ImeMode")));
			this.txtDescription.Location = ((System.Drawing.Point)(resources.GetObject("txtDescription.Location")));
			this.txtDescription.MaxLength = ((int)(resources.GetObject("txtDescription.MaxLength")));
			this.txtDescription.Multiline = ((bool)(resources.GetObject("txtDescription.Multiline")));
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.PasswordChar = ((char)(resources.GetObject("txtDescription.PasswordChar")));
			this.txtDescription.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtDescription.RightToLeft")));
			this.txtDescription.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtDescription.ScrollBars")));
			this.txtDescription.Size = ((System.Drawing.Size)(resources.GetObject("txtDescription.Size")));
			this.txtDescription.TabIndex = ((int)(resources.GetObject("txtDescription.TabIndex")));
			this.txtDescription.Text = resources.GetString("txtDescription.Text");
			this.txtDescription.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtDescription.TextAlign")));
			this.txtDescription.Visible = ((bool)(resources.GetObject("txtDescription.Visible")));
			this.txtDescription.WordWrap = ((bool)(resources.GetObject("txtDescription.WordWrap")));
			// 
			// openFileDialog
			// 
			this.openFileDialog.DefaultExt = "snk";
			this.openFileDialog.Filter = resources.GetString("openFileDialog.Filter");
			this.openFileDialog.Title = resources.GetString("openFileDialog.Title");
			// 
			// WzPageResourceSetup
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.Controls.Add(this.txtTooltip);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtDescription);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtId);
			this.Controls.Add(this.label2);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.errorProvider.SetError(this, resources.GetString("$this.Error"));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.errorProvider.SetIconAlignment(this, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("$this.IconAlignment"))));
			this.errorProvider.SetIconPadding(this, ((int)(resources.GetObject("$this.IconPadding"))));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.Name = "WzPageResourceSetup";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.Size = ((System.Drawing.Size)(resources.GetObject("$this.Size")));
			this.SubTitle = "Specify Functoid Properties";
			this.Title = "Functoid Properties";
			this.Leave += new System.EventHandler(this.WzPageResourceSetup_Leave);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.txtId, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.txtName, 0);
			this.Controls.SetChildIndex(this.label3, 0);
			this.Controls.SetChildIndex(this.txtDescription, 0);
			this.Controls.SetChildIndex(this.label5, 0);
			this.Controls.SetChildIndex(this.txtTooltip, 0);
			this.Controls.SetChildIndex(this.panelHeader, 0);
			this.panelHeader.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

	}
}

