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
using System.ComponentModel ;

namespace Nzl.Rtf
{
	/// <summary>
	/// RTF Document format information
	/// </summary>
    [Serializable()]
	public class DocumentFormatInfo
	{
        /// <summary>
        /// Initialize instance
        /// </summary>
        public DocumentFormatInfo()
        {
        }
		
		private DocumentFormatInfo myParent = null;
		/// <summary>
		/// If this instance is create by Clone , return the parent instance
		/// </summary>
        [Browsable( false )]
		public DocumentFormatInfo Parent
		{
			get
            {
                return myParent ;
            }
		}


        private bool bolLeftBorder = false;

        /// <summary>
        /// Display left border line
        /// </summary>
        [DefaultValue( false )]
        public bool LeftBorder
        {
            get
            {
                return bolLeftBorder; 
            }
            set
            {
                bolLeftBorder = value; 
            }
        }

        private bool bolTopBorder = false;
        /// <summary>
        /// Display top border line
        /// </summary>
        [DefaultValue( false )]
        public bool TopBorder
        {
            get
            {
                return bolTopBorder; 
            }
            set
            {
                bolTopBorder = value; 
            }
        }

        private bool bolRightBorder = false;
        /// <summary>
        /// Display right border line
        /// </summary>
        [DefaultValue(false)]
        public bool RightBorder
        {
            get
            {
                return bolRightBorder; 
            }
            set
            {
                bolRightBorder = value; 
            }
        }

        private bool bolBottomBorder = false;
        /// <summary>
        /// 是否显示下边框线
        /// </summary>
        [DefaultValue( false )]
        public bool BottomBorder
        {
            get
            {
                return bolBottomBorder; 
            }
            set
            {
                bolBottomBorder = value; 
            }
        }

        private System.Drawing.Color intBorderColor 
            = System.Drawing.Color.Transparent;
        /// <summary>
        /// Border line color
        /// </summary>
        [DefaultValue(typeof(System.Drawing.Color), "Transparent")]
        public System.Drawing.Color BorderColor
        {
            get
            {
                return intBorderColor; 
            }
            set
            {
                intBorderColor = value; 
            }
        }

		private bool bolMultiline = false;
        /// <summary>
        /// Word wrap
        /// </summary>
        [DefaultValue(false)]
        public bool Multiline
        {
            get
            {
                return bolMultiline; 
            }
            set
            {
                bolMultiline = value; 
            }
        }

		private int intStandTabWidth = 100;
        /// <summary>
        /// Standard tab width
        /// </summary>
        [DefaultValue(100)]
        public int StandTabWidth
        {
            get
            {
                return intStandTabWidth; 
            }
            set
            {
                intStandTabWidth = value; 
            }
        }

		private int intParagraphFirstLineIndent = 0;
        /// <summary>
        /// indent of first line in a paragraph
        /// </summary>
        [DefaultValue(0)]
        public int ParagraphFirstLineIndent
        {
            get
            {
                return intParagraphFirstLineIndent; 
            }
            set
            {
                intParagraphFirstLineIndent = value; 
            }
        }

		private int intLeftIndent = 0;
        /// <summary>
        /// Indent of wholly paragraph
        /// </summary>
        [DefaultValue(0)]
        public int LeftIndent
        {
            get
            {
                return intLeftIndent; 
            }
            set
            {
                intLeftIndent = value; 
            }
        }

		private int intSpacing = 0;
        /// <summary>
        /// character spacing
        /// </summary>
        [DefaultValue(0)]
        public int Spacing
        {
            get
            {
                return intSpacing; 
            }
            set
            {
                intSpacing = value; 
            }
        }

		private int intLineSpacing = 0;
        /// <summary>
        /// line spacing
        /// </summary>
        [DefaultValue(0)]
        public int LineSpacing
        {
            get
            {
                return intLineSpacing; 
            }
            set
            {
                intLineSpacing = value; 
            }
        }

        private RTFAlignment intAlign = RTFAlignment.Left;
        /// <summary>
        /// text alignment
        /// </summary>
        [DefaultValue(RTFAlignment.Left)]
        public RTFAlignment Align
        {
            get
            {
                return intAlign; 
            }
            set
            {
                intAlign = value; 
            }
        }

        /// <summary>
        /// nest level in native rtf document
        /// </summary>
        public int NativeLevel = 0;

		public void SetAlign( System.Drawing.StringAlignment align )
		{
			if( align == System.Drawing.StringAlignment.Center )
				Align = RTFAlignment.Center  ;
			else if( align == System.Drawing.StringAlignment.Far )
				Align =  RTFAlignment.Right  ;
			else
				Align = RTFAlignment.Left  ;
		}
        
