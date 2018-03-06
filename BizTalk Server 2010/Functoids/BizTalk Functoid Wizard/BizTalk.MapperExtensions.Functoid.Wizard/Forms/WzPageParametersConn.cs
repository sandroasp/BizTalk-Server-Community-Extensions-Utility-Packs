using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.BizTalk.BaseFunctoids; 
using System.Text.RegularExpressions;

namespace BizTalk.MapperExtensions.Functoid.Wizard
{
	public class WzPageParametersConn : Microsoft.BizTalk.Wizard.WizardInteriorPage, WizardControlInterface
	{
		#region Global Variables
		private const string ParamsRegEx = @"^[0-9]+$";
		public event AddWizardResultEvent _AddWizardResultEvent;
		public event AddFunctoidParameterEvent _AddFunctoidParameterEvent; 
		private bool _IsLoaded = false;
		#endregion

		#region Form Components
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtFunctoidParameter;
		private System.Windows.Forms.Button cmdFunctoidParameterDel;
		private System.Windows.Forms.Button cmdFunctoidParameterAdd;
		private System.Windows.Forms.ListBox lstFunctoidParameters;
		private System.Windows.Forms.ComboBox cmbParameterDataType;
		private System.Windows.Forms.ErrorProvider errorProvider;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtMinParams;
		private System.Windows.Forms.TextBox txtMaxParams;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox cmbInputConnType;
		private System.Windows.Forms.ComboBox cmbOutputConnType;
		private System.Windows.Forms.ComboBox cmbReturnDataType;
		private System.Windows.Forms.Button cmdMoveUp;
		private System.Windows.Forms.Button cmdMoveDown;
		private System.Windows.Forms.GroupBox grpParams;
		#endregion

		public WzPageParametersConn()
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

		protected void AddFunctoidParameter(string strName,string strValue)
		{
			PropertyPairEvent PropertyPair = new PropertyPairEvent(strName,strValue);
			OnAddFunctoidParameter(PropertyPair);
		}

		protected void RemoveFunctoidParameter(string strName)
		{
			PropertyPairEvent PropertyPair = new PropertyPairEvent(strName,null,true);
			OnAddFunctoidParameter(PropertyPair);
		}

		// The protected OnAddFunctoidParameter method raises the event by invoking 
		// the delegates. The sender is always this, the current instance 
		// of the class.
		protected virtual void OnAddFunctoidParameter(PropertyPairEvent e)
		{
			if (e != null) 
			{
				// Invokes the delegates. 
				_AddFunctoidParameterEvent(this,e);
			}
		}

		private void WzPageParametersConn_Leave(object sender, System.EventArgs e)
		{
			try
			{
				// Save functoid parameters
				int ParamCount = lstFunctoidParameters.Items.Count;
				for(int i=0; i < ParamCount; i++)
				{
					string strVal = lstFunctoidParameters.Items[i].ToString();
					string strPropName = strVal.Substring(0,strVal.IndexOf("(") - 1);
					string strPropType = strVal.Replace(strPropName + " (","");
					strPropType = strPropType.Replace(")","");
					// I remove every parameter so that they stay in the list order
					RemoveFunctoidParameter(strPropName);
					AddFunctoidParameter(strPropName,strPropType);
				}

				// Save other wizard results
				AddWizardResult(WizardValues.FunctoidInputConnType, cmbInputConnType.Text);
                AddWizardResult(WizardValues.FunctoidOutputConnType, cmbOutputConnType.Text);
                AddWizardResult(WizardValues.FunctoidReturnType, cmbReturnDataType.Text);
                AddWizardResult(WizardValues.FunctoidMinParams, txtMinParams.Text);
                AddWizardResult(WizardValues.FunctoidMaxParams, txtMaxParams.Text);
			}
			catch(Exception err)
			{
				MessageBox.Show(err.Message);
				Trace.WriteLine(err.Message + Environment.NewLine + err.StackTrace);
			}		
		}

		private void WzPageParametersConn_Load(object sender, System.EventArgs e)
		{
			string[] strDataTypes = {"System.Boolean",
				"System.Byte",
				"System.Char",
				"System.DateTime",
				"System.Decimal",
				"System.Double",
				"System.Int16",
				"System.Int32",
				"System.Int64",
				"System.Object",
				"System.Sbyte",
				"System.Single",
				"System.String",
				"System.UInt16",
				"System.UInt32",
				"System.UInt64",
			};
			string[] strConnectionTypes = Enum.GetNames(typeof(ConnectionType));

			try
			{
				if (_IsLoaded)
					return;
				
				foreach(string strDataType in strDataTypes)
				{
					cmbParameterDataType.Items.Add(strDataType);
					cmbReturnDataType.Items.Add(strDataType);
				}
				cmbParameterDataType.Text = "System.String";
				cmbReturnDataType.Text = "System.String";

				foreach(string strConnectionType in strConnectionTypes)
				{
					cmbInputConnType.Items.Add(strConnectionType);
					cmbOutputConnType.Items.Add(strConnectionType);
				}
				cmbInputConnType.Text = "AllExceptRecord";
				cmbOutputConnType.Text = "AllExceptRecord";

				_IsLoaded = true;
			}
			catch(Exception err)
			{
				MessageBox.Show(err.Message);
				Trace.WriteLine(err.Message + Environment.NewLine + err.StackTrace);
			}
		}


		private bool VarNameAlreadyExists(string strValue)
		{
			foreach(object o in lstFunctoidParameters.Items)
			{
				string strObjVal = o.ToString();
				strObjVal = strObjVal.Remove(strObjVal.IndexOf(" ("),strObjVal.Length - strObjVal.IndexOf(" ("));
				if (strObjVal == strValue)
					return true;
			}

			return false;
		}

		/// <summary>
		/// Resets all of the errorproviders when anything succeeds
		/// </summary>
		private void ResetAllErrProviders()
		{
			foreach(Control ctl in this.Controls)
			{
				if (errorProvider.GetError(ctl) != "")
					errorProvider.SetError(ctl, "");
			}
		}


		private void cmdFunctoidParameterAdd_Click(object sender, System.EventArgs e)
		{
			try
			{
				ResetAllErrProviders();
				if (!Regex.IsMatch(txtFunctoidParameter.Text,@"^[_a-zA-Z][_a-zA-Z0-9]*$"))
				{
					errorProvider.SetError(txtFunctoidParameter,
						"Please enter a valid name for the new parameter");
					return;
				}
				if (VarNameAlreadyExists(txtFunctoidParameter.Text))
				{
					errorProvider.SetError(txtFunctoidParameter,
						"Please enter a unique name. Two parameters cannot have the same name");
					return;
				}
				lstFunctoidParameters.Items.Add(txtFunctoidParameter.Text + " (" + cmbParameterDataType.Text + ")");
				txtFunctoidParameter.Clear();
				cmbParameterDataType.Text = "System.String";
				ProcessMinMaxParams();
				txtFunctoidParameter.Focus();
			}
			catch(Exception err)
			{
				MessageBox.Show(err.Message);
				Trace.WriteLine(err.Message + Environment.NewLine + err.StackTrace);
			}
		}

