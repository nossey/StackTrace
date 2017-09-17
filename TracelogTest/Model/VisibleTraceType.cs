using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet;

namespace TracelogTest.Model
{
    public class VisibleTraceType : NotificationObject
    {
        #region Declaration
        #endregion

        #region Fields

        bool _IsSelected = false;
        SingleTrace.TraceType _Type;

        #endregion

        #region Properties

        public SingleTrace.TraceType Type
        {
            get
            {
                return _Type;
            }
            private set
            {
                _Type = value;
                RaisePropertyChanged();
            }
        }

        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                if (_IsSelected == value)
                    return;
                _IsSelected = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Public methods
        
        public VisibleTraceType(SingleTrace.TraceType type)
        {
            Type = type;
        }

        #endregion

        #region Private methods
        #endregion
    }
}