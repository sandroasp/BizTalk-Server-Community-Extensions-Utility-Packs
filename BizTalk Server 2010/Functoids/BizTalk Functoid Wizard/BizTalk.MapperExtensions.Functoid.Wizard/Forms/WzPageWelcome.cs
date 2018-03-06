using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace BizTalk.MapperExtensions.Functoid.Wizard
{
	public class WzPageWelcome : Microsoft.BizTalk.Wizard.WizardWelcomePage, WizardControlInterface
	{
		private System.ComponentModel.IContainer components = null;

		public WzPageWelcome()
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

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WzPageWelcome));
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            resources.ApplyResources(this.labelTitle, "labelTitle");
            // 
            // checkBoxSkipWelcome
            // 
            resources.ApplyResources(this.checkBoxSkipWelcome, "checkBoxSkipWelcome");
            // 
            // labelNavigation
            // 
            resources.ApplyResources(this.labelNavigation, "labelNavigation");
            this.labelNavigation.TabStop = false;
            // 
            // textBoxSubTitle
            // 
            resources.ApplyResources(this.textBoxSubTitle, "textBoxSubTitle");
            // 
            // WzPageWelcome
            // 
            this.Name = "WzPageWelcome";
            this.SubTitle = "This wizard will guide you through the steps needed to generate a BizTalk Functoi" +
                "d.";
            this.Title = "BizTalk Server Functoid Project Wizard";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
	}
}

