using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace BizTalk.MapperExtensions.Functoid.Wizard
{
	public class WzPageSummary : Microsoft.BizTalk.Wizard.WizardCompletionPage, WizardControlInterface
	{
		private System.ComponentModel.IContainer components = null;
		private string _Summary = null;

		public WzPageSummary()
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
			get {	return true;	}
		}

		public string Summary
		{
			get {	return _Summary;	}
			set 
			{
				_Summary = value;
				textBoxSubTitle.Text = Summary;
			}

		}
		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WzPageSummary));
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            resources.ApplyResources(this.labelTitle, "labelTitle");
            // 
            // checkBoxRunAgain
            // 
            resources.ApplyResources(this.checkBoxRunAgain, "checkBoxRunAgain");
            // 
            // labelNavigation
            // 
            resources.ApplyResources(this.labelNavigation, "labelNavigation");
            // 
            // textBoxSubTitle
            // 
            resources.ApplyResources(this.textBoxSubTitle, "textBoxSubTitle");
            // 
            // WzPageSummary
            // 
            this.Name = "WzPageSummary";
            this.SubTitle = "The Functoid Wizard will create the following projects:";
            this.Title = "BizTalk Server Functoid Project Wizard";
            this.Load += new System.EventHandler(this.WzPageSummary_Load);
            this.VisibleChanged += new System.EventHandler(this.WzPageSummary_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void WzPageSummary_Load(object sender, System.EventArgs e)
		{
		}

		private void WzPageSummary_VisibleChanged(object sender, System.EventArgs e)
		{
		}
	}
}