        [Browsable( false )]
		public System.Drawing.Font Font
		{
			set
			{
				if( value != null )
				{
					FontName = value.Name ;
					FontSize = value.Size ;
					Bold = value.Bold ;
					Italic = value.Italic ;
					Underline = value.Underline ;
					Strikeout = value.Strikeout ;
				}
			}
		}

		private string strFontName = System.Windows.Forms.Control.DefaultFont.Name;
        /// <summary>
        /// font name
        /// </summary>
        public string FontName
        {
            get
            {
                return strFontName; 
            }
            set
            {
                strFontName = value; 
            }
        }

		private float fFontSize = 12f;
        /// <summary>
        /// font size
        /// </summary>
        [DefaultValue(12f)]
        public float FontSize
        {
            get
            {
                return fFontSize; 
            }
            set
            {
                fFontSize = value; 
            }
        }

		private bool bolBold = false;
        /// <summary>
        /// bold style
        /// </summary>
        [DefaultValue(false)]
        public bool Bold
        {
            get
            {
                return bolBold; 
            }
            set
            {
                bolBold = value; 
            }
        }

		private bool bolItalic = false;
        /// <summary>
        /// italic style
        /// </summary>
        [DefaultValue(false)]
        public bool Italic
        {
            get
            {
                return bolItalic; 
            }
            set
            {
                bolItalic = value; 
            }
        }

		private bool bolUnderline = false;
        /// <summary>
        /// underline style
        /// </summary>
        [DefaultValue(false)]
        public bool Underline
        {
            get
            {
                return bolUnderline; 
            }
            set
            {
                bolUnderline = value; 
            }
        }

		private bool bolStrikeout = false;
        /// <summary>
        /// strickout style
        /// </summary>
        [DefaultValue(false)]
        public bool Strikeout
        {
            get
            {
                return bolStrikeout; 
            }
            set
            {
                bolStrikeout = value; 
            }
        }

		private System.Drawing.Color intTextColor = System.Drawing.Color.Black;
        /// <summary>
        /// text color
        /// </summary>
        [DefaultValue(typeof(System.Drawing.Color), "Black")]
        public System.Drawing.Color TextColor
        {
            get
            {
                return intTextColor; 
            }
            set
            {
                intTextColor = value; 
            }
        }

		private System.Drawing.Color intBackColor = System.Drawing.Color.Empty;
        /// <summary>
        /// back color
        /// </summary>
        [DefaultValue(typeof(System.Drawing.Color), "Empty")]
        public System.Drawing.Color BackColor
        {
            get
            {
                return intBackColor; 
            }
            set
            {
                intBackColor = value; 
            }
        }

        ///// <summary>
        ///// 边框线颜色
        ///// </summary>
        //public System.Drawing.Color BorderColor = System.Drawing.Color.Empty;
		private string strLink = null;
        /// <summary>
        /// link
        /// </summary>
        [DefaultValue(null)]
        public string Link
        {
            get
            {
                return strLink; 
            }
            set
            {
                strLink = value; 
            }
        }

		private bool bolSuperscript = false;
        /// <summary>
        /// superscript
        /// </summary>
        [DefaultValue(false)]
        public bool Superscript
        {
            get
            {
                return bolSuperscript; 
            }
            set
            {
                bolSuperscript = value; 
            }
        }
        
		private bool bolSubscript = false;
        /// <summary>
        /// subscript
        /// </summary>
        [DefaultValue(false)]
        public bool Subscript
        {
            get
            {
                return bolSubscript; 
            }
            set
            {
                bolSubscript = value; 
            }
        }

		private bool bolBulletedList = false;
        /// <summary>
        /// list in bulleted style
        /// </summary>
        [DefaultValue(false)]
        public bool BulletedList
        {
            get 
            {
                return bolBulletedList; 
            }
            set
            {
                bolBulletedList = value; 
            }
        }
		
        private bool bolNumberedList = false;
        /// <summary>
        /// list in numbered style
        /// </summary>
        [DefaultValue(false)]
        public bool NumberedList
        {
            get
            {
                return bolNumberedList; 
            }
            set
            {
                bolNumberedList = value; 
            }
        }

        private bool bolNoWwrap = true;
        /// <summary>
        /// no wrap in word
        /// </summary>
        [DefaultValue(true)]
        public bool NoWwrap
        {
            get
            {
                return bolNoWwrap; 
            }
            set
            {
                bolNoWwrap = value; 
            }
        }
        
        internal bool ReadText = true;

