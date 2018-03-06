namespace BizTalk.MapperExtensions.Functoid.Wizard.Forms
{
    partial class PageWelcomeControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSubTitle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxRunAgain = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(102)))), ((int)(((byte)(51)))));
            this.label1.Location = new System.Drawing.Point(164, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(320, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "BizTalk Server Functoid Project Wizard";
            // 
            // textBoxSubTitle
            // 
            this.textBoxSubTitle.Location = new System.Drawing.Point(168, 109);
            this.textBoxSubTitle.Multiline = true;
            this.textBoxSubTitle.Name = "textBoxSubTitle";
            this.textBoxSubTitle.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxSubTitle.Size = new System.Drawing.Size(312, 136);
            this.textBoxSubTitle.TabIndex = 1;
            this.textBoxSubTitle.Text = "The Functoid Wizard will create the following projects:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(212, 268);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(211, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "To create the Functoid project, click Finish.";
            // 
            // checkBoxRunAgain
            // 
            this.checkBoxRunAgain.AutoSize = true;
            this.checkBoxRunAgain.BackColor = System.Drawing.Color.Transparent;
            this.checkBoxRunAgain.Location = new System.Drawing.Point(215, 285);
            this.checkBoxRunAgain.Name = "checkBoxRunAgain";
            this.checkBoxRunAgain.Size = new System.Drawing.Size(130, 17);
            this.checkBoxRunAgain.TabIndex = 3;
            this.checkBoxRunAgain.Text = "&Run this wizard again.";
            this.checkBoxRunAgain.UseVisualStyleBackColor = false;
            // 
            // PageWelcomeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::BizTalk.MapperExtensions.Functoid.Wizard.Properties.Resources.bts_wallpapper;
            this.Controls.Add(this.checkBoxRunAgain);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxSubTitle);
            this.Controls.Add(this.label1);
            this.Name = "PageWelcomeControl";
            this.Size = new System.Drawing.Size(496, 318);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSubTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxRunAgain;
    }
}
