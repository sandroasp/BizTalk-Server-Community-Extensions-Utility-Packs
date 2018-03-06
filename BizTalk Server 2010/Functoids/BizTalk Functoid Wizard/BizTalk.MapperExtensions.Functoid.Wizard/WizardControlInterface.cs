using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizTalk.MapperExtensions.Functoid.Wizard
{
    /// <summary>
    /// Summary description for WizardControlInterface.
    /// </summary>
    internal interface WizardControlInterface
    {
        bool NextButtonEnabled
        {
            get;
        }
        bool NeedSummary
        {
            get;
        }
    }
}
