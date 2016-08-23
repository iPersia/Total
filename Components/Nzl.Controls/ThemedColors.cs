namespace Nzl.Controls
{
    using System.Drawing;
    using System.Windows.Forms;
    using System.Windows.Forms.VisualStyles;

    /// <summary>
    /// 
    /// </summary>
    public class ThemedColors
    {
        #region Variables and Constants
        /// <summary>
        /// 
        /// </summary>
        private const string NormalColor = "NormalColor";

        /// <summary>
        /// 
        /// </summary>
        private const string HomeStead = "HomeStead";

        /// <summary>
        /// 
        /// </summary>
        private const string Metallic = "Metallic";

        /// <summary>
        /// 
        /// </summary>
        private const string NoTheme = "NoTheme";
        
        /// <summary>
        /// 
        /// </summary>
        private static Color[] _toolBorder;
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public static int CurrentThemeIndex
        {
            get
            {
                return ThemedColors.GetCurrentThemeIndex();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string CurrentThemeName
        {
            get
            {
                return ThemedColors.GetCurrentThemeName();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Color ToolBorder
        {
            get
            {
                return ThemedColors._toolBorder[ThemedColors.CurrentThemeIndex];
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        private ThemedColors()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        static ThemedColors()
        {
            Color[] colorArray1;
            colorArray1 = new Color[] { Color.FromArgb(127, 157, 185), Color.FromArgb(164, 185, 127), Color.FromArgb(165, 172, 178), Color.FromArgb(132, 130, 132) };
            ThemedColors._toolBorder = colorArray1;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static int GetCurrentThemeIndex()
        {
            int theme = (int)ColorScheme.NoTheme;
            if (VisualStyleInformation.IsSupportedByOS && VisualStyleInformation.IsEnabledByUser && Application.RenderWithVisualStyles)
            {
                switch (VisualStyleInformation.ColorScheme)
                {
                    case NormalColor:
                        theme = (int)ColorScheme.NormalColor;
                        break;
                    case HomeStead:
                        theme = (int)ColorScheme.HomeStead;
                        break;
                    case Metallic:
                        theme = (int)ColorScheme.Metallic;
                        break;
                    default:
                        theme = (int)ColorScheme.NoTheme;
                        break;
                }
            }
            return theme;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static string GetCurrentThemeName()
        {
            string theme = NoTheme;
            if (VisualStyleInformation.IsSupportedByOS && VisualStyleInformation.IsEnabledByUser && Application.RenderWithVisualStyles)
            {
                theme = VisualStyleInformation.ColorScheme;
            }
            return theme;
        }

        /// <summary>
        /// 
        /// </summary>
        public enum ColorScheme
        {
            /// <summary>
            /// 
            /// </summary>
            NormalColor = 0,

            /// <summary>
            /// 
            /// </summary>
            HomeStead = 1,

            /// <summary>
            /// 
            /// </summary>
            Metallic = 2,

            /// <summary>
            /// 
            /// </summary>
            NoTheme = 3
        }
    }
}