/***************************************************************************

  Rtf Dom Parser

  Copyright (c) 2010 sinosoft , written by yuans.
  http://www.sinoreport.net

  This program is free software; you can redistribute it and/or
  modify it under the terms of the GNU General Public License
  as published by the Free Software Foundation; either version 2
  of the License, or (at your option) any later version.
  
  This program is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.
  
  You should have received a copy of the GNU General Public License
  along with this program; if not, write to the Free Software
  Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.

****************************************************************************/

using System;
using System.Text;

namespace Nzl.Rtf
{
    /// <summary>
    /// shape group
    /// </summary>
    [Serializable()]
    public class RTFDomShapeGroup : RTFDomElement
    {
        /// <summary>
        /// initialize instance
        /// </summary>
        public RTFDomShapeGroup()
        {
        }

        private StringAttributeCollection myExtAttrbutes = new StringAttributeCollection();
        /// <summary>
        /// extern attributes
        /// </summary>
        public StringAttributeCollection ExtAttrbutes
        {
            get
            {
                return myExtAttrbutes;
            }
            set
            {
                myExtAttrbutes = value;
            }
        }

        public override string ToString()
        {
            return "ShapeGroup";
        }
    }
}
