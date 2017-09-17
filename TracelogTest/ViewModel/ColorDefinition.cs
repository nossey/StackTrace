using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TracelogTest.ViewModel
{
    public static class ColorDefinition
    {
        #region Declaration
        #endregion

        #region Fields
        #endregion

        #region Properties

        public static SolidColorBrush InfoColor { get; } = new SolidColorBrush(Colors.White);
        public static SolidColorBrush WarningColor { get; } = new SolidColorBrush(Colors.Yellow);
        public static SolidColorBrush ErrorColor { get; } = new SolidColorBrush(Colors.Red);

        #endregion

        #region Public methods
        #endregion

        #region Private methods
        #endregion
    }
}