		private void cmdFunctoidParameterDel_Click(object sender, System.EventArgs e)
		{
			try
			{
				ResetAllErrProviders();
				if (lstFunctoidParameters.SelectedItem == null)
				{
					errorProvider.SetError(cmdFunctoidParameterDel,
						"Please select a value in the parameter list");
					return;
				}

				int selectedPos = lstFunctoidParameters.SelectedIndex;
				Object objItem = lstFunctoidParameters.SelectedItem;
				string strVal = objItem.ToString();
				string strPropName = strVal.Substring(0,strVal.IndexOf("(") - 1);
				RemoveFunctoidParameter(strPropName);
				lstFunctoidParameters.Items.Remove(lstFunctoidParameters.SelectedItem);
				ProcessMinMaxParams();

				// Selects the next item in the list
				if (lstFunctoidParameters.Items.Count > 0)
				{
					if (lstFunctoidParameters.Items.Count <= selectedPos)
						selectedPos = lstFunctoidParameters.Items.Count - 1;
					
					lstFunctoidParameters.SelectedIndex = selectedPos;
				}
			}
			catch(Exception err)
			{
				MessageBox.Show(err.Message);
				Trace.WriteLine(err.Message + Environment.NewLine + err.StackTrace);
			}	
		}

		private void ProcessMinMaxParams()
		{
			txtMinParams.Text = lstFunctoidParameters.Items.Count.ToString();
			txtMaxParams.Text = lstFunctoidParameters.Items.Count.ToString();

			Params_Validating(grpParams,new System.ComponentModel.CancelEventArgs(false));
		}

		private void Element_Changed(object sender, System.EventArgs e)
		{
			EnableNext(GetAllStates());
		}

		private bool GetAllStates()
		{
			return (Regex.IsMatch(txtMinParams.Text, ParamsRegEx) &&
				Regex.IsMatch(txtMaxParams.Text, ParamsRegEx) &&
				Convert.ToInt32(txtMaxParams.Text) >= Convert.ToInt32(txtMinParams.Text));
		}

		private void Params_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			// Validate Min. Params
			if (!Regex.IsMatch(txtMinParams.Text,ParamsRegEx))
				errorProvider.SetError(txtMinParams, "Min parameters must be a number and can't be empty");
			else
				errorProvider.SetError(txtMinParams,"");

			// Validate Max. Params
			if (!Regex.IsMatch(txtMaxParams.Text,ParamsRegEx))
				errorProvider.SetError(txtMaxParams, "Max parameters must be a number and can't be empty");
			else
				errorProvider.SetError(txtMaxParams,"");

			// Validate Min. Params <= Max. Params
			if (errorProvider.GetError(txtMinParams) == "" && errorProvider.GetError(txtMaxParams) == "")
			{
				if (Convert.ToInt32(txtMinParams.Text) > Convert.ToInt32(txtMaxParams.Text))
					errorProvider.SetError(grpParams, "Max parameters can't be lower than Min parameters");
				else
					errorProvider.SetError(grpParams,"");
			}
			else
				errorProvider.SetError(grpParams,"");
		
			// Enable Next if everything's OK
			EnableNext(GetAllStates());
		}

		private void cmdMoveUp_Click(object sender, System.EventArgs e)
		{
			ResetAllErrProviders();
			if (lstFunctoidParameters.SelectedItem == null)
			{
				errorProvider.SetError(lstFunctoidParameters,
					"Please select a value in the parameter list");
				return;
			}

			if (lstFunctoidParameters.SelectedIndex > 0)
			{
				int selectedPos = lstFunctoidParameters.SelectedIndex;
				string auxParameter = (string)lstFunctoidParameters.Items[selectedPos-1];
				lstFunctoidParameters.Items[selectedPos-1] = lstFunctoidParameters.Items[selectedPos];
				lstFunctoidParameters.Items[selectedPos] = auxParameter;
				lstFunctoidParameters.SelectedIndex = selectedPos-1;
			}
		}

