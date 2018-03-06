using Microsoft.BizTalk.Wizard;
using System;
using System.Windows.Forms;
namespace BizTalk.MapperExtensions.Functoid.Wizard
{
    partial class FunctoidProjectWizardForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.SuspendLayout();
            // 
            // FunctoidProjectWizardForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(4, 13);
            this.ClientSize = new System.Drawing.Size(501, 348);
            this.Enabled = false;
            this.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FunctoidProjectWizardForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "BizTalk Functoid Project Wizard";
            this.ResumeLayout(false);

		}
		#endregion

		private void buttonNext_Click(object sender, System.EventArgs e)
		{
			try
			{
				PageEventArgs e2 = new PageEventArgs((WizardPage) _PageCollection[_PageCount], PageEventButton.Next);
				_PageCount = AdjustPageCount(_PageCount, true);
				if(((WizardControlInterface) _PageCollection[_PageCount]).NeedSummary)
				{
					((WzPageSummary) _PageCollection[_PageCount]).Summary = CreateSummary();
				}
				SetCurrentPage((WizardPage) _PageCollection[_PageCount], e2);
				ButtonNext.Enabled = ((WizardControlInterface) _PageCollection[_PageCount]).NextButtonEnabled;
			}
			catch(Exception exc)
			{
                #if DEBUG
				                MessageBox.Show(this, exc.ToString(), this.Text,MessageBoxButtons.OK, MessageBoxIcon.Error);
                #else
				                MessageBox.Show(this, exc.Message, this.Text,MessageBoxButtons.OK, MessageBoxIcon.Error);
                #endif
			}
		}

		private void buttonBack_Click(object sender, System.EventArgs e)
		{
			try
			{
				PageEventArgs e2 = new PageEventArgs((WizardPage)_PageCollection[_PageCount],PageEventButton.Back);
				_PageCount = AdjustPageCount(_PageCount,false);
				SetCurrentPage((WizardPage)_PageCollection[_PageCount],e2);
			}
			catch(Exception exc)
			{
#if DEBUG
				MessageBox.Show(this, exc.ToString(), this.Text,MessageBoxButtons.OK, MessageBoxIcon.Error);
#else
				MessageBox.Show(this,exc.Message,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error);
#endif
			}
		}

		protected override void OnHelp(EventArgs e)
		{
			try
			{
				//_VsHelp.DisplayTopicFromKeyword(_HelpPages[_PageCount]);
			}
			catch(Exception exc)
			{
#if DEBUG
				MessageBox.Show(this, exc.ToString(), this.Text,MessageBoxButtons.OK, MessageBoxIcon.Error);
#else
				MessageBox.Show(this,exc.Message,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error);
#endif
			}
		}
    }
}