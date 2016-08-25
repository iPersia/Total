using System;
using System.Text;

namespace Nzl.Rtf
{
    /// <summary>
    /// RTF element container
    /// </summary>
    [Serializable()]
    public class RTFDomElementContainer : RTFDomElement
    {
        /// <summary>
        /// initialize instance
        /// </summary>
        public RTFDomElementContainer()
        {
        }

        private string strName = null;
        /// <summary>
        /// name
        /// </summary>
        [System.ComponentModel.DefaultValue( null )]
        public string Name
        {
            get
            {
                return strName; 
            }
            set
            {
                strName = value; 
            }
        }

        public override string ToString()
        {
            return "Container : " + strName;
        }
    }
}
