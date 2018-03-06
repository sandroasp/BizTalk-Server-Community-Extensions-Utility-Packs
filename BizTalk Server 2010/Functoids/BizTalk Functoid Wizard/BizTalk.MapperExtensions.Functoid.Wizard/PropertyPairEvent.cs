using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizTalk.MapperExtensions.Functoid.Wizard
{
    /// <summary>
    /// PropertyPairEvent class holds name and value for
    /// transportation in events. The remove field indicates
    /// that the value must be removed from the resultcollection
    /// </summary>
    public class PropertyPairEvent : EventArgs
    {
        private string _strName = null;
        private object _Value = null;
        private bool _Remove = false;

        public PropertyPairEvent(string strName, object Value)
        {
            _strName = strName;
            _Value = Value;
        }

        public PropertyPairEvent(string strName, object Value, bool remove)
        {
            _strName = strName;
            _Value = Value;
            _Remove = remove;
        }

        public string Name
        {
            set { _strName = value; }
            get { return _strName; }
        }

        public object Value
        {
            set { _Value = value; }
            get { return _Value; }
        }

        public bool Remove
        {
            get { return _Remove; }
            set { _Remove = value; }
        }
    }
}