		private void cmdMoveDown_Click(object sender, System.EventArgs e)
		{
			ResetAllErrProviders();
			if (lstFunctoidParameters.SelectedItem == null)
			{
				errorProvider.SetError(lstFunctoidParameters,
					"Please select a value in the parameter list");
				return;
			}

			if (lstFunctoidParameters.SelectedIndex < lstFunctoidParameters.Items.Count-1)
			{
				int selectedPos = lstFunctoidParameters.SelectedIndex;
				string auxParameter = (string)lstFunctoidParameters.Items[selectedPos+1];
				lstFunctoidParameters.Items[selectedPos+1] = lstFunctoidParameters.Items[selectedPos];
				lstFunctoidParameters.Items[selectedPos] = auxParameter;
				lstFunctoidParameters.SelectedIndex = selectedPos+1;
			}
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(WzPageParametersConn));
			this.txtFunctoidParameter = new System.Windows.Forms.TextBox();
			this.cmdFunctoidParameterDel = new System.Windows.Forms.Button();
			this.cmdFunctoidParameterAdd = new System.Windows.Forms.Button();
			this.cmbParameterDataType = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.lstFunctoidParameters = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.errorProvider = new System.Windows.Forms.ErrorProvider();
			this.txtMinParams = new System.Windows.Forms.TextBox();
			this.txtMaxParams = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.cmbInputConnType = new System.Windows.Forms.ComboBox();
			this.cmbOutputConnType = new System.Windows.Forms.ComboBox();
			this.cmbReturnDataType = new System.Windows.Forms.ComboBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.grpParams = new System.Windows.Forms.GroupBox();
			this.cmdMoveUp = new System.Windows.Forms.Button();
			this.cmdMoveDown = new System.Windows.Forms.Button();
			this.panelHeader.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.grpParams.SuspendLayout();
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
			// txtFunctoidParameter
			// 
			this.txtFunctoidParameter.AccessibleDescription = resources.GetString("txtFunctoidParameter.AccessibleDescription");
			this.txtFunctoidParameter.AccessibleName = resources.GetString("txtFunctoidParameter.AccessibleName");
			this.txtFunctoidParameter.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtFunctoidParameter.Anchor")));
			this.txtFunctoidParameter.AutoSize = ((bool)(resources.GetObject("txtFunctoidParameter.AutoSize")));
			this.txtFunctoidParameter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtFunctoidParameter.BackgroundImage")));
			this.txtFunctoidParameter.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtFunctoidParameter.Dock")));
			this.txtFunctoidParameter.Enabled = ((bool)(resources.GetObject("txtFunctoidParameter.Enabled")));
			this.errorProvider.SetError(this.txtFunctoidParameter, resources.GetString("txtFunctoidParameter.Error"));
			this.txtFunctoidParameter.Font = ((System.Drawing.Font)(resources.GetObject("txtFunctoidParameter.Font")));
			this.errorProvider.SetIconAlignment(this.txtFunctoidParameter, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtFunctoidParameter.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.txtFunctoidParameter, ((int)(resources.GetObject("txtFunctoidParameter.IconPadding"))));
			this.txtFunctoidParameter.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtFunctoidParameter.ImeMode")));
			this.txtFunctoidParameter.Location = ((System.Drawing.Point)(resources.GetObject("txtFunctoidParameter.Location")));
			this.txtFunctoidParameter.MaxLength = ((int)(resources.GetObject("txtFunctoidParameter.MaxLength")));
			this.txtFunctoidParameter.Multiline = ((bool)(resources.GetObject("txtFunctoidParameter.Multiline")));
			this.txtFunctoidParameter.Name = "txtFunctoidParameter";
			this.txtFunctoidParameter.PasswordChar = ((char)(resources.GetObject("txtFunctoidParameter.PasswordChar")));
			this.txtFunctoidParameter.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtFunctoidParameter.RightToLeft")));
			this.txtFunctoidParameter.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtFunctoidParameter.ScrollBars")));
			this.txtFunctoidParameter.Size = ((System.Drawing.Size)(resources.GetObject("txtFunctoidParameter.Size")));
			this.txtFunctoidParameter.TabIndex = ((int)(resources.GetObject("txtFunctoidParameter.TabIndex")));
			this.txtFunctoidParameter.Text = resources.GetString("txtFunctoidParameter.Text");
			this.txtFunctoidParameter.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtFunctoidParameter.TextAlign")));
			this.txtFunctoidParameter.Visible = ((bool)(resources.GetObject("txtFunctoidParameter.Visible")));
			this.txtFunctoidParameter.WordWrap = ((bool)(resources.GetObject("txtFunctoidParameter.WordWrap")));
			// 
			// cmdFunctoidParameterDel
			// 
			this.cmdFunctoidParameterDel.AccessibleDescription = resources.GetString("cmdFunctoidParameterDel.AccessibleDescription");
			this.cmdFunctoidParameterDel.AccessibleName = resources.GetString("cmdFunctoidParameterDel.AccessibleName");
			this.cmdFunctoidParameterDel.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cmdFunctoidParameterDel.Anchor")));
			this.cmdFunctoidParameterDel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdFunctoidParameterDel.BackgroundImage")));
			this.cmdFunctoidParameterDel.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cmdFunctoidParameterDel.Dock")));
			this.cmdFunctoidParameterDel.Enabled = ((bool)(resources.GetObject("cmdFunctoidParameterDel.Enabled")));
			this.errorProvider.SetError(this.cmdFunctoidParameterDel, resources.GetString("cmdFunctoidParameterDel.Error"));
			this.cmdFunctoidParameterDel.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cmdFunctoidParameterDel.FlatStyle")));
			this.cmdFunctoidParameterDel.Font = ((System.Drawing.Font)(resources.GetObject("cmdFunctoidParameterDel.Font")));
			this.errorProvider.SetIconAlignment(this.cmdFunctoidParameterDel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cmdFunctoidParameterDel.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.cmdFunctoidParameterDel, ((int)(resources.GetObject("cmdFunctoidParameterDel.IconPadding"))));
			this.cmdFunctoidParameterDel.Image = ((System.Drawing.Image)(resources.GetObject("cmdFunctoidParameterDel.Image")));
			this.cmdFunctoidParameterDel.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cmdFunctoidParameterDel.ImageAlign")));
			this.cmdFunctoidParameterDel.ImageIndex = ((int)(resources.GetObject("cmdFunctoidParameterDel.ImageIndex")));
			this.cmdFunctoidParameterDel.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cmdFunctoidParameterDel.ImeMode")));
			this.cmdFunctoidParameterDel.Location = ((System.Drawing.Point)(resources.GetObject("cmdFunctoidParameterDel.Location")));
			this.cmdFunctoidParameterDel.Name = "cmdFunctoidParameterDel";
			this.cmdFunctoidParameterDel.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cmdFunctoidParameterDel.RightToLeft")));
			this.cmdFunctoidParameterDel.Size = ((System.Drawing.Size)(resources.GetObject("cmdFunctoidParameterDel.Size")));
			this.cmdFunctoidParameterDel.TabIndex = ((int)(resources.GetObject("cmdFunctoidParameterDel.TabIndex")));
			this.cmdFunctoidParameterDel.Text = resources.GetString("cmdFunctoidParameterDel.Text");
			this.cmdFunctoidParameterDel.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cmdFunctoidParameterDel.TextAlign")));
			this.cmdFunctoidParameterDel.Visible = ((bool)(resources.GetObject("cmdFunctoidParameterDel.Visible")));
			this.cmdFunctoidParameterDel.Click += new System.EventHandler(this.cmdFunctoidParameterDel_Click);
			// 
			// cmdFunctoidParameterAdd
			// 
			this.cmdFunctoidParameterAdd.AccessibleDescription = resources.GetString("cmdFunctoidParameterAdd.AccessibleDescription");
			this.cmdFunctoidParameterAdd.AccessibleName = resources.GetString("cmdFunctoidParameterAdd.AccessibleName");
			this.cmdFunctoidParameterAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cmdFunctoidParameterAdd.Anchor")));
			this.cmdFunctoidParameterAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdFunctoidParameterAdd.BackgroundImage")));
			this.cmdFunctoidParameterAdd.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cmdFunctoidParameterAdd.Dock")));
			this.cmdFunctoidParameterAdd.Enabled = ((bool)(resources.GetObject("cmdFunctoidParameterAdd.Enabled")));
			this.errorProvider.SetError(this.cmdFunctoidParameterAdd, resources.GetString("cmdFunctoidParameterAdd.Error"));
			this.cmdFunctoidParameterAdd.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cmdFunctoidParameterAdd.FlatStyle")));
			this.cmdFunctoidParameterAdd.Font = ((System.Drawing.Font)(resources.GetObject("cmdFunctoidParameterAdd.Font")));
			this.errorProvider.SetIconAlignment(this.cmdFunctoidParameterAdd, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cmdFunctoidParameterAdd.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.cmdFunctoidParameterAdd, ((int)(resources.GetObject("cmdFunctoidParameterAdd.IconPadding"))));
			this.cmdFunctoidParameterAdd.Image = ((System.Drawing.Image)(resources.GetObject("cmdFunctoidParameterAdd.Image")));
			this.cmdFunctoidParameterAdd.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cmdFunctoidParameterAdd.ImageAlign")));
			this.cmdFunctoidParameterAdd.ImageIndex = ((int)(resources.GetObject("cmdFunctoidParameterAdd.ImageIndex")));
			this.cmdFunctoidParameterAdd.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cmdFunctoidParameterAdd.ImeMode")));
			this.cmdFunctoidParameterAdd.Location = ((System.Drawing.Point)(resources.GetObject("cmdFunctoidParameterAdd.Location")));
			this.cmdFunctoidParameterAdd.Name = "cmdFunctoidParameterAdd";
			this.cmdFunctoidParameterAdd.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cmdFunctoidParameterAdd.RightToLeft")));
			this.cmdFunctoidParameterAdd.Size = ((System.Drawing.Size)(resources.GetObject("cmdFunctoidParameterAdd.Size")));
			this.cmdFunctoidParameterAdd.TabIndex = ((int)(resources.GetObject("cmdFunctoidParameterAdd.TabIndex")));
			this.cmdFunctoidParameterAdd.Text = resources.GetString("cmdFunctoidParameterAdd.Text");
			this.cmdFunctoidParameterAdd.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cmdFunctoidParameterAdd.TextAlign")));
			this.cmdFunctoidParameterAdd.Visible = ((bool)(resources.GetObject("cmdFunctoidParameterAdd.Visible")));
			this.cmdFunctoidParameterAdd.Click += new System.EventHandler(this.cmdFunctoidParameterAdd_Click);
			// 
			// cmbParameterDataType
			// 
			this.cmbParameterDataType.AccessibleDescription = resources.GetString("cmbParameterDataType.AccessibleDescription");
			this.cmbParameterDataType.AccessibleName = resources.GetString("cmbParameterDataType.AccessibleName");
			this.cmbParameterDataType.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cmbParameterDataType.Anchor")));
			this.cmbParameterDataType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmbParameterDataType.BackgroundImage")));
			this.cmbParameterDataType.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cmbParameterDataType.Dock")));
			this.cmbParameterDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbParameterDataType.Enabled = ((bool)(resources.GetObject("cmbParameterDataType.Enabled")));
			this.errorProvider.SetError(this.cmbParameterDataType, resources.GetString("cmbParameterDataType.Error"));
			this.cmbParameterDataType.Font = ((System.Drawing.Font)(resources.GetObject("cmbParameterDataType.Font")));
			this.errorProvider.SetIconAlignment(this.cmbParameterDataType, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cmbParameterDataType.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.cmbParameterDataType, ((int)(resources.GetObject("cmbParameterDataType.IconPadding"))));
			this.cmbParameterDataType.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cmbParameterDataType.ImeMode")));
			this.cmbParameterDataType.IntegralHeight = ((bool)(resources.GetObject("cmbParameterDataType.IntegralHeight")));
			this.cmbParameterDataType.ItemHeight = ((int)(resources.GetObject("cmbParameterDataType.ItemHeight")));
			this.cmbParameterDataType.Location = ((System.Drawing.Point)(resources.GetObject("cmbParameterDataType.Location")));
			this.cmbParameterDataType.MaxDropDownItems = ((int)(resources.GetObject("cmbParameterDataType.MaxDropDownItems")));
			this.cmbParameterDataType.MaxLength = ((int)(resources.GetObject("cmbParameterDataType.MaxLength")));
			this.cmbParameterDataType.Name = "cmbParameterDataType";
			this.cmbParameterDataType.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cmbParameterDataType.RightToLeft")));
			this.cmbParameterDataType.Size = ((System.Drawing.Size)(resources.GetObject("cmbParameterDataType.Size")));
			this.cmbParameterDataType.Sorted = true;
			this.cmbParameterDataType.TabIndex = ((int)(resources.GetObject("cmbParameterDataType.TabIndex")));
			this.cmbParameterDataType.Text = resources.GetString("cmbParameterDataType.Text");
			this.cmbParameterDataType.Visible = ((bool)(resources.GetObject("cmbParameterDataType.Visible")));
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
			// lstFunctoidParameters
			// 
			this.lstFunctoidParameters.AccessibleDescription = resources.GetString("lstFunctoidParameters.AccessibleDescription");
			this.lstFunctoidParameters.AccessibleName = resources.GetString("lstFunctoidParameters.AccessibleName");
			this.lstFunctoidParameters.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("lstFunctoidParameters.Anchor")));
			this.lstFunctoidParameters.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lstFunctoidParameters.BackgroundImage")));
			this.lstFunctoidParameters.ColumnWidth = ((int)(resources.GetObject("lstFunctoidParameters.ColumnWidth")));
			this.lstFunctoidParameters.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("lstFunctoidParameters.Dock")));
			this.lstFunctoidParameters.Enabled = ((bool)(resources.GetObject("lstFunctoidParameters.Enabled")));
			this.errorProvider.SetError(this.lstFunctoidParameters, resources.GetString("lstFunctoidParameters.Error"));
			this.lstFunctoidParameters.Font = ((System.Drawing.Font)(resources.GetObject("lstFunctoidParameters.Font")));
			this.lstFunctoidParameters.HorizontalExtent = ((int)(resources.GetObject("lstFunctoidParameters.HorizontalExtent")));
			this.lstFunctoidParameters.HorizontalScrollbar = ((bool)(resources.GetObject("lstFunctoidParameters.HorizontalScrollbar")));
			this.errorProvider.SetIconAlignment(this.lstFunctoidParameters, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("lstFunctoidParameters.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.lstFunctoidParameters, ((int)(resources.GetObject("lstFunctoidParameters.IconPadding"))));
			this.lstFunctoidParameters.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("lstFunctoidParameters.ImeMode")));
			this.lstFunctoidParameters.IntegralHeight = ((bool)(resources.GetObject("lstFunctoidParameters.IntegralHeight")));
			this.lstFunctoidParameters.ItemHeight = ((int)(resources.GetObject("lstFunctoidParameters.ItemHeight")));
			this.lstFunctoidParameters.Location = ((System.Drawing.Point)(resources.GetObject("lstFunctoidParameters.Location")));
			this.lstFunctoidParameters.Name = "lstFunctoidParameters";
			this.lstFunctoidParameters.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("lstFunctoidParameters.RightToLeft")));
			this.lstFunctoidParameters.ScrollAlwaysVisible = ((bool)(resources.GetObject("lstFunctoidParameters.ScrollAlwaysVisible")));
			this.lstFunctoidParameters.Size = ((System.Drawing.Size)(resources.GetObject("lstFunctoidParameters.Size")));
			this.lstFunctoidParameters.TabIndex = ((int)(resources.GetObject("lstFunctoidParameters.TabIndex")));
			this.lstFunctoidParameters.Visible = ((bool)(resources.GetObject("lstFunctoidParameters.Visible")));
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
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			this.errorProvider.Icon = ((System.Drawing.Icon)(resources.GetObject("errorProvider.Icon")));
			// 
			// txtMinParams
			// 
			this.txtMinParams.AccessibleDescription = resources.GetString("txtMinParams.AccessibleDescription");
			this.txtMinParams.AccessibleName = resources.GetString("txtMinParams.AccessibleName");
			this.txtMinParams.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtMinParams.Anchor")));
			this.txtMinParams.AutoSize = ((bool)(resources.GetObject("txtMinParams.AutoSize")));
			this.txtMinParams.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtMinParams.BackgroundImage")));
			this.txtMinParams.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtMinParams.Dock")));
			this.txtMinParams.Enabled = ((bool)(resources.GetObject("txtMinParams.Enabled")));
			this.errorProvider.SetError(this.txtMinParams, resources.GetString("txtMinParams.Error"));
			this.txtMinParams.Font = ((System.Drawing.Font)(resources.GetObject("txtMinParams.Font")));
			this.errorProvider.SetIconAlignment(this.txtMinParams, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtMinParams.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.txtMinParams, ((int)(resources.GetObject("txtMinParams.IconPadding"))));
			this.txtMinParams.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtMinParams.ImeMode")));
			this.txtMinParams.Location = ((System.Drawing.Point)(resources.GetObject("txtMinParams.Location")));
			this.txtMinParams.MaxLength = ((int)(resources.GetObject("txtMinParams.MaxLength")));
			this.txtMinParams.Multiline = ((bool)(resources.GetObject("txtMinParams.Multiline")));
			this.txtMinParams.Name = "txtMinParams";
			this.txtMinParams.PasswordChar = ((char)(resources.GetObject("txtMinParams.PasswordChar")));
			this.txtMinParams.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtMinParams.RightToLeft")));
			this.txtMinParams.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtMinParams.ScrollBars")));
			this.txtMinParams.Size = ((System.Drawing.Size)(resources.GetObject("txtMinParams.Size")));
			this.txtMinParams.TabIndex = ((int)(resources.GetObject("txtMinParams.TabIndex")));
			this.txtMinParams.Text = resources.GetString("txtMinParams.Text");
			this.txtMinParams.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtMinParams.TextAlign")));
			this.txtMinParams.Visible = ((bool)(resources.GetObject("txtMinParams.Visible")));
			this.txtMinParams.WordWrap = ((bool)(resources.GetObject("txtMinParams.WordWrap")));
			this.txtMinParams.Validating += new System.ComponentModel.CancelEventHandler(this.Params_Validating);
			this.txtMinParams.TextChanged += new System.EventHandler(this.Element_Changed);
			// 
			// txtMaxParams
			// 
			this.txtMaxParams.AccessibleDescription = resources.GetString("txtMaxParams.AccessibleDescription");
			this.txtMaxParams.AccessibleName = resources.GetString("txtMaxParams.AccessibleName");
			this.txtMaxParams.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("txtMaxParams.Anchor")));
			this.txtMaxParams.AutoSize = ((bool)(resources.GetObject("txtMaxParams.AutoSize")));
			this.txtMaxParams.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("txtMaxParams.BackgroundImage")));
			this.txtMaxParams.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("txtMaxParams.Dock")));
			this.txtMaxParams.Enabled = ((bool)(resources.GetObject("txtMaxParams.Enabled")));
			this.errorProvider.SetError(this.txtMaxParams, resources.GetString("txtMaxParams.Error"));
			this.txtMaxParams.Font = ((System.Drawing.Font)(resources.GetObject("txtMaxParams.Font")));
			this.errorProvider.SetIconAlignment(this.txtMaxParams, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtMaxParams.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.txtMaxParams, ((int)(resources.GetObject("txtMaxParams.IconPadding"))));
			this.txtMaxParams.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("txtMaxParams.ImeMode")));
			this.txtMaxParams.Location = ((System.Drawing.Point)(resources.GetObject("txtMaxParams.Location")));
			this.txtMaxParams.MaxLength = ((int)(resources.GetObject("txtMaxParams.MaxLength")));
			this.txtMaxParams.Multiline = ((bool)(resources.GetObject("txtMaxParams.Multiline")));
			this.txtMaxParams.Name = "txtMaxParams";
			this.txtMaxParams.PasswordChar = ((char)(resources.GetObject("txtMaxParams.PasswordChar")));
			this.txtMaxParams.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("txtMaxParams.RightToLeft")));
			this.txtMaxParams.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("txtMaxParams.ScrollBars")));
			this.txtMaxParams.Size = ((System.Drawing.Size)(resources.GetObject("txtMaxParams.Size")));
			this.txtMaxParams.TabIndex = ((int)(resources.GetObject("txtMaxParams.TabIndex")));
			this.txtMaxParams.Text = resources.GetString("txtMaxParams.Text");
			this.txtMaxParams.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("txtMaxParams.TextAlign")));
			this.txtMaxParams.Visible = ((bool)(resources.GetObject("txtMaxParams.Visible")));
			this.txtMaxParams.WordWrap = ((bool)(resources.GetObject("txtMaxParams.WordWrap")));
			this.txtMaxParams.Validating += new System.ComponentModel.CancelEventHandler(this.Params_Validating);
			this.txtMaxParams.TextChanged += new System.EventHandler(this.Element_Changed);
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
			// label4
			// 
			this.label4.AccessibleDescription = resources.GetString("label4.AccessibleDescription");
			this.label4.AccessibleName = resources.GetString("label4.AccessibleName");
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label4.Anchor")));
			this.label4.AutoSize = ((bool)(resources.GetObject("label4.AutoSize")));
			this.label4.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label4.Dock")));
			this.label4.Enabled = ((bool)(resources.GetObject("label4.Enabled")));
			this.errorProvider.SetError(this.label4, resources.GetString("label4.Error"));
			this.label4.Font = ((System.Drawing.Font)(resources.GetObject("label4.Font")));
			this.errorProvider.SetIconAlignment(this.label4, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label4.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.label4, ((int)(resources.GetObject("label4.IconPadding"))));
			this.label4.Image = ((System.Drawing.Image)(resources.GetObject("label4.Image")));
			this.label4.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label4.ImageAlign")));
			this.label4.ImageIndex = ((int)(resources.GetObject("label4.ImageIndex")));
			this.label4.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label4.ImeMode")));
			this.label4.Location = ((System.Drawing.Point)(resources.GetObject("label4.Location")));
			this.label4.Name = "label4";
			this.label4.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label4.RightToLeft")));
			this.label4.Size = ((System.Drawing.Size)(resources.GetObject("label4.Size")));
			this.label4.TabIndex = ((int)(resources.GetObject("label4.TabIndex")));
			this.label4.Text = resources.GetString("label4.Text");
			this.label4.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label4.TextAlign")));
			this.label4.Visible = ((bool)(resources.GetObject("label4.Visible")));
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
			// label6
			// 
			this.label6.AccessibleDescription = resources.GetString("label6.AccessibleDescription");
			this.label6.AccessibleName = resources.GetString("label6.AccessibleName");
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label6.Anchor")));
			this.label6.AutoSize = ((bool)(resources.GetObject("label6.AutoSize")));
			this.label6.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label6.Dock")));
			this.label6.Enabled = ((bool)(resources.GetObject("label6.Enabled")));
			this.errorProvider.SetError(this.label6, resources.GetString("label6.Error"));
			this.label6.Font = ((System.Drawing.Font)(resources.GetObject("label6.Font")));
			this.errorProvider.SetIconAlignment(this.label6, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label6.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.label6, ((int)(resources.GetObject("label6.IconPadding"))));
			this.label6.Image = ((System.Drawing.Image)(resources.GetObject("label6.Image")));
			this.label6.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label6.ImageAlign")));
			this.label6.ImageIndex = ((int)(resources.GetObject("label6.ImageIndex")));
			this.label6.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label6.ImeMode")));
			this.label6.Location = ((System.Drawing.Point)(resources.GetObject("label6.Location")));
			this.label6.Name = "label6";
			this.label6.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label6.RightToLeft")));
			this.label6.Size = ((System.Drawing.Size)(resources.GetObject("label6.Size")));
			this.label6.TabIndex = ((int)(resources.GetObject("label6.TabIndex")));
			this.label6.Text = resources.GetString("label6.Text");
			this.label6.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label6.TextAlign")));
			this.label6.Visible = ((bool)(resources.GetObject("label6.Visible")));
			// 
			// label7
			// 
			this.label7.AccessibleDescription = resources.GetString("label7.AccessibleDescription");
			this.label7.AccessibleName = resources.GetString("label7.AccessibleName");
			this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("label7.Anchor")));
			this.label7.AutoSize = ((bool)(resources.GetObject("label7.AutoSize")));
			this.label7.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("label7.Dock")));
			this.label7.Enabled = ((bool)(resources.GetObject("label7.Enabled")));
			this.errorProvider.SetError(this.label7, resources.GetString("label7.Error"));
			this.label7.Font = ((System.Drawing.Font)(resources.GetObject("label7.Font")));
			this.errorProvider.SetIconAlignment(this.label7, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label7.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.label7, ((int)(resources.GetObject("label7.IconPadding"))));
			this.label7.Image = ((System.Drawing.Image)(resources.GetObject("label7.Image")));
			this.label7.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label7.ImageAlign")));
			this.label7.ImageIndex = ((int)(resources.GetObject("label7.ImageIndex")));
			this.label7.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("label7.ImeMode")));
			this.label7.Location = ((System.Drawing.Point)(resources.GetObject("label7.Location")));
			this.label7.Name = "label7";
			this.label7.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("label7.RightToLeft")));
			this.label7.Size = ((System.Drawing.Size)(resources.GetObject("label7.Size")));
			this.label7.TabIndex = ((int)(resources.GetObject("label7.TabIndex")));
			this.label7.Text = resources.GetString("label7.Text");
			this.label7.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("label7.TextAlign")));
			this.label7.Visible = ((bool)(resources.GetObject("label7.Visible")));
			// 
			// cmbInputConnType
			// 
			this.cmbInputConnType.AccessibleDescription = resources.GetString("cmbInputConnType.AccessibleDescription");
			this.cmbInputConnType.AccessibleName = resources.GetString("cmbInputConnType.AccessibleName");
			this.cmbInputConnType.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cmbInputConnType.Anchor")));
			this.cmbInputConnType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmbInputConnType.BackgroundImage")));
			this.cmbInputConnType.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cmbInputConnType.Dock")));
			this.cmbInputConnType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbInputConnType.Enabled = ((bool)(resources.GetObject("cmbInputConnType.Enabled")));
			this.errorProvider.SetError(this.cmbInputConnType, resources.GetString("cmbInputConnType.Error"));
			this.cmbInputConnType.Font = ((System.Drawing.Font)(resources.GetObject("cmbInputConnType.Font")));
			this.errorProvider.SetIconAlignment(this.cmbInputConnType, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cmbInputConnType.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.cmbInputConnType, ((int)(resources.GetObject("cmbInputConnType.IconPadding"))));
			this.cmbInputConnType.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cmbInputConnType.ImeMode")));
			this.cmbInputConnType.IntegralHeight = ((bool)(resources.GetObject("cmbInputConnType.IntegralHeight")));
			this.cmbInputConnType.ItemHeight = ((int)(resources.GetObject("cmbInputConnType.ItemHeight")));
			this.cmbInputConnType.Location = ((System.Drawing.Point)(resources.GetObject("cmbInputConnType.Location")));
			this.cmbInputConnType.MaxDropDownItems = ((int)(resources.GetObject("cmbInputConnType.MaxDropDownItems")));
			this.cmbInputConnType.MaxLength = ((int)(resources.GetObject("cmbInputConnType.MaxLength")));
			this.cmbInputConnType.Name = "cmbInputConnType";
			this.cmbInputConnType.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cmbInputConnType.RightToLeft")));
			this.cmbInputConnType.Size = ((System.Drawing.Size)(resources.GetObject("cmbInputConnType.Size")));
			this.cmbInputConnType.Sorted = true;
			this.cmbInputConnType.TabIndex = ((int)(resources.GetObject("cmbInputConnType.TabIndex")));
			this.cmbInputConnType.Text = resources.GetString("cmbInputConnType.Text");
			this.cmbInputConnType.Visible = ((bool)(resources.GetObject("cmbInputConnType.Visible")));
			this.cmbInputConnType.Leave += new System.EventHandler(this.WzPageParametersConn_Leave);
			// 
			// cmbOutputConnType
			// 
			this.cmbOutputConnType.AccessibleDescription = resources.GetString("cmbOutputConnType.AccessibleDescription");
			this.cmbOutputConnType.AccessibleName = resources.GetString("cmbOutputConnType.AccessibleName");
			this.cmbOutputConnType.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cmbOutputConnType.Anchor")));
			this.cmbOutputConnType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmbOutputConnType.BackgroundImage")));
			this.cmbOutputConnType.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cmbOutputConnType.Dock")));
			this.cmbOutputConnType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbOutputConnType.Enabled = ((bool)(resources.GetObject("cmbOutputConnType.Enabled")));
			this.errorProvider.SetError(this.cmbOutputConnType, resources.GetString("cmbOutputConnType.Error"));
			this.cmbOutputConnType.Font = ((System.Drawing.Font)(resources.GetObject("cmbOutputConnType.Font")));
			this.errorProvider.SetIconAlignment(this.cmbOutputConnType, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cmbOutputConnType.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.cmbOutputConnType, ((int)(resources.GetObject("cmbOutputConnType.IconPadding"))));
			this.cmbOutputConnType.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cmbOutputConnType.ImeMode")));
			this.cmbOutputConnType.IntegralHeight = ((bool)(resources.GetObject("cmbOutputConnType.IntegralHeight")));
			this.cmbOutputConnType.ItemHeight = ((int)(resources.GetObject("cmbOutputConnType.ItemHeight")));
			this.cmbOutputConnType.Location = ((System.Drawing.Point)(resources.GetObject("cmbOutputConnType.Location")));
			this.cmbOutputConnType.MaxDropDownItems = ((int)(resources.GetObject("cmbOutputConnType.MaxDropDownItems")));
			this.cmbOutputConnType.MaxLength = ((int)(resources.GetObject("cmbOutputConnType.MaxLength")));
			this.cmbOutputConnType.Name = "cmbOutputConnType";
			this.cmbOutputConnType.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cmbOutputConnType.RightToLeft")));
			this.cmbOutputConnType.Size = ((System.Drawing.Size)(resources.GetObject("cmbOutputConnType.Size")));
			this.cmbOutputConnType.Sorted = true;
			this.cmbOutputConnType.TabIndex = ((int)(resources.GetObject("cmbOutputConnType.TabIndex")));
			this.cmbOutputConnType.Text = resources.GetString("cmbOutputConnType.Text");
			this.cmbOutputConnType.Visible = ((bool)(resources.GetObject("cmbOutputConnType.Visible")));
			// 
			// cmbReturnDataType
			// 
			this.cmbReturnDataType.AccessibleDescription = resources.GetString("cmbReturnDataType.AccessibleDescription");
			this.cmbReturnDataType.AccessibleName = resources.GetString("cmbReturnDataType.AccessibleName");
			this.cmbReturnDataType.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cmbReturnDataType.Anchor")));
			this.cmbReturnDataType.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmbReturnDataType.BackgroundImage")));
			this.cmbReturnDataType.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cmbReturnDataType.Dock")));
			this.cmbReturnDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbReturnDataType.Enabled = ((bool)(resources.GetObject("cmbReturnDataType.Enabled")));
			this.errorProvider.SetError(this.cmbReturnDataType, resources.GetString("cmbReturnDataType.Error"));
			this.cmbReturnDataType.Font = ((System.Drawing.Font)(resources.GetObject("cmbReturnDataType.Font")));
			this.errorProvider.SetIconAlignment(this.cmbReturnDataType, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cmbReturnDataType.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.cmbReturnDataType, ((int)(resources.GetObject("cmbReturnDataType.IconPadding"))));
			this.cmbReturnDataType.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cmbReturnDataType.ImeMode")));
			this.cmbReturnDataType.IntegralHeight = ((bool)(resources.GetObject("cmbReturnDataType.IntegralHeight")));
			this.cmbReturnDataType.ItemHeight = ((int)(resources.GetObject("cmbReturnDataType.ItemHeight")));
			this.cmbReturnDataType.Location = ((System.Drawing.Point)(resources.GetObject("cmbReturnDataType.Location")));
			this.cmbReturnDataType.MaxDropDownItems = ((int)(resources.GetObject("cmbReturnDataType.MaxDropDownItems")));
			this.cmbReturnDataType.MaxLength = ((int)(resources.GetObject("cmbReturnDataType.MaxLength")));
			this.cmbReturnDataType.Name = "cmbReturnDataType";
			this.cmbReturnDataType.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cmbReturnDataType.RightToLeft")));
			this.cmbReturnDataType.Size = ((System.Drawing.Size)(resources.GetObject("cmbReturnDataType.Size")));
			this.cmbReturnDataType.Sorted = true;
			this.cmbReturnDataType.TabIndex = ((int)(resources.GetObject("cmbReturnDataType.TabIndex")));
			this.cmbReturnDataType.Text = resources.GetString("cmbReturnDataType.Text");
			this.cmbReturnDataType.Visible = ((bool)(resources.GetObject("cmbReturnDataType.Visible")));
			// 
			// groupBox1
			// 
			this.groupBox1.AccessibleDescription = resources.GetString("groupBox1.AccessibleDescription");
			this.groupBox1.AccessibleName = resources.GetString("groupBox1.AccessibleName");
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("groupBox1.Anchor")));
			this.groupBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox1.BackgroundImage")));
			this.groupBox1.Controls.Add(this.cmbInputConnType);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.cmbOutputConnType);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.cmbReturnDataType);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("groupBox1.Dock")));
			this.groupBox1.Enabled = ((bool)(resources.GetObject("groupBox1.Enabled")));
			this.errorProvider.SetError(this.groupBox1, resources.GetString("groupBox1.Error"));
			this.groupBox1.Font = ((System.Drawing.Font)(resources.GetObject("groupBox1.Font")));
			this.errorProvider.SetIconAlignment(this.groupBox1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBox1.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.groupBox1, ((int)(resources.GetObject("groupBox1.IconPadding"))));
			this.groupBox1.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("groupBox1.ImeMode")));
			this.groupBox1.Location = ((System.Drawing.Point)(resources.GetObject("groupBox1.Location")));
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("groupBox1.RightToLeft")));
			this.groupBox1.Size = ((System.Drawing.Size)(resources.GetObject("groupBox1.Size")));
			this.groupBox1.TabIndex = ((int)(resources.GetObject("groupBox1.TabIndex")));
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = resources.GetString("groupBox1.Text");
			this.groupBox1.Visible = ((bool)(resources.GetObject("groupBox1.Visible")));
			// 
			// grpParams
			// 
			this.grpParams.AccessibleDescription = resources.GetString("grpParams.AccessibleDescription");
			this.grpParams.AccessibleName = resources.GetString("grpParams.AccessibleName");
			this.grpParams.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("grpParams.Anchor")));
			this.grpParams.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("grpParams.BackgroundImage")));
			this.grpParams.Controls.Add(this.txtMinParams);
			this.grpParams.Controls.Add(this.txtMaxParams);
			this.grpParams.Controls.Add(this.label3);
			this.grpParams.Controls.Add(this.label4);
			this.grpParams.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("grpParams.Dock")));
			this.grpParams.Enabled = ((bool)(resources.GetObject("grpParams.Enabled")));
			this.errorProvider.SetError(this.grpParams, resources.GetString("grpParams.Error"));
			this.grpParams.Font = ((System.Drawing.Font)(resources.GetObject("grpParams.Font")));
			this.errorProvider.SetIconAlignment(this.grpParams, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("grpParams.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.grpParams, ((int)(resources.GetObject("grpParams.IconPadding"))));
			this.grpParams.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("grpParams.ImeMode")));
			this.grpParams.Location = ((System.Drawing.Point)(resources.GetObject("grpParams.Location")));
			this.grpParams.Name = "grpParams";
			this.grpParams.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("grpParams.RightToLeft")));
			this.grpParams.Size = ((System.Drawing.Size)(resources.GetObject("grpParams.Size")));
			this.grpParams.TabIndex = ((int)(resources.GetObject("grpParams.TabIndex")));
			this.grpParams.TabStop = false;
			this.grpParams.Text = resources.GetString("grpParams.Text");
			this.grpParams.Visible = ((bool)(resources.GetObject("grpParams.Visible")));
			// 
			// cmdMoveUp
			// 
			this.cmdMoveUp.AccessibleDescription = resources.GetString("cmdMoveUp.AccessibleDescription");
			this.cmdMoveUp.AccessibleName = resources.GetString("cmdMoveUp.AccessibleName");
			this.cmdMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cmdMoveUp.Anchor")));
			this.cmdMoveUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdMoveUp.BackgroundImage")));
			this.cmdMoveUp.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cmdMoveUp.Dock")));
			this.cmdMoveUp.Enabled = ((bool)(resources.GetObject("cmdMoveUp.Enabled")));
			this.errorProvider.SetError(this.cmdMoveUp, resources.GetString("cmdMoveUp.Error"));
			this.cmdMoveUp.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cmdMoveUp.FlatStyle")));
			this.cmdMoveUp.Font = ((System.Drawing.Font)(resources.GetObject("cmdMoveUp.Font")));
			this.errorProvider.SetIconAlignment(this.cmdMoveUp, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cmdMoveUp.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.cmdMoveUp, ((int)(resources.GetObject("cmdMoveUp.IconPadding"))));
			this.cmdMoveUp.Image = ((System.Drawing.Image)(resources.GetObject("cmdMoveUp.Image")));
			this.cmdMoveUp.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cmdMoveUp.ImageAlign")));
			this.cmdMoveUp.ImageIndex = ((int)(resources.GetObject("cmdMoveUp.ImageIndex")));
			this.cmdMoveUp.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cmdMoveUp.ImeMode")));
			this.cmdMoveUp.Location = ((System.Drawing.Point)(resources.GetObject("cmdMoveUp.Location")));
			this.cmdMoveUp.Name = "cmdMoveUp";
			this.cmdMoveUp.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cmdMoveUp.RightToLeft")));
			this.cmdMoveUp.Size = ((System.Drawing.Size)(resources.GetObject("cmdMoveUp.Size")));
			this.cmdMoveUp.TabIndex = ((int)(resources.GetObject("cmdMoveUp.TabIndex")));
			this.cmdMoveUp.Text = resources.GetString("cmdMoveUp.Text");
			this.cmdMoveUp.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cmdMoveUp.TextAlign")));
			this.cmdMoveUp.Visible = ((bool)(resources.GetObject("cmdMoveUp.Visible")));
			this.cmdMoveUp.Click += new System.EventHandler(this.cmdMoveUp_Click);
			// 
			// cmdMoveDown
			// 
			this.cmdMoveDown.AccessibleDescription = resources.GetString("cmdMoveDown.AccessibleDescription");
			this.cmdMoveDown.AccessibleName = resources.GetString("cmdMoveDown.AccessibleName");
			this.cmdMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("cmdMoveDown.Anchor")));
			this.cmdMoveDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdMoveDown.BackgroundImage")));
			this.cmdMoveDown.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("cmdMoveDown.Dock")));
			this.cmdMoveDown.Enabled = ((bool)(resources.GetObject("cmdMoveDown.Enabled")));
			this.errorProvider.SetError(this.cmdMoveDown, resources.GetString("cmdMoveDown.Error"));
			this.cmdMoveDown.FlatStyle = ((System.Windows.Forms.FlatStyle)(resources.GetObject("cmdMoveDown.FlatStyle")));
			this.cmdMoveDown.Font = ((System.Drawing.Font)(resources.GetObject("cmdMoveDown.Font")));
			this.errorProvider.SetIconAlignment(this.cmdMoveDown, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cmdMoveDown.IconAlignment"))));
			this.errorProvider.SetIconPadding(this.cmdMoveDown, ((int)(resources.GetObject("cmdMoveDown.IconPadding"))));
			this.cmdMoveDown.Image = ((System.Drawing.Image)(resources.GetObject("cmdMoveDown.Image")));
			this.cmdMoveDown.ImageAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cmdMoveDown.ImageAlign")));
			this.cmdMoveDown.ImageIndex = ((int)(resources.GetObject("cmdMoveDown.ImageIndex")));
			this.cmdMoveDown.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("cmdMoveDown.ImeMode")));
			this.cmdMoveDown.Location = ((System.Drawing.Point)(resources.GetObject("cmdMoveDown.Location")));
			this.cmdMoveDown.Name = "cmdMoveDown";
			this.cmdMoveDown.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("cmdMoveDown.RightToLeft")));
			this.cmdMoveDown.Size = ((System.Drawing.Size)(resources.GetObject("cmdMoveDown.Size")));
			this.cmdMoveDown.TabIndex = ((int)(resources.GetObject("cmdMoveDown.TabIndex")));
			this.cmdMoveDown.Text = resources.GetString("cmdMoveDown.Text");
			this.cmdMoveDown.TextAlign = ((System.Drawing.ContentAlignment)(resources.GetObject("cmdMoveDown.TextAlign")));
			this.cmdMoveDown.Visible = ((bool)(resources.GetObject("cmdMoveDown.Visible")));
			this.cmdMoveDown.Click += new System.EventHandler(this.cmdMoveDown_Click);
			// 
			// WzPageParametersConn
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.Controls.Add(this.cmdMoveDown);
			this.Controls.Add(this.cmdMoveUp);
			this.Controls.Add(this.grpParams);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.txtFunctoidParameter);
			this.Controls.Add(this.cmdFunctoidParameterDel);
			this.Controls.Add(this.cmdFunctoidParameterAdd);
			this.Controls.Add(this.cmbParameterDataType);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.lstFunctoidParameters);
			this.Controls.Add(this.label1);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.errorProvider.SetError(this, resources.GetString("$this.Error"));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.errorProvider.SetIconAlignment(this, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("$this.IconAlignment"))));
			this.errorProvider.SetIconPadding(this, ((int)(resources.GetObject("$this.IconPadding"))));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.Name = "WzPageParametersConn";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.Size = ((System.Drawing.Size)(resources.GetObject("$this.Size")));
			this.SubTitle = "Specify Functoid Parameters && Connection Types";
			this.Title = "Functoid Parameters && Connection Types";
			this.Load += new System.EventHandler(this.WzPageParametersConn_Load);
			this.Leave += new System.EventHandler(this.WzPageParametersConn_Leave);
			this.Controls.SetChildIndex(this.panelHeader, 0);
			this.Controls.SetChildIndex(this.label1, 0);
			this.Controls.SetChildIndex(this.lstFunctoidParameters, 0);
			this.Controls.SetChildIndex(this.label2, 0);
			this.Controls.SetChildIndex(this.cmbParameterDataType, 0);
			this.Controls.SetChildIndex(this.cmdFunctoidParameterAdd, 0);
			this.Controls.SetChildIndex(this.cmdFunctoidParameterDel, 0);
			this.Controls.SetChildIndex(this.txtFunctoidParameter, 0);
			this.Controls.SetChildIndex(this.groupBox1, 0);
			this.Controls.SetChildIndex(this.grpParams, 0);
			this.Controls.SetChildIndex(this.cmdMoveUp, 0);
			this.Controls.SetChildIndex(this.cmdMoveDown, 0);
			this.panelHeader.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.grpParams.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

	}
}

