using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet;

namespace TracelogTest.Model
{
    public class SingleTrace : NotificationObject
    {
        #region Declaration

        public enum TraceType 
        {
            All,
            Info,
            Warning,
            Error,
        }

        #endregion

        #region Fields

        string _Text;
        bool _Visible = true;
        string _Stacktrace;
        bool _IsSelected = false;

        #endregion

        #region Properties

        public string Text 
        {
            get
            {
                return _Text;
            }
            set
            {
                if (_Text == value)
                    return;
                _Text = value;
                RaisePropertyChanged();
            }
        }

        public TraceType Type { get; set; }

        public bool Visible
        {
            get
            {
                return _Visible;
            }
            set
            {
                if (_Visible == value)
                    return;
                _Visible = value;
                RaisePropertyChanged();
            }
        }

        public string Stacktrace
        {
            get
            {
                return _Stacktrace;
            }
            set
            {
                if (_Stacktrace == value)
                    return;
                _Stacktrace = value;
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
        #endregion

        #region Private methods
        #endregion
    }
}