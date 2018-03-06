using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Specialized;
using Microsoft.BizTalk.Wizard;

namespace BizTalk.MapperExtensions.Functoid.Wizard
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FunctoidProjectWizardForm : Microsoft.BizTalk.Wizard.WizardForm
	{
		// Page Enum
		private enum Pages
		{
			WzPageWelcome = 0,
			WzPageGeneralSetup = 1,
			WzPageResourceSetup = 2,
			WzPageResourceSetup2 = 3,
			WzPageParametersConn = 4,
			WzPageSummary = 5
		}
		
		private Hashtable _wizardResults = new Hashtable();
		private NameValueCollection _functoidParameters = new NameValueCollection();

		private ArrayList _PageCollection = new ArrayList();
		private int _PageCount = 0;
		
		// Page Forms
        private WzPageWelcome wzPageWelcome = new WzPageWelcome();
        private WzPageGeneralSetup wzPageGeneralSetup = new WzPageGeneralSetup();
        private WzPageResourceSetup wzPageResourceSetup = new WzPageResourceSetup();
        private WzPageResourceSetup2 wzPageResourceSetup2 = new WzPageResourceSetup2();
        private WzPageParametersConn wzPageParametersConn = new WzPageParametersConn();
        private WzPageSummary wzPageSummary = new WzPageSummary();

        /// <summary>
        /// 
        /// </summary>
		public FunctoidProjectWizardForm()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			this.ButtonNext.Click += new System.EventHandler(buttonNext_Click);
			this.ButtonBack.Click += new System.EventHandler(buttonBack_Click);

			// Add pages to the Wizard Form
			AddPage(wzPageWelcome, false);
			AddPage(wzPageGeneralSetup, false);
			AddPage(wzPageResourceSetup, false);
			AddPage(wzPageResourceSetup2, false);
			AddPage(wzPageParametersConn, false);
			AddPage(wzPageSummary, false);

			// Add pages to the Page Collection
			_PageCollection.Add(wzPageWelcome);
			_PageCollection.Add(wzPageGeneralSetup);
			_PageCollection.Add(wzPageResourceSetup);
			_PageCollection.Add(wzPageResourceSetup2);
			_PageCollection.Add(wzPageParametersConn);
			_PageCollection.Add(wzPageSummary);

			// Add handlers for events from each page
			wzPageGeneralSetup._AddWizardResultEvent += new AddWizardResultEvent(AddWizardResult);
			wzPageResourceSetup._AddWizardResultEvent += new AddWizardResultEvent(AddWizardResult);
			wzPageResourceSetup2._AddWizardResultEvent += new AddWizardResultEvent(AddWizardResult);
			wzPageParametersConn._AddWizardResultEvent += new AddWizardResultEvent(AddWizardResult);
			wzPageParametersConn._AddFunctoidParameterEvent += new AddFunctoidParameterEvent(AddFunctoidParameters);

			ButtonHelp.Enabled = false;
		}

        /// <summary>
        /// 
        /// </summary>
        public Hashtable WizardResults
        {
            get { return this._wizardResults; }
        }

        /// <summary>
        /// 
        /// </summary>
        public NameValueCollection FunctoidParameters
        {
            get { return this._functoidParameters; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageCount"></param>
        /// <param name="countingUp"></param>
        /// <returns></returns>
        private int AdjustPageCount(int pageCount, bool countingUp)
        {
            return (countingUp ? ++pageCount : --pageCount);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddWizardResult(object sender, PropertyPairEvent e)
        {
            try
            {
                // Replace the value if it already exists
                if (_wizardResults[e.Name] != null)
                    _wizardResults.Remove(e.Name);
                _wizardResults.Add(e.Name, e.Value);
            }
            catch (Exception err)
            {
                MessageBox.Show(this, err.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nvc"></param>
        /// <param name="e"></param>
        private void AddProperty(Hashtable nvc, PropertyPairEvent e)
        {
            if (e.Remove)
            {
                if (nvc[e.Name] != null)
                    nvc.Remove(e.Name);
                return;
            }

            // Replace the value if it already exists
            if (nvc[e.Name] != null)
                nvc.Remove(e.Name);
            nvc.Add(e.Name, e.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nvc"></param>
        /// <param name="e"></param>
        private void AddProperty(NameValueCollection nvc, PropertyPairEvent e)
        {
            if (e.Remove)
            {
                if (nvc[e.Name] != null)
                    nvc.Remove(e.Name);
                return;
            }

            // Replace the value if it already exists
            if (nvc[e.Name] != null)
                nvc.Remove(e.Name);
            nvc.Add(e.Name, (string)e.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddFunctoidParameters(object sender, PropertyPairEvent e)
        {
            try
            {
                AddProperty(FunctoidParameters, e);
            }
            catch (Exception err)
            {
                MessageBox.Show(this, err.Message);
            }
        }

        /// <summary>
        /// Creates a summary based on the properties assembled by the wizard. To
        /// be shown on the endpage of the wizard.
        /// </summary>
        /// <returns></returns>
        private string CreateSummary()
        {
            string Summary =
                "The functoid wizard will create the following projects:" + Environment.NewLine + Environment.NewLine;

            Summary += "- A project for the " + (string)_wizardResults["Name"] + " functoid" + Environment.NewLine;

            return Summary;
        }
	}
}