        public bool EqualsSettings(DocumentFormatInfo format )
        {
            if (format == this)
                return true;
            if (format == null)
                return false;
            if (this.Align != format.Align)
                return false;
            if (this.BackColor != format.BackColor)
                return false;
            if (this.Bold != format.Bold)
                return false;
            if (this.BorderColor != format.BorderColor)
                return false;
            if (this.LeftBorder != format.LeftBorder)
                return false;
            if (this.TopBorder != format.TopBorder)
                return false;
            if (this.RightBorder != format.RightBorder)
                return false;
            if (this.BottomBorder != format.BottomBorder)
                return false;
            if (this.BulletedList != format.BulletedList)
                return false;
            if (this.FontName != format.FontName)
                return false;
            if (this.FontSize != format.FontSize)
                return false;
            if (this.Italic != format.Italic)
                return false;
            if (this.LeftIndent != format.LeftIndent)
                return false;
            if (this.LineSpacing != format.LineSpacing)
                return false;
            if (this.Link != format.Link)
                return false;
            if (this.Multiline != format.Multiline)
                return false;
            if (this.NoWwrap != format.NoWwrap)
                return false;
            if (this.NumberedList != format.NumberedList)
                return false;
            if (this.ParagraphFirstLineIndent != format.ParagraphFirstLineIndent)
                return false;
            if (this.Spacing != format.Spacing)
                return false;
            if (this.StandTabWidth != format.StandTabWidth)
                return false;
            if (this.Strikeout != format.Strikeout)
                return false;
            if (this.Subscript != format.Subscript)
                return false;
            if (this.Superscript != format.Superscript)
                return false;
            if (this.TextColor != format.TextColor)
                return false;
            if (this.Underline != format.Underline)
                return false;
            if (this.ReadText != format.ReadText)
                return false;
            return true;
        }

        /// <summary>
        /// close instance
        /// </summary>
        /// <returns>new instance</returns>
        public DocumentFormatInfo Clone()
        {
            DocumentFormatInfo format = new DocumentFormatInfo();
            format.ParagraphFirstLineIndent = this.ParagraphFirstLineIndent;
            format.LeftIndent = this.LeftIndent;
            format.Spacing = this.Spacing;
            format.LineSpacing = this.LineSpacing;
            format.Align = this.Align;
            format.FontName = this.FontName;
            format.FontSize = this.FontSize;
            format.Bold = this.Bold;
            format.Italic = this.Italic;
            format.Underline = this.Underline;
            format.Strikeout = this.Strikeout;
            format.TextColor = this.TextColor;
            format.BackColor = this.BackColor;
            format.Link = this.Link;
            format.Superscript = this.Superscript;
            format.Subscript = this.Subscript;
            format.BulletedList = this.BulletedList;
            format.NumberedList = this.NumberedList;
            format.StandTabWidth = this.StandTabWidth;
            format.Multiline = this.Multiline;
            format.NoWwrap = this.NoWwrap;
            format.myParent = this.myParent;
            format.LeftBorder = this.LeftBorder;
            format.TopBorder = this.TopBorder;
            format.RightBorder = this.RightBorder;
            format.BottomBorder = this.BottomBorder;
            format.BorderColor = this.BorderColor;
            format.ReadText = this.ReadText;
            format.NativeLevel = this.NativeLevel;
            return format;
        }


        public void ResetText()
        {
            this.FontName = System.Windows.Forms.Control.DefaultFont.Name;
            this.FontSize = 12;
            this.Bold = false;
            this.Italic = false;
            this.Underline = false;
            this.Strikeout = false;
            this.TextColor = System.Drawing.Color.Black;
            this.BackColor = System.Drawing.Color.Empty;
            //this.Link = null ;
            this.Subscript = false;
            this.Superscript = false;
            this.Multiline = true;
        }
        public void ResetParagraph()
        {
            this.ParagraphFirstLineIndent = 0;
            this.Align = 0;
            this.BulletedList = false;
            this.NumberedList = false;
            this.LeftIndent = 0;
            //this.LeftBorder = false;
            //this.TopBorder = false;
            //this.RightBorder = false;
            //this.BottomBorder = false;
            //this.BorderColor = System.Drawing.Color.Transparent;
        }
        public void Reset()
        {
            this.ParagraphFirstLineIndent = 0;
            this.LeftIndent = 0;
            this.LeftIndent = 0;
            this.Spacing = 0;
            this.LineSpacing = 0;
            this.Align = 0;
            this.FontName = System.Windows.Forms.Control.DefaultFont.Name;
            this.FontSize = 12;
            this.Bold = false;
            this.Italic = false;
            this.Underline = false;
            this.Strikeout = false;
            this.TextColor = System.Drawing.Color.Black;
            this.BackColor = System.Drawing.Color.Empty;
            this.Link = null;
            this.Subscript = false;
            this.Superscript = false;
            this.BulletedList = false;
            this.NumberedList = false;
            this.Multiline = true;
            this.NoWwrap = true;
            this.LeftBorder = false;
            this.TopBorder = false;
            this.RightBorder = false;
            this.BottomBorder = false;
            this.BorderColor = System.Drawing.Color.Transparent;
            this.ReadText = true;
            this.NativeLevel = 0;
        }

	}
}