using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Livet;

namespace TracelogTest.Model
{
    public class DebugMessageType : NotificationObject
    {
        #region Declaration
        #endregion

        #region Fields

        SingleTrace.TraceType _Type = SingleTrace.TraceType.Info;
        bool _IsSelected = false;

        #endregion

        #region Properties

        public SingleTrace.TraceType Type
        {
            get
            {
                return _Type;
            }
            set
            {
                if (_Type == value)
                    return;
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
        #endregion

        #region Private methods
        #endregion
    }


    public class Debug : NotificationObject
    {
        #region Declaration
        #endregion

        #region Fields

        string _DebugMessage = string.Empty;

        #endregion

        #region Properties

        public string DebugMessage
        {
            get
            {
                return _DebugMessage;
            }
            set
            {
                if (_DebugMessage == value)
                    return;
                _DebugMessage = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<DebugMessageType> Types = new ObservableCollection<DebugMessageType>();


        public SingleTrace.TraceType SelectedType
        {
            get
            {
                var find = Types.FirstOrDefault(type => type.IsSelected);
                if (find == null)
                    return SingleTrace.TraceType.Info;
                return find.Type;
            }
        }

        #endregion

        #region Public methods

        public Debug()
        {
            Types.Add(new DebugMessageType() { Type = SingleTrace.TraceType.Info, IsSelected = true });
            Types.Add(new DebugMessageType() { Type = SingleTrace.TraceType.Warning, IsSelected = false });
            Types.Add(new DebugMessageType() { Type = SingleTrace.TraceType.Error, IsSelected = false });
        }

        #endregion

        #region Private methods
        #endregion
    }
}
