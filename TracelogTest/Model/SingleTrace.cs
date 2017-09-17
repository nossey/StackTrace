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
            Info,
            Warning,
            Error,
        }

        #endregion

        #region Fields

        string _Text;

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

        #endregion

        #region Public methods
        #endregion

        #region Private methods
        #endregion
    }
}