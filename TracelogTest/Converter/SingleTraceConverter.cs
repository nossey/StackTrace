using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;
using TracelogTest.ViewModel;

namespace TracelogTest.Converter
{
    public class SingleTraceConverter : IValueConverter
    {
        #region Declaration
        #endregion

        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Public methods

        public object Convert(object value, Type type, object param, CultureInfo info)
        {
            var vmTraces = (Livet.ReadOnlyDispatcherCollection<SingleTraceViewModel>)value;
            return vmTraces.Select(vm => vm.Trace);
        }

        public object ConvertBack(object src, Type type, object dst, CultureInfo info)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private methods
        #endregion
    }
}